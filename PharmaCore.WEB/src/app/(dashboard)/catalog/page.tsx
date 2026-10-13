"use client"

import { useState, useEffect } from "react"
import { Card, CardContent, CardHeader, CardTitle, CardDescription } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogDescription, DialogFooter } from "@/components/ui/dialog"
import { Label } from "@/components/ui/label"
import { useLanguage } from "@/components/providers/language-provider"
import { Search, Plus, Loader2, Package, Layers, Info, Trash2, Edit } from "lucide-react"
import apiClient from "@/lib/api-client"
import { toast } from "sonner"

interface Product {
    id: string
    productCode: string
    nameEn: string
    nameAr: string | null
    searchName: string
    categoryName: string | null
    activeIngredient: string | null
    companyName: string | null
    usageType: string | null
    batchCount: number
    totalStock: number
}

export default function CatalogPage() {
    const { language, t } = useLanguage()
    const isRtl = language === "ar"
    
    const [products, setProducts] = useState<Product[]>([])
    const [total, setTotal] = useState(0)
    const [page, setPage] = useState(1)
    const [search, setSearch] = useState("")
    const [isLoading, setIsLoading] = useState(true)
    const [isOpen, setIsOpen] = useState(false)
    const [isSaving, setIsSaving] = useState(false)
    
    // New Product form state
    const [nameEn, setNameEn] = useState("")
    const [nameAr, setNameAr] = useState("")
    const [companyName, setCompanyName] = useState("")
    const [activeIngredient, setActiveIngredient] = useState("")
    const [usageType, setUsageType] = useState("")
    const [notes, setNotes] = useState("")

    // Product detail & suppliers states
    const [selectedProduct, setSelectedProduct] = useState<Product | null>(null)
    const [isInfoOpen, setIsInfoOpen] = useState(false)
    const [suppliersHistory, setSuppliersHistory] = useState<any[]>([])
    const [allRegisteredSuppliers, setAllRegisteredSuppliers] = useState<any[]>([])
    const [isLoadingSuppliers, setIsLoadingSuppliers] = useState(false)

    const handleProductClick = async (product: Product) => {
        setSelectedProduct(product)
        setIsInfoOpen(true)
        setIsLoadingSuppliers(true)
        setSuppliersHistory([])
        setAllRegisteredSuppliers([])
        try {
            const res = await apiClient.get(`/products/${product.id}/suppliers`)
            if (res.data) {
                setSuppliersHistory(res.data.history || [])
                setAllRegisteredSuppliers(res.data.allSuppliers || [])
            }
        } catch (err) {
            console.error("Failed to load product suppliers", err)
        } finally {
            setIsLoadingSuppliers(false)
        }
    }

    const fetchProducts = async () => {
        setIsLoading(true)
        try {
            const res = await apiClient.get(`/products?search=${encodeURIComponent(search)}&page=${page}&size=10`)
            if (res.data) {
                setProducts(res.data.items || [])
                setTotal(res.data.total || 0)
            }
        } catch (err: any) {
            console.error("Failed to load catalog", err)
            toast.error(isRtl ? "فشل تحميل الكتالوج" : "Failed to load catalog")
        } finally {
            setIsLoading(false)
        }
    }

    useEffect(() => {
        fetchProducts()
    }, [page, search])

    const handleCreateProduct = async (e: React.FormEvent) => {
        e.preventDefault()
        if (!nameEn) {
            toast.warning(isRtl ? "اسم الدواء بالإنجليزية مطلوب" : "English name is required")
            return
        }

        setIsSaving(true)
        try {
            await apiClient.post("/products", {
                nameEn,
                nameAr: nameAr || null,
                companyName: companyName || null,
                usageType: usageType || null,
                notes: notes || null,
                isEnglishPrimary: true
            })
            toast.success(isRtl ? "تمت إضافة المنتج بنجاح" : "Product added successfully")
            setIsOpen(false)
            // Reset form
            setNameEn("")
            setNameAr("")
            setCompanyName("")
            setActiveIngredient("")
            setUsageType("")
            setNotes("")
            // Reload
            fetchProducts()
        } catch (err: any) {
            console.error("Failed to create product", err)
            toast.error(err.response?.data?.message || (isRtl ? "فشل إضافة المنتج" : "Failed to add product"))
        } finally {
            setIsSaving(false)
        }
    }

    return (
        <div className="flex-1 space-y-6 p-4 md:p-6 lg:p-8" dir={isRtl ? "rtl" : "ltr"}>
            <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
                <div>
                    <h2 className="text-2xl md:text-3xl font-bold tracking-tight flex items-center gap-2">
                        <Package className="h-7 w-7 text-primary" />
                        {t("catalog.title")}
                    </h2>
                    <p className="text-sm text-muted-foreground">
                        {t("catalog.desc")}
                    </p>
                </div>
                <Button onClick={() => setIsOpen(true)} className="flex items-center gap-2">
                    <Plus className="h-4 w-4" />
                    {t("catalog.add_product")}
                </Button>
            </div>

            {/* Search and Filters */}
            <Card className="shadow-sm border border-border/50 bg-gradient-to-br from-card to-background">
                <CardContent className="pt-6">
                    <div className="relative">
                        <Search className={isRtl ? "absolute right-3 top-2.5 h-4 w-4 text-muted-foreground" : "absolute left-3 top-2.5 h-4 w-4 text-muted-foreground"} />
                        <Input
                            placeholder={isRtl ? "البحث بالاسم بالإنجليزية أو العربية أو كود الدواء..." : "Search by code, English or Arabic name..."}
                            value={search}
                            onChange={(e) => { setSearch(e.target.value); setPage(1); }}
                            className={isRtl ? "pr-9 pl-4" : "pl-9 pr-4"}
                        />
                    </div>
                </CardContent>
            </Card>

            {/* Products Table Card */}
            <Card className="shadow-sm border border-border/50 bg-gradient-to-br from-card to-background">
                <CardContent className="p-0">
                    <div className="overflow-x-auto">
                        <table className="w-full text-sm text-left rtl:text-right">
                            <thead>
                                <tr className="border-b border-border/50 text-xs text-muted-foreground bg-muted/40">
                                    <th className="p-4 font-semibold">{t("catalog.product_code")}</th>
                                    <th className="p-4 font-semibold">{t("catalog.product_name")}</th>
                                    <th className="p-4 font-semibold">{t("catalog.company")}</th>
                                    <th className="p-4 font-semibold text-center">{t("catalog.batches")}</th>
                                    <th className="p-4 font-semibold text-right rtl:text-left">{t("catalog.total_stock")}</th>
                                </tr>
                            </thead>
                            <tbody className="divide-y divide-border/30">
                                {isLoading ? (
                                    <tr>
                                        <td colSpan={5} className="py-12 text-center text-muted-foreground">
                                            <Loader2 className="h-7 w-7 animate-spin mx-auto text-primary mb-2" />
                                            <span>{isRtl ? "جاري تحميل قائمة الأدوية..." : "Loading medicines catalog..."}</span>
                                        </td>
                                    </tr>
                                ) : products.length > 0 ? (
                                    products.map((p) => (
                                        <tr key={p.id} onClick={() => handleProductClick(p)} className="hover:bg-muted/20 transition-colors cursor-pointer">
                                            <td className="p-4 font-mono font-bold text-xs text-primary">{p.productCode}</td>
                                            <td className="p-4">
                                                <div className="flex items-center gap-2 flex-wrap">
                                                    <span className="font-semibold text-zinc-900 dark:text-zinc-100">{p.nameEn}</span>
                                                    {p.usageType && (
                                                        <span className="inline-flex items-center px-1.5 py-0.5 rounded text-[10px] font-semibold bg-amber-500/10 text-amber-600 dark:text-amber-400 border border-amber-500/20">
                                                            {p.usageType}
                                                        </span>
                                                    )}
                                                </div>
                                                {p.nameAr && <div className="text-xs text-muted-foreground">{p.nameAr}</div>}
                                            </td>
                                            <td className="p-4 text-muted-foreground text-xs">{p.companyName || "—"}</td>
                                            <td className="p-4 text-center">
                                                <span className="inline-flex items-center gap-1.5 px-2 py-0.5 rounded-full text-[10px] font-bold bg-indigo-500/10 text-indigo-500 border border-indigo-500/20">
                                                    <Layers className="h-3 w-3" />
                                                    {p.batchCount} {isRtl ? "تشغيلات" : "batches"}
                                                </span>
                                            </td>
                                            <td className="p-4 text-right rtl:text-left font-mono font-bold text-zinc-800 dark:text-zinc-200">
                                                {p.totalStock.toLocaleString()} {isRtl ? "وحدة" : "units"}
                                            </td>
                                        </tr>
                                    ))
                                ) : (
                                    <tr>
                                        <td colSpan={5} className="py-8 text-center text-muted-foreground text-xs">
                                            {isRtl ? "لم يتم العثور على أدوية مطابقة للبحث." : "No matching products found."}
                                        </td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>

                    {/* Pagination */}
                    {total > 10 && (
                        <div className="flex items-center justify-between p-4 border-t border-border/50">
                            <span className="text-xs text-muted-foreground">
                                {isRtl ? `عرض ${(page - 1) * 10 + 1}-${Math.min(page * 10, total)} من أصل ${total} دواء` : `Showing ${(page - 1) * 10 + 1}-${Math.min(page * 10, total)} of ${total} medicines`}
                            </span>
                            <div className="flex gap-2">
                                <Button
                                    variant="outline"
                                    size="sm"
                                    disabled={page === 1}
                                    onClick={() => setPage(prev => prev - 1)}
                                >
                                    {isRtl ? "السابق" : "Previous"}
                                </Button>
                                <Button
                                    variant="outline"
                                    size="sm"
                                    disabled={page * 10 >= total}
                                    onClick={() => setPage(prev => prev + 1)}
                                >
                                    {isRtl ? "التالي" : "Next"}
                                </Button>
                            </div>
                        </div>
                    )}
                </CardContent>
            </Card>

            {/* Add Product Modal */}
            <Dialog open={isOpen} onOpenChange={setIsOpen}>
                <DialogContent className="sm:max-w-[500px]" dir={isRtl ? "rtl" : "ltr"}>
                    <DialogHeader className={isRtl ? "text-right" : "text-left"}>
                        <DialogTitle>{t("catalog.add_product")}</DialogTitle>
                        <DialogDescription>
                            {isRtl ? "أدخل تفاصيل الدواء الجديد لإضافته للكتالوج المحلي." : "Fill in the drug details to add it to the local catalog."}
                        </DialogDescription>
                    </DialogHeader>
                    <form onSubmit={handleCreateProduct} className="space-y-4 py-2">
                        <div className="space-y-1">
                            <Label htmlFor="pNameEn">{isRtl ? "اسم الدواء بالإنجليزية *" : "English Name *"}</Label>
                            <Input
                                id="pNameEn"
                                value={nameEn}
                                onChange={(e) => setNameEn(e.target.value)}
                                placeholder="Panadol Extra 500mg"
                                required
                            />
                        </div>
                        <div className="space-y-1">
                            <Label htmlFor="pNameAr">{isRtl ? "اسم الدواء بالعربية" : "Arabic Name"}</Label>
                            <Input
                                id="pNameAr"
                                value={nameAr}
                                onChange={(e) => setNameAr(e.target.value)}
                                placeholder="بنادول اكسترا 500 مجم"
                            />
                        </div>
                        <div className="grid grid-cols-2 gap-4">
                            <div className="space-y-1">
                                <Label htmlFor="pCompany">{isRtl ? "الشركة المصنعة" : "Manufacturer"}</Label>
                                <Input
                                    id="pCompany"
                                    value={companyName}
                                    onChange={(e) => setCompanyName(e.target.value)}
                                    placeholder="GlaxoSmithKline"
                                />
                            </div>
                            <div className="space-y-1">
                                <Label htmlFor="pUsage">{isRtl ? "طريقة الاستخدام" : "Usage Type"}</Label>
                                <Input
                                    id="pUsage"
                                    value={usageType}
                                    onChange={(e) => setUsageType(e.target.value)}
                                    placeholder="Oral / Injection / Topical"
                                />
                            </div>
                        </div>
                        <div className="space-y-1">
                            <Label htmlFor="pNotes">{isRtl ? "ملاحظات إضافية" : "Additional Notes"}</Label>
                            <Input
                                id="pNotes"
                                value={notes}
                                onChange={(e) => setNotes(e.target.value)}
                                placeholder="E.g., Keep in dry place"
                            />
                        </div>
                        <DialogFooter className="pt-4 gap-2">
                            <Button type="button" variant="outline" onClick={() => setIsOpen(false)}>
                                {isRtl ? "إلغاء" : "Cancel"}
                            </Button>
                            <Button type="submit" disabled={isSaving}>
                                {isSaving ? <Loader2 className="h-4 w-4 animate-spin me-2" /> : null}
                                {t("common.save")}
                            </Button>
                        </DialogFooter>
                    </form>
                </DialogContent>
            </Dialog>

            {/* Product Details & Suppliers Modal */}
            <Dialog open={isInfoOpen} onOpenChange={setIsInfoOpen}>
                <DialogContent className="sm:max-w-[650px] max-h-[85vh] overflow-y-auto bg-card border-border" dir={isRtl ? "rtl" : "ltr"}>
                    <DialogHeader className={isRtl ? "text-right" : "text-left"}>
                        <DialogTitle className="text-xl flex items-center gap-2">
                            <Info className="h-5 w-5 text-indigo-500" />
                            {isRtl ? "تفاصيل الدواء والشركات الموزعة" : "Medicine Details & Distributors"}
                        </DialogTitle>
                        <DialogDescription>
                            {isRtl 
                                ? "معلومات الدواء والشركات الموردة وتاريخ المشتريات الخاص به." 
                                : "Information, history, and active suppliers for this product."}
                        </DialogDescription>
                    </DialogHeader>

                    {selectedProduct && (
                        <div className="space-y-6 py-4">
                            {/* General Details */}
                            <div className="grid grid-cols-2 gap-4 bg-muted/30 p-4 rounded-lg border border-border/40">
                                <div>
                                    <div className="text-xs text-muted-foreground">{isRtl ? "كود الدواء" : "Product Code"}</div>
                                    <div className="font-mono font-bold text-primary">{selectedProduct.productCode}</div>
                                </div>
                                <div>
                                    <div className="text-xs text-muted-foreground">{isRtl ? "الاسم بالإنجليزية" : "English Name"}</div>
                                    <div className="font-semibold">{selectedProduct.nameEn}</div>
                                </div>
                                <div>
                                    <div className="text-xs text-muted-foreground">{isRtl ? "الاسم بالعربية" : "Arabic Name"}</div>
                                    <div className="font-semibold">{selectedProduct.nameAr || "—"}</div>
                                </div>
                                <div>
                                    <div className="text-xs text-muted-foreground">{isRtl ? "الشركة المصنعة" : "Manufacturer"}</div>
                                    <div className="font-semibold">{selectedProduct.companyName || "—"}</div>
                                </div>
                                <div>
                                    <div className="text-xs text-muted-foreground">{isRtl ? "الشكل الدوائي" : "Dosage Form / Type"}</div>
                                    <div className="font-semibold">{selectedProduct.usageType || "—"}</div>
                                </div>
                                <div>
                                    <div className="text-xs text-muted-foreground">{isRtl ? "إجمالي المخزون الحالي" : "Total Current Stock"}</div>
                                    <div className="font-semibold font-mono text-indigo-600 dark:text-indigo-400">
                                        {selectedProduct.totalStock.toLocaleString()} {isRtl ? "وحدة" : "units"}
                                    </div>
                                </div>
                            </div>

                            {/* Suppliers Sections */}
                            <div className="space-y-4">
                                <h3 className="text-sm font-bold border-b pb-2 flex items-center gap-1.5">
                                    <Package className="h-4 w-4 text-emerald-500" />
                                    {isRtl ? "سجل المشتريات وشركات التوزيع المتعامل معها" : "Purchase History & Distributors"}
                                </h3>

                                {isLoadingSuppliers ? (
                                    <div className="py-8 text-center text-muted-foreground text-xs">
                                        <Loader2 className="h-5 w-5 animate-spin mx-auto text-primary mb-2" />
                                        {isRtl ? "جاري تحميل سجل الموردين..." : "Loading suppliers history..."}
                                    </div>
                                ) : suppliersHistory.length > 0 ? (
                                    <div className="border border-border/40 rounded-lg overflow-hidden">
                                        <table className="w-full text-xs text-left rtl:text-right">
                                            <thead>
                                                <tr className="border-b border-border/40 bg-muted/40 text-muted-foreground">
                                                    <th className="p-3 font-semibold">{isRtl ? "المورد" : "Supplier"}</th>
                                                    <th className="p-3 font-semibold">{isRtl ? "رقم الفاتورة" : "Invoice No."}</th>
                                                    <th className="p-3 font-semibold">{isRtl ? "سعر الشراء" : "Buy Price"}</th>
                                                    <th className="p-3 font-semibold text-right rtl:text-left">{isRtl ? "تاريخ التوريد" : "Purchase Date"}</th>
                                                </tr>
                                            </thead>
                                            <tbody className="divide-y divide-border/30">
                                                {suppliersHistory.map((sh, idx) => (
                                                    <tr key={idx} className="hover:bg-muted/10">
                                                        <td className="p-3">
                                                            <div className="font-semibold">{sh.supplierName}</div>
                                                            <div className="text-[10px] text-muted-foreground">{sh.supplierCode}</div>
                                                        </td>
                                                        <td className="p-3 font-mono">{sh.invoiceNumber}</td>
                                                        <td className="p-3 font-mono font-bold text-zinc-700 dark:text-zinc-300">
                                                            {sh.buyPrice.toFixed(2)} EGP
                                                        </td>
                                                        <td className="p-3 text-right rtl:text-left text-muted-foreground">
                                                            {new Date(sh.purchaseDate).toLocaleDateString(language === "ar" ? "ar-EG" : "en-US")}
                                                        </td>
                                                    </tr>
                                                ))}
                                            </tbody>
                                        </table>
                                    </div>
                                ) : (
                                    <div className="text-center py-6 border border-dashed rounded-lg text-xs text-muted-foreground">
                                        {isRtl 
                                            ? "لا يوجد فواتير شراء مسجلة سابقاً لهذا الدواء من الموردين." 
                                            : "No purchase history registered for this product."}
                                    </div>
                                )}
                            </div>

                            {/* Registered Suppliers */}
                            <div className="space-y-2">
                                <h3 className="text-xs font-bold text-muted-foreground uppercase tracking-wider">
                                    {isRtl ? "الموردون المتاحون في النظام لطلب التوريد" : "Available Registered Suppliers"}
                                </h3>
                                <div className="grid grid-cols-2 gap-2 max-h-[150px] overflow-y-auto pr-1">
                                    {allRegisteredSuppliers.length > 0 ? (
                                        allRegisteredSuppliers.map((s) => (
                                            <div key={s.supplierId} className="p-2 border border-border/40 rounded bg-card flex flex-col text-xs">
                                                <span className="font-semibold text-zinc-800 dark:text-zinc-200">{s.supplierName}</span>
                                                <span className="text-[10px] text-muted-foreground">{s.supplierCode} {s.phone ? `| ${s.phone}` : ""}</span>
                                            </div>
                                        ))
                                    ) : (
                                        <div className="col-span-2 text-center text-[10px] text-muted-foreground py-2">
                                            {isRtl ? "لا يوجد موردين مسجلين حالياً." : "No suppliers registered."}
                                        </div>
                                    )}
                                </div>
                            </div>
                        </div>
                    )}
                    <DialogFooter className="pt-2">
                        <Button onClick={() => setIsInfoOpen(false)} className="w-full sm:w-auto">
                            {isRtl ? "إغلاق" : "Close"}
                        </Button>
                    </DialogFooter>
                </DialogContent>
            </Dialog>
        </div>
    )
}
