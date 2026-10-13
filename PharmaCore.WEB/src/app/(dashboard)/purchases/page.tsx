"use client"

import { useState, useEffect } from "react"
import { Card, CardContent } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogDescription, DialogFooter } from "@/components/ui/dialog"
import { Label } from "@/components/ui/label"
import { useLanguage } from "@/components/providers/language-provider"
import { Search, Plus, Loader2, ShoppingBag, ShoppingCart, Calendar, Check, Circle, Trash2, Info } from "lucide-react"
import apiClient from "@/lib/api-client"
import { toast } from "sonner"

interface Purchase {
    id: string
    invoiceNumber: string
    date: string
    supplierName: string
    total: number
    isPaid: boolean
    branchName: string | null
}

interface SupplierLookup {
    id: string
    name: string
}

interface BranchLookup {
    id: string
    name: string
    nameAr: string | null
}

interface ProductLookup {
    id: string
    productCode: string
    nameEn: string
    nameAr: string | null
}

interface InvoiceLine {
    productId: string
    barcode: string
    quantity: number
    unitPrice: number
    expiryDate: string
}

export default function PurchasesPage() {
    const { language, t } = useLanguage()
    const isRtl = language === "ar"

    const [purchases, setPurchases] = useState<Purchase[]>([])
    const [search, setSearch] = useState("")
    const [isLoading, setIsLoading] = useState(true)
    const [isOpen, setIsOpen] = useState(false)
    const [isSaving, setIsSaving] = useState(false)

    // Lookup states
    const [suppliers, setSuppliers] = useState<SupplierLookup[]>([])
    const [branches, setBranches] = useState<BranchLookup[]>([])
    const [products, setProducts] = useState<ProductLookup[]>([])

    // Invoice Form states
    const [branchId, setBranchId] = useState("")
    const [supplierId, setSupplierId] = useState("")
    const [invoiceNumber, setInvoiceNumber] = useState("")
    const [invoiceDate, setInvoiceDate] = useState(new Date().toISOString().split('T')[0])
    const [notes, setNotes] = useState("")
    const [isPaid, setIsPaid] = useState(false)
    const [lines, setLines] = useState<InvoiceLine[]>([])

    // Active line state
    const [activeProductId, setActiveProductId] = useState("")
    const [activeBarcode, setActiveBarcode] = useState("")
    const [activeQty, setActiveQty] = useState(1)
    const [activePrice, setActivePrice] = useState(0)
    const [activeExpiry, setActiveExpiry] = useState("")

    const fetchPurchases = async () => {
        setIsLoading(true)
        try {
            const res = await apiClient.get(`/purchases?search=${encodeURIComponent(search)}`)
            if (res.data) {
                setPurchases(res.data)
            }
        } catch (err: any) {
            console.error("Failed to load purchases", err)
            toast.error(isRtl ? "فشل تحميل المشتريات" : "Failed to load purchases")
        } finally {
            setIsLoading(false)
        }
    }

    const fetchLookups = async () => {
        try {
            const [supRes, brRes, prodRes] = await Promise.all([
                apiClient.get("/suppliers"),
                apiClient.get("/branches"),
                apiClient.get("/products/light")
            ])
            setSuppliers(supRes.data || [])
            setBranches(brRes.data || [])
            setProducts(prodRes.data || [])
            
            // Set defaults if available
            if (brRes.data && brRes.data.length > 0) setBranchId(brRes.data[0].id)
            if (supRes.data && supRes.data.length > 0) setSupplierId(supRes.data[0].id)
        } catch (err: any) {
            console.error("Failed to load invoice creation lookups", err)
        }
    }

    useEffect(() => {
        fetchPurchases()
        fetchLookups()
    }, [search])

    const handleAddLine = () => {
        if (!activeProductId) {
            toast.warning(isRtl ? "يرجى اختيار المنتج أولاً" : "Please select a product first")
            return
        }
        if (!activeBarcode) {
            toast.warning(isRtl ? "يرجى إدخال الباركود" : "Please enter a barcode")
            return
        }
        if (activeQty <= 0) {
            toast.warning(isRtl ? "الكمية يجب أن تكون أكبر من الصفر" : "Quantity must be greater than zero")
            return
        }

        const newLine: InvoiceLine = {
            productId: activeProductId,
            barcode: activeBarcode,
            quantity: activeQty,
            unitPrice: activePrice,
            expiryDate: activeExpiry || new Date(Date.now() + 1000 * 60 * 60 * 24 * 365 * 2).toISOString().split('T')[0] // default 2 years
        }

        setLines(prev => [...prev, newLine])
        
        // Reset line entry form
        setActiveProductId("")
        setActiveBarcode("")
        setActiveQty(1)
        setActivePrice(0)
        setActiveExpiry("")
    }

    const handleRemoveLine = (idx: number) => {
        setLines(prev => prev.filter((_, i) => i !== idx))
    }

    const handleSaveInvoice = async (e: React.FormEvent) => {
        e.preventDefault()
        if (!supplierId) {
            toast.warning(isRtl ? "يرجى اختيار المورد" : "Please select a supplier")
            return
        }
        if (!branchId) {
            toast.warning(isRtl ? "يرجى اختيار الفرع المستلم" : "Please select receiving branch")
            return
        }
        if (!invoiceNumber) {
            toast.warning(isRtl ? "يرجى إدخال رقم الفاتورة" : "Please enter invoice number")
            return
        }
        if (lines.length === 0) {
            toast.warning(isRtl ? "يرجى إضافة صنف واحد على الأقل للفاتورة" : "Please add at least one item to the invoice")
            return
        }

        setIsSaving(true)
        try {
            await apiClient.post("/purchases", {
                branchId,
                supplierId,
                invoiceNumber,
                date: new Date(invoiceDate).toISOString(),
                notes: notes || null,
                isPaid,
                lines
            })

            toast.success(isRtl ? "تم تسجيل فاتورة الشراء وتوريد المخزون بنجاح" : "Purchase invoice registered and stock updated")
            setIsOpen(false)
            // Reset form
            setInvoiceNumber("")
            setNotes("")
            setLines([])
            setIsPaid(false)
            // Reload list
            fetchPurchases()
        } catch (err: any) {
            console.error("Failed to save purchase invoice", err)
            toast.error(isRtl ? "فشل حفظ الفاتورة وتوريد المخزون" : "Failed to register purchase invoice")
        } finally {
            setIsSaving(false)
        }
    }

    const getInvoiceTotal = () => {
        return lines.reduce((sum, line) => sum + (line.quantity * line.unitPrice), 0)
    }

    return (
        <div className="flex-1 space-y-6 p-4 md:p-6 lg:p-8" dir={isRtl ? "rtl" : "ltr"}>
            <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
                <div>
                    <h2 className="text-2xl md:text-3xl font-bold tracking-tight flex items-center gap-2">
                        <ShoppingBag className="h-7 w-7 text-primary" />
                        {t("purchases.title")}
                    </h2>
                    <p className="text-sm text-muted-foreground">
                        {t("purchases.desc")}
                    </p>
                </div>
                <Button onClick={() => setIsOpen(true)} className="flex items-center gap-2">
                    <Plus className="h-4 w-4" />
                    {t("purchases.new_invoice")}
                </Button>
            </div>

            {/* Search Box */}
            <Card className="shadow-sm border border-border/50 bg-gradient-to-br from-card to-background">
                <CardContent className="pt-6">
                    <div className="relative">
                        <Search className={isRtl ? "absolute right-3 top-2.5 h-4 w-4 text-muted-foreground" : "absolute left-3 top-2.5 h-4 w-4 text-muted-foreground"} />
                        <Input
                            placeholder={isRtl ? "البحث برقم الفاتورة أو اسم المورد..." : "Search by invoice number or supplier..."}
                            value={search}
                            onChange={(e) => setSearch(e.target.value)}
                            className={isRtl ? "pr-9 pl-4" : "pl-9 pr-4"}
                        />
                    </div>
                </CardContent>
            </Card>

            {/* Purchases List */}
            <Card className="shadow-sm border border-border/50 bg-gradient-to-br from-card to-background">
                <CardContent className="p-0">
                    <div className="overflow-x-auto">
                        <table className="w-full text-sm text-left rtl:text-right">
                            <thead>
                                <tr className="border-b border-border/50 text-xs text-muted-foreground bg-muted/40">
                                    <th className="p-4 font-semibold">{t("purchases.invoice_number")}</th>
                                    <th className="p-4 font-semibold">{t("purchases.supplier")}</th>
                                    <th className="p-4 font-semibold">{t("purchases.date")}</th>
                                    <th className="p-4 font-semibold">{t("purchases.branch")}</th>
                                    <th className="p-4 font-semibold text-right rtl:text-left">{t("purchases.total")}</th>
                                    <th className="p-4 text-center font-semibold">{t("purchases.status")}</th>
                                </tr>
                            </thead>
                            <tbody className="divide-y divide-border/30">
                                {isLoading ? (
                                    <tr>
                                        <td colSpan={6} className="py-12 text-center text-muted-foreground">
                                            <Loader2 className="h-7 w-7 animate-spin mx-auto text-primary mb-2" />
                                            <span>{isRtl ? "جاري تحميل الفواتير..." : "Loading invoices..."}</span>
                                        </td>
                                    </tr>
                                ) : purchases.length > 0 ? (
                                    purchases.map((p) => (
                                        <tr key={p.id} className="hover:bg-muted/20 transition-colors">
                                            <td className="p-4 font-mono font-bold text-xs text-primary">{p.invoiceNumber}</td>
                                            <td className="p-4 font-semibold text-zinc-900 dark:text-zinc-100">{p.supplierName}</td>
                                            <td className="p-4 text-xs font-mono text-muted-foreground">
                                                {new Date(p.date).toLocaleDateString()}
                                            </td>
                                            <td className="p-4 text-xs text-muted-foreground">{p.branchName || "—"}</td>
                                            <td className="p-4 text-right rtl:text-left font-mono font-bold text-emerald-500">
                                                {p.total.toLocaleString(undefined, { minimumFractionDigits: 2 })} EGP
                                            </td>
                                            <td className="p-4 text-center">
                                                <span className={`inline-flex items-center gap-1 px-2.5 py-0.5 rounded-full text-[10px] font-bold border ${
                                                    p.isPaid 
                                                        ? "bg-green-500/10 text-green-500 border-green-500/20" 
                                                        : "bg-red-500/10 text-red-500 border-red-500/20"
                                                }`}>
                                                    {p.isPaid ? t("purchases.paid") : t("purchases.unpaid")}
                                                </span>
                                            </td>
                                        </tr>
                                    ))
                                ) : (
                                    <tr>
                                        <td colSpan={6} className="py-8 text-center text-muted-foreground text-xs">
                                            {isRtl ? "لا توجد فواتير مشتريات مسجلة." : "No purchase invoices registered."}
                                        </td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                </CardContent>
            </Card>

            {/* Create Invoice Dialog */}
            <Dialog open={isOpen} onOpenChange={setIsOpen}>
                <DialogContent className="sm:max-w-[700px] max-h-[90vh] overflow-y-auto" dir={isRtl ? "rtl" : "ltr"}>
                    <DialogHeader className={isRtl ? "text-right" : "text-left"}>
                        <DialogTitle>{t("purchases.new_invoice")}</DialogTitle>
                        <DialogDescription>
                            {isRtl ? "سجل تفاصيل التوريد والأصناف المشتراة لترقية مستويات المخزون بالفروع." : "Enter purchase details and products to restock pharmacy branches."}
                        </DialogDescription>
                    </DialogHeader>
                    <form onSubmit={handleSaveInvoice} className="space-y-4 py-2">
                        
                        {/* Header Details */}
                        <div className="grid grid-cols-2 gap-4">
                            <div className="space-y-1">
                                <Label>{t("purchases.invoice_number")} *</Label>
                                <Input 
                                    value={invoiceNumber} 
                                    onChange={(e) => setInvoiceNumber(e.target.value)} 
                                    placeholder="E.g., INV-98765" 
                                    required 
                                />
                            </div>
                            <div className="space-y-1">
                                <Label>{t("purchases.date")} *</Label>
                                <Input 
                                    type="date" 
                                    value={invoiceDate} 
                                    onChange={(e) => setInvoiceDate(e.target.value)} 
                                    required 
                                />
                            </div>
                        </div>

                        <div className="grid grid-cols-2 gap-4">
                            <div className="space-y-1">
                                <Label>{t("purchases.supplier")} *</Label>
                                <select 
                                    value={supplierId} 
                                    onChange={(e) => setSupplierId(e.target.value)}
                                    className="w-full bg-background border rounded-md p-2 text-sm"
                                >
                                    {suppliers.map(s => <option key={s.id} value={s.id}>{s.name}</option>)}
                                </select>
                            </div>
                            <div className="space-y-1">
                                <Label>{t("purchases.branch")} *</Label>
                                <select 
                                    value={branchId} 
                                    onChange={(e) => setBranchId(e.target.value)}
                                    className="w-full bg-background border rounded-md p-2 text-sm"
                                >
                                    {branches.map(b => <option key={b.id} value={b.id}>{isRtl ? (b.nameAr || b.name) : b.name}</option>)}
                                </select>
                            </div>
                        </div>

                        {/* Invoice Items Area */}
                        <div className="border border-border/60 rounded-xl p-4 bg-muted/20 space-y-3">
                            <h3 className="text-xs font-bold text-zinc-500 uppercase flex items-center gap-1.5">
                                <ShoppingCart className="h-4 w-4 text-primary" />
                                {isRtl ? "إضافة أصناف الفاتورة" : "Add Invoice Items"}
                            </h3>
                            
                            <div className="grid grid-cols-2 gap-3">
                                <div className="space-y-1">
                                    <Label className="text-xs">{t("purchases.product")}</Label>
                                    <select 
                                        value={activeProductId} 
                                        onChange={(e) => {
                                            setActiveProductId(e.target.value)
                                            // Pre-fill barcode matching search
                                            const match = products.find(p => p.id === e.target.value)
                                            if (match) setActiveBarcode(match.productCode)
                                        }}
                                        className="w-full bg-background border rounded-md p-1.5 text-xs"
                                    >
                                        <option value="">{isRtl ? "اختر دواء..." : "Choose product..."}</option>
                                        {products.map(p => <option key={p.id} value={p.id}>{isRtl ? (p.nameAr || p.nameEn) : p.nameEn}</option>)}
                                    </select>
                                </div>
                                <div className="space-y-1">
                                    <Label className="text-xs">{t("purchases.barcode")}</Label>
                                    <Input 
                                        value={activeBarcode} 
                                        onChange={(e) => setActiveBarcode(e.target.value)} 
                                        placeholder="622..." 
                                        className="h-8 text-xs"
                                    />
                                </div>
                            </div>

                            <div className="grid grid-cols-3 gap-3">
                                <div className="space-y-1">
                                    <Label className="text-xs">{t("purchases.quantity")}</Label>
                                    <Input 
                                        type="number" 
                                        value={activeQty} 
                                        onChange={(e) => setActiveQty(parseFloat(e.target.value) || 0)} 
                                        className="h-8 text-xs"
                                    />
                                </div>
                                <div className="space-y-1">
                                    <Label className="text-xs">{t("purchases.unit_price")} (EGP)</Label>
                                    <Input 
                                        type="number" 
                                        step="0.01"
                                        value={activePrice} 
                                        onChange={(e) => setActivePrice(parseFloat(e.target.value) || 0)} 
                                        className="h-8 text-xs"
                                    />
                                </div>
                                <div className="space-y-1">
                                    <Label className="text-xs">{t("purchases.expiry_date")}</Label>
                                    <Input 
                                        type="date" 
                                        value={activeExpiry} 
                                        onChange={(e) => setActiveExpiry(e.target.value)} 
                                        className="h-8 text-xs"
                                    />
                                </div>
                            </div>

                            <Button type="button" variant="secondary" size="sm" onClick={handleAddLine} className="w-full text-xs h-8">
                                {isRtl ? "+ إضافة للجدول" : "+ Add Item to Grid"}
                            </Button>
                        </div>

                        {/* Invoice Lines Grid */}
                        <div className="border border-border/50 rounded-lg overflow-hidden">
                            <table className="w-full text-xs text-left rtl:text-right">
                                <thead>
                                    <tr className="bg-muted/40 border-b border-border/50 text-muted-foreground">
                                        <th className="p-2.5 font-semibold">{t("purchases.product")}</th>
                                        <th className="p-2.5 font-semibold text-center">{t("purchases.quantity")}</th>
                                        <th className="p-2.5 font-semibold text-center">{t("purchases.unit_price")}</th>
                                        <th className="p-2.5 font-semibold text-right rtl:text-left">{t("purchases.total")}</th>
                                        <th className="p-2.5 text-center font-semibold"></th>
                                    </tr>
                                </thead>
                                <tbody className="divide-y divide-border/30">
                                    {lines.length > 0 ? (
                                        lines.map((line, idx) => {
                                            const prod = products.find(p => p.id === line.productId)
                                            return (
                                                <tr key={idx}>
                                                    <td className="p-2.5 font-medium">
                                                        {prod ? (isRtl ? (prod.nameAr || prod.nameEn) : prod.nameEn) : "Unknown"}
                                                        <div className="text-[10px] text-muted-foreground font-mono">{line.barcode}</div>
                                                    </td>
                                                    <td className="p-2.5 text-center font-mono">{line.quantity}</td>
                                                    <td className="p-2.5 text-center font-mono">{line.unitPrice.toFixed(2)} EGP</td>
                                                    <td className="p-2.5 text-right rtl:text-left font-mono font-bold text-emerald-500">
                                                        {(line.quantity * line.unitPrice).toFixed(2)} EGP
                                                    </td>
                                                    <td className="p-2.5 text-center">
                                                        <Button type="button" variant="ghost" size="icon" className="h-6 w-6 text-red-500" onClick={() => handleRemoveLine(idx)}>
                                                            <Trash2 className="h-3.5 w-3.5" />
                                                        </Button>
                                                    </td>
                                                </tr>
                                            )
                                        })
                                    ) : (
                                        <tr>
                                            <td colSpan={5} className="p-6 text-center text-muted-foreground text-xs">
                                                {isRtl ? "لا توجد أصناف مضافة بعد." : "No items added to invoice yet."}
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                        </div>

                        {/* Invoice Summary Details */}
                        <div className="flex flex-col sm:flex-row items-center justify-between border-t border-border/50 pt-4 gap-4">
                            <div className="flex items-center gap-4">
                                <label className="flex items-center gap-2 text-sm font-semibold cursor-pointer">
                                    <input 
                                        type="checkbox" 
                                        checked={isPaid} 
                                        onChange={(e) => setIsPaid(e.target.checked)} 
                                        className="rounded border-gray-300 text-primary focus:ring-primary h-4 w-4"
                                    />
                                    <span>{t("purchases.paid")} ({isRtl ? "تم الدفع نقداً للمورد" : "Paid cash to vendor"})</span>
                                </label>
                            </div>
                            <div className="text-right rtl:text-left">
                                <span className="text-xs text-muted-foreground block">{isRtl ? "إجمالي قيمة الفاتورة" : "Invoice Total"}</span>
                                <span className="text-lg font-black text-emerald-500">
                                    {getInvoiceTotal().toLocaleString(undefined, { minimumFractionDigits: 2 })} EGP
                                </span>
                            </div>
                        </div>

                        <div className="space-y-1 pt-2">
                            <Label>{isRtl ? "بيانات إضافية" : "Additional Notes"}</Label>
                            <Input 
                                value={notes} 
                                onChange={(e) => setNotes(e.target.value)} 
                                placeholder="E.g., Delivered via Giza logistics team" 
                            />
                        </div>

                        <DialogFooter className="pt-4 gap-2">
                            <Button type="button" variant="outline" onClick={() => setIsOpen(false)}>
                                {isRtl ? "إلغاء" : "Cancel"}
                            </Button>
                            <Button type="submit" disabled={isSaving}>
                                {isSaving ? <Loader2 className="h-4 w-4 animate-spin me-2" /> : null}
                                {t("purchases.save_invoice")}
                            </Button>
                        </DialogFooter>
                    </form>
                </DialogContent>
            </Dialog>
        </div>
    )
}
