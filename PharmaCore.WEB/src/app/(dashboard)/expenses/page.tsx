"use client"

import { useState, useEffect } from "react"
import { useLanguage } from "@/components/providers/language-provider"
import { 
    Receipt, 
    Plus, 
    Search, 
    Building2, 
    TrendingUp, 
    Wallet, 
    Landmark, 
    Calendar, 
    User, 
    Loader2, 
    Tag, 
    Filter,
    ArrowUpDown,
    CheckCircle2
} from "lucide-react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Textarea } from "@/components/ui/textarea"
import { Progress } from "@/components/ui/progress"
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
    DialogTrigger,
    DialogFooter,
} from "@/components/ui/dialog"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { toast } from "sonner"
import apiClient from "@/lib/api-client"
import { Branch } from "@/types/branch"
import { Expense } from "@/types/expense"

// Defined Categories with HSL colors
const categoryDetails: Record<string, { labelEn: string; labelAr: string; color: string; glowColor: string }> = {
    "Rent": { labelEn: "Rent", labelAr: "إيجار", color: "hsl(200, 90%, 50%)", glowColor: "rgba(14, 165, 233, 0.4)" },
    "Utilities": { labelEn: "Utilities", labelAr: "مرافق كهرباء ومياه", color: "hsl(45, 95%, 50%)", glowColor: "rgba(245, 158, 11, 0.4)" },
    "Salaries": { labelEn: "Salaries", labelAr: "رواتب ومكافآت", color: "hsl(142, 70%, 45%)", glowColor: "rgba(34, 197, 94, 0.4)" },
    "Supplies": { labelEn: "Office Supplies", labelAr: "أدوات مكتبية ومستلزمات", color: "hsl(280, 85%, 60%)", glowColor: "rgba(168, 85, 247, 0.4)" },
    "Maintenance": { labelEn: "Maintenance", labelAr: "صيانة وتصليح", color: "hsl(12, 90%, 55%)", glowColor: "rgba(249, 115, 22, 0.4)" },
    "Marketing": { labelEn: "Marketing", labelAr: "تسويق وإعلان", color: "hsl(330, 85%, 55%)", glowColor: "rgba(236, 72, 153, 0.4)" },
    "Other": { labelEn: "Other Expenses", labelAr: "مصاريف أخرى", color: "hsl(0, 0%, 60%)", glowColor: "rgba(156, 163, 175, 0.4)" }
};

// Help parse the category and remaining description from the description field
function parseExpenseDescription(desc: string | undefined) {
    if (!desc) return { category: 'Other', cleanDescription: '' };
    const match = desc.match(/^\[(.*?)\]\s*(.*)/);
    if (match) {
        return {
            category: match[1],
            cleanDescription: match[2]
        };
    }
    return {
        category: 'Other',
        cleanDescription: desc
    };
}

// Help map standard category keys, English names, and Arabic names
function getCategoryInfo(categoryName: string) {
    const normalized = categoryName.trim().toLowerCase();
    
    if (normalized === 'rent' || normalized === 'إيجار') return { key: 'Rent', ...categoryDetails['Rent'] };
    if (normalized === 'utilities' || normalized === 'مرافق' || normalized === 'مرافق كهرباء ومياه') return { key: 'Utilities', ...categoryDetails['Utilities'] };
    if (normalized === 'salaries' || normalized === 'رواتب' || normalized === 'الرواتب' || normalized === 'رواتب ومكافآت') return { key: 'Salaries', ...categoryDetails['Salaries'] };
    if (normalized === 'supplies' || normalized === 'أدوات مكتبية' || normalized === 'أدوات مكتبية ومستلزمات') return { key: 'Supplies', ...categoryDetails['Supplies'] };
    if (normalized === 'maintenance' || normalized === 'صيانة' || normalized === 'صيانة وتصليح') return { key: 'Maintenance', ...categoryDetails['Maintenance'] };
    if (normalized === 'marketing' || normalized === 'تسويق' || normalized === 'تسويق وإعلان') return { key: 'Marketing', ...categoryDetails['Marketing'] };
    
    return { key: 'Other', ...categoryDetails['Other'] };
}

export default function ExpensesPage() {
    const { language } = useLanguage()
    const isRtl = language === "ar"

    // States
    const [expenses, setExpenses] = useState<Expense[]>([])
    const [branches, setBranches] = useState<Branch[]>([])
    const [isLoading, setIsLoading] = useState(true)
    const [isSaving, setIsSaving] = useState(false)
    const [isDialogOpen, setIsDialogOpen] = useState(false)

    // Form inputs state
    const [formAmount, setFormAmount] = useState("")
    const [formBranchId, setFormBranchId] = useState("null")
    const [formCategory, setFormCategory] = useState("Rent")
    const [formDate, setFormDate] = useState(new Date().toISOString().split("T")[0])
    const [formIsBank, setFormIsBank] = useState("false")
    const [formDescription, setFormDescription] = useState("")

    // Filter states
    const [searchTerm, setSearchTerm] = useState("")
    const [branchFilter, setBranchFilter] = useState("all")
    const [categoryFilter, setCategoryFilter] = useState("all")
    const [paymentFilter, setPaymentFilter] = useState("all")

    // Sort states
    const [sortAsc, setSortAsc] = useState(false)

    // Translated strings
    const t = {
        title: isRtl ? "إدارة المصاريف الإدارية" : "Administrative Expenses",
        subtitle: isRtl ? "متابعة وتوزيع وتسجيل النفقات الإدارية والتشغيلية للمؤسسة" : "Track, distribute, and log administrative and operational outlays",
        logExpense: isRtl ? "تسجيل مصروف جديد" : "Log New Expense",
        totalSpent: isRtl ? "إجمالي المصاريف" : "Total Administrative Spend",
        cashVsBank: isRtl ? "توزيع الدفع" : "Payment Outlets Distribution",
        cash: isRtl ? "نقدي (الخزينة)" : "Cash (Treasury)",
        bank: isRtl ? "بنكي (فيزا/تحويل)" : "Bank (Visa/Transfer)",
        branchesBreakdown: isRtl ? "المصاريف حسب الفروع" : "Branch Spend Outlays",
        categoryDistribution: isRtl ? "توزيع المصاريف حسب التصنيف" : "Category Outlay Distribution",
        ledgerTitle: isRtl ? "دفتر الأستاذ للمصاريف الإدارية" : "Administrative Expense Ledger",
        searchPlaceholder: isRtl ? "البحث بالبيان أو اسم الموظف..." : "Search description or employee...",
        allBranches: isRtl ? "جميع الفروع" : "All Branches",
        allCategories: isRtl ? "جميع التصنيفات" : "All Categories",
        allPaymentMethods: isRtl ? "جميع طرق الدفع" : "All Payment Methods",
        date: isRtl ? "التاريخ" : "Date",
        category: isRtl ? "التصنيف" : "Category",
        description: isRtl ? "البيان (الوصف)" : "Description",
        branch: isRtl ? "الفرع" : "Branch",
        amount: isRtl ? "المبلغ" : "Amount",
        paymentMethod: isRtl ? "طريقة الدفع" : "Payment Method",
        loggedBy: isRtl ? "سُجل بواسطة" : "Logged By",
        dialogTitle: isRtl ? "تسجيل مصروف جديد" : "Log New Expense",
        dialogDesc: isRtl ? "أدخل تفاصيل المصروف الإداري لتسجيل المعاملة في الخزينة." : "Enter details to record this administrative expense.",
        branchLabel: isRtl ? "الفرع المرتبط" : "Associated Branch",
        categoryLabel: isRtl ? "التصنيف / النوع" : "Category / Type",
        amountLabel: isRtl ? "المبلغ" : "Amount",
        dateLabel: isRtl ? "التاريخ" : "Date",
        paymentLabel: isRtl ? "طريقة الصرف" : "Payment Outlets",
        descriptionLabel: isRtl ? "بيان المصروف بالتفصيل" : "Description / Details",
        save: isRtl ? "تسجيل المصروف" : "Record Expense",
        cancel: isRtl ? "إلغاء" : "Cancel",
        loading: isRtl ? "جاري التحميل..." : "Loading...",
        noExpenses: isRtl ? "لا توجد مصاريف مسجلة مطابقة للبحث" : "No logged expenses found matching filters.",
        currency: isRtl ? "ج.م" : "EGP",
        errorFetch: isRtl ? "فشل تحميل البيانات" : "Failed to load data.",
        successSave: isRtl ? "تم تسجيل المصروف بنجاح" : "Expense logged successfully.",
        errorSave: isRtl ? "فشل تسجيل المصروف" : "Failed to log expense.",
        mainOffice: isRtl ? "المكتب الرئيسي" : "Main Office",
    }

    const fetchExpenses = async () => {
        try {
            const response = await apiClient.get<Expense[]>("/treasury/expense")
            if (Array.isArray(response.data)) {
                setExpenses(response.data)
            }
        } catch (error: any) {
            console.error("Failed to fetch expenses", error)
            toast.error(t.errorFetch)
        }
    }

    const fetchBranches = async () => {
        try {
            const response = await apiClient.get<Branch[]>("/branches")
            if (Array.isArray(response.data)) {
                setBranches(response.data)
            }
        } catch (error: any) {
            console.error("Failed to fetch branches", error)
        }
    }

    useEffect(() => {
        const loadInitialData = async () => {
            setIsLoading(true)
            await Promise.all([fetchExpenses(), fetchBranches()])
            setIsLoading(false)
        }
        loadInitialData()
    }, [])

    const handleSaveExpense = async (e: React.FormEvent) => {
        e.preventDefault()
        const parsedAmount = parseFloat(formAmount)
        if (isNaN(parsedAmount) || parsedAmount <= 0) {
            toast.error(isRtl ? "المبلغ يجب أن يكون أكبر من الصفر" : "Amount must be greater than zero.")
            return
        }

        setIsSaving(true)
        try {
            const payload = {
                branchId: formBranchId === "null" || !formBranchId ? null : formBranchId,
                amount: parsedAmount,
                category: formCategory,
                description: formDescription,
                isBank: formIsBank === "true",
                date: new Date(formDate).toISOString()
            }

            await apiClient.post("/treasury/expense", payload)
            toast.success(t.successSave)
            setIsDialogOpen(false)

            // Reset Form
            setFormAmount("")
            setFormBranchId("null")
            setFormCategory("Rent")
            setFormDate(new Date().toISOString().split("T")[0])
            setFormIsBank("false")
            setFormDescription("")

            await fetchExpenses()
        } catch (error: any) {
            console.error("Failed to save expense", error)
            const errorMsg = error.response?.data?.message || error.response?.data || error.message || t.errorSave
            toast.error(typeof errorMsg === "string" ? errorMsg : t.errorSave)
        } finally {
            setIsSaving(false)
        }
    }

    // Calculations & Metrics
    const totalSpentVal = expenses.reduce((sum, item) => sum + item.amount, 0)
    const bankSpentVal = expenses.filter(item => item.isBank).reduce((sum, item) => sum + item.amount, 0)
    const cashSpentVal = expenses.filter(item => !item.isBank).reduce((sum, item) => sum + item.amount, 0)

    const bankPercent = totalSpentVal > 0 ? (bankSpentVal / totalSpentVal) * 100 : 0
    const cashPercent = totalSpentVal > 0 ? (cashSpentVal / totalSpentVal) * 100 : 0

    // Summing by Category
    const categoryTotals = expenses.reduce((acc, exp) => {
        const { category } = parseExpenseDescription(exp.description)
        const info = getCategoryInfo(category)
        acc[info.key] = (acc[info.key] || 0) + exp.amount
        return acc;
    }, {} as Record<string, number>)

    // Summing by Branch
    const branchTotals = expenses.reduce((acc, exp) => {
        const bName = exp.branchName || t.mainOffice
        acc[bName] = (acc[bName] || 0) + exp.amount
        return acc;
    }, {} as Record<string, number>)

    // Filtered Ledger List
    const filteredExpenses = expenses.filter((exp) => {
        const parsed = parseExpenseDescription(exp.description)
        const categoryInfo = getCategoryInfo(parsed.category)

        const matchesSearch = 
            (exp.description || "").toLowerCase().includes(searchTerm.toLowerCase()) ||
            (exp.employeeName || "").toLowerCase().includes(searchTerm.toLowerCase())
        
        const matchesBranch = 
            branchFilter === "all" || 
            (branchFilter === "null" && !exp.branchId) ||
            exp.branchId === branchFilter

        const matchesCategory = 
            categoryFilter === "all" || 
            categoryInfo.key === categoryFilter

        const matchesPayment = 
            paymentFilter === "all" || 
            (paymentFilter === "bank" && exp.isBank) ||
            (paymentFilter === "cash" && !exp.isBank)

        return matchesSearch && matchesBranch && matchesCategory && matchesPayment
    })

    // Sorted list
    const sortedExpenses = [...filteredExpenses].sort((a, b) => {
        const timeA = new Date(a.date).getTime()
        const timeB = new Date(b.date).getTime()
        return sortAsc ? timeA - timeB : timeB - timeA
    })

    return (
        <div className="space-y-8 p-1">
            {/* Header section */}
            <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
                <div>
                    <h1 className="text-3xl font-extrabold tracking-tight bg-gradient-to-r from-primary to-primary/60 bg-clip-text text-transparent">
                        {t.title}
                    </h1>
                    <p className="text-muted-foreground mt-1">
                        {t.subtitle}
                    </p>
                </div>
                <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
                    <DialogTrigger asChild>
                        <Button className="font-semibold shadow-lg hover:shadow-primary/20 transition-all duration-300 gap-2">
                            <Plus className="h-5 w-5" />
                            {t.logExpense}
                        </Button>
                    </DialogTrigger>
                    <DialogContent className="max-w-md bg-background border border-border">
                        <DialogHeader>
                            <DialogTitle className="text-xl font-bold">{t.dialogTitle}</DialogTitle>
                            <DialogDescription>{t.dialogDesc}</DialogDescription>
                        </DialogHeader>
                        <form onSubmit={handleSaveExpense} className="space-y-4 py-2">
                            <div className="space-y-1">
                                <label className="text-sm font-medium">{t.amountLabel}</label>
                                <div className="relative">
                                    <Input
                                        type="number"
                                        step="0.01"
                                        placeholder="0.00"
                                        value={formAmount}
                                        onChange={(e) => setFormAmount(e.target.value)}
                                        required
                                        disabled={isSaving}
                                        className="pe-12 font-semibold text-lg"
                                    />
                                    <span className="absolute right-3 top-1/2 -translate-y-1/2 text-muted-foreground text-sm font-medium">
                                        {t.currency}
                                    </span>
                                </div>
                            </div>

                            <div className="space-y-1">
                                <label className="text-sm font-medium">{t.branchLabel}</label>
                                <Select value={formBranchId} onValueChange={setFormBranchId} disabled={isSaving}>
                                    <SelectTrigger className="w-full">
                                        <SelectValue />
                                    </SelectTrigger>
                                    <SelectContent>
                                        <SelectItem value="null">{t.mainOffice}</SelectItem>
                                        {branches.map(b => (
                                            <SelectItem key={b.id} value={b.id}>
                                                {isRtl && b.nameAr ? b.nameAr : b.name}
                                            </SelectItem>
                                        ))}
                                    </SelectContent>
                                </Select>
                            </div>

                            <div className="grid grid-cols-2 gap-4">
                                <div className="space-y-1">
                                    <label className="text-sm font-medium">{t.categoryLabel}</label>
                                    <Select value={formCategory} onValueChange={setFormCategory} disabled={isSaving}>
                                        <SelectTrigger className="w-full">
                                            <SelectValue />
                                        </SelectTrigger>
                                        <SelectContent>
                                            {Object.entries(categoryDetails).map(([key, info]) => (
                                                <SelectItem key={key} value={key}>
                                                    {isRtl ? info.labelAr : info.labelEn}
                                                </SelectItem>
                                            ))}
                                        </SelectContent>
                                    </Select>
                                </div>

                                <div className="space-y-1">
                                    <label className="text-sm font-medium">{t.paymentLabel}</label>
                                    <Select value={formIsBank} onValueChange={setFormIsBank} disabled={isSaving}>
                                        <SelectTrigger className="w-full">
                                            <SelectValue />
                                        </SelectTrigger>
                                        <SelectContent>
                                            <SelectItem value="false">{t.cash}</SelectItem>
                                            <SelectItem value="true">{t.bank}</SelectItem>
                                        </SelectContent>
                                    </Select>
                                </div>
                            </div>

                            <div className="space-y-1">
                                <label className="text-sm font-medium">{t.dateLabel}</label>
                                <Input
                                    type="date"
                                    value={formDate}
                                    onChange={(e) => setFormDate(e.target.value)}
                                    required
                                    disabled={isSaving}
                                />
                            </div>

                            <div className="space-y-1">
                                <label className="text-sm font-medium">{t.descriptionLabel}</label>
                                <Textarea
                                    rows={3}
                                    placeholder={isRtl ? "مثال: إيجار المقر لشهر مايو" : "Example: Rent for HQ building..."}
                                    value={formDescription}
                                    onChange={(e) => setFormDescription(e.target.value)}
                                    disabled={isSaving}
                                />
                            </div>

                            <DialogFooter className="gap-2 pt-4">
                                <Button type="button" variant="outline" onClick={() => setIsDialogOpen(false)} disabled={isSaving}>
                                    {t.cancel}
                                </Button>
                                <Button type="submit" disabled={isSaving} className="gap-2">
                                    {isSaving && <Loader2 className="h-4 w-4 animate-spin" />}
                                    {t.save}
                                </Button>
                            </DialogFooter>
                        </form>
                    </DialogContent>
                </Dialog>
            </div>

            {/* KPI metrics row */}
            <div className="grid gap-6 md:grid-cols-3">
                {/* Total spent card */}
                <Card className="relative overflow-hidden transition-all duration-300 hover:shadow-lg border border-border/40 bg-gradient-to-br from-card to-background">
                    <CardHeader className="flex flex-row items-center justify-between pb-2">
                        <div className="space-y-1">
                            <CardDescription className="text-xs uppercase font-semibold text-muted-foreground tracking-wider">
                                {t.totalSpent}
                            </CardDescription>
                            <CardTitle className="text-3xl font-black">
                                {totalSpentVal.toLocaleString(undefined, { minimumFractionDigits: 2 })}
                                <span className="text-sm font-normal text-muted-foreground ms-1">{t.currency}</span>
                            </CardTitle>
                        </div>
                        <div className="p-3 bg-primary/10 rounded-xl text-primary">
                            <Receipt className="h-6 w-6" />
                        </div>
                    </CardHeader>
                    <CardContent className="pt-2">
                        <div className="flex items-center gap-1.5 text-xs text-muted-foreground">
                            <TrendingUp className="h-3.5 w-3.5 text-emerald-500" />
                            <span>{expenses.length} {isRtl ? "عملية مسجلة" : "logged expenses"}</span>
                        </div>
                        <div className="absolute bottom-0 inset-x-0 h-1 bg-gradient-to-r from-primary to-primary/40" />
                    </CardContent>
                </Card>


                {/* Cash vs Bank Distribution card */}
                <Card className="transition-all duration-300 hover:shadow-lg border border-border/40 bg-gradient-to-br from-card to-background">
                    <CardHeader className="pb-2">
                        <CardDescription className="text-xs uppercase font-semibold text-muted-foreground tracking-wider">
                            {t.cashVsBank}
                        </CardDescription>
                    </CardHeader>
                    <CardContent className="space-y-4">
                        <div className="space-y-1">
                            <div className="flex justify-between text-xs font-semibold">
                                <span className="flex items-center gap-1"><Wallet className="h-3.5 w-3.5 text-emerald-500" /> {t.cash}</span>
                                <span>{cashPercent.toFixed(1)}% ({cashSpentVal.toLocaleString()} {t.currency})</span>
                            </div>
                            <Progress value={cashPercent} className="h-2 bg-emerald-500/10" style={{ '--glow-color': 'rgba(34, 197, 94, 0.2)' } as React.CSSProperties} />
                        </div>
                        <div className="space-y-1">
                            <div className="flex justify-between text-xs font-semibold">
                                <span className="flex items-center gap-1"><Landmark className="h-3.5 w-3.5 text-sky-500" /> {t.bank}</span>
                                <span>{bankPercent.toFixed(1)}% ({bankSpentVal.toLocaleString()} {t.currency})</span>
                            </div>
                            <Progress value={bankPercent} className="h-2 bg-sky-500/10" style={{ '--glow-color': 'rgba(14, 165, 233, 0.2)' } as React.CSSProperties} />
                        </div>
                    </CardContent>
                </Card>

                {/* Top spenders (Branches) */}
                <Card className="transition-all duration-300 hover:shadow-lg border border-border/40 bg-gradient-to-br from-card to-background">
                    <CardHeader className="pb-2">
                        <CardDescription className="text-xs uppercase font-semibold text-muted-foreground tracking-wider">
                            {t.branchesBreakdown}
                        </CardDescription>
                    </CardHeader>
                    <CardContent className="pt-0 max-h-[120px] overflow-y-auto space-y-3 scrollbar-thin">
                        {Object.keys(branchTotals).length === 0 ? (
                            <div className="text-xs text-muted-foreground text-center py-4">{t.noExpenses}</div>
                        ) : (
                            Object.entries(branchTotals)
                                .sort((a, b) => b[1] - a[1])
                                .map(([bName, val]) => {
                                    const percent = totalSpentVal > 0 ? (val / totalSpentVal) * 100 : 0
                                    return (
                                        <div key={bName} className="space-y-1">
                                            <div className="flex justify-between text-xs font-medium">
                                                <span className="flex items-center gap-1 truncate"><Building2 className="h-3 w-3 text-muted-foreground" /> {bName}</span>
                                                <span className="font-semibold">{val.toLocaleString()} {t.currency} ({percent.toFixed(0)}%)</span>
                                            </div>
                                            <div className="h-1 w-full bg-secondary rounded-full overflow-hidden">
                                                <div className="h-full bg-primary/60 rounded-full" style={{ width: `${percent}%` }} />
                                            </div>
                                        </div>
                                    )
                                })
                        )}
                    </CardContent>
                </Card>
            </div>

            {/* Category distributions grid with custom HSL and hover-glow effects */}
            <div className="space-y-4">
                <div className="flex items-center gap-2">
                    <Tag className="h-5 w-5 text-primary" />
                    <h2 className="text-xl font-bold tracking-tight">{t.categoryDistribution}</h2>
                </div>
                <div className="grid gap-6 sm:grid-cols-2 lg:grid-cols-4">
                    {Object.entries(categoryDetails).map(([key, info]) => {
                        const amount = categoryTotals[key] || 0
                        const percentage = totalSpentVal > 0 ? (amount / totalSpentVal) * 100 : 0
                        
                        return (
                            <div 
                                key={key}
                                style={{ 
                                    '--category-color': info.color, 
                                    '--category-glow': info.glowColor 
                                } as React.CSSProperties}
                                className="relative p-5 rounded-xl border border-border/40 bg-card/50 backdrop-blur-sm transition-all duration-300 hover:shadow-[0_0_20px_var(--category-glow)] hover:border-[var(--category-color)]/40 hover:bg-card/75 group"
                            >
                                <div className="flex justify-between items-start mb-3">
                                    <div className="flex items-center gap-2">
                                        <span 
                                            className="w-2.5 h-2.5 rounded-full transition-all duration-300 group-hover:scale-125" 
                                            style={{ 
                                                backgroundColor: info.color, 
                                                boxShadow: `0 0 10px ${info.color}` 
                                            }}
                                        />
                                        <h3 className="font-bold text-sm text-card-foreground group-hover:text-primary transition-colors">
                                            {isRtl ? info.labelAr : info.labelEn}
                                        </h3>
                                    </div>
                                    <span className="text-xs bg-muted px-2 py-0.5 rounded-full font-semibold text-muted-foreground group-hover:text-foreground transition-colors">
                                        {percentage.toFixed(1)}%
                                    </span>
                                </div>
                                <div className="text-2xl font-black tracking-tight mb-3">
                                    {amount.toLocaleString(undefined, { minimumFractionDigits: 2 })} 
                                    <span className="text-xs font-medium text-muted-foreground ms-1">{t.currency}</span>
                                </div>
                                
                                {/* HSL progress indicator */}
                                <div className="h-2 w-full bg-secondary/80 rounded-full overflow-hidden">
                                    <div 
                                        className="h-full rounded-full transition-all duration-700 ease-out"
                                        style={{ 
                                            width: `${percentage}%`, 
                                            backgroundColor: info.color,
                                            boxShadow: `0 0 8px ${info.color}`
                                        }}
                                    />
                                </div>
                            </div>
                        )
                    })}
                </div>
            </div>

            {/* Ledger List Section */}
            <Card className="border border-border/40 bg-card/30 backdrop-blur-sm">
                <CardHeader className="flex flex-col gap-4 md:flex-row md:items-center md:justify-between pb-6">
                    <div>
                        <CardTitle className="text-xl font-bold flex items-center gap-2">
                            <Receipt className="h-5 w-5 text-primary" />
                            {t.ledgerTitle}
                        </CardTitle>
                        <CardDescription>
                            {isRtl ? "قائمة بكافة السجلات والمصاريف المقيدة" : "Consolidated ledger of recorded operational outlays"}
                        </CardDescription>
                    </div>

                    {/* Filter and Search Bar */}
                    <div className="flex flex-wrap items-center gap-3 w-full md:w-auto">
                        <div className="relative flex-1 md:w-[260px] md:flex-initial">
                            <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                            <Input
                                placeholder={t.searchPlaceholder}
                                value={searchTerm}
                                onChange={(e) => setSearchTerm(e.target.value)}
                                className="pl-9 bg-background/50 border-border/60"
                                disabled={isLoading}
                            />
                        </div>

                        <Select value={branchFilter} onValueChange={setBranchFilter} disabled={isLoading}>
                            <SelectTrigger className="w-[140px] bg-background/50 border-border/60">
                                <SelectValue placeholder={t.branch} />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value="all">{t.allBranches}</SelectItem>
                                <SelectItem value="null">{t.mainOffice}</SelectItem>
                                {branches.map(b => (
                                    <SelectItem key={b.id} value={b.id}>
                                        {isRtl && b.nameAr ? b.nameAr : b.name}
                                    </SelectItem>
                                ))}
                            </SelectContent>
                        </Select>

                        <Select value={categoryFilter} onValueChange={setCategoryFilter} disabled={isLoading}>
                            <SelectTrigger className="w-[140px] bg-background/50 border-border/60">
                                <SelectValue placeholder={t.category} />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value="all">{t.allCategories}</SelectItem>
                                {Object.entries(categoryDetails).map(([key, info]) => (
                                    <SelectItem key={key} value={key}>
                                        {isRtl ? info.labelAr : info.labelEn}
                                    </SelectItem>
                                ))}
                            </SelectContent>
                        </Select>

                        <Select value={paymentFilter} onValueChange={setPaymentFilter} disabled={isLoading}>
                            <SelectTrigger className="w-[140px] bg-background/50 border-border/60">
                                <SelectValue placeholder={t.paymentMethod} />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value="all">{t.allPaymentMethods}</SelectItem>
                                <SelectItem value="cash">{t.cash}</SelectItem>
                                <SelectItem value="bank">{t.bank}</SelectItem>
                            </SelectContent>
                        </Select>
                    </div>
                </CardHeader>
                <CardContent className="p-0">
                    {isLoading ? (
                        <div className="h-64 flex flex-col items-center justify-center gap-2 text-muted-foreground">
                            <Loader2 className="h-8 w-8 animate-spin text-primary" />
                            <span>{t.loading}</span>
                        </div>
                    ) : sortedExpenses.length === 0 ? (
                        <div className="h-48 flex items-center justify-center text-muted-foreground border-t border-border/40 text-sm">
                            {t.noExpenses}
                        </div>
                    ) : (
                        <div className="overflow-x-auto border-t border-border/40">
                            <Table>
                                <TableHeader className="bg-muted/40">
                                    <TableRow>
                                        <TableHead className="w-[150px] font-bold">
                                            <Button 
                                                variant="ghost" 
                                                onClick={() => setSortAsc(!sortAsc)}
                                                className="hover:bg-transparent p-0 font-bold flex items-center gap-1"
                                            >
                                                {t.date}
                                                <ArrowUpDown className="h-3 w-3" />
                                            </Button>
                                        </TableHead>
                                        <TableHead className="font-bold">{t.category}</TableHead>
                                        <TableHead className="font-bold">{t.description}</TableHead>
                                        <TableHead className="font-bold">{t.branch}</TableHead>
                                        <TableHead className="font-bold">{t.paymentMethod}</TableHead>
                                        <TableHead className="font-bold">{t.loggedBy}</TableHead>
                                        <TableHead className="text-end font-bold pe-6">{t.amount}</TableHead>
                                    </TableRow>
                                </TableHeader>
                                <TableBody>
                                    {sortedExpenses.map((exp) => {
                                        const parsed = parseExpenseDescription(exp.description)
                                        const catInfo = getCategoryInfo(parsed.category)
                                        const displayCat = isRtl ? catInfo.labelAr : catInfo.labelEn
                                        const formattedDate = new Date(exp.date).toLocaleDateString(isRtl ? 'ar-EG' : 'en-US', {
                                            year: 'numeric',
                                            month: 'short',
                                            day: 'numeric'
                                        })

                                        return (
                                            <TableRow key={exp.id} className="hover:bg-muted/20 transition-colors">
                                                <TableCell className="font-medium">
                                                    <div className="flex items-center gap-1.5 text-muted-foreground text-xs md:text-sm">
                                                        <Calendar className="h-3.5 w-3.5" />
                                                        {formattedDate}
                                                    </div>
                                                </TableCell>
                                                <TableCell>
                                                    <div className="flex items-center gap-1.5">
                                                        <span 
                                                            className="w-2 h-2 rounded-full" 
                                                            style={{ 
                                                                backgroundColor: catInfo.color,
                                                                boxShadow: `0 0 6px ${catInfo.color}` 
                                                            }}
                                                        />
                                                        <span className="font-semibold text-xs md:text-sm">
                                                            {displayCat}
                                                        </span>
                                                    </div>
                                                </TableCell>
                                                <TableCell className="max-w-[240px] truncate text-xs md:text-sm">
                                                    {parsed.cleanDescription || "-"}
                                                </TableCell>
                                                <TableCell className="text-xs md:text-sm font-medium">
                                                    <div className="flex items-center gap-1.5">
                                                        <Building2 className="h-3.5 w-3.5 text-muted-foreground" />
                                                        {exp.branchName || t.mainOffice}
                                                    </div>
                                                </TableCell>
                                                <TableCell>
                                                    <span className={`inline-flex items-center gap-1 px-2.5 py-0.5 rounded-full text-xs font-semibold ${
                                                        exp.isBank 
                                                            ? "bg-sky-500/10 text-sky-500 border border-sky-500/20" 
                                                            : "bg-emerald-500/10 text-emerald-500 border border-emerald-500/20"
                                                    }`}>
                                                        {exp.isBank ? <Landmark className="h-3 w-3" /> : <Wallet className="h-3 w-3" />}
                                                        {exp.isBank ? t.bank : t.cash}
                                                    </span>
                                                </TableCell>
                                                <TableCell className="text-muted-foreground text-xs md:text-sm">
                                                    <div className="flex items-center gap-1">
                                                        <User className="h-3 w-3" />
                                                        {exp.employeeName}
                                                    </div>
                                                </TableCell>
                                                <TableCell className="text-end font-extrabold text-xs md:text-sm pe-6 text-foreground">
                                                    {exp.amount.toLocaleString(undefined, { minimumFractionDigits: 2 })}
                                                    <span className="text-[10px] font-normal text-muted-foreground ms-1">{t.currency}</span>
                                                </TableCell>
                                            </TableRow>
                                        )
                                    })}
                                </TableBody>
                            </Table>
                        </div>
                    )}
                </CardContent>
            </Card>
        </div>
    )
}
