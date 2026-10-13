"use client"

import { useState, useEffect } from "react"
import { Shield, Save, CheckSquare, Square, RefreshCw, User } from "lucide-react"
import { Button } from "@/components/ui/button"
import { Checkbox } from "@/components/ui/checkbox"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Employee, employeeService, FlatRole } from "@/services/employee-service"
import { toast } from "sonner"
import apiClient from "@/lib/api-client"

// Human-readable Arabic labels for each permission key (synced with backend Labels registry)
const PERMISSION_LABELS: Record<string, string> = {
    // Settings
    "settings.view": "عرض الإعدادات",
    "settings.edit": "تعديل الإعدادات",
    "permissions.manage": "إدارة الصلاحيات",
    // Products
    "products.add": "إضافة منتج",
    "products.view": "عرض المنتجات",
    "products.activate": "تفعيل المنتجات",
    "products.barcode": "طباعة باركود",
    "products.required": "نواقص المنتجات",
    "products.expire": "تاريخ الصلاحية",
    "products.unit": "الوحدات والعبوات",
    "products.company": "الشركات المصنعة",
    "products.financials": "البيانات المالية للمنتج",
    // Customers
    "customers.view": "عرض العملاء",
    "customers.money": "مالية العملاء",
    "customers.report": "تقارير العملاء",
    "customers.points": "نقاط العملاء",
    "customers.points_report": "تقارير النقاط",
    // Suppliers
    "suppliers.view": "عرض الموردين",
    "suppliers.money": "مالية الموردين",
    "suppliers.report": "تقارير الموردين",
    // Purchases
    "purchases.create": "إنشاء فاتورة شراء",
    "purchases.na89": "الأصناف الناقصة للمشتريات",
    "purchases.report": "تقارير المشتريات",
    // Sales
    "sales.create": "إنشاء فاتورة بيع",
    "sales.insurance": "مبيعات التأمين",
    "sales.insurance_company": "شركات التأمين",
    "sales.user_report": "تقارير المستخدمين",
    "sales.report": "تقارير المبيعات",
    "sales.insurance_report": "تقارير التأمين",
    "sales.profit": "تقارير الأرباح",
    // Returns
    "returns.sale": "مرتجع بيع",
    "returns.buy": "مرتجع شراء",
    "returns.report": "تقارير المرتجعات",
    // Treasury
    "treasury.add_stock": "إضافة خزينة",
    "treasury.stock_deposit": "إيداع خزينة",
    "treasury.bank_deposit": "إيداع بنكي",
    "treasury.stock_withdraw": "سحب من الخزينة",
    "treasury.bank_withdraw": "سحب بنكي",
    "treasury.transfer": "تحويل مالي بين الخزن",
    "treasury.bank_stock_transfer": "تحويل بنك/خزينة",
    "treasury.balance": "عرض الأرصدة الحالية",
    "treasury.report": "تقارير الخزينة",
    // Employees
    "employees.add": "إضافة موظف",
    "employees.borrow_items": "عهد وأصناف مستعارة",
    "employees.borrow_money": "سلف مالية",
    "employees.salary": "المرتبات والأجور",
    "employees.borrow_items_report": "تقارير العهد",
    "employees.borrow_money_report": "تقارير السلف",
    "employees.salary_report": "تقارير المرتبات",
    // Accounting
    "accounting.type": "أنواع المديونيات",
    "accounting.entry": "القيود والمديونيات",
    "accounting.report": "تقارير الحسابات العامة",
    "accounting.receipt": "سند قبض",
    "accounting.payment": "سند صرف مالي",
    "accounting.voucher_report": "تقارير السندات",
    "accounting.tax_report": "تقارير الضرائب",
    // Reports
    "reports.customers": "تقارير العملاء الشاملة",
    "reports.suppliers": "تقارير الموردين الشاملة",
    "reports.purchases": "تقارير المشتريات الشاملة",
    "reports.sales": "تقارير المبيعات الشاملة",
    "reports.returns": "تقارير المرتجعات الشاملة",
    "reports.accounting": "تقارير الحسابات الشاملة",
    "reports.shifts": "تقارير الشيفتات الشاملة",
    // Backup
    "backup.create": "إنشاء نسخة احتياطية",
    "backup.restore": "استعادة نسخة احتياطية",
    // Warehouse
    "warehouse.add": "إضافة مخزن",
    "warehouse.inventory": "جرد المخازن",
    "warehouse.transfer": "تحويل بين المخازن",
    "warehouse.out": "إخراج تالف/عينات",
    "warehouse.transfer_report": "تقارير التحويلات",
    "warehouse.out_report": "تقارير الهوالك والتوالف",
    // Shifts
    "shifts.open": "فتح الوردية/الشيفت",
    "shifts.close": "إغلاق الوردية/الشيفت"
}

export function PermissionsMatrix() {
    const [employees, setEmployees] = useState<Employee[]>([])
    const [roles, setRoles] = useState<FlatRole[]>([])
    const [selectedEmployeeId, setSelectedEmployeeId] = useState<string>("")
    const [selectedRoleId, setSelectedRoleId] = useState<string>("")
    
    const [availablePermissions, setAvailablePermissions] = useState<Record<string, string[]>>({})
    const [selectedPermissions, setSelectedPermissions] = useState<string[]>([])
    
    const [isLoading, setIsLoading] = useState(false)
    const [isSaving, setIsSaving] = useState(false)

    // Load initial configuration data
    useEffect(() => {
        loadData()
    }, [])

    const loadData = async () => {
        setIsLoading(true)
        try {
            const [empList, rolesRes, permsRes] = await Promise.all([
                employeeService.getEmployees(),
                employeeService.getRoles(),
                apiClient.get<Record<string, string[]>>("/employee-permissions/available")
            ])
            
            // Format hierarchical roles
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

            setEmployees(empList)
            setRoles(list)
            setAvailablePermissions(permsRes.data)
            
            if (empList.length > 0) {
                setSelectedEmployeeId(empList[0].id)
            }
        } catch (err) {
            toast.error("فشل تحميل الصلاحيات الأساسية للمنظومة")
        } finally {
            setIsLoading(false)
        }
    }

    // Load employee-specific permissions when employee changes
    useEffect(() => {
        if (selectedEmployeeId) {
            loadEmployeePermissions(selectedEmployeeId)
        }
    }, [selectedEmployeeId])

    const loadEmployeePermissions = async (empId: string) => {
        setIsLoading(true)
        try {
            const emp = employees.find(e => e.id === empId)
            if (emp) {
                setSelectedRoleId(emp.roleId || "")
            }

            const res = await apiClient.get<string[]>(`/employee-permissions/${empId}`)
            // Normalize inputs
            setSelectedPermissions(res.data.map(p => p.toLowerCase().trim()))
        } catch (err) {
            toast.error("حدث خطأ أثناء تحميل صلاحيات هذا الموظف")
        } finally {
            setIsLoading(false)
        }
    }

    // Auto-select standard permissions for a role when changed
    const handleRoleChange = async (roleId: string) => {
        setSelectedRoleId(roleId)
        setIsLoading(true)
        try {
            const perms = await employeeService.getRolePermissions(roleId)
            setSelectedPermissions(perms.map(p => p.toLowerCase().trim()))
        } catch (err) {
            toast.error("حدث خطأ أثناء سحب صلاحيات الرتبة")
        } finally {
            setIsLoading(false)
        }
    }

    const togglePermission = (key: string) => {
        const lowerKey = key.toLowerCase().trim()
        if (selectedPermissions.includes(lowerKey)) {
            setSelectedPermissions(selectedPermissions.filter(p => p !== lowerKey))
        } else {
            setSelectedPermissions([...selectedPermissions, lowerKey])
        }
    }

    const toggleCategory = (category: string, keys: string[]) => {
        const lowerKeys = keys.map(k => k.toLowerCase().trim())
        const allSelected = lowerKeys.every(k => selectedPermissions.includes(k))
        
        if (allSelected) {
            setSelectedPermissions(selectedPermissions.filter(p => !lowerKeys.includes(p)))
        } else {
            const added = [...selectedPermissions, ...lowerKeys]
            setSelectedPermissions(Array.from(new Set(added)))
        }
    }

    const handleSave = async () => {
        if (!selectedEmployeeId) return
        setIsSaving(true)
        try {
            await employeeService.setEmployeePermissions(selectedEmployeeId, {
                roleId: selectedRoleId || undefined,
                permissions: selectedPermissions
            })
            toast.success("تم حفظ صلاحيات الموظف وتحديث رتبته بنجاح")
            // Update local employee structure
            setEmployees(employees.map(e => {
                if (e.id === selectedEmployeeId) {
                    return {
                        ...e,
                        roleId: selectedRoleId || null,
                        role: roles.find(r => r.id === selectedRoleId)?.name || "Staff"
                    }
                }
                return e
            }))
        } catch (err: any) {
            toast.error(err.response?.data?.error || "حدث خطأ أثناء حفظ التعديلات")
        } finally {
            setIsSaving(false)
        }
    }

    const selectedEmployee = employees.find(e => e.id === selectedEmployeeId)

    return (
        <div className="space-y-6">
            <div className="flex flex-col md:flex-row gap-4 justify-between items-start md:items-center bg-card p-4 rounded-lg border">
                <div className="flex flex-wrap gap-4 items-center flex-1 w-full">
                    <div className="flex items-center gap-2 flex-1 min-w-[240px]">
                        <User className="h-4 w-4 text-muted-foreground" />
                        <span className="text-sm font-medium whitespace-nowrap">اختر الموظف:</span>
                        <Select value={selectedEmployeeId} onValueChange={setSelectedEmployeeId}>
                            <SelectTrigger className="w-full">
                                <SelectValue placeholder="حدد الموظف لتعديل صلاحياته" />
                            </SelectTrigger>
                            <SelectContent>
                                {employees.map((emp) => (
                                    <SelectItem key={emp.id} value={emp.id}>
                                        {emp.fullName} ({emp.employeeCode || "لا يوجد كود"})
                                    </SelectItem>
                                ))}
                            </SelectContent>
                        </Select>
                    </div>

                    <div className="flex items-center gap-2 flex-1 min-w-[240px]">
                        <Shield className="h-4 w-4 text-muted-foreground" />
                        <span className="text-sm font-medium whitespace-nowrap">الرتبة الوظيفية:</span>
                        <Select value={selectedRoleId} onValueChange={handleRoleChange}>
                            <SelectTrigger className="w-full">
                                <SelectValue placeholder="اختر رتبة الموظف" />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value="none">بدون رتبة محددة</SelectItem>
                                {roles.map((r) => (
                                    <SelectItem key={r.id} value={r.id}>
                                        <span className="font-mono text-xs">{r.display}</span>
                                    </SelectItem>
                                ))}
                            </SelectContent>
                        </Select>
                    </div>
                </div>

                <div className="flex gap-2 w-full md:w-auto mt-4 md:mt-0">
                    <Button variant="outline" size="icon" onClick={loadData} disabled={isLoading}>
                        <RefreshCw className={`h-4 w-4 ${isLoading ? "animate-spin" : ""}`} />
                    </Button>
                    <Button onClick={handleSave} disabled={isSaving || !selectedEmployeeId} className="flex-1 md:flex-initial">
                        <Save className="ml-2 h-4 w-4" />
                        {isSaving ? "جاري الحفظ..." : "حفظ الصلاحيات النشطة"}
                    </Button>
                </div>
            </div>

            {selectedEmployee && (
                <div className="bg-amber-500/10 text-amber-500 text-xs px-4 py-3 rounded-lg border border-amber-500/20 flex gap-2 items-center">
                    <span>💡 <strong>ملاحظة:</strong> تعديل الصلاحيات بالأسفل سينطبق خصيصاً على الحساب الشخصي للموظف <strong>{selectedEmployee.fullName}</strong>. تغيير الرتبة بالأعلى يقوم بتحميل الصلاحيات الافتراضية للرتبة كقالب مساعد.</span>
                </div>
            )}

            {isLoading ? (
                <div className="flex justify-center items-center py-20 text-muted-foreground text-sm">
                    <RefreshCw className="animate-spin ml-2 h-5 w-5" />
                    جاري تحميل خريطة الصلاحيات...
                </div>
            ) : Object.keys(availablePermissions).length === 0 ? (
                <div className="text-center py-20 border border-dashed rounded-lg text-muted-foreground text-sm">
                    لا توجد صلاحيات مسجلة في المنظومة
                </div>
            ) : (
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                    {Object.entries(availablePermissions).map(([category, keys]) => {
                        const lowerKeys = keys.map(k => k.toLowerCase().trim())
                        const categorySelected = lowerKeys.every(k => selectedPermissions.includes(k))
                        const someSelected = lowerKeys.some(k => selectedPermissions.includes(k)) && !categorySelected

                        return (
                            <Card key={category} className="shadow-sm border">
                                <CardHeader className="pb-3 bg-muted/30 border-b flex flex-row items-center justify-between space-y-0 py-3">
                                    <div>
                                        <CardTitle className="text-sm font-bold">{category}</CardTitle>
                                    </div>
                                    <Button 
                                        type="button" 
                                        variant="ghost" 
                                        size="sm" 
                                        className="h-8 px-2 text-xs gap-1"
                                        onClick={() => toggleCategory(category, keys)}
                                    >
                                        {categorySelected ? (
                                            <>
                                                <Square className="h-3 w-3" />
                                                إلغاء الكل
                                            </>
                                        ) : (
                                            <>
                                                <CheckSquare className="h-3 w-3" />
                                                تحديد الكل
                                            </>
                                        )}
                                    </Button>
                                </CardHeader>
                                <CardContent className="pt-4 space-y-3">
                                    {keys.map((key) => {
                                        const lowerKey = key.toLowerCase().trim()
                                        const isChecked = selectedPermissions.includes(lowerKey)
                                        const label = PERMISSION_LABELS[lowerKey] || key

                                        return (
                                            <div key={key} className="flex items-center space-x-reverse space-x-2">
                                                <Checkbox
                                                    id={key}
                                                    checked={isChecked}
                                                    onCheckedChange={() => togglePermission(key)}
                                                />
                                                <label
                                                    htmlFor={key}
                                                    className="text-xs leading-none font-medium peer-disabled:cursor-not-allowed peer-disabled:opacity-70 cursor-pointer select-none"
                                                >
                                                    {label}
                                                </label>
                                            </div>
                                        )
                                    })}
                                </CardContent>
                            </Card>
                        )
                    })}
                </div>
            )}
        </div>
    )
}
