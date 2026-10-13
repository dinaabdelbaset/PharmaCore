"use client"

import { useState, useEffect } from "react"
import { 
    Search, 
    Plus, 
    Loader2, 
    Coins, 
    Briefcase, 
    Calendar, 
    DollarSign, 
    UserCheck, 
    CreditCard, 
    ArrowUpRight, 
    ArrowDownRight, 
    Sparkles, 
    Filter, 
    Database, 
    RefreshCw,
    FileText,
    TrendingUp,
    AlertCircle
} from "lucide-react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Textarea } from "@/components/ui/textarea"
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
import { Badge } from "@/components/ui/badge"
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
    DialogFooter,
} from "@/components/ui/dialog"
import { Label } from "@/components/ui/label"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { format } from "date-fns"
import apiClient from "@/lib/api-client"
import { toast } from "sonner"

// ── Types ──────────────────────────────────────────────────────────

interface Employee {
    id: string
    employeeCode: string
    fullName: string
    jobTitle?: string | null
    grade?: string | null
    role: string
    isActive: boolean
    orgId: string
    branchId?: string | null
    roleId?: string | null
    // Virtual clientside attributes for simulation
    salary?: number
}

interface CashRegisterEntry {
    id: string
    orgId: string
    branchId?: string | null
    saleId?: string | null
    amount: number
    entryType: string // 'Advance' or 'Salary'
    description?: string | null
    employeeName: string
    employeeId?: string | null // virtual link
    isBank: boolean // true = Bank/Visa, false = Cash
    date: string
    isActive: boolean
    // Virtual attributes for salary payouts details
    grossSalary?: number
    deductions?: number
    bonus?: number
    month?: number
    year?: number
}

// ── High-Fidelity Simulated Data ─────────────────────────────────────

const simulatedEmployees: Employee[] = [
    { id: "e1", employeeCode: "E1001", fullName: "Ahmed Hassan", jobTitle: "Operations Manager", role: "Manager", isActive: true, orgId: "org-000", salary: 6500 },
    { id: "e2", employeeCode: "E1002", fullName: "Sara Mohamed", jobTitle: "HR Business Partner", role: "HR", isActive: true, orgId: "org-000", salary: 5200 },
    { id: "e3", employeeCode: "E1003", fullName: "Khaled Ali", jobTitle: "Lead Pharmacist", role: "Supervisor", isActive: true, orgId: "org-000", salary: 4500 },
    { id: "e4", employeeCode: "E1004", fullName: "Nour Ibrahim", jobTitle: "Pharmacist", role: "Employee", isActive: true, orgId: "org-000", salary: 3800 },
    { id: "e5", employeeCode: "E1005", fullName: "Omar Abdel", jobTitle: "POS Cashier", role: "Employee", isActive: true, orgId: "org-000", salary: 3000 }
]

const simulatedLedger: CashRegisterEntry[] = [
    {
        id: "l1",
        orgId: "org-000",
        amount: 3000,
        entryType: "Salary",
        description: "Salary payout for May 2026. Gross: 3000 | Deductions: 0 | Bonus: 0",
        employeeName: "Omar Abdel",
        employeeId: "e5",
        isBank: true,
        date: "2026-05-24T10:00:00Z",
        isActive: true,
        grossSalary: 3000,
        deductions: 0,
        bonus: 0,
        month: 5,
        year: 2026
    },
    {
        id: "l2",
        orgId: "org-000",
        amount: 500,
        entryType: "Advance",
        description: "Emergency cash advance for family visit",
        employeeName: "Ahmed Hassan",
        employeeId: "e1",
        isBank: false,
        date: "2026-05-15T14:30:00Z",
        isActive: true
    },
    {
        id: "l3",
        orgId: "org-000",
        amount: 300,
        entryType: "Advance",
        description: "Cash advance request",
        employeeName: "Khaled Ali",
        employeeId: "e3",
        isBank: false,
        date: "2026-05-02T11:15:00Z",
        isActive: true
    },
    {
        id: "l4",
        orgId: "org-000",
        amount: 3350,
        entryType: "Salary",
        description: "Salary payout for April 2026. Gross: 3800 | Deductions: 450 | Bonus: 0",
        employeeName: "Nour Ibrahim",
        employeeId: "e4",
        isBank: true,
        date: "2026-04-30T16:00:00Z",
        isActive: true,
        grossSalary: 3800,
        deductions: 450,
        bonus: 0,
        month: 4,
        year: 2026
    },
    {
        id: "l5",
        orgId: "org-000",
        amount: 450,
        entryType: "Advance",
        description: "Pre-salary advance request",
        employeeName: "Nour Ibrahim",
        employeeId: "e4",
        isBank: false,
        date: "2026-04-12T09:00:00Z",
        isActive: true
    },
    {
        id: "l6",
        orgId: "org-000",
        amount: 5200,
        entryType: "Salary",
        description: "Salary payout for March 2026. Gross: 5200 | Deductions: 0 | Bonus: 0",
        employeeName: "Sara Mohamed",
        employeeId: "e2",
        isBank: true,
        date: "2026-03-31T17:30:00Z",
        isActive: true,
        grossSalary: 5200,
        deductions: 0,
        bonus: 0,
        month: 3,
        year: 2026
    },
    {
        id: "l7",
        orgId: "org-000",
        amount: 250,
        entryType: "Advance",
        description: "Personal cash advance",
        employeeName: "Nour Ibrahim",
        employeeId: "e4",
        isBank: false,
        date: "2026-03-05T12:00:00Z",
        isActive: true
    },
    {
        id: "l8",
        orgId: "org-000",
        amount: 6500,
        entryType: "Salary",
        description: "Salary payout for February 2026. Gross: 6500 | Deductions: 0 | Bonus: 0",
        employeeName: "Ahmed Hassan",
        employeeId: "e1",
        isBank: true,
        date: "2026-02-28T15:00:00Z",
        isActive: true,
        grossSalary: 6500,
        deductions: 0,
        bonus: 0,
        month: 2,
        year: 2026
    },
    {
        id: "l9",
        orgId: "org-000",
        amount: 2800,
        entryType: "Salary",
        description: "Salary payout for January 2026. Gross: 3000 | Deductions: 200 | Bonus: 0",
        employeeName: "Omar Abdel",
        employeeId: "e5",
        isBank: false,
        date: "2026-01-30T14:00:00Z",
        isActive: true,
        grossSalary: 3000,
        deductions: 200,
        bonus: 0,
        month: 1,
        year: 2026
    },
    {
        id: "l10",
        orgId: "org-000",
        amount: 200,
        entryType: "Advance",
        description: "First cash advance",
        employeeName: "Omar Abdel",
        employeeId: "e5",
        isBank: false,
        date: "2026-01-10T10:30:00Z",
        isActive: true
    }
]

export default function StaffFinancePage() {
    // ── State Management ─────────────────────────────────────────────
    const [employees, setEmployees] = useState<Employee[]>([])
    const [ledgerEntries, setLedgerEntries] = useState<CashRegisterEntry[]>([])
    
    const [isLoadingEmployees, setIsLoadingEmployees] = useState(true)
    const [isLoadingLedger, setIsLoadingLedger] = useState(true)
    
    // Switch between Live database mode and Simulation (Sandbox) mode
    const [isSimulated, setIsSimulated] = useState(true)
    
    // Dialog modal open states
    const [isAdvanceDialogOpen, setIsAdvanceDialogOpen] = useState(false)
    const [isSalaryDialogOpen, setIsSalaryDialogOpen] = useState(false)
    
    // Dialog submit progress states
    const [isSubmittingAdvance, setIsSubmittingAdvance] = useState(false)
    const [isSubmittingSalary, setIsSubmittingSalary] = useState(false)

    // Filter and Search states
    const [searchTerm, setSearchTerm] = useState("")
    const [typeFilter, setTypeFilter] = useState<string>("all")
    const [paymentFilter, setPaymentFilter] = useState<string>("all")
    const [employeeFilter, setEmployeeFilter] = useState<string>("all")

    // Form inputs state: Advance
    const [advEmployeeId, setAdvEmployeeId] = useState("")
    const [advAmount, setAdvAmount] = useState("")
    const [advIsBank, setAdvIsBank] = useState("false")
    const [advDate, setAdvDate] = useState(() => format(new Date(), "yyyy-MM-dd"))
    const [advDescription, setAdvDescription] = useState("")

    // Form inputs state: Salary
    const [salEmployeeId, setSalEmployeeId] = useState("")
    const [salMonth, setSalMonth] = useState(() => String(new Date().getMonth() + 1))
    const [salYear, setSalYear] = useState(() => String(new Date().getFullYear()))
    const [salGross, setSalGross] = useState("")
    const [salDeduction, setSalDeduction] = useState("")
    const [salBonus, setSalBonus] = useState("")
    const [salIsBank, setSalIsBank] = useState("true")
    const [salDate, setSalDate] = useState(() => format(new Date(), "yyyy-MM-dd"))
    const [salDescription, setSalDescription] = useState("")

    // ── Load and Sync Data ───────────────────────────────────────────
    const loadData = async () => {
        setIsLoadingEmployees(true)
        setIsLoadingLedger(true)
        
        let apiEmployeesLoaded = false
        let apiLedgerLoaded = false

        // 1. Fetch Employees
        try {
            const response = await apiClient.get<Employee[]>("/employees")
            if (response.data && Array.isArray(response.data)) {
                // Enrich employee items with mock salaries if not present
                const enriched = response.data.map((emp, index) => ({
                    ...emp,
                    salary: emp.salary || (1800 + (index * 600))
                }))
                setEmployees(enriched)
                apiEmployeesLoaded = true
            }
        } catch (error) {
            console.warn("Failed to fetch live employees. Falling back to sandbox.", error)
        }

        // 2. Fetch Ledger (Cash Register Entries)
        try {
            // Fetch treasury entries (handles paginated object or flat array)
            const response = await apiClient.get<any>("/treasury/entries")
            if (response.data) {
                if (response.data.items && Array.isArray(response.data.items)) {
                    setLedgerEntries(response.data.items)
                    apiLedgerLoaded = true
                } else if (Array.isArray(response.data)) {
                    setLedgerEntries(response.data)
                    apiLedgerLoaded = true
                }
            }
        } catch (error) {
            console.warn("Failed to fetch live ledger log. Falling back to sandbox.", error)
        }

        // If both loaded from API, disable simulation mode. Otherwise enable it as fallback.
        if (apiEmployeesLoaded && apiLedgerLoaded) {
            setIsSimulated(false)
        } else {
            setIsSimulated(true)
            setEmployees(simulatedEmployees)
            setLedgerEntries(simulatedLedger)
        }

        setIsLoadingEmployees(false)
        setIsLoadingLedger(false)
    }

    useEffect(() => {
        loadData()
    }, [])

    const handleRefresh = async () => {
        await loadData()
        toast.success("Synchronized cash ledger database")
    }

    // Toggle simulation switch
    const toggleDemoMode = (checked: boolean) => {
        setIsSimulated(checked)
        if (checked) {
            setEmployees(simulatedEmployees)
            setLedgerEntries(simulatedLedger)
            toast.info("Sandbox Mode activated. Showing simulated financial telemetry.")
        } else {
            loadData()
        }
    }

    // ── Live Calculation Engine ──────────────────────────────────────
    
    // Calculates outstanding advance balance per employee by summing up all their
    // Advances and subtracting all Deductions applied in their Salary payouts.
    const getOutstandingAdvanceForEmployee = (employeeName: string) => {
        const advancesSum = ledgerEntries
            .filter(e => e.employeeName.toLowerCase() === employeeName.toLowerCase() && e.entryType === 'Advance' && e.isActive)
            .reduce((sum, e) => sum + e.amount, 0)

        const deductionsSum = ledgerEntries
            .filter(e => e.employeeName.toLowerCase() === employeeName.toLowerCase() && e.entryType === 'Salary' && e.isActive)
            .reduce((sum, e) => sum + (e.deductions || 0), 0)

        return Math.max(0, advancesSum - deductionsSum)
    }

    // Dynamic metrics calculated from current state
    const outstandingAdvancesTotal = employees.reduce((sum, emp) => {
        return sum + getOutstandingAdvanceForEmployee(emp.fullName)
    }, 0)

    const currentMonthSalariesTotal = ledgerEntries
        .filter(e => {
            if (e.entryType !== 'Salary' || !e.isActive) return false
            const entryDate = new Date(e.date)
            const today = new Date()
            return entryDate.getMonth() === today.getMonth() && entryDate.getFullYear() === today.getFullYear()
        })
        .reduce((sum, e) => sum + e.amount, 0)

    const totalStaffCount = employees.filter(e => e.isActive).length

    // ── Action Handlers ──────────────────────────────────────────────

    // Opens Issue Advance Dialog
    const handleOpenAdvance = () => {
        setAdvEmployeeId(employees[0]?.id || "")
        setAdvAmount("")
        setAdvIsBank("false")
        setAdvDate(format(new Date(), "yyyy-MM-dd"))
        setAdvDescription("")
        setIsAdvanceDialogOpen(true)
    }

    // Opens Pay Salary Dialog
    const handleOpenSalary = () => {
        setSalEmployeeId(employees[0]?.id || "")
        setSalMonth(String(new Date().getMonth() + 1))
        setSalYear(String(new Date().getFullYear()))
        setSalBonus("")
        setSalIsBank("true")
        setSalDate(format(new Date(), "yyyy-MM-dd"))
        setSalDescription("")
        
        // Prefill salary details for the first employee
        const emp = employees[0]
        if (emp) {
            setSalGross(String(emp.salary || 3000))
            const outstanding = getOutstandingAdvanceForEmployee(emp.fullName)
            setSalDeduction(String(outstanding))
        } else {
            setSalGross("")
            setSalDeduction("0")
        }
        setIsSalaryDialogOpen(true)
    }

    // Triggered when employee is selected in Pay Salary Dialog
    const handleSalaryEmployeeChange = (empId: string) => {
        setSalEmployeeId(empId)
        const emp = employees.find(e => e.id === empId)
        if (emp) {
            setSalGross(String(emp.salary || 3000))
            const outstanding = getOutstandingAdvanceForEmployee(emp.fullName)
            setSalDeduction(String(outstanding))
        }
    }

    // Issue Cash Advance submit POST
    const handleIssueAdvanceSubmit = async (e: React.FormEvent) => {
        e.preventDefault()
        if (!advEmployeeId || !advAmount || parseFloat(advAmount) <= 0) {
            toast.error("Please select an employee and enter a valid advance amount.")
            return
        }

        const employee = employees.find(emp => emp.id === advEmployeeId)
        if (!employee) return

        setIsSubmittingAdvance(true)
        
        const payload = {
            employeeId: employee.id,
            employeeName: employee.fullName,
            amount: parseFloat(advAmount),
            isBank: advIsBank === "true",
            date: new Date(advDate).toISOString(),
            description: advDescription || `صرف سلفة للموظف: ${employee.fullName}`,
            orgId: employee.orgId,
            branchId: employee.branchId
        }

        try {
            // Attempt to hit live treasury advance API
            const response = await apiClient.post("/treasury/advance", payload)
            toast.success(`Successfully logged cash advance of $${payload.amount} for ${employee.fullName}`)
            setIsAdvanceDialogOpen(false)
            loadData() // Refresh
        } catch (err: any) {
            console.warn("Backend /api/treasury/advance endpoint not active or returned error. Processing via Sandbox Simulator.", err)
            
            // Simulating in UI state
            const simulatedRecord: CashRegisterEntry = {
                id: `l-adv-${Date.now()}`,
                orgId: employee.orgId,
                branchId: employee.branchId,
                amount: payload.amount,
                entryType: "Advance",
                description: payload.description,
                employeeName: employee.fullName,
                employeeId: employee.id,
                isBank: payload.isBank,
                date: payload.date,
                isActive: true
            }

            setLedgerEntries(prev => [simulatedRecord, ...prev])
            toast.success(`Advance Issued (Sandbox Mode): $${payload.amount} logged for ${employee.fullName}`)
            setIsAdvanceDialogOpen(false)
        } finally {
            setIsSubmittingAdvance(false)
        }
    }

    // Process Salary submit POST
    const handlePaySalarySubmit = async (e: React.FormEvent) => {
        e.preventDefault()
        const grossVal = parseFloat(salGross) || 0
        const dedVal = parseFloat(salDeduction) || 0
        const bonusVal = parseFloat(salBonus) || 0
        const netVal = grossVal + bonusVal - dedVal

        if (!salEmployeeId) {
            toast.error("Please select an employee.")
            return
        }
        if (netVal <= 0) {
            toast.error("Calculated net payout must be greater than 0.")
            return
        }

        const employee = employees.find(emp => emp.id === salEmployeeId)
        if (!employee) return

        setIsSubmittingSalary(true)

        const defaultDesc = `صرف راتب شهر ${salMonth}/${salYear} - الأساسي: ${grossVal} | الاستقطاع: ${dedVal} | الحوافز: ${bonusVal}`
        const payload = {
            employeeId: employee.id,
            employeeName: employee.fullName,
            amount: netVal, // Amount in cash register is the Net Paid
            entryType: "Salary",
            isBank: salIsBank === "true",
            date: new Date(salDate).toISOString(),
            description: salDescription || defaultDesc,
            orgId: employee.orgId,
            branchId: employee.branchId,
            // Custom extended fields
            grossSalary: grossVal,
            deductions: dedVal,
            bonus: bonusVal,
            month: parseInt(salMonth),
            year: parseInt(salYear)
        }

        try {
            // Attempt live API
            const response = await apiClient.post("/treasury/salary", payload)
            toast.success(`Successfully processed salary payout of $${netVal} for ${employee.fullName}`)
            setIsSalaryDialogOpen(false)
            loadData() // Refresh
        } catch (err: any) {
            console.warn("Backend /api/treasury/salary endpoint not active or returned error. Processing via Sandbox Simulator.", err)
            
            // Simulating in UI state
            const simulatedRecord: CashRegisterEntry = {
                id: `l-sal-${Date.now()}`,
                orgId: employee.orgId,
                branchId: employee.branchId,
                amount: payload.amount,
                entryType: "Salary",
                description: payload.description,
                employeeName: employee.fullName,
                employeeId: employee.id,
                isBank: payload.isBank,
                date: payload.date,
                isActive: true,
                grossSalary: payload.grossSalary,
                deductions: payload.deductions,
                bonus: payload.bonus,
                month: payload.month,
                year: payload.year
            }

            setLedgerEntries(prev => [simulatedRecord, ...prev])
            toast.success(`Salary Processed (Sandbox Mode): $${netVal} paid to ${employee.fullName}`)
            setIsSalaryDialogOpen(false)
        } finally {
            setIsSubmittingSalary(false)
        }
    }

    // ── Filtering Logic ──────────────────────────────────────────────
    const filteredLedger = ledgerEntries.filter(entry => {
        const matchesSearch = 
            entry.employeeName.toLowerCase().includes(searchTerm.toLowerCase()) ||
            (entry.description && entry.description.toLowerCase().includes(searchTerm.toLowerCase()))
        
        const matchesType = typeFilter === "all" || entry.entryType === typeFilter
        
        const matchesPayment = paymentFilter === "all" || 
            (paymentFilter === "bank" && entry.isBank) || 
            (paymentFilter === "cash" && !entry.isBank)

        const matchesEmployee = employeeFilter === "all" || 
            entry.employeeId === employeeFilter || 
            entry.employeeName === employees.find(emp => emp.id === employeeFilter)?.fullName

        return matchesSearch && matchesType && matchesPayment && matchesEmployee
    })

    return (
        <div className="flex-1 space-y-6 p-4 md:p-6 lg:p-8 pt-4 md:pt-6">
            {/* Header & Controls */}
            <div className="flex flex-col md:flex-row md:items-center justify-between gap-4">
                <div>
                    <h1 className="text-3xl font-extrabold tracking-tight bg-gradient-to-r from-zinc-900 to-zinc-600 dark:from-zinc-100 dark:to-zinc-400 bg-clip-text text-transparent">
                        Staff Finance & Custody
                    </h1>
                    <p className="text-sm text-muted-foreground mt-1">
                        إدارة عهد الموظفين والرواتب الشهرية وسجلات الخزينة
                    </p>
                </div>

                <div className="flex items-center flex-wrap gap-3">
                    {/* Sandbox / Simulation Switch */}
                    <div className="flex items-center gap-2 bg-zinc-100 dark:bg-zinc-900 border border-zinc-200 dark:border-zinc-800 p-2 rounded-lg shadow-sm">
                        <div className="flex items-center gap-1 text-xs font-semibold text-amber-500">
                            <Sparkles className="h-4 w-4 animate-pulse" />
                            <span>Sandbox Mode</span>
                        </div>
                        <button
                            role="switch"
                            id="demo-mode"
                            aria-checked={isSimulated}
                            onClick={() => toggleDemoMode(!isSimulated)}
                            className={`relative inline-flex h-5 w-9 shrink-0 cursor-pointer items-center rounded-full transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 ${isSimulated ? "bg-amber-500" : "bg-zinc-600"}`}
                        >
                            <span
                                className={`pointer-events-none block h-4 w-4 rounded-full bg-white shadow-lg ring-0 transition-transform ${isSimulated ? "translate-x-[18px]" : "translate-x-0.5"}`}
                            />
                        </button>
                    </div>

                    {/* Refresh Button */}
                    <Button variant="outline" size="icon" onClick={handleRefresh} title="Sync database data">
                        <RefreshCw className="h-4 w-4" />
                    </Button>
                </div>
            </div>

            {/* Glowing Sandbox Banner */}
            {isSimulated && (
                <div className="p-4 border border-amber-500/20 rounded-xl bg-gradient-to-r from-amber-500/5 to-orange-500/5 shadow-md flex items-center justify-between gap-4 animate-fade-in">
                    <div className="flex items-center gap-3 text-sm text-amber-800 dark:text-amber-300">
                        <AlertCircle className="h-5 w-5 shrink-0" />
                        <span>
                            <strong>Demo & Sandbox Fallback Active</strong>: Real-time telemetry is simulating payroll actions. Posting forms will update the UI state locally and alert the system logs.
                        </span>
                    </div>
                </div>
            )}

            {/* Stats Cards Row */}
            <div className="grid gap-4 md:grid-cols-3">
                {/* Card 1: Outstanding Advances */}
                <Card className="relative overflow-hidden hover:border-primary/20 transition-all duration-200 shadow-md bg-gradient-to-br from-card to-zinc-50/50 dark:to-zinc-950/20">
                    <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                        <CardTitle className="text-sm font-semibold text-muted-foreground flex items-center gap-1.5">
                            Outstanding Advances
                            <span className="text-[10px] bg-amber-500/10 text-amber-500 py-0.5 px-1.5 rounded-full font-mono font-medium">سلف قائمة</span>
                        </CardTitle>
                        <div className="p-2 rounded-lg bg-amber-500/10 text-amber-500">
                            <Coins className="h-5 w-5" />
                        </div>
                    </CardHeader>
                    <CardContent className="pt-2">
                        <div className="text-3xl font-black font-mono text-amber-600 dark:text-amber-400">
                            EGP {outstandingAdvancesTotal.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                        </div>
                        <p className="text-xs text-muted-foreground mt-1 flex items-center gap-1">
                            <TrendingUp className="h-3 w-3 text-amber-500" />
                            <span>Total un-settled staff cash custody</span>
                        </p>
                    </CardContent>
                </Card>

                {/* Card 2: Salaries Paid This Month */}
                <Card className="relative overflow-hidden hover:border-primary/20 transition-all duration-200 shadow-md bg-gradient-to-br from-card to-zinc-50/50 dark:to-zinc-950/20">
                    <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                        <CardTitle className="text-sm font-semibold text-muted-foreground flex items-center gap-1.5">
                            Payroll Issued (This Month)
                            <span className="text-[10px] bg-emerald-500/10 text-emerald-500 py-0.5 px-1.5 rounded-full font-mono font-medium">رواتب هذا الشهر</span>
                        </CardTitle>
                        <div className="p-2 rounded-lg bg-emerald-500/10 text-emerald-500">
                            <DollarSign className="h-5 w-5" />
                        </div>
                    </CardHeader>
                    <CardContent className="pt-2">
                        <div className="text-3xl font-black font-mono text-emerald-600 dark:text-emerald-400">
                            EGP {currentMonthSalariesTotal.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                        </div>
                        <p className="text-xs text-muted-foreground mt-1">
                            Net salary transactions processed in {format(new Date(), "MMMM yyyy")}
                        </p>
                    </CardContent>
                </Card>

                {/* Card 3: Active Staff Coverage */}
                <Card className="relative overflow-hidden hover:border-primary/20 transition-all duration-200 shadow-md bg-gradient-to-br from-card to-zinc-50/50 dark:to-zinc-950/20">
                    <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                        <CardTitle className="text-sm font-semibold text-muted-foreground flex items-center gap-1.5">
                            Active Employees
                            <span className="text-[10px] bg-blue-500/10 text-blue-500 py-0.5 px-1.5 rounded-full font-mono font-medium">الموظفون المسجلون</span>
                        </CardTitle>
                        <div className="p-2 rounded-lg bg-blue-500/10 text-blue-500">
                            <UserCheck className="h-5 w-5" />
                        </div>
                    </CardHeader>
                    <CardContent className="pt-2">
                        <div className="text-3xl font-black font-mono text-blue-600 dark:text-blue-400">
                            {totalStaffCount} <span className="text-lg font-normal text-muted-foreground">Staff</span>
                        </div>
                        <p className="text-xs text-muted-foreground mt-1">
                            Full payroll eligible accounts in org directory
                        </p>
                    </CardContent>
                </Card>
            </div>

            {/* Quick Actions & Launch Buttons */}
            <div className="p-5 border border-zinc-200/50 dark:border-zinc-800/50 bg-white/40 dark:bg-black/20 backdrop-blur-md rounded-2xl shadow-sm flex flex-col sm:flex-row items-center justify-between gap-4">
                <div className="space-y-0.5 text-center sm:text-left">
                    <h3 className="font-bold text-lg">Financial Operations Console</h3>
                    <p className="text-xs text-muted-foreground">Log cash advances or process salary payouts for staff in the current register session</p>
                </div>
                <div className="flex items-center gap-3 w-full sm:w-auto justify-center">
                    <Button onClick={handleOpenAdvance} className="flex-1 sm:flex-initial bg-amber-600 hover:bg-amber-700 text-white font-medium">
                        <Plus className="mr-2 h-4 w-4" />
                        Issue Cash Advance (سلفة)
                    </Button>
                    <Button onClick={handleOpenSalary} className="flex-1 sm:flex-initial bg-emerald-600 hover:bg-emerald-700 text-white font-medium">
                        <Plus className="mr-2 h-4 w-4" />
                        Pay Salary (راتب شهري)
                    </Button>
                </div>
            </div>

            {/* Filter controls & Ledger Log Table */}
            <div className="space-y-4">
                <div className="flex flex-col lg:flex-row items-stretch lg:items-center justify-between gap-4 bg-zinc-50/50 dark:bg-zinc-950/20 p-4 border border-zinc-200/40 dark:border-zinc-800/40 rounded-xl">
                    {/* Search and Filters */}
                    <div className="flex items-center gap-2 flex-1">
                        <div className="relative flex-1 max-w-sm">
                            <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                            <Input
                                placeholder="Search ledger by staff or details..."
                                value={searchTerm}
                                onChange={(e) => setSearchTerm(e.target.value)}
                                className="pl-9 text-sm"
                            />
                        </div>
                        
                        <div className="flex items-center gap-2 flex-wrap">
                            {/* Type filter */}
                            <Select value={typeFilter} onValueChange={setTypeFilter}>
                                <SelectTrigger className="w-[140px] text-xs h-9">
                                    <SelectValue placeholder="Entry Type" />
                                </SelectTrigger>
                                <SelectContent>
                                    <SelectItem value="all">All Types</SelectItem>
                                    <SelectItem value="Advance">Advances (سلف)</SelectItem>
                                    <SelectItem value="Salary">Salaries (رواتب)</SelectItem>
                                </SelectContent>
                            </Select>

                            {/* Payment mode filter */}
                            <Select value={paymentFilter} onValueChange={setPaymentFilter}>
                                <SelectTrigger className="w-[140px] text-xs h-9">
                                    <SelectValue placeholder="Payment Mode" />
                                </SelectTrigger>
                                <SelectContent>
                                    <SelectItem value="all">All Methods</SelectItem>
                                    <SelectItem value="cash">Cash (نقدي)</SelectItem>
                                    <SelectItem value="bank">Bank / Visa (فيزا)</SelectItem>
                                </SelectContent>
                            </Select>

                            {/* Employee filter */}
                            <Select value={employeeFilter} onValueChange={setEmployeeFilter}>
                                <SelectTrigger className="w-[180px] text-xs h-9">
                                    <SelectValue placeholder="Filter by Staff" />
                                </SelectTrigger>
                                <SelectContent>
                                    <SelectItem value="all">All Employees</SelectItem>
                                    {employees.map(emp => (
                                        <SelectItem key={emp.id} value={emp.id}>{emp.fullName}</SelectItem>
                                    ))}
                                </SelectContent>
                            </Select>
                        </div>
                    </div>

                    <div className="flex items-center justify-end text-xs text-muted-foreground gap-1.5">
                        <Filter className="h-3 w-3" />
                        <span>Showing {filteredLedger.length} transaction logs</span>
                    </div>
                </div>

                {/* Ledger Log Table */}
                <div className="rounded-xl border bg-card text-card-foreground shadow-sm overflow-hidden">
                    <Table>
                        <TableHeader className="bg-zinc-50/50 dark:bg-zinc-900/50">
                            <TableRow>
                                <TableHead className="w-[110px]">Date</TableHead>
                                <TableHead>Employee</TableHead>
                                <TableHead>Transaction Type</TableHead>
                                <TableHead>Payout Amount</TableHead>
                                <TableHead>Payment Method</TableHead>
                                <TableHead className="max-w-xs md:max-w-md">Notes / Breakdown</TableHead>
                                <TableHead className="w-[80px] text-center">Status</TableHead>
                            </TableRow>
                        </TableHeader>
                        <TableBody>
                            {isLoadingLedger ? (
                                <TableRow>
                                    <TableCell colSpan={7} className="h-32 text-center">
                                        <div className="flex flex-col items-center justify-center gap-2 text-muted-foreground">
                                            <Loader2 className="h-6 w-6 animate-spin text-primary" />
                                            <span>Reading cash ledger entries...</span>
                                        </div>
                                    </TableCell>
                                </TableRow>
                            ) : filteredLedger.length === 0 ? (
                                <TableRow>
                                    <TableCell colSpan={7} className="h-32 text-center text-muted-foreground text-sm font-medium">
                                        No financial entries found matching the filter criteria.
                                    </TableCell>
                                </TableRow>
                            ) : (
                                filteredLedger.map((entry) => (
                                    <TableRow key={entry.id} className="hover:bg-zinc-50/30 dark:hover:bg-zinc-900/10 transition-colors">
                                        {/* Date */}
                                        <TableCell className="font-mono text-xs whitespace-nowrap">
                                            {format(new Date(entry.date), "yyyy-MM-dd HH:mm")}
                                        </TableCell>
                                        
                                        {/* Employee Name */}
                                        <TableCell className="font-semibold text-sm">
                                            {entry.employeeName}
                                        </TableCell>
                                        
                                        {/* Entry Type Badge */}
                                        <TableCell>
                                            {entry.entryType === "Advance" ? (
                                                <Badge variant="outline" className="bg-amber-500/10 text-amber-700 dark:text-amber-400 border-amber-500/20 text-xs font-semibold gap-1">
                                                    <ArrowUpRight className="h-3 w-3" />
                                                    سلفة موظف (Advance)
                                                </Badge>
                                            ) : (
                                                <Badge variant="outline" className="bg-emerald-500/10 text-emerald-700 dark:text-emerald-400 border-emerald-500/20 text-xs font-semibold gap-1">
                                                    <ArrowDownRight className="h-3 w-3" />
                                                    راتب شهري (Salary)
                                                </Badge>
                                            )}
                                        </TableCell>
                                        
                                        {/* Amount */}
                                        <TableCell className="font-mono font-bold text-sm">
                                            EGP {entry.amount.toFixed(2)}
                                        </TableCell>
                                        
                                        {/* Payment method */}
                                        <TableCell>
                                            <div className="flex items-center gap-1.5 text-xs">
                                                <CreditCard className="h-3.5 w-3.5 text-muted-foreground" />
                                                <span>{entry.isBank ? "بنك / فيزا" : "نقدي (Cash)"}</span>
                                            </div>
                                        </TableCell>
                                        
                                        {/* Description */}
                                        <TableCell className="max-w-xs md:max-w-md truncate text-xs text-muted-foreground" title={entry.description || ""}>
                                            {entry.description || "—"}
                                        </TableCell>
                                        
                                        {/* Status */}
                                        <TableCell className="text-center">
                                            <Badge variant="outline" className="bg-green-500/15 text-green-700 dark:text-green-300 border-green-500/20 text-[10px]">
                                                Approved
                                            </Badge>
                                        </TableCell>
                                    </TableRow>
                                ))
                            )}
                        </TableBody>
                    </Table>
                </div>
            </div>

            {/* ── DIALOG 1: ISSUE CASH ADVANCE (صرف سلفة) ────────────────────── */}
            <Dialog open={isAdvanceDialogOpen} onOpenChange={setIsAdvanceDialogOpen}>
                <DialogContent className="max-w-md">
                    <DialogHeader>
                        <DialogTitle className="flex items-center gap-2">
                            <Coins className="h-5 w-5 text-amber-500" />
                            Issue Cash Advance / صرف سلفة موظف
                        </DialogTitle>
                        <DialogDescription>
                            Deductible cash advance allocated directly to the employee's payroll file.
                        </DialogDescription>
                    </DialogHeader>
                    
                    <form onSubmit={handleIssueAdvanceSubmit} className="space-y-4 pt-2">
                        {/* Employee Select */}
                        <div className="space-y-1.5">
                            <Label htmlFor="adv-emp">Select Employee / اسم الموظف</Label>
                            <Select value={advEmployeeId} onValueChange={setAdvEmployeeId}>
                                <SelectTrigger id="adv-emp">
                                    <SelectValue placeholder="Select Staff Member" />
                                </SelectTrigger>
                                <SelectContent>
                                    {employees.map(emp => (
                                        <SelectItem key={emp.id} value={emp.id}>
                                            {emp.fullName} ({emp.employeeCode})
                                        </SelectItem>
                                    ))}
                                </SelectContent>
                            </Select>
                            {advEmployeeId && (
                                <div className="text-[11px] text-amber-600 dark:text-amber-400 font-medium">
                                    Current outstanding advances: EGP {getOutstandingAdvanceForEmployee(employees.find(e => e.id === advEmployeeId)?.fullName || "").toFixed(2)}
                                </div>
                            )}
                        </div>

                        {/* Amount */}
                        <div className="space-y-1.5">
                            <Label htmlFor="adv-amt">Advance Amount / قيمة السلفة</Label>
                            <div className="relative">
                                <span className="absolute left-3 top-1/2 -translate-y-1/2 text-xs text-muted-foreground font-mono">EGP</span>
                                <Input
                                    id="adv-amt"
                                    type="number"
                                    min="1"
                                    step="0.01"
                                    placeholder="Enter amount (e.g. 500)"
                                    className="pl-12 font-mono"
                                    value={advAmount}
                                    onChange={(e) => setAdvAmount(e.target.value)}
                                    required
                                />
                            </div>
                        </div>

                        {/* Payment Type */}
                        <div className="space-y-1.5">
                            <Label htmlFor="adv-pay-mode">Payment Source / طريقة الدفع</Label>
                            <Select value={advIsBank} onValueChange={setAdvIsBank}>
                                <SelectTrigger id="adv-pay-mode">
                                    <SelectValue />
                                </SelectTrigger>
                                <SelectContent>
                                    <SelectItem value="false">Cash Register (صندوق الخزينة نقدي)</SelectItem>
                                    <SelectItem value="true">Bank Transfer / Card (بنكي / فيزا)</SelectItem>
                                </SelectContent>
                            </Select>
                        </div>

                        {/* Date */}
                        <div className="space-y-1.5">
                            <Label htmlFor="adv-date">Transaction Date / التاريخ</Label>
                            <Input
                                id="adv-date"
                                type="date"
                                value={advDate}
                                onChange={(e) => setAdvDate(e.target.value)}
                                required
                            />
                        </div>

                        {/* Description */}
                        <div className="space-y-1.5">
                            <Label htmlFor="adv-desc">Description / Notes / ملاحظات</Label>
                            <Textarea
                                id="adv-desc"
                                placeholder="E.g. Medical emergency advance, deduction scheduled for next month..."
                                value={advDescription}
                                onChange={(e) => setAdvDescription(e.target.value)}
                                className="h-20 text-sm"
                            />
                        </div>

                        <DialogFooter className="pt-2">
                            <Button type="button" variant="outline" onClick={() => setIsAdvanceDialogOpen(false)}>
                                Cancel
                            </Button>
                            <Button type="submit" disabled={isSubmittingAdvance} className="bg-amber-600 hover:bg-amber-700 text-white">
                                {isSubmittingAdvance ? (
                                    <>
                                        <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                                        Processing...
                                    </>
                                ) : "Approve & Issue Payout"}
                            </Button>
                        </DialogFooter>
                    </form>
                </DialogContent>
            </Dialog>

            {/* ── DIALOG 2: PROCESS SALARY PAYOUT (صرف راتب شهري) ────────────────────── */}
            <Dialog open={isSalaryDialogOpen} onOpenChange={setIsSalaryDialogOpen}>
                <DialogContent className="max-w-md">
                    <DialogHeader>
                        <DialogTitle className="flex items-center gap-2">
                            <Briefcase className="h-5 w-5 text-emerald-500" />
                            Pay Monthly Salary / صرف راتب شهري
                        </DialogTitle>
                        <DialogDescription>
                            Calculate salary breakdown and settle outstanding advances dynamically.
                        </DialogDescription>
                    </DialogHeader>
                    
                    <form onSubmit={handlePaySalarySubmit} className="space-y-4 pt-2">
                        {/* Employee Select */}
                        <div className="space-y-1.5">
                            <Label htmlFor="sal-emp">Select Employee / اسم الموظف</Label>
                            <Select value={salEmployeeId} onValueChange={handleSalaryEmployeeChange}>
                                <SelectTrigger id="sal-emp">
                                    <SelectValue placeholder="Select Staff Member" />
                                </SelectTrigger>
                                <SelectContent>
                                    {employees.map(emp => (
                                        <SelectItem key={emp.id} value={emp.id}>
                                            {emp.fullName} ({emp.employeeCode})
                                        </SelectItem>
                                    ))}
                                </SelectContent>
                            </Select>
                        </div>

                        {/* Month / Year */}
                        <div className="grid grid-cols-2 gap-3">
                            <div className="space-y-1.5">
                                <Label htmlFor="sal-m">Payout Month / الشهر</Label>
                                <Select value={salMonth} onValueChange={setSalMonth}>
                                    <SelectTrigger id="sal-m">
                                        <SelectValue />
                                    </SelectTrigger>
                                    <SelectContent>
                                        {Array.from({ length: 12 }, (_, i) => String(i + 1)).map(m => (
                                            <SelectItem key={m} value={m}>
                                                {format(new Date(2026, parseInt(m) - 1, 1), "MMMM")} ({m})
                                            </SelectItem>
                                        ))}
                                    </SelectContent>
                                </Select>
                            </div>
                            <div className="space-y-1.5">
                                <Label htmlFor="sal-y">Payout Year / السنة</Label>
                                <Select value={salYear} onValueChange={setSalYear}>
                                    <SelectTrigger id="sal-y">
                                        <SelectValue />
                                    </SelectTrigger>
                                    <SelectContent>
                                        <SelectItem value="2026">2026</SelectItem>
                                        <SelectItem value="2025">2025</SelectItem>
                                    </SelectContent>
                                </Select>
                            </div>
                        </div>

                        {/* Gross Salary */}
                        <div className="grid grid-cols-2 gap-3">
                            <div className="space-y-1.5">
                                <Label htmlFor="sal-gross">Basic Gross Salary / الأساسي</Label>
                                <Input
                                    id="sal-gross"
                                    type="number"
                                    placeholder="Salary"
                                    value={salGross}
                                    onChange={(e) => setSalGross(e.target.value)}
                                    className="font-mono text-sm"
                                    required
                                />
                            </div>

                            {/* Bonus */}
                            <div className="space-y-1.5">
                                <Label htmlFor="sal-bonus">Bonuses / الحوافز والمكافآت</Label>
                                <Input
                                    id="sal-bonus"
                                    type="number"
                                    placeholder="0"
                                    value={salBonus}
                                    onChange={(e) => setSalBonus(e.target.value)}
                                    className="font-mono text-sm"
                                />
                            </div>
                        </div>

                        {/* Deduction / Advance Settlement */}
                        <div className="space-y-1.5 p-3 border border-dashed rounded-lg bg-zinc-50 dark:bg-zinc-950/20">
                            <div className="flex justify-between items-center mb-1">
                                <Label htmlFor="sal-deduct" className="text-xs font-semibold text-amber-600 dark:text-amber-400">
                                    Advance Settlement / استقطاع السلفة
                                </Label>
                                {salEmployeeId && (
                                    <span className="text-[10px] text-muted-foreground font-mono">
                                        Max outstanding: EGP {getOutstandingAdvanceForEmployee(employees.find(e => e.id === salEmployeeId)?.fullName || "").toFixed(2)}
                                    </span>
                                )}
                            </div>
                            <Input
                                id="sal-deduct"
                                type="number"
                                placeholder="0"
                                max={salEmployeeId ? getOutstandingAdvanceForEmployee(employees.find(e => e.id === salEmployeeId)?.fullName || "") : undefined}
                                value={salDeduction}
                                onChange={(e) => setSalDeduction(e.target.value)}
                                className="font-mono text-sm"
                            />
                            <p className="text-[10px] text-muted-foreground mt-1">
                                Settle outstanding advances from this payout. Fully or partially.
                            </p>
                        </div>

                        {/* Calculations Breakdown */}
                        <div className="p-3 border rounded-lg bg-zinc-900 text-zinc-100 font-mono text-xs space-y-1">
                            <div className="flex justify-between text-zinc-400">
                                <span>Gross Salary (+)</span>
                                <span>EGP {(parseFloat(salGross) || 0).toFixed(2)}</span>
                            </div>
                            <div className="flex justify-between text-zinc-400">
                                <span>Bonuses/Additions (+)</span>
                                <span>EGP {(parseFloat(salBonus) || 0).toFixed(2)}</span>
                            </div>
                            <div className="flex justify-between text-amber-400">
                                <span>Advance Settlement (-)</span>
                                <span>EGP {(parseFloat(salDeduction) || 0).toFixed(2)}</span>
                            </div>
                            <div className="border-t border-zinc-800 my-1 pt-1 flex justify-between font-bold text-sm text-emerald-400">
                                <span>Net Payout (=)</span>
                                <span>EGP {( (parseFloat(salGross) || 0) + (parseFloat(salBonus) || 0) - (parseFloat(salDeduction) || 0) ).toFixed(2)}</span>
                            </div>
                        </div>

                        {/* Payment Type */}
                        <div className="space-y-1.5">
                            <Label htmlFor="sal-pay-mode">Payment Source / طريقة الدفع</Label>
                            <Select value={salIsBank} onValueChange={setSalIsBank}>
                                <SelectTrigger id="sal-pay-mode">
                                    <SelectValue />
                                </SelectTrigger>
                                <SelectContent>
                                    <SelectItem value="true">Bank Transfer / Card (تحويل بنكي / فيزا)</SelectItem>
                                    <SelectItem value="false">Cash Register (نقدي من الخزينة)</SelectItem>
                                </SelectContent>
                            </Select>
                        </div>

                        {/* Date */}
                        <div className="space-y-1.5">
                            <Label htmlFor="sal-date">Transaction Date / التاريخ</Label>
                            <Input
                                id="sal-date"
                                type="date"
                                value={salDate}
                                onChange={(e) => setSalDate(e.target.value)}
                                required
                            />
                        </div>

                        {/* Description */}
                        <div className="space-y-1.5">
                            <Label htmlFor="sal-desc">Description / Notes / ملاحظات</Label>
                            <Textarea
                                id="sal-desc"
                                placeholder="E.g. Regular monthly payout with advance settlement..."
                                value={salDescription}
                                onChange={(e) => setSalDescription(e.target.value)}
                                className="h-16 text-sm"
                            />
                        </div>

                        <DialogFooter className="pt-2">
                            <Button type="button" variant="outline" onClick={() => setIsSalaryDialogOpen(false)}>
                                Cancel
                            </Button>
                            <Button type="submit" disabled={isSubmittingSalary} className="bg-emerald-600 hover:bg-emerald-700 text-white">
                                {isSubmittingSalary ? (
                                    <>
                                        <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                                        Processing...
                                    </>
                                ) : "Process Salary Payout"}
                            </Button>
                        </DialogFooter>
                    </form>
                </DialogContent>
            </Dialog>
        </div>
    )
}
