"use client"

import { useEffect, useState } from "react"
import { Plus, Search, RefreshCw, Users as UsersIcon, ShieldCheck } from "lucide-react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { UserTable } from "@/components/users/user-table"
import { UserDialog } from "@/components/users/user-dialog"
import { ResetPinDialog } from "@/components/users/reset-pin-dialog"
import { RolesTab } from "@/components/users/roles-tab"
import { Employee, employeeService } from "@/services/employee-service"
import { toast } from "sonner"
import { PermissionsMatrix } from "@/components/users/permissions-matrix"
import { AuditLogsTab } from "@/components/users/audit-logs-tab"
import { PerformanceReports } from "@/components/users/performance-reports"
import { Activity, ShieldAlert, BarChart3 } from "lucide-react"

export default function UsersPage() {
    const [employees, setEmployees] = useState<Employee[]>([])
    const [searchTerm, setSearchTerm] = useState("")
    const [isLoading, setIsLoading] = useState(false)
    
    // Dialog control states
    const [isUserDialogOpen, setIsUserDialogOpen] = useState(false)
    const [isResetPinOpen, setIsResetPinOpen] = useState(false)
    const [selectedEmployee, setSelectedEmployee] = useState<Employee | null>(null)

    useEffect(() => {
        loadEmployees()
    }, [])

    const loadEmployees = async () => {
        setIsLoading(true)
        try {
            const data = await employeeService.getEmployees()
            setEmployees(data)
        } catch (err) {
            toast.error("فشل تحميل قائمة الحسابات")
        } finally {
            setIsLoading(false)
        }
    }

    const handleAddEmployee = () => {
        setSelectedEmployee(null)
        setIsUserDialogOpen(true)
    }

    const handleEditEmployee = (employee: Employee) => {
        setSelectedEmployee(employee)
        setIsUserDialogOpen(true)
    }

    const handleResetPin = (employee: Employee) => {
        setSelectedEmployee(employee)
        setIsResetPinOpen(true)
    }

    const handleDeleteEmployee = async (employee: Employee) => {
        if (!confirm(`هل أنت متأكد من تعطيل/حذف حساب الموظف "${employee.fullName}"؟`)) return

        try {
            await employeeService.deleteEmployee(employee.id)
            toast.success("تم حذف حساب الموظف بنجاح")
            loadEmployees()
        } catch (err: any) {
            toast.error(err.response?.data?.error || "فشل حذف الموظف")
        }
    }

    const filteredEmployees = employees.filter((emp) => {
        const query = searchTerm.toLowerCase()
        return (
            (emp.fullName || "").toLowerCase().includes(query) ||
            (emp.employeeCode || "").toLowerCase().includes(query) ||
            (emp.jobTitle || "").toLowerCase().includes(query) ||
            (emp.role || "").toLowerCase().includes(query)
        )
    })

    return (
        <div className="space-y-6" dir="rtl">
            <div className="flex items-center justify-between">
                <div>
                    <h1 className="text-3xl font-bold tracking-tight">إدارة الحسابات والصلاحيات</h1>
                    <p className="text-muted-foreground">
                        قم بإنشاء حسابات الكاشير والموظفين، وتعيين الرتب والهيكل التنظيمي للمنظومة
                    </p>
                </div>
            </div>

            <Tabs defaultValue="users" className="w-full space-y-4">
                <TabsList className="grid w-full grid-cols-5 max-w-[1000px]">
                    <TabsTrigger value="users" className="gap-2">
                        <UsersIcon className="h-4 w-4" />
                        حسابات الموظفين
                    </TabsTrigger>
                    <TabsTrigger value="roles" className="gap-2">
                        <ShieldCheck className="h-4 w-4" />
                        هيكل الرتب (SAP)
                    </TabsTrigger>
                    <TabsTrigger value="permissions" className="gap-2">
                        <ShieldAlert className="h-4 w-4" />
                        مصفوفة الصلاحيات التفصيلية
                    </TabsTrigger>
                    <TabsTrigger value="logs" className="gap-2">
                        <Activity className="h-4 w-4" />
                        سجل الرقابة والنشاط
                    </TabsTrigger>
                    <TabsTrigger value="performance" className="gap-2">
                        <BarChart3 className="h-4 w-4" />
                        تقارير أداء الكاشير
                    </TabsTrigger>
                </TabsList>

                {/* Tab 1: Employees management */}
                <TabsContent value="users" className="space-y-4">
                    <div className="flex items-center gap-4">
                        <div className="relative flex-1">
                            <Search className="absolute right-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                            <Input
                                placeholder="ابحث باسم الموظف، الكود، الرتبة أو المسمى الوظيفي..."
                                value={searchTerm}
                                onChange={(e) => setSearchTerm(e.target.value)}
                                className="pr-10 pl-4"
                            />
                        </div>
                        <Button variant="outline" size="icon" onClick={loadEmployees} title="تحديث القائمة">
                            <RefreshCw className="h-4 w-4" />
                        </Button>
                        <Button onClick={handleAddEmployee}>
                            <Plus className="ml-2 h-4 w-4" />
                            إضافة حساب
                        </Button>
                    </div>

                    {isLoading ? (
                        <div className="flex flex-col items-center justify-center py-12 gap-3 text-muted-foreground">
                            <RefreshCw className="h-8 w-8 animate-spin" />
                            <span>جاري تحميل الحسابات من السيرفر...</span>
                        </div>
                    ) : (
                        <UserTable
                            users={filteredEmployees}
                            onEdit={handleEditEmployee}
                            onResetPin={handleResetPin}
                            onDelete={handleDeleteEmployee}
                        />
                    )}
                </TabsContent>

                {/* Tab 2: Roles and SAP hierarchy tree management */}
                <TabsContent value="roles" className="space-y-4">
                    <RolesTab />
                </TabsContent>

                {/* Tab 3: Detailed permissions matrix */}
                <TabsContent value="permissions" className="space-y-4">
                    <PermissionsMatrix />
                </TabsContent>

                {/* Tab 4: Employee activity audit logs */}
                <TabsContent value="logs" className="space-y-4">
                    <AuditLogsTab />
                </TabsContent>

                {/* Tab 5: Cashier sales performance reports */}
                <TabsContent value="performance" className="space-y-4">
                    <PerformanceReports />
                </TabsContent>
            </Tabs>

            {/* Account Creation & Edit Dialog */}
            <UserDialog
                open={isUserDialogOpen}
                onOpenChange={setIsUserDialogOpen}
                onSave={loadEmployees}
                user={selectedEmployee}
            />

            {/* Password/PIN Reset Dialog */}
            <ResetPinDialog
                open={isResetPinOpen}
                onOpenChange={setIsResetPinOpen}
                employee={selectedEmployee}
            />
        </div>
    )
}
