"use client"

import { useEffect, useState } from "react"
import { Shield, ShieldAlert, Plus, Trash2, Save, RefreshCw, ChevronDown, ChevronRight, Key, Layers } from "lucide-react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Card, CardContent, CardDescription, CardHeader, CardTitle, CardFooter } from "@/components/ui/card"
import { Checkbox } from "@/components/ui/checkbox"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import { EmployeeRole, employeeService } from "@/services/employee-service"
import { toast } from "sonner"

interface RoleNode extends EmployeeRole {
    children: RoleNode[]
}

// Static permission label translation helper for fallback
const permissionLabels: Record<string, string> = {
    "settings.view": "عرض الإعدادات",
    "settings.edit": "تعديل الإعدادات",
    "permissions.manage": "إدارة الصلاحيات",
    "products.add": "إضافة منتج",
    "products.view": "عرض المنتجات",
    "products.activate": "تفعيل المنتجات",
    "products.barcode": "طباعة الباركود",
    "products.required": "المنتجات المطلوبة",
    "products.expire": "تاريخ الصلاحية",
    "products.unit": "الوحدات",
    "products.company": "الشركات المصنعة",
    "products.financials": "البيانات المالية للمنتج",
    "customers.view": "عرض العملاء",
    "customers.money": "حسابات العملاء المادية",
    "customers.report": "تقارير العملاء",
    "customers.points": "نقاط العملاء",
    "customers.points_report": "تقارير نقاط العملاء",
    "suppliers.view": "عرض الموردين",
    "suppliers.money": "حسابات الموردين المادية",
    "suppliers.report": "تقارير الموردين",
    "purchases.create": "إنشاء فاتورة مشتريات",
    "purchases.na89": "النواقص المعلقة",
    "purchases.report": "تقارير المشتريات",
    "sales.create": "إنشاء فاتورة مبيعات",
    "sales.insurance": "فواتير التأمين الصحي",
    "sales.insurance_company": "شركات التأمين الصحي",
    "sales.user_report": "تقارير مبيعات المستخدمين",
    "sales.report": "تقارير المبيعات",
    "sales.insurance_report": "تقارير مبيعات التأمين",
    "sales.profit": "تقارير الأرباح",
    "returns.sale": "مرتجع مبيعات",
    "returns.buy": "مرتجع مشتريات",
    "returns.report": "تقارير المرتجعات",
    "treasury.add_stock": "إضافة خزينة جديدة",
    "treasury.stock_deposit": "إيداع خزنة",
    "treasury.bank_deposit": "إيداع بنك",
    "treasury.stock_withdraw": "سحب خزنة",
    "treasury.bank_withdraw": "سحب بنك",
    "treasury.transfer": "تحويل أموال بين الخزائن",
    "treasury.bank_stock_transfer": "تحويل بنك/خزينة",
    "treasury.balance": "أرصدة الخزائن والبنوك",
    "treasury.report": "تقارير الخزنة والبنوك",
    "employees.add": "إضافة موظف",
    "employees.borrow_items": "استعارة أصناف للموظف",
    "employees.borrow_money": "سلفيات الموظفين",
    "employees.salary": "صرف المرتبات",
    "employees.borrow_items_report": "تقارير عهد الموظفين",
    "employees.borrow_money_report": "تقارير السلف",
    "employees.salary_report": "تقارير مرتبات الموظفين",
    "accounting.type": "أنواع المصروفات والمديونيات",
    "accounting.entry": "سندات القيود اليومية",
    "accounting.report": "التقارير المالية والمديونيات",
    "accounting.receipt": "إنشاء سند قبض",
    "accounting.payment": "إنشاء سند صرف",
    "accounting.voucher_report": "تقارير سندات الصرف والقبض",
    "accounting.tax_report": "التقارير الضريبية",
    "reports.customers": "تقارير العملاء العامة",
    "reports.suppliers": "تقارير الموردين العامة",
    "reports.purchases": "تقارير المشتريات العامة",
    "reports.sales": "تقارير المبيعات العامة",
    "reports.returns": "تقارير المرتجعات العامة",
    "reports.accounting": "التقارير المحاسبية والمالية العامة",
    "reports.shifts": "تقارير الشفتات والورديات",
    "backup.create": "إنشاء نسخة احتياطية",
    "backup.restore": "استعادة نسخة احتياطية",
    "warehouse.add": "إضافة مخزن جديد",
    "warehouse.inventory": "جرد المخازن",
    "warehouse.transfer": "تحويل بين المخازن",
    "warehouse.out": "إخراج وإتلاف بضاعة من المخزن",
    "warehouse.transfer_report": "تقارير التحويلات المخزنية",
    "warehouse.out_report": "تقارير البواقي والتالف من المخزن",
    "shifts.open": "فتح شيفت وردية",
    "shifts.close": "إغلاق شيفت وردية"
}

export function RolesTab() {
    const [roles, setRoles] = useState<EmployeeRole[]>([])
    const [availablePermissions, setAvailablePermissions] = useState<Record<string, string[]>>({})
    const [selectedRole, setSelectedRole] = useState<EmployeeRole | null>(null)
    const [selectedRolePermissions, setSelectedRolePermissions] = useState<string[]>([])
    const [collapsedNodes, setCollapsedNodes] = useState<Record<string, boolean>>({})
    const [isLoading, setIsLoading] = useState(false)
    const [isSaving, setIsSaving] = useState(false)

    // Form states for Create/Edit
    const [isCreatingNew, setIsCreatingNew] = useState(false)
    const [newRoleName, setNewRoleName] = useState("")
    const [newRoleDesc, setNewRoleDesc] = useState("")
    const [newRoleParentId, setNewRoleParentId] = useState<string>("none")

    useEffect(() => {
        loadInitialData()
    }, [])

    const loadInitialData = async () => {
        setIsLoading(true)
        try {
            const [rolesList, permsGrouped] = await Promise.all([
                employeeService.getRoles(),
                employeeService.getAvailablePermissions(),
            ])
            setRoles(rolesList)
            setAvailablePermissions(permsGrouped)

            // Select Owner as default if exists
            const owner = rolesList.find(r => r.name.toLowerCase() === "owner")
            if (owner) {
                handleSelectRole(owner)
            } else if (rolesList.length > 0) {
                handleSelectRole(rolesList[0])
            }
        } catch (err) {
            toast.error("فشل تحميل البيانات التمهيدية")
        } finally {
            setIsLoading(false)
        }
    }

    const handleSelectRole = async (role: EmployeeRole) => {
        setIsCreatingNew(false)
        setSelectedRole(role)
        try {
            const rolePerms = await employeeService.getRolePermissions(role.id)
            setSelectedRolePermissions(rolePerms)
        } catch (err) {
            toast.error("فشل تحميل صلاحيات الرتبة")
        }
    }

    const handleTogglePermission = (permKey: string) => {
        if (selectedRole?.isSystemRole) return // System roles (Owner) cannot have their permissions changed

        setSelectedRolePermissions(prev =>
            prev.includes(permKey)
                ? prev.filter(k => k !== permKey)
                : [...prev, permKey]
        )
    }

    const handleSavePermissions = async () => {
        if (!selectedRole) return
        if (selectedRole.isSystemRole) {
            toast.error("لا يمكن تعديل صلاحيات رتبة النظام (المالك)")
            return
        }

        setIsSaving(true)
        try {
            await employeeService.updateRole(selectedRole.id, {
                name: selectedRole.name,
                parentRoleId: selectedRole.parentRoleId,
                permissions: selectedRolePermissions,
            })
            toast.success("تم حفظ الصلاحيات بنجاح")
            // Reload list
            const updated = await employeeService.getRoles()
            setRoles(updated)
        } catch (err: any) {
            toast.error(err.response?.data?.error || "فشل حفظ الصلاحيات")
        } finally {
            setIsSaving(false)
        }
    }

    const handleSeedDefaults = async () => {
        setIsLoading(true)
        try {
            const res = await employeeService.seedDefaultRoles()
            toast.success(res.message)
            loadInitialData()
        } catch (err: any) {
            toast.error(err.response?.data?.error || "فشل تهيئة الرتب")
        } finally {
            setIsLoading(false)
        }
    }

    const handleStartCreateRole = () => {
        setIsCreatingNew(true)
        setSelectedRole(null)
        setNewRoleName("")
        setNewRoleDesc("")
        setNewRoleParentId(selectedRole?.id || "none")
        setSelectedRolePermissions([])
    }

    const handleCreateRole = async (e: React.FormEvent) => {
        e.preventDefault()
        if (!newRoleName.trim()) {
            toast.error("اسم الرتبة مطلوب")
            return
        }

        setIsSaving(true)
        try {
            const parentId = newRoleParentId === "none" ? null : newRoleParentId
            const newRole = await employeeService.createRole({
                name: newRoleName,
                parentRoleId: parentId,
                permissions: selectedRolePermissions,
            })
            toast.success("تم إنشاء الرتبة بنجاح")
            
            // Reload roles
            const rolesList = await employeeService.getRoles()
            setRoles(rolesList)
            
            // Select the newly created role
            const created = rolesList.find(r => r.id === newRole.id)
            if (created) {
                handleSelectRole(created)
            }
        } catch (err: any) {
            toast.error(err.response?.data?.error || "فشل إنشاء الرتبة")
        } finally {
            setIsSaving(false)
        }
    }

    const handleDeleteRole = async () => {
        if (!selectedRole) return
        if (selectedRole.isSystemRole) {
            toast.error("لا يمكن حذف رتب النظام الأساسية")
            return
        }

        if (!confirm(`هل أنت متأكد من حذف رتبة "${selectedRole.description || selectedRole.name}"؟`)) return

        setIsSaving(true)
        try {
            await employeeService.deleteRole(selectedRole.id)
            toast.success("تم حذف الرتبة بنجاح")
            loadInitialData()
        } catch (err: any) {
            toast.error(err.response?.data?.error || "فشل حذف الرتبة")
        } finally {
            setIsSaving(false)
        }
    }

    // Build the tree hierarchy helper
    const buildTree = (flatRoles: EmployeeRole[]): RoleNode[] => {
        const map: Record<string, RoleNode> = {}
        flatRoles.forEach(role => {
            map[role.id] = { ...role, children: [] }
        })
        const roots: RoleNode[] = []
        flatRoles.forEach(role => {
            const node = map[role.id]
            if (role.parentRoleId && map[role.parentRoleId]) {
                map[role.parentRoleId].children.push(node)
            } else {
                roots.push(node)
            }
        })
        return roots
    }

    const toggleCollapse = (id: string) => {
        setCollapsedNodes(prev => ({ ...prev, [id]: !prev[id] }))
    }

    // Recursive Tree node renderer
    const renderTreeNode = (node: RoleNode, level: number = 0) => {
        const hasChildren = node.children.length > 0
        const isCollapsed = !!collapsedNodes[node.id]
        const isSelected = selectedRole?.id === node.id

        return (
            <div key={node.id} className="select-none">
                <div
                    className={`flex items-center gap-2 py-2 px-3 rounded-lg cursor-pointer transition-colors duration-200 ${
                        isSelected
                            ? "bg-primary text-primary-foreground font-semibold shadow-sm"
                            : "hover:bg-muted text-muted-foreground hover:text-foreground"
                    }`}
                    style={{ paddingLeft: `${level * 16 + 12}px` }}
                    onClick={() => handleSelectRole(node)}
                >
                    {hasChildren ? (
                        <span
                            onClick={(e) => {
                                e.stopPropagation()
                                toggleCollapse(node.id)
                            }}
                            className="p-1 hover:bg-muted-foreground/20 rounded cursor-pointer"
                        >
                            {isCollapsed ? (
                                <ChevronRight className="h-4 w-4" />
                            ) : (
                                <ChevronDown className="h-4 w-4" />
                            )}
                        </span>
                    ) : (
                        <span className="w-6 h-6 flex items-center justify-center">
                            <span className="w-1.5 h-1.5 rounded-full bg-border" />
                        </span>
                    )}

                    <Shield className={`h-4 w-4 ${node.isSystemRole ? "text-amber-500 fill-amber-500/20" : ""}`} />
                    <span className="text-sm font-medium">
                        {node.description || node.name}
                    </span>
                    {node.isSystemRole && (
                        <span className="text-[10px] uppercase font-bold tracking-wider px-1.5 py-0.5 rounded bg-amber-500/10 text-amber-500 border border-amber-500/20">
                            نظام
                        </span>
                    )}
                </div>

                {hasChildren && !isCollapsed && (
                    <div className="relative mt-1">
                        {/* Connecting vertical line */}
                        <div
                            className="absolute left-4 top-0 bottom-3 w-0.5 bg-border/60"
                            style={{ left: `${level * 16 + 23}px` }}
                        />
                        <div className="space-y-1">
                            {node.children.map(child => renderTreeNode(child, level + 1))}
                        </div>
                    </div>
                )}
            </div>
        )
    }

    const roleTree = buildTree(roles)

    return (
        <div className="grid grid-cols-1 lg:grid-cols-12 gap-6">
            {/* Left panel: Tree View */}
            <Card className="lg:col-span-5 h-[calc(100vh-220px)] overflow-hidden flex flex-col">
                <CardHeader className="border-b pb-4">
                    <div className="flex items-center justify-between">
                        <div>
                            <CardTitle className="text-lg">هيكل الرتب والصلاحيات</CardTitle>
                            <CardDescription>هرمية رتب الموظفين بصلاحيات متوارثة</CardDescription>
                        </div>
                        <div className="flex gap-2">
                            <Button variant="outline" size="icon" onClick={loadInitialData} title="تحديث">
                                <RefreshCw className="h-4 w-4" />
                            </Button>
                            <Button size="icon" onClick={handleStartCreateRole} title="إضافة رتبة">
                                <Plus className="h-4 w-4" />
                            </Button>
                        </div>
                    </div>
                </CardHeader>
                <CardContent className="flex-grow overflow-y-auto p-4 space-y-2">
                    {isLoading ? (
                        <div className="flex flex-col items-center justify-center py-12 gap-3 text-muted-foreground">
                            <RefreshCw className="h-8 w-8 animate-spin" />
                            <span>جاري تحميل هيكل الرتب...</span>
                        </div>
                    ) : roles.length === 0 ? (
                        <div className="flex flex-col items-center justify-center py-12 gap-4 text-center">
                            <ShieldAlert className="h-12 w-12 text-muted-foreground" />
                            <div>
                                <h3 className="font-semibold text-lg">لا توجد رتب حالياً</h3>
                                <p className="text-sm text-muted-foreground mt-1">يمكنك البدء بتهيئة الرتب الافتراضية للنظام</p>
                            </div>
                            <Button onClick={handleSeedDefaults}>
                                تهيئة الرتب الافتراضية
                            </Button>
                        </div>
                    ) : (
                        <div className="space-y-1">
                            {roleTree.map(root => renderTreeNode(root))}
                        </div>
                    )}
                </CardContent>
            </Card>

            {/* Right panel: Details & Permissions */}
            <div className="lg:col-span-7 h-[calc(100vh-220px)] overflow-hidden flex flex-col">
                {isCreatingNew ? (
                    // Create New Role Panel
                    <Card className="flex-grow flex flex-col overflow-hidden">
                        <form onSubmit={handleCreateRole} className="h-full flex flex-col">
                            <CardHeader className="border-b pb-4">
                                <CardTitle className="text-lg">إنشاء رتبة جديدة</CardTitle>
                                <CardDescription>قم بتعيين الرتبة المديرة والصلاحيات المناسبة لها</CardDescription>
                            </CardHeader>
                            <CardContent className="flex-grow overflow-y-auto p-6 space-y-6">
                                <div className="grid grid-cols-2 gap-4">
                                    <div className="space-y-2">
                                        <Label htmlFor="name">اسم الرتبة بالإنجليزية (Name)</Label>
                                        <Input
                                            id="name"
                                            placeholder="e.g. Branch Supervisor"
                                            value={newRoleName}
                                            onChange={e => setNewRoleName(e.target.value)}
                                            required
                                        />
                                    </div>
                                    <div className="space-y-2">
                                        <Label htmlFor="desc">الاسم المعرب (الوصف)</Label>
                                        <Input
                                            id="desc"
                                            placeholder="مثال: مشرف فرع"
                                            value={newRoleDesc}
                                            onChange={e => setNewRoleDesc(e.target.value)}
                                        />
                                    </div>
                                </div>

                                <div className="space-y-2">
                                    <Label>الرتبة الأعلى (المديرة)</Label>
                                    <Select value={newRoleParentId} onValueChange={setNewRoleParentId}>
                                        <SelectTrigger>
                                            <SelectValue placeholder="اختر الرتبة الأعلى" />
                                        </SelectTrigger>
                                        <SelectContent>
                                            <SelectItem value="none">بدون رتبة أعلى (رتبة رئيسية)</SelectItem>
                                            {roles.map(r => (
                                                <SelectItem key={r.id} value={r.id}>
                                                    {r.description || r.name}
                                                </SelectItem>
                                            ))}
                                        </SelectContent>
                                    </Select>
                                </div>

                                {/* Permissions Selection */}
                                <div className="space-y-4 pt-4 border-t">
                                    <h3 className="font-semibold text-sm flex items-center gap-2">
                                        <Key className="h-4 w-4 text-primary" />
                                        تحديد صلاحيات الرتبة
                                    </h3>
                                    {Object.entries(availablePermissions).map(([category, permKeys]) => (
                                        <div key={category} className="space-y-3 bg-muted/40 p-4 rounded-lg">
                                            <h4 className="font-medium text-sm text-primary">{category}</h4>
                                            <div className="grid grid-cols-2 gap-3">
                                                {permKeys.map(key => (
                                                    <div key={key} className="flex items-center gap-2">
                                                        <Checkbox
                                                            id={`new-${key}`}
                                                            checked={selectedRolePermissions.includes(key)}
                                                            onCheckedChange={() => handleTogglePermission(key)}
                                                        />
                                                        <label htmlFor={`new-${key}`} className="text-xs cursor-pointer">
                                                            {permissionLabels[key] || key}
                                                        </label>
                                                    </div>
                                                ))}
                                            </div>
                                        </div>
                                    ))}
                                </div>
                            </CardContent>
                            <CardFooter className="border-t p-4 flex justify-between">
                                <Button type="button" variant="outline" onClick={() => setIsCreatingNew(false)}>
                                    إلغاء
                                </Button>
                                <Button type="submit" disabled={isSaving}>
                                    {isSaving ? "جاري الإنشاء..." : "إنشاء الرتبة"}
                                </Button>
                            </CardFooter>
                        </form>
                    </Card>
                ) : selectedRole ? (
                    // View/Edit Role Permissions Panel
                    <Card className="flex-grow flex flex-col overflow-hidden">
                        <CardHeader className="border-b pb-4">
                            <div className="flex items-center justify-between">
                                <div className="space-y-1">
                                    <div className="flex items-center gap-2">
                                        <CardTitle className="text-lg">
                                            صلاحيات رتبة: {selectedRole.description || selectedRole.name}
                                        </CardTitle>
                                        {selectedRole.isSystemRole && (
                                            <span className="text-[10px] uppercase font-bold px-1.5 py-0.5 rounded bg-amber-500/10 text-amber-500 border border-amber-500/20">
                                                مالك النظام
                                            </span>
                                        )}
                                    </div>
                                    <CardDescription>
                                        {selectedRole.isSystemRole
                                            ? "رتبة مالك النظام تمتلك كافة الصلاحيات بشكل كامل وتلقائي ولا يمكن تعديلها."
                                            : "قم بتعديل وحفظ الصلاحيات لهذه الرتبة أدناه."}
                                    </CardDescription>
                                </div>
                                {!selectedRole.isSystemRole && (
                                    <Button variant="outline" size="sm" className="text-destructive hover:bg-destructive/10" onClick={handleDeleteRole}>
                                        <Trash2 className="h-4 w-4 me-1" />
                                        حذف
                                    </Button>
                                )}
                            </div>
                        </CardHeader>
                        <CardContent className="flex-grow overflow-y-auto p-6 space-y-6">
                            <div className="space-y-4">
                                {Object.entries(availablePermissions).map(([category, permKeys]) => (
                                    <div key={category} className="space-y-3 bg-muted/40 p-4 rounded-lg">
                                        <h4 className="font-medium text-sm text-primary flex items-center gap-1.5">
                                            <Layers className="h-3.5 w-3.5" />
                                            {category}
                                        </h4>
                                        <div className="grid grid-cols-2 md:grid-cols-3 gap-3">
                                            {permKeys.map(key => (
                                                <div key={key} className="flex items-center gap-2">
                                                    <Checkbox
                                                        id={`role-${key}`}
                                                        checked={selectedRole.isSystemRole || selectedRolePermissions.includes(key)}
                                                        disabled={selectedRole.isSystemRole}
                                                        onCheckedChange={() => handleTogglePermission(key)}
                                                    />
                                                    <label
                                                        htmlFor={`role-${key}`}
                                                        className={`text-xs cursor-pointer ${
                                                            selectedRole.isSystemRole ? "text-muted-foreground" : ""
                                                        }`}
                                                    >
                                                        {permissionLabels[key] || key}
                                                    </label>
                                                </div>
                                            ))}
                                        </div>
                                    </div>
                                ))}
                            </div>
                        </CardContent>
                        {!selectedRole.isSystemRole && (
                            <CardFooter className="border-t p-4 flex justify-end">
                                <Button onClick={handleSavePermissions} disabled={isSaving}>
                                    <Save className="h-4 w-4 me-2" />
                                    {isSaving ? "جاري الحفظ..." : "حفظ صلاحيات الرتبة"}
                                </Button>
                            </CardFooter>
                        )}
                    </Card>
                ) : (
                    // Empty state (should rarely happen since default is selected)
                    <Card className="flex-grow flex items-center justify-center p-12 text-center text-muted-foreground">
                        <div className="space-y-2">
                            <Shield className="h-12 w-12 mx-auto opacity-50" />
                            <h3 className="font-semibold text-lg">لم يتم تحديد رتبة</h3>
                            <p className="text-sm">اختر رتبة من القائمة الجانبية لاستعراض وتعديل صلاحياتها.</p>
                        </div>
                    </Card>
                )}
            </div>
        </div>
    )
}
