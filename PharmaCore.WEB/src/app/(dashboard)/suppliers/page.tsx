"use client"

import { useState, useEffect } from "react"
import { Card, CardContent } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogDescription, DialogFooter } from "@/components/ui/dialog"
import { Label } from "@/components/ui/label"
import { useLanguage } from "@/components/providers/language-provider"
import { Search, Plus, Loader2, Truck, Phone, Mail, Landmark, Pencil } from "lucide-react"
import apiClient from "@/lib/api-client"
import { toast } from "sonner"

interface Supplier {
    id: string
    code: string
    name: string
    phone: string | null
    email: string | null
    address: string | null
    taxNumber: string | null
    balance: number
}

export default function SuppliersPage() {
    const { language, t } = useLanguage()
    const isRtl = language === "ar"

    const [suppliers, setSuppliers] = useState<Supplier[]>([])
    const [search, setSearch] = useState("")
    const [isLoading, setIsLoading] = useState(true)
    const [isOpen, setIsOpen] = useState(false)
    const [isEdit, setIsEdit] = useState(false)
    const [isSaving, setIsSaving] = useState(false)

    // Form state
    const [editingId, setEditingId] = useState<string | null>(null)
    const [name, setName] = useState("")
    const [phone, setPhone] = useState("")
    const [email, setEmail] = useState("")
    const [address, setAddress] = useState("")
    const [taxNumber, setTaxNumber] = useState("")
    const [balance, setBalance] = useState(0)

    const fetchSuppliers = async () => {
        setIsLoading(true)
        try {
            const res = await apiClient.get(`/suppliers?search=${encodeURIComponent(search)}`)
            if (res.data) {
                setSuppliers(res.data)
            }
        } catch (err: any) {
            console.error("Failed to load suppliers", err)
            toast.error(isRtl ? "فشل تحميل قائمة الموردين" : "Failed to load suppliers")
        } finally {
            setIsLoading(false)
        }
    }

    useEffect(() => {
        fetchSuppliers()
    }, [search])

    const handleOpenCreate = () => {
        setIsEdit(false)
        setEditingId(null)
        setName("")
        setPhone("")
        setEmail("")
        setAddress("")
        setTaxNumber("")
        setBalance(0)
        setIsOpen(true)
    }

    const handleOpenEdit = (sup: Supplier) => {
        setIsEdit(true)
        setEditingId(sup.id)
        setName(sup.name)
        setPhone(sup.phone || "")
        setEmail(sup.email || "")
        setAddress(sup.address || "")
        setTaxNumber(sup.taxNumber || "")
        setBalance(sup.balance)
        setIsOpen(true)
    }

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault()
        if (!name) {
            toast.warning(isRtl ? "اسم المورد مطلوب" : "Supplier name is required")
            return
        }

        setIsSaving(true)
        try {
            if (isEdit && editingId) {
                await apiClient.put(`/suppliers/${editingId}`, {
                    name,
                    phone: phone || null,
                    email: email || null,
                    address: address || null,
                    taxNumber: taxNumber || null,
                    balance
                })
                toast.success(isRtl ? "تم تحديث بيانات المورد بنجاح" : "Supplier updated successfully")
            } else {
                await apiClient.post("/suppliers", {
                    name,
                    phone: phone || null,
                    email: email || null,
                    address: address || null,
                    taxNumber: taxNumber || null
                })
                toast.success(isRtl ? "تمت إضافة المورد بنجاح" : "Supplier added successfully")
            }
            setIsOpen(false)
            fetchSuppliers()
        } catch (err: any) {
            console.error("Failed to save supplier", err)
            toast.error(isRtl ? "فشل حفظ المورد" : "Failed to save supplier")
        } finally {
            setIsSaving(false)
        }
    }

    return (
        <div className="flex-1 space-y-6 p-4 md:p-6 lg:p-8" dir={isRtl ? "rtl" : "ltr"}>
            <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
                <div>
                    <h2 className="text-2xl md:text-3xl font-bold tracking-tight flex items-center gap-2">
                        <Truck className="h-7 w-7 text-primary" />
                        {t("suppliers.title")}
                    </h2>
                    <p className="text-sm text-muted-foreground">
                        {t("suppliers.desc")}
                    </p>
                </div>
                <Button onClick={handleOpenCreate} className="flex items-center gap-2">
                    <Plus className="h-4 w-4" />
                    {t("suppliers.add_supplier")}
                </Button>
            </div>

            {/* Search Box */}
            <Card className="shadow-sm border border-border/50 bg-gradient-to-br from-card to-background">
                <CardContent className="pt-6">
                    <div className="relative">
                        <Search className={isRtl ? "absolute right-3 top-2.5 h-4 w-4 text-muted-foreground" : "absolute left-3 top-2.5 h-4 w-4 text-muted-foreground"} />
                        <Input
                            placeholder={isRtl ? "البحث باسم المورد أو الكود..." : "Search by vendor name or supplier code..."}
                            value={search}
                            onChange={(e) => setSearch(e.target.value)}
                            className={isRtl ? "pr-9 pl-4" : "pl-9 pr-4"}
                        />
                    </div>
                </CardContent>
            </Card>

            {/* Suppliers Table */}
            <Card className="shadow-sm border border-border/50 bg-gradient-to-br from-card to-background">
                <CardContent className="p-0">
                    <div className="overflow-x-auto">
                        <table className="w-full text-sm text-left rtl:text-right">
                            <thead>
                                <tr className="border-b border-border/50 text-xs text-muted-foreground bg-muted/40">
                                    <th className="p-4 font-semibold">{t("suppliers.supplier_code")}</th>
                                    <th className="p-4 font-semibold">{t("suppliers.supplier_name")}</th>
                                    <th className="p-4 font-semibold">{t("suppliers.phone")}</th>
                                    <th className="p-4 font-semibold">{t("suppliers.email")}</th>
                                    <th className="p-4 font-semibold text-right rtl:text-left">{t("suppliers.balance")}</th>
                                    <th className="p-4 text-center font-semibold">{isRtl ? "إجراءات" : "Actions"}</th>
                                </tr>
                            </thead>
                            <tbody className="divide-y divide-border/30">
                                {isLoading ? (
                                    <tr>
                                        <td colSpan={6} className="py-12 text-center text-muted-foreground">
                                            <Loader2 className="h-7 w-7 animate-spin mx-auto text-primary mb-2" />
                                            <span>{isRtl ? "جاري تحميل الموردين..." : "Loading suppliers..."}</span>
                                        </td>
                                    </tr>
                                ) : suppliers.length > 0 ? (
                                    suppliers.map((sup) => (
                                        <tr key={sup.id} className="hover:bg-muted/20 transition-colors">
                                            <td className="p-4 font-mono font-bold text-xs text-primary">{sup.code}</td>
                                            <td className="p-4 font-semibold text-zinc-900 dark:text-zinc-100">{sup.name}</td>
                                            <td className="p-4 text-xs font-mono text-muted-foreground">
                                                {sup.phone ? (
                                                    <span className="flex items-center gap-1">
                                                        <Phone className="h-3 w-3 text-zinc-400" />
                                                        {sup.phone}
                                                    </span>
                                                ) : "—"}
                                            </td>
                                            <td className="p-4 text-xs text-muted-foreground">
                                                {sup.email ? (
                                                    <span className="flex items-center gap-1">
                                                        <Mail className="h-3 w-3 text-zinc-400" />
                                                        {sup.email}
                                                    </span>
                                                ) : "—"}
                                            </td>
                                            <td className="p-4 text-right rtl:text-left font-mono font-bold text-rose-500">
                                                {sup.balance.toLocaleString(undefined, { minimumFractionDigits: 2 })} EGP
                                            </td>
                                            <td className="p-4 text-center">
                                                <Button variant="ghost" size="icon" onClick={() => handleOpenEdit(sup)} title={isRtl ? "تعديل المورد" : "Edit Supplier"}>
                                                    <Pencil className="h-4 w-4 text-zinc-500" />
                                                </Button>
                                            </td>
                                        </tr>
                                    ))
                                ) : (
                                    <tr>
                                        <td colSpan={6} className="py-8 text-center text-muted-foreground text-xs">
                                            {isRtl ? "لم يتم العثور على أي موردين." : "No suppliers registered."}
                                        </td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                </CardContent>
            </Card>

            {/* Create/Edit Modal */}
            <Dialog open={isOpen} onOpenChange={setIsOpen}>
                <DialogContent className="sm:max-w-[500px]" dir={isRtl ? "rtl" : "ltr"}>
                    <DialogHeader className={isRtl ? "text-right" : "text-left"}>
                        <DialogTitle>{isEdit ? t("suppliers.edit_supplier") : t("suppliers.add_supplier")}</DialogTitle>
                        <DialogDescription>
                            {isRtl ? "أدخل بيانات الاتصال والتفاصيل المالية للمورد." : "Fill in supplier contact details and financial balances."}
                        </DialogDescription>
                    </DialogHeader>
                    <form onSubmit={handleSubmit} className="space-y-4 py-2">
                        <div className="space-y-1">
                            <Label htmlFor="supName">{isRtl ? "اسم المورد / الشركة *" : "Supplier Name *"}</Label>
                            <Input
                                id="supName"
                                value={name}
                                onChange={(e) => setName(e.target.value)}
                                placeholder="E.g., Ezzaby Distributors"
                                required
                            />
                        </div>
                        <div className="grid grid-cols-2 gap-4">
                            <div className="space-y-1">
                                <Label htmlFor="supPhone">{isRtl ? "رقم الهاتف" : "Phone Number"}</Label>
                                <Input
                                    id="supPhone"
                                    value={phone}
                                    onChange={(e) => setPhone(e.target.value)}
                                    placeholder="+20 100 123 4567"
                                />
                            </div>
                            <div className="space-y-1">
                                <Label htmlFor="supEmail">{isRtl ? "البريد الإلكتروني" : "Email"}</Label>
                                <Input
                                    id="supEmail"
                                    type="email"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    placeholder="sales@company.com"
                                />
                            </div>
                        </div>
                        <div className="grid grid-cols-2 gap-4">
                            <div className="space-y-1">
                                <Label htmlFor="supTax">{isRtl ? "البطاقة الضريبية" : "Tax Card ID"}</Label>
                                <Input
                                    id="supTax"
                                    value={taxNumber}
                                    onChange={(e) => setTaxNumber(e.target.value)}
                                    placeholder="123-456-789"
                                />
                            </div>
                            {isEdit && (
                                <div className="space-y-1">
                                    <Label htmlFor="supBal">{isRtl ? "الحساب المستحق الحالي (ج.م)" : "Outstanding Balance (EGP)"}</Label>
                                    <Input
                                        id="supBal"
                                        type="number"
                                        step="0.01"
                                        value={balance}
                                        onChange={(e) => setBalance(parseFloat(e.target.value) || 0)}
                                    />
                                </div>
                            )}
                        </div>
                        <div className="space-y-1">
                            <Label htmlFor="supAddress">{isRtl ? "العنوان" : "Address"}</Label>
                            <Input
                                id="supAddress"
                                value={address}
                                onChange={(e) => setAddress(e.target.value)}
                                placeholder="Cairo, Egypt"
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
        </div>
    )
}
