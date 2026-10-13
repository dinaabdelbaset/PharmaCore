"use client"

import { useEffect, useState } from "react"
import { useForm } from "react-hook-form"
import { zodResolver } from "@hookform/resolvers/zod"
import * as z from "zod"
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
    DialogFooter,
} from "@/components/ui/dialog"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import { Tabs, TabsList, TabsTrigger, TabsContent } from "@/components/ui/tabs"
import { Trash2 } from "lucide-react"
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table"
import { Employee, employeeService, FlatRole } from "@/services/employee-service"
import apiClient from "@/lib/api-client"
import { toast } from "sonner"

const EGYPT_GOVERNORATES = [
    "القاهرة",
    "الجيزة",
    "الإسكندرية",
    "القليوبية",
    "الدقهلية (المنصورة)",
    "الشرقية",
    "الغربية",
    "المنوفية",
    "البحيرة",
    "كفر الشيخ",
    "بورسعيد",
    "الإسماعيلية",
    "السويس",
    "البحر الأحمر (الغردقة)",
    "جنوب سيناء (شرم الشيخ)",
    "مطروح (الساحل الشمالي)",
    "6 أكتوبر",
    "الشيخ زايد",
    "العاصمة الإدارية الجديدة",
    "أخرى"
]

const userSchema = z.object({
    fullName: z.string().min(3, "الاسم يجب أن يكون 3 حروف على الأقل"),
    employeeCode: z.string().optional(),
    pin: z.string().optional(),
    roleId: z.string().min(1, "الرتبة مطلوبة"),
    jobTitle: z.string().optional(),
    grade: z.string().optional(),
    branchId: z.string().optional(),
    governorate: z.string().optional(),
})

interface UserDialogProps {
    open: boolean
    onOpenChange: (open: boolean) => void
    onSave: () => void
    user: Employee | null
}

interface Branch {
    id: string
    name: string
    city?: string
    regionId?: string
}

export function UserDialog({
    open,
    onOpenChange,
    onSave,
    user,
}: UserDialogProps) {
    const [flatRoles, setFlatRoles] = useState<FlatRole[]>([])
    const [branches, setBranches] = useState<Branch[]>([])
    const [isLoadingData, setIsLoadingData] = useState(false)
    const [isSaving, setIsSaving] = useState(false)

    const [activeTab, setActiveTab] = useState<string>("basic")
    const [branchAccesses, setBranchAccesses] = useState<any[]>([])
    const [isLoadingAccesses, setIsLoadingAccesses] = useState<boolean>(false)

    const [addBranchId, setAddBranchId] = useState<string>("")
    const [addAccessType, setAddAccessType] = useState<number>(0)
    const [addAllowedFrom, setAddAllowedFrom] = useState<string>("")
    const [addAllowedTo, setAddAllowedTo] = useState<string>("")

    const form = useForm<z.infer<typeof userSchema>>({
        resolver: zodResolver(userSchema),
        defaultValues: {
            fullName: "",
            employeeCode: "",
            pin: "",
            roleId: "",
            jobTitle: "",
            grade: "",
            branchId: "",
            governorate: "",
        },
    })

    const selectedGov = form.watch("governorate") || ""

    const loadDropdownData = async () => {
        setIsLoadingData(true)
        try {
            const [rolesRes, branchesRes] = await Promise.all([
                employeeService.getRoles(),
                apiClient.get<Branch[]>("/branches")
            ])
            
            // Build hierarchical roles
            const list: FlatRole[] = []
            const traverse = (node: any, depth: number) => {
                const prefix = "── ".repeat(depth)
                list.push({
                    id: node.id,
                    name: node.name,
                    display: prefix + node.name,
                    description: node.description || null,
                    sortOrder: node.sortOrder || 0
                })
                if (node.children && node.children.length > 0) {
                    node.children.forEach((child: any) => traverse(child, depth + 1))
                }
            }
            rolesRes.forEach((root: any) => traverse(root, 0))
            setFlatRoles(list)
            setBranches(branchesRes.data)
        } catch (err) {
            toast.error("حدث خطأ أثناء تحميل البيانات المساعدة")
        } finally {
            setIsLoadingData(false)
        }
    }

    const fetchBranchAccesses = async () => {
        if (!user) return
        setIsLoadingAccesses(true)
        try {
            const res = await apiClient.get(`/employee-access/${user.id}`)
            setBranchAccesses(res.data)
        } catch (err) {
            console.error("Error loading branch accesses", err)
            toast.error("حدث خطأ أثناء تحميل صلاحيات الفروع")
        } finally {
            setIsLoadingAccesses(false)
        }
    }

    const handleAddAccess = async () => {
        if (!addBranchId) {
            toast.error("يرجى اختيار الفرع")
            return
        }
        try {
            await apiClient.post(`/branches/${addBranchId}/employee-access`, {
                orgId: user?.orgId || "",
                employeeId: user?.id,
                branchId: addBranchId,
                accessType: Number(addAccessType),
                allowedFrom: addAllowedFrom ? new Date(addAllowedFrom).toISOString() : null,
                allowedTo: addAllowedTo ? new Date(addAllowedTo).toISOString() : null,
                isActive: true
            })
            toast.success("تمت إضافة صلاحية الوصول للفرع بنجاح")
            setAddBranchId("")
            setAddAccessType(0)
            setAddAllowedFrom("")
            setAddAllowedTo("")
            fetchBranchAccesses()
        } catch (err: any) {
            toast.error(err.response?.data?.error || "حدث خطأ أثناء إضافة الصلاحية")
        }
    }

    const handleDeleteAccess = async (accessId: string, bId: string) => {
        try {
            await apiClient.delete(`/branches/${bId}/employee-access/${accessId}`)
            toast.success("تمت إزالة صلاحية الوصول للفرع")
            fetchBranchAccesses()
        } catch (err) {
            toast.error("حدث خطأ أثناء إزالة الصلاحية")
        }
    }

    useEffect(() => {
        if (open) {
            setActiveTab("basic")
            loadDropdownData()
        }
    }, [open])

    useEffect(() => {
        if (user && branches.length > 0) {
            const userBranch = branches.find(b => b.id === user.branchId)
            let userGov = ""
            if (userBranch && userBranch.city) {
                const match = EGYPT_GOVERNORATES.find(gov => userBranch.city?.includes(gov))
                userGov = match || "أخرى"
            }
            form.reset({
                fullName: user.fullName,
                employeeCode: user.employeeCode,
                pin: "",
                roleId: user.roleId || "",
                jobTitle: user.jobTitle || "",
                grade: user.grade || "",
                branchId: user.branchId || "",
                governorate: userGov,
            })
        } else if (!user) {
            form.reset({
                fullName: "",
                employeeCode: "",
                pin: "",
                roleId: "",
                jobTitle: "",
                grade: "",
                branchId: "",
                governorate: "",
            })
        }
    }, [user, branches])

    useEffect(() => {
        if (open && user && activeTab === "branches") {
            fetchBranchAccesses()
        }
    }, [open, user, activeTab])

    const onSubmit = async (data: z.infer<typeof userSchema>) => {
        if (!user && !data.pin) {
            toast.error("الرقم السري مطلوب للموظف الجديد")
            return
        }
        if (!user && !data.branchId) {
            toast.error("الفرع الأساسي مطلوب للموظف الجديد")
            return
        }

        setIsSaving(true)
        try {
            const selectedRole = flatRoles.find(r => r.id === data.roleId)
            const roleName = selectedRole ? selectedRole.name : "Staff"

            if (user) {
                await employeeService.updateEmployee(user.id, {
                    fullName: data.fullName,
                    role: roleName,
                    roleId: data.roleId,
                    jobTitle: data.jobTitle,
                    grade: data.grade,
                    isActive: user.isActive,
                    branchId: data.branchId || undefined,
                    regionId: branches.find(b => b.id === data.branchId)?.regionId || undefined
                })
                toast.success("تم تحديث بيانات الموظف بنجاح")
            } else {
                await employeeService.createEmployee({
                    fullName: data.fullName,
                    employeeCode: data.employeeCode || undefined,
                    pin: data.pin || "",
                    role: roleName,
                    roleId: data.roleId,
                    jobTitle: data.jobTitle || undefined,
                    grade: data.grade || undefined,
                    branchId: data.branchId || undefined,
                })
                toast.success("تم إنشاء حساب الموظف بنجاح")
            }
            onSave()
            onOpenChange(false)
        } catch (err: any) {
            toast.error(err.response?.data?.error || "حدث خطأ أثناء حفظ البيانات")
        } finally {
            setIsSaving(false)
        }
    }

    const filteredBranches = branches.filter(b => {
        const branchName = b.name || ""
        const branchCity = b.city || ""
        if (!selectedGov) return true
        if (selectedGov === "أخرى") {
            return !branchCity || !EGYPT_GOVERNORATES.slice(0, -1).some(gov => branchCity.includes(gov) || branchName.includes(gov))
        }
        return branchCity.includes(selectedGov) || branchName.includes(selectedGov)
    })

    return (
        <Dialog open={open} onOpenChange={onOpenChange}>
            <DialogContent className="max-w-2xl max-h-[90vh] overflow-y-auto" dir="rtl">
                <DialogHeader className="text-right">
                    <DialogTitle>{user ? "تعديل بيانات الحساب" : "إنشاء حساب موظف جديد"}</DialogTitle>
                    <DialogDescription>
                        {user
                            ? "تحديث بيانات الموظف الحالي في النظام"
                            : "أدخل البيانات المطلوبة لإنشاء حساب كاشير أو موظف جديد"}
                    </DialogDescription>
                </DialogHeader>

                <Tabs value={activeTab} onValueChange={setActiveTab} className="w-full" dir="rtl">
                    {user && (
                        <TabsList className="grid w-full grid-cols-2 mb-6">
                            <TabsTrigger value="basic">البيانات الأساسية</TabsTrigger>
                            <TabsTrigger value="branches">صلاحيات الفروع الإضافية</TabsTrigger>
                        </TabsList>
                    )}

                    <TabsContent value="basic" className="mt-0">
                        <Form {...form}>
                            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6">
                                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                    <FormField
                                        control={form.control}
                                        name="fullName"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel>الاسم بالكامل *</FormLabel>
                                                <FormControl>
                                                    <Input placeholder="أدخل اسم الموظف" {...field} />
                                                </FormControl>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />

                                    <FormField
                                        control={form.control}
                                        name="employeeCode"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel>كود الموظف (اختياري)</FormLabel>
                                                <FormControl>
                                                    <Input placeholder="يتم توليده تلقائياً إن ترك فارغاً" {...field} disabled={!!user} autoComplete="off" />
                                                </FormControl>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />

                                    {!user && (
                                        <FormField
                                            control={form.control}
                                            name="pin"
                                            render={({ field }) => (
                                                <FormItem>
                                                    <FormLabel>الرقم السري (PIN Code) *</FormLabel>
                                                    <FormControl>
                                                        <Input
                                                            type="password"
                                                            placeholder="4 أرقام على الأقل"
                                                            maxLength={8}
                                                            {...field}
                                                            onChange={(e) => field.onChange(e.target.value.replace(/\D/g, ""))}
                                                            autoComplete="new-password"
                                                        />
                                                    </FormControl>
                                                    <FormMessage />
                                                </FormItem>
                                            )}
                                        />
                                    )}

                                    <FormField
                                        control={form.control}
                                        name="roleId"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel>الرتبة والمستوى الهرمي *</FormLabel>
                                                <Select
                                                    onValueChange={(val) => {
                                                        field.onChange(val)
                                                        const selected = flatRoles.find(r => r.id === val)
                                                        if (selected) {
                                                            form.setValue("jobTitle", selected.name)
                                                            const indentCount = (selected.display.match(/─/g) || []).length
                                                            const level = indentCount > 0 ? (indentCount / 2) + 1 : 1
                                                            form.setValue("grade", `مستوى إداري ${level}`)
                                                        }
                                                    }}
                                                    value={field.value}
                                                    disabled={isLoadingData}
                                                >
                                                    <FormControl>
                                                        <SelectTrigger>
                                                            <SelectValue placeholder={isLoadingData ? "جاري التحميل..." : "اختر الرتبة"} />
                                                        </SelectTrigger>
                                                    </FormControl>
                                                    <SelectContent>
                                                        {flatRoles.map((role) => (
                                                            <SelectItem key={role.id} value={role.id}>
                                                                <span className="font-mono text-xs">{role.display}</span>
                                                            </SelectItem>
                                                        ))}
                                                    </SelectContent>
                                                </Select>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />

                                    <FormField
                                        control={form.control}
                                        name="jobTitle"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel>المسمى الوظيفي</FormLabel>
                                                <FormControl>
                                                    <Input placeholder="مثال: كاشير فرعي" {...field} />
                                                </FormControl>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />

                                    <FormField
                                        control={form.control}
                                        name="grade"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel>الدرجة الوظيفية</FormLabel>
                                                <FormControl>
                                                    <Input placeholder="مثال: المستوى الأول" {...field} />
                                                </FormControl>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />

                                    <FormField
                                        control={form.control}
                                        name="governorate"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel>المحافظة / المدينة</FormLabel>
                                                <Select
                                                    onValueChange={(val) => {
                                                        field.onChange(val)
                                                        form.setValue("branchId", "")
                                                    }}
                                                    value={field.value}
                                                    disabled={isLoadingData}
                                                >
                                                    <FormControl>
                                                        <SelectTrigger>
                                                            <SelectValue placeholder="اختر المحافظة" />
                                                        </SelectTrigger>
                                                    </FormControl>
                                                    <SelectContent>
                                                        {EGYPT_GOVERNORATES.map((gov) => (
                                                            <SelectItem key={gov} value={gov}>
                                                                {gov}
                                                            </SelectItem>
                                                        ))}
                                                    </SelectContent>
                                                </Select>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />

                                    <FormField
                                        control={form.control}
                                        name="branchId"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel>الفرع الأساسي للعمل *</FormLabel>
                                                <Select
                                                    onValueChange={field.onChange}
                                                    value={field.value}
                                                    disabled={isLoadingData}
                                                >
                                                    <FormControl>
                                                        <SelectTrigger>
                                                            <SelectValue placeholder={selectedGov ? "اختر فرع المحافظة" : "حدد المحافظة أولاً"} />
                                                        </SelectTrigger>
                                                    </FormControl>
                                                    <SelectContent>
                                                        {filteredBranches.map((branch) => (
                                                            <SelectItem key={branch.id} value={branch.id}>
                                                                {branch.name}
                                                            </SelectItem>
                                                        ))}
                                                    </SelectContent>
                                                </Select>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />
                                </div>

                                <DialogFooter className="gap-2 sm:gap-0 flex-row-reverse justify-end pt-4 border-t">
                                    <Button type="submit" disabled={isSaving}>
                                        {isSaving ? "جاري الحفظ..." : user ? "تحديث الحساب" : "إنشاء الحساب"}
                                    </Button>
                                    <Button
                                        type="button"
                                        variant="outline"
                                        onClick={() => onOpenChange(false)}
                                        disabled={isSaving}
                                    >
                                        إلغاء
                                    </Button>
                                </DialogFooter>
                            </form>
                        </Form>
                    </TabsContent>

                    {user && (
                        <TabsContent value="branches" className="mt-0 space-y-6">
                            <div className="bg-muted/40 p-4 rounded-lg border space-y-4">
                                <h3 className="font-medium text-sm">إضافة صلاحية وصول لفرع جديد</h3>
                                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                    <div className="space-y-1">
                                        <label className="text-xs font-medium">الفرع</label>
                                        <Select value={addBranchId} onValueChange={setAddBranchId}>
                                            <SelectTrigger>
                                                <SelectValue placeholder="اختر الفرع" />
                                            </SelectTrigger>
                                            <SelectContent>
                                                {branches
                                                    .filter(b => b.id !== user.branchId && !branchAccesses.some(a => a.branchId === b.id))
                                                    .map(b => (
                                                        <SelectItem key={b.id} value={b.id}>
                                                            {b.name} {b.city ? `(${b.city})` : ""}
                                                        </SelectItem>
                                                    ))
                                                }
                                            </SelectContent>
                                        </Select>
                                    </div>

                                    <div className="space-y-1">
                                        <label className="text-xs font-medium">نوع الوصول</label>
                                        <Select value={String(addAccessType)} onValueChange={val => setAddAccessType(Number(val))}>
                                            <SelectTrigger>
                                                <SelectValue />
                                            </SelectTrigger>
                                            <SelectContent>
                                                <SelectItem value="0">دائم (Always)</SelectItem>
                                                <SelectItem value="1">فترة ساعات (Time Window)</SelectItem>
                                                <SelectItem value="2">نطاق تواريخ (Days Range)</SelectItem>
                                            </SelectContent>
                                        </Select>
                                    </div>

                                    {addAccessType !== 0 && (
                                        <>
                                            <div className="space-y-1">
                                                <label className="text-xs font-medium">مسموح من</label>
                                                <Input type="datetime-local" value={addAllowedFrom} onChange={e => setAddAllowedFrom(e.target.value)} />
                                            </div>
                                            <div className="space-y-1">
                                                <label className="text-xs font-medium">مسموح إلى</label>
                                                <Input type="datetime-local" value={addAllowedTo} onChange={e => setAddAllowedTo(e.target.value)} />
                                            </div>
                                        </>
                                    )}
                                </div>
                                <div className="flex justify-end pt-2">
                                    <Button type="button" size="sm" onClick={handleAddAccess}>
                                        إضافة صلاحية الفرع
                                    </Button>
                                </div>
                            </div>

                            <div className="space-y-2">
                                <h3 className="font-medium text-sm">صلاحيات الوصول النشطة</h3>
                                {isLoadingAccesses ? (
                                    <div className="text-center py-6 text-sm text-muted-foreground">جاري التحميل...</div>
                                ) : branchAccesses.length === 0 ? (
                                    <div className="text-center py-6 text-sm text-muted-foreground border border-dashed rounded-lg">
                                        لا توجد صلاحيات فروع إضافية مسجلة لهذا الموظف
                                    </div>
                                ) : (
                                    <div className="border rounded-lg overflow-hidden">
                                        <Table>
                                            <TableHeader>
                                                <TableRow>
                                                    <TableHead className="text-right">الفرع</TableHead>
                                                    <TableHead className="text-right">نوع الوصول</TableHead>
                                                    <TableHead className="text-right">الفترة</TableHead>
                                                    <TableHead className="w-[80px]"></TableHead>
                                                </TableRow>
                                            </TableHeader>
                                            <TableBody>
                                                {branchAccesses.map((acc) => {
                                                    const br = branches.find(b => b.id === acc.branchId)
                                                    let typeStr = "دائم"
                                                    if (acc.accessType === 1) typeStr = "فترة ساعات"
                                                    if (acc.accessType === 2) typeStr = "نطاق تواريخ"

                                                    const fromStr = acc.allowedFrom ? new Date(acc.allowedFrom).toLocaleString("ar-EG") : "-"
                                                    const toStr = acc.allowedTo ? new Date(acc.allowedTo).toLocaleString("ar-EG") : "-"

                                                    return (
                                                        <TableRow key={acc.id}>
                                                            <TableCell className="font-medium">{br ? br.name : "فرع غير معروف"}</TableCell>
                                                            <TableCell>{typeStr}</TableCell>
                                                            <TableCell className="text-xs whitespace-pre-line">
                                                                {acc.accessType !== 0 ? `من: ${fromStr}\nإلى: ${toStr}` : "وصول غير محدود"}
                                                            </TableCell>
                                                            <TableCell>
                                                                <Button
                                                                    type="button"
                                                                    variant="ghost"
                                                                    size="icon"
                                                                    className="h-8 w-8 text-destructive"
                                                                    onClick={() => handleDeleteAccess(acc.id, acc.branchId)}
                                                                >
                                                                    <Trash2 className="h-4 w-4" />
                                                                </Button>
                                                            </TableCell>
                                                        </TableRow>
                                                    )
                                                })}
                                            </TableBody>
                                        </Table>
                                    </div>
                                )}
                            </div>
                        </TabsContent>
                    )}
                </Tabs>
            </DialogContent>
        </Dialog>
    )
}
