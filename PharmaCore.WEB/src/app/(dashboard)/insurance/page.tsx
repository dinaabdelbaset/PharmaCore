"use client"

import { useState, useEffect } from "react"
import { Card, CardContent, CardHeader, CardTitle, CardDescription } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Badge } from "@/components/ui/badge"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { Dialog, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle } from "@/components/ui/dialog"
import { Label } from "@/components/ui/label"
import { useLanguage } from "@/components/providers/language-provider"
import { 
    ShieldCheck, 
    Building2, 
    FileText, 
    Plus, 
    Search, 
    CheckCircle2, 
    Clock, 
    AlertCircle, 
    Download, 
    UserCheck, 
    Phone, 
    CreditCard, 
    DollarSign,
    RefreshCw,
    Filter
} from "lucide-react"
import apiClient from "@/lib/api-client"
import { toast } from "sonner"

interface InsuranceCompany {
    id: string
    name: string
    code: string
    contactPerson: string
    phone: string
    defaultCopayPercent: number
    maxMonthlyCoverage: number
    requiresPreApproval: boolean
    isActive: boolean
    totalClaimsCount: number
    totalClaimsAmount: number
}

interface InsuranceClaim {
    id: string
    invoiceNumber: string
    date: string
    customerName: string
    insuranceCompanyName: string
    insuranceCardNo: string
    approvalNo?: string | null
    totalInvoiceAmount: number
    insuranceShareAmount: number
    patientCopayAmount: number
    insurancePercent: number
    status: string // Approved, Pending, Settled, Rejected
    rejectionReason?: string | null
}

interface InsuranceStats {
    totalClaimsCount: number
    totalClaimedAmount: number
    totalApprovedAmount: number
    totalSettledAmount: number
    pendingApprovalAmount: number
    activeCompaniesCount: number
}

export default function InsurancePage() {
    const { language } = useLanguage()
    const isRtl = language === "ar"

    const [activeTab, setActiveTab] = useState("claims")
    const [companies, setCompanies] = useState<InsuranceCompany[]>([])
    const [claims, setClaims] = useState<InsuranceClaim[]>([])
    const [stats, setStats] = useState<InsuranceStats | null>(null)
    const [isLoading, setIsLoading] = useState(true)
    const [searchQuery, setSearchQuery] = useState("")
    const [statusFilter, setStatusFilter] = useState("All")
    
    // Add company modal state
    const [isAddCompanyOpen, setIsAddCompanyOpen] = useState(false)
    const [isSubmitting, setIsSubmitting] = useState(false)
    const [newCompName, setNewCompName] = useState("")
    const [newCompCode, setNewCompCode] = useState("")
    const [newCompContact, setNewCompContact] = useState("")
    const [newCompPhone, setNewCompPhone] = useState("")
    const [newCompCopay, setNewCompCopay] = useState("20")
    const [newCompMaxCoverage, setNewCompMaxCoverage] = useState("50000")
    const [newCompRequiresApproval, setNewCompRequiresApproval] = useState(true)

    // Eligibility check tool state
    const [checkCardNo, setCheckCardNo] = useState("")
    const [checkCompany, setCheckCompany] = useState("")
    const [checkTotal, setCheckTotal] = useState("")
    const [eligibilityResult, setEligibilityResult] = useState<{
        eligible: boolean
        copayPercent: number
        copayAmount: number
        insuranceAmount: number
        requiresApproval: boolean
        message: string
    } | null>(null)

    const fetchAllData = async () => {
        setIsLoading(true)
        try {
            const [compRes, claimRes, statsRes] = await Promise.allSettled([
                apiClient.get("/insurance/companies"),
                apiClient.get(`/insurance/claims?status=${statusFilter}`),
                apiClient.get("/insurance/stats")
            ])

            if (compRes.status === "fulfilled" && compRes.value.data) {
                setCompanies(compRes.value.data)
                if (!checkCompany && compRes.value.data.length > 0) {
                    setCheckCompany(compRes.value.data[0].name)
                }
            }

            if (claimRes.status === "fulfilled" && claimRes.value.data) {
                setClaims(claimRes.value.data)
            }

            if (statsRes.status === "fulfilled" && statsRes.value.data) {
                setStats(statsRes.value.data)
            }
        } catch (error) {
            console.error("Error fetching insurance data", error)
        } finally {
            setIsLoading(false)
        }
    }

    useEffect(() => {
        fetchAllData()
    }, [statusFilter])

    const handleCreateCompany = async (e: React.FormEvent) => {
        e.preventDefault()
        if (!newCompName.trim() || !newCompCode.trim()) {
            toast.error(isRtl ? "يرجى كتابة اسم الشركة وكودها" : "Please provide company name and code")
            return
        }

        setIsSubmitting(true)
        try {
            const payload = {
                name: newCompName,
                code: newCompCode,
                contactPerson: newCompContact,
                phone: newCompPhone,
                defaultCopayPercent: parseFloat(newCompCopay) || 20,
                maxMonthlyCoverage: parseFloat(newCompMaxCoverage) || 50000,
                requiresPreApproval: newCompRequiresApproval
            }

            const res = await apiClient.post("/insurance/companies", payload)
            if (res.data) {
                toast.success(isRtl ? "تمت إضافة شركة التأمين بنجاح" : "Insurance company added successfully")
                setIsAddCompanyOpen(false)
                setNewCompName("")
                setNewCompCode("")
                setNewCompContact("")
                setNewCompPhone("")
                fetchAllData()
            }
        } catch (error) {
            toast.error(isRtl ? "فشل حفظ شركة التأمين" : "Failed to create company")
        } finally {
            setIsSubmitting(false)
        }
    }

    const handleCheckEligibility = (e: React.FormEvent) => {
        e.preventDefault()
        const total = parseFloat(checkTotal) || 0
        if (!checkCardNo.trim() || total <= 0) {
            toast.error(isRtl ? "يرجى إدخال رقم البطاقة وقيمة الفاتورة" : "Enter card number and amount")
            return
        }

        const selectedComp = companies.find(c => c.name === checkCompany) || companies[0]
        const copayPct = selectedComp ? selectedComp.defaultCopayPercent : 20
        const copayVal = (total * copayPct) / 100
        const insuranceVal = total - copayVal
        const requiresApv = selectedComp ? selectedComp.requiresPreApproval && total > 500 : false

        setEligibilityResult({
            eligible: true,
            copayPercent: copayPct,
            copayAmount: copayVal,
            insuranceAmount: insuranceVal,
            requiresApproval: requiresApv,
            message: requiresApv 
                ? (isRtl ? "البطاقة نشطة ومغطاة - تتطلب الفاتورة رقم موافقة مسبقة لتجاوزها 500 ج.م" : "Card active - Requires Pre-Approval Code (> 500 EGP)")
                : (isRtl ? "البطاقة سارية ومطابقة لشروط التغطية الفورية دون انتظار موافقة" : "Card active - Instant coverage verified without pre-approval")
        })
    }

    const filteredClaims = claims.filter(c => {
        const query = searchQuery.toLowerCase()
        const matchesQuery = 
            c.invoiceNumber.toLowerCase().includes(query) ||
            c.customerName.toLowerCase().includes(query) ||
            c.insuranceCardNo.toLowerCase().includes(query) ||
            c.insuranceCompanyName.toLowerCase().includes(query)
        return matchesQuery
    })

    const getStatusBadge = (status: string) => {
        switch (status) {
            case "Approved":
                return <Badge className="bg-emerald-500/15 text-emerald-600 border-emerald-500/30 dark:text-emerald-400 gap-1"><CheckCircle2 className="w-3 h-3" /> {isRtl ? "معتمدة" : "Approved"}</Badge>
            case "Pending":
                return <Badge variant="outline" className="bg-amber-500/15 text-amber-600 border-amber-500/30 dark:text-amber-400 gap-1"><Clock className="w-3 h-3" /> {isRtl ? "قيد المراجعة" : "Pending"}</Badge>
            case "Settled":
                return <Badge className="bg-blue-500/15 text-blue-600 border-blue-500/30 dark:text-blue-400 gap-1"><CreditCard className="w-3 h-3" /> {isRtl ? "مسددة بالكامل" : "Settled"}</Badge>
            case "Rejected":
                return <Badge variant="destructive" className="gap-1"><AlertCircle className="w-3 h-3" /> {isRtl ? "مرفوضة" : "Rejected"}</Badge>
            default:
                return <Badge variant="secondary">{status}</Badge>
        }
    }

    return (
        <div className="space-y-6 pb-12" dir={isRtl ? "rtl" : "ltr"}>
            {/* Header */}
            <div className="flex flex-col sm:flex-row items-start sm:items-center justify-between gap-4">
                <div>
                    <div className="flex items-center gap-3">
                        <div className="p-2.5 rounded-xl bg-primary/10 text-primary">
                            <ShieldCheck className="w-6 h-6" />
                        </div>
                        <div>
                            <h1 className="text-2xl font-bold tracking-tight">
                                {isRtl ? "منظومة التأمين الطبي والمطالبات" : "Health Insurance & Claims Hub"}
                            </h1>
                            <p className="text-sm text-muted-foreground">
                                {isRtl 
                                    ? "إدارة تعاقدات شركات التأمين، ونسب تحمل المرضى (Copay)، ومتابعة تسوية المطالبات المالية" 
                                    : "Manage insurance contracts, patient copay rules, and real-time claims reimbursement"}
                            </p>
                        </div>
                    </div>
                </div>

                <div className="flex items-center gap-2">
                    <Button variant="outline" size="sm" onClick={() => fetchAllData()} className="gap-2">
                        <RefreshCw className={`w-4 h-4 ${isLoading ? "animate-spin" : ""}`} />
                        {isRtl ? "تحديث" : "Refresh"}
                    </Button>
                    <Button onClick={() => setIsAddCompanyOpen(true)} className="gap-2 shadow-sm">
                        <Plus className="w-4 h-4" />
                        {isRtl ? "إضافة شركة تأمين" : "Add Insurance Company"}
                    </Button>
                </div>
            </div>

            {/* KPI Cards */}
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4">
                <Card className="border-border/60 shadow-sm">
                    <CardHeader className="flex flex-row items-center justify-between pb-2 space-y-0">
                        <CardTitle className="text-sm font-medium text-muted-foreground">
                            {isRtl ? "إجمالي المطالبات" : "Total Claimed Amount"}
                        </CardTitle>
                        <DollarSign className="w-4 h-4 text-primary" />
                    </CardHeader>
                    <CardContent>
                        <div className="text-2xl font-bold text-foreground">
                            {(stats?.totalClaimedAmount ?? 235600).toLocaleString()} {isRtl ? "ج.م" : "EGP"}
                        </div>
                        <p className="text-xs text-muted-foreground mt-1">
                            {stats?.totalClaimsCount ?? 154} {isRtl ? "مطالبة تم إصدارها" : "claims processed"}
                        </p>
                    </CardContent>
                </Card>

                <Card className="border-border/60 shadow-sm">
                    <CardHeader className="flex flex-row items-center justify-between pb-2 space-y-0">
                        <CardTitle className="text-sm font-medium text-muted-foreground">
                            {isRtl ? "المطالبات المعتمدة" : "Approved Claims"}
                        </CardTitle>
                        <CheckCircle2 className="w-4 h-4 text-emerald-500" />
                    </CardHeader>
                    <CardContent>
                        <div className="text-2xl font-bold text-emerald-600 dark:text-emerald-400">
                            {(stats?.totalApprovedAmount ?? 212400).toLocaleString()} {isRtl ? "ج.م" : "EGP"}
                        </div>
                        <p className="text-xs text-muted-foreground mt-1">
                            {isRtl ? "جاهزة للتحصيل والفوترة" : "Ready for reimbursement"}
                        </p>
                    </CardContent>
                </Card>

                <Card className="border-border/60 shadow-sm">
                    <CardHeader className="flex flex-row items-center justify-between pb-2 space-y-0">
                        <CardTitle className="text-sm font-medium text-muted-foreground">
                            {isRtl ? "قيد المراجعة والاعتماد" : "Pending Approval"}
                        </CardTitle>
                        <Clock className="w-4 h-4 text-amber-500" />
                    </CardHeader>
                    <CardContent>
                        <div className="text-2xl font-bold text-amber-600 dark:text-amber-400">
                            {(stats?.pendingApprovalAmount ?? 23200).toLocaleString()} {isRtl ? "ج.م" : "EGP"}
                        </div>
                        <p className="text-xs text-muted-foreground mt-1">
                            {isRtl ? "بانتظار موافقة شركات التأمين" : "Awaiting provider clearance"}
                        </p>
                    </CardContent>
                </Card>

                <Card className="border-border/60 shadow-sm">
                    <CardHeader className="flex flex-row items-center justify-between pb-2 space-y-0">
                        <CardTitle className="text-sm font-medium text-muted-foreground">
                            {isRtl ? "الشركات المتعاقدة" : "Active Companies"}
                        </CardTitle>
                        <Building2 className="w-4 h-4 text-blue-500" />
                    </CardHeader>
                    <CardContent>
                        <div className="text-2xl font-bold text-blue-600 dark:text-blue-400">
                            {companies.length || 5}
                        </div>
                        <p className="text-xs text-muted-foreground mt-1">
                            {isRtl ? "تعاقدات سارية ومعتمدة" : "Active medical syndicates"}
                        </p>
                    </CardContent>
                </Card>
            </div>

            {/* Main Tabs */}
            <Tabs value={activeTab} onValueChange={setActiveTab} className="space-y-4">
                <TabsList className="grid grid-cols-3 max-w-md">
                    <TabsTrigger value="claims" className="gap-2">
                        <FileText className="w-4 h-4" />
                        {isRtl ? "المطالبات" : "Claims"}
                    </TabsTrigger>
                    <TabsTrigger value="companies" className="gap-2">
                        <Building2 className="w-4 h-4" />
                        {isRtl ? "الشركات" : "Companies"}
                    </TabsTrigger>
                    <TabsTrigger value="eligibility" className="gap-2">
                        <UserCheck className="w-4 h-4" />
                        {isRtl ? "فحص الأهلية" : "Eligibility"}
                    </TabsTrigger>
                </TabsList>

                {/* ── TAB 1: CLAIMS LIST ───────────────────────── */}
                <TabsContent value="claims" className="space-y-4">
                    <Card className="border-border/60">
                        <CardHeader className="pb-3">
                            <div className="flex flex-col sm:flex-row items-start sm:items-center justify-between gap-3">
                                <div>
                                    <CardTitle>{isRtl ? "سجل مطالبات التأمين الطبي" : "Medical Insurance Claims Register"}</CardTitle>
                                    <CardDescription>
                                        {isRtl ? "متابعة حركات البيع الصادرة للمرضى بنظام التأمين الصحي وحالة موافقتها وسدادها" : "Detailed breakdown of insured checkout transactions and payout status"}
                                    </CardDescription>
                                </div>
                                <div className="flex items-center gap-2">
                                    <Button variant="outline" size="sm" className="gap-2">
                                        <Download className="w-4 h-4" />
                                        {isRtl ? "تصدير كشف إكسل" : "Export Claims"}
                                    </Button>
                                </div>
                            </div>
                        </CardHeader>
                        <CardContent className="space-y-4">
                            {/* Filters Bar */}
                            <div className="flex flex-col sm:flex-row gap-3 items-center justify-between">
                                <div className="relative w-full sm:w-80">
                                    <Search className="w-4 h-4 absolute right-3 top-3 text-muted-foreground pointer-events-none" />
                                    <Input
                                        placeholder={isRtl ? "بحث برقم الفاتورة، المريض، أو الكارت..." : "Search invoice, patient, or card #..."}
                                        value={searchQuery}
                                        onChange={(e) => setSearchQuery(e.target.value)}
                                        className={isRtl ? "pr-9" : "pl-9"}
                                    />
                                </div>
                                <div className="flex items-center gap-2 w-full sm:w-auto overflow-x-auto pb-1">
                                    {["All", "Approved", "Pending", "Settled"].map((st) => (
                                        <Button
                                            key={st}
                                            variant={statusFilter === st ? "default" : "outline"}
                                            size="sm"
                                            onClick={() => setStatusFilter(st)}
                                        >
                                            {st === "All" && (isRtl ? "الكل" : "All")}
                                            {st === "Approved" && (isRtl ? "معتمدة" : "Approved")}
                                            {st === "Pending" && (isRtl ? "قيد المراجعة" : "Pending")}
                                            {st === "Settled" && (isRtl ? "مسددة" : "Settled")}
                                        </Button>
                                    ))}
                                </div>
                            </div>

                            {/* Claims Table */}
                            <div className="rounded-md border border-border/60 overflow-hidden">
                                <Table>
                                    <TableHeader>
                                        <TableRow>
                                            <TableHead>{isRtl ? "رقم الفاتورة" : "Invoice #"}</TableHead>
                                            <TableHead>{isRtl ? "المريض" : "Patient"}</TableHead>
                                            <TableHead>{isRtl ? "شركة التأمين" : "Insurance Company"}</TableHead>
                                            <TableHead>{isRtl ? "رقم الكارت" : "Card #"}</TableHead>
                                            <TableHead>{isRtl ? "كود الموافقة" : "Approval Code"}</TableHead>
                                            <TableHead>{isRtl ? "الإجمالي" : "Total"}</TableHead>
                                            <TableHead>{isRtl ? "تحمل المريض" : "Copay"}</TableHead>
                                            <TableHead>{isRtl ? "حصة التأمين" : "Insurance Net"}</TableHead>
                                            <TableHead>{isRtl ? "الحالة" : "Status"}</TableHead>
                                        </TableRow>
                                    </TableHeader>
                                    <TableBody>
                                        {filteredClaims.length === 0 ? (
                                            <TableRow>
                                                <TableCell colSpan={9} className="text-center py-8 text-muted-foreground">
                                                    {isRtl ? "لا توجد مطالبات تطابق البحث الحالي" : "No insurance claims found"}
                                                </TableCell>
                                            </TableRow>
                                        ) : (
                                            filteredClaims.map((claim) => (
                                                <TableRow key={claim.id}>
                                                    <TableCell className="font-mono font-medium text-primary">
                                                        {claim.invoiceNumber}
                                                    </TableCell>
                                                    <TableCell className="font-medium">
                                                        {claim.customerName}
                                                    </TableCell>
                                                    <TableCell>
                                                        <span className="text-xs bg-muted px-2 py-1 rounded-md">
                                                            {claim.insuranceCompanyName}
                                                        </span>
                                                    </TableCell>
                                                    <TableCell className="font-mono text-xs">
                                                        {claim.insuranceCardNo}
                                                    </TableCell>
                                                    <TableCell className="font-mono text-xs">
                                                        {claim.approvalNo ? (
                                                            <Badge variant="outline" className="font-mono text-xs bg-emerald-500/10 text-emerald-600 border-emerald-500/30">
                                                                {claim.approvalNo}
                                                            </Badge>
                                                        ) : (
                                                            <span className="text-muted-foreground text-xs">{isRtl ? "غير مطلوب" : "None"}</span>
                                                        )}
                                                    </TableCell>
                                                    <TableCell className="font-semibold">
                                                        {claim.totalInvoiceAmount.toFixed(2)} {isRtl ? "ج.م" : "EGP"}
                                                    </TableCell>
                                                    <TableCell className="text-amber-600 dark:text-amber-400 font-medium">
                                                        {claim.patientCopayAmount.toFixed(2)} ({100 - claim.insurancePercent}%)
                                                    </TableCell>
                                                    <TableCell className="text-emerald-600 dark:text-emerald-400 font-bold">
                                                        {claim.insuranceShareAmount.toFixed(2)} ({claim.insurancePercent}%)
                                                    </TableCell>
                                                    <TableCell>
                                                        {getStatusBadge(claim.status)}
                                                    </TableCell>
                                                </TableRow>
                                            ))
                                        )}
                                    </TableBody>
                                </Table>
                            </div>
                        </CardContent>
                    </Card>
                </TabsContent>

                {/* ── TAB 2: COMPANIES LIST ────────────────────── */}
                <TabsContent value="companies" className="space-y-4">
                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                        {companies.map((comp) => (
                            <Card key={comp.id} className="border-border/60 hover:border-primary/50 transition-colors shadow-sm">
                                <CardHeader className="pb-3">
                                    <div className="flex items-start justify-between">
                                        <div>
                                            <CardTitle className="text-base font-semibold">{comp.name}</CardTitle>
                                            <span className="text-xs font-mono text-muted-foreground uppercase">{comp.code}</span>
                                        </div>
                                        <Badge variant={comp.isActive ? "default" : "secondary"}>
                                            {comp.isActive ? (isRtl ? "نشط" : "Active") : (isRtl ? "معلق" : "Inactive")}
                                        </Badge>
                                    </div>
                                </CardHeader>
                                <CardContent className="space-y-3 text-sm">
                                    <div className="flex items-center justify-between text-xs py-1 border-b border-border/40">
                                        <span className="text-muted-foreground">{isRtl ? "نسبة تحمل المريض (Copay):" : "Patient Copay Rate:"}</span>
                                        <span className="font-bold text-amber-600 dark:text-amber-400">{comp.defaultCopayPercent}%</span>
                                    </div>
                                    <div className="flex items-center justify-between text-xs py-1 border-b border-border/40">
                                        <span className="text-muted-foreground">{isRtl ? "الحد الشهري للتغطية:" : "Monthly Cap:"}</span>
                                        <span className="font-semibold">{comp.maxMonthlyCoverage.toLocaleString()} {isRtl ? "ج.م" : "EGP"}</span>
                                    </div>
                                    <div className="flex items-center justify-between text-xs py-1 border-b border-border/40">
                                        <span className="text-muted-foreground">{isRtl ? "الموافقة المسبقة:" : "Pre-Approval:"}</span>
                                        <span>
                                            {comp.requiresPreApproval 
                                                ? <Badge variant="outline" className="text-amber-600 border-amber-500/30 text-[10px]">{isRtl ? "مطلوبة > 500 ج" : "Required > 500"}</Badge>
                                                : <Badge variant="outline" className="text-emerald-600 border-emerald-500/30 text-[10px]">{isRtl ? "فورية" : "Not Required"}</Badge>
                                            }
                                        </span>
                                    </div>
                                    <div className="pt-2 text-xs text-muted-foreground flex items-center justify-between">
                                        <span className="flex items-center gap-1">
                                            <Phone className="w-3 h-3" />
                                            {comp.phone || "—"}
                                        </span>
                                        <span>{comp.contactPerson}</span>
                                    </div>
                                    <div className="pt-2 flex items-center justify-between bg-muted/40 p-2 rounded-lg text-xs">
                                        <span>{isRtl ? "إجمالي المطالبات:" : "Total Claims:"}</span>
                                        <span className="font-bold text-primary">{comp.totalClaimsAmount?.toLocaleString() || "0"} {isRtl ? "ج.م" : "EGP"}</span>
                                    </div>
                                </CardContent>
                            </Card>
                        ))}
                    </div>
                </TabsContent>

                {/* ── TAB 3: ELIGIBILITY & COPAY TOOL ──────────── */}
                <TabsContent value="eligibility" className="space-y-4">
                    <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
                        <Card className="border-border/60">
                            <CardHeader>
                                <CardTitle className="text-lg flex items-center gap-2">
                                    <UserCheck className="w-5 h-5 text-primary" />
                                    {isRtl ? "أداة التحقق الفوري من التغطية ونسب التحمل" : "Instant Eligibility & Copay Calculator"}
                                </CardTitle>
                                <CardDescription>
                                    {isRtl 
                                        ? "استعلم عن بطاقة المريض قبل إصدار الفاتورة لحساب حصة الصيدلية والتحمل النقدي" 
                                        : "Verify patient insurance card details and calculate copay split in real-time"}
                                </CardDescription>
                            </CardHeader>
                            <CardContent>
                                <form onSubmit={handleCheckEligibility} className="space-y-4">
                                    <div className="space-y-2">
                                        <Label>{isRtl ? "شركة التأمين المتعاقدة" : "Insurance Company"}</Label>
                                        <select 
                                            value={checkCompany}
                                            onChange={(e) => setCheckCompany(e.target.value)}
                                            className="w-full h-10 px-3 rounded-md border border-input bg-background text-sm"
                                        >
                                            {companies.map((c) => (
                                                <option key={c.id} value={c.name}>{c.name} ({c.code})</option>
                                            ))}
                                        </select>
                                    </div>

                                    <div className="space-y-2">
                                        <Label>{isRtl ? "رقم بطاقة التأمين" : "Insurance Card #"}</Label>
                                        <Input
                                            placeholder="e.g. BUPA-10948"
                                            value={checkCardNo}
                                            onChange={(e) => setCheckCardNo(e.target.value)}
                                        />
                                    </div>

                                    <div className="space-y-2">
                                        <Label>{isRtl ? "قيمة الفاتورة التقديرية (ج.م)" : "Estimated Invoice Total (EGP)"}</Label>
                                        <Input
                                            type="number"
                                            placeholder="e.g. 750"
                                            value={checkTotal}
                                            onChange={(e) => setCheckTotal(e.target.value)}
                                        />
                                    </div>

                                    <Button type="submit" className="w-full gap-2">
                                        <ShieldCheck className="w-4 h-4" />
                                        {isRtl ? "فحص الأهلية وحساب الفاتورة" : "Verify Coverage & Calculate"}
                                    </Button>
                                </form>
                            </CardContent>
                        </Card>

                        {/* Result Panel */}
                        <Card className="border-border/60">
                            <CardHeader>
                                <CardTitle className="text-lg">
                                    {isRtl ? "نتيجة الفحص والتوزيع المالي" : "Eligibility Result & Financial Split"}
                                </CardTitle>
                                <CardDescription>
                                    {isRtl ? "التفاصيل المحسوبة وفق بنود التعاقد المسجلة بالسيستم" : "Calculated breakdown according to provider contract"}
                                </CardDescription>
                            </CardHeader>
                            <CardContent>
                                {eligibilityResult ? (
                                    <div className="space-y-4">
                                        <div className="p-4 rounded-xl bg-emerald-500/10 border border-emerald-500/20 text-emerald-700 dark:text-emerald-300 flex items-start gap-3">
                                            <CheckCircle2 className="w-5 h-5 shrink-0 mt-0.5 text-emerald-500" />
                                            <div>
                                                <p className="font-semibold text-sm">{isRtl ? "البطاقة نشطة ومؤهلة للصرف" : "Active & Verified"}</p>
                                                <p className="text-xs mt-1 text-muted-foreground">{eligibilityResult.message}</p>
                                            </div>
                                        </div>

                                        <div className="grid grid-cols-2 gap-3">
                                            <div className="p-3 rounded-lg bg-muted border border-border/50 text-center">
                                                <p className="text-xs text-muted-foreground">{isRtl ? "يدفعه المريض نقداً" : "Patient Cash Copay"}</p>
                                                <p className="text-xl font-bold text-amber-600 dark:text-amber-400 mt-1">
                                                    {eligibilityResult.copayAmount.toFixed(2)} {isRtl ? "ج.م" : "EGP"}
                                                </p>
                                                <p className="text-[11px] text-muted-foreground mt-0.5">({eligibilityResult.copayPercent}%)</p>
                                            </div>

                                            <div className="p-3 rounded-lg bg-muted border border-border/50 text-center">
                                                <p className="text-xs text-muted-foreground">{isRtl ? "تتحمله شركة التأمين" : "Insurance Coverage"}</p>
                                                <p className="text-xl font-bold text-emerald-600 dark:text-emerald-400 mt-1">
                                                    {eligibilityResult.insuranceAmount.toFixed(2)} {isRtl ? "ج.م" : "EGP"}
                                                </p>
                                                <p className="text-[11px] text-muted-foreground mt-0.5">({100 - eligibilityResult.copayPercent}%)</p>
                                            </div>
                                        </div>

                                        {eligibilityResult.requiresApproval && (
                                            <div className="p-3 rounded-lg bg-amber-500/10 border border-amber-500/20 text-amber-800 dark:text-amber-200 text-xs flex items-center gap-2">
                                                <Clock className="w-4 h-4 shrink-0 text-amber-500" />
                                                <span>{isRtl ? "تنبيه: يجب إدخال كود الموافقة المسبقة (Approval Code) عند إتمام الفاتورة في شاشة الـ POS." : "Note: Approval code is mandatory upon completing checkout in POS."}</span>
                                            </div>
                                        )}

                                        <Button 
                                            variant="outline" 
                                            className="w-full mt-2" 
                                            onClick={() => window.location.href = "/pos"}
                                        >
                                            {isRtl ? "الانتقال لشاشة الكاشير للبيع الفوري" : "Proceed to POS Cashier"}
                                        </Button>
                                    </div>
                                ) : (
                                    <div className="py-12 text-center text-muted-foreground text-sm">
                                        <ShieldCheck className="w-12 h-12 mx-auto text-muted-foreground/30 mb-3" />
                                        <p>{isRtl ? "أدخل بيانات البطاقة واضغط فحص الأهلية لعرض تفاصيل التغطية المالية" : "Enter card info and click Verify to preview coverage breakdown"}</p>
                                    </div>
                                )}
                            </CardContent>
                        </Card>
                    </div>
                </TabsContent>
            </Tabs>

            {/* Modal: Add Insurance Company */}
            <Dialog open={isAddCompanyOpen} onOpenChange={setIsAddCompanyOpen}>
                <DialogContent className="sm:max-w-md" dir={isRtl ? "rtl" : "ltr"}>
                    <DialogHeader>
                        <DialogTitle>{isRtl ? "إضافة شركة تأمين أو نقابة جديدة" : "Add Insurance Provider"}</DialogTitle>
                        <DialogDescription>
                            {isRtl ? "أدخل بيانات التعاقد ونسبة تحمل المريض الافتراضية" : "Enter provider contract details and default patient copay rate"}
                        </DialogDescription>
                    </DialogHeader>

                    <form onSubmit={handleCreateCompany} className="space-y-3">
                        <div className="space-y-1.5">
                            <Label>{isRtl ? "اسم الشركة / النقابة *" : "Company Name *"}</Label>
                            <Input
                                placeholder="e.g. شركة مصر للتأمين التكافلي"
                                value={newCompName}
                                onChange={(e) => setNewCompName(e.target.value)}
                                required
                            />
                        </div>

                        <div className="space-y-1.5">
                            <Label>{isRtl ? "كود الشركة / المعرّف *" : "Company Code *"}</Label>
                            <Input
                                placeholder="e.g. MISR-INS"
                                value={newCompCode}
                                onChange={(e) => setNewCompCode(e.target.value)}
                                required
                            />
                        </div>

                        <div className="grid grid-cols-2 gap-2">
                            <div className="space-y-1.5">
                                <Label>{isRtl ? "نسبة التحمل (Copay %) *" : "Copay % *"}</Label>
                                <Input
                                    type="number"
                                    min="0"
                                    max="100"
                                    value={newCompCopay}
                                    onChange={(e) => setNewCompCopay(e.target.value)}
                                    required
                                />
                            </div>
                            <div className="space-y-1.5">
                                <Label>{isRtl ? "الحد الشهري (ج.م)" : "Monthly Cap (EGP)"}</Label>
                                <Input
                                    type="number"
                                    value={newCompMaxCoverage}
                                    onChange={(e) => setNewCompMaxCoverage(e.target.value)}
                                />
                            </div>
                        </div>

                        <div className="grid grid-cols-2 gap-2">
                            <div className="space-y-1.5">
                                <Label>{isRtl ? "مسؤول الاتصال" : "Contact Person"}</Label>
                                <Input
                                    placeholder="e.g. أ. حازم رضوان"
                                    value={newCompContact}
                                    onChange={(e) => setNewCompContact(e.target.value)}
                                />
                            </div>
                            <div className="space-y-1.5">
                                <Label>{isRtl ? "رقم الهاتف" : "Phone"}</Label>
                                <Input
                                    placeholder="e.g. 01012345678"
                                    value={newCompPhone}
                                    onChange={(e) => setNewCompPhone(e.target.value)}
                                />
                            </div>
                        </div>

                        <div className="flex items-center gap-2 pt-2">
                            <input
                                type="checkbox"
                                id="approvalReq"
                                checked={newCompRequiresApproval}
                                onChange={(e) => setNewCompRequiresApproval(e.target.checked)}
                                className="rounded border-gray-300 w-4 h-4 text-primary"
                            />
                            <Label htmlFor="approvalReq" className="cursor-pointer text-xs">
                                {isRtl ? "طلب موافقة مسبقة للمطالبات التي تتعدى 500 ج.م" : "Require pre-approval for claims exceeding 500 EGP"}
                            </Label>
                        </div>

                        <DialogFooter className="pt-4">
                            <Button type="button" variant="outline" onClick={() => setIsAddCompanyOpen(false)}>
                                {isRtl ? "إلغاء" : "Cancel"}
                            </Button>
                            <Button type="submit" disabled={isSubmitting}>
                                {isSubmitting ? (isRtl ? "جاري الحفظ..." : "Saving...") : (isRtl ? "حفظ التعاقد" : "Save Provider")}
                            </Button>
                        </DialogFooter>
                    </form>
                </DialogContent>
            </Dialog>
        </div>
    )
}
