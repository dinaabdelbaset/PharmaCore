"use client"

import { useState, useEffect, useMemo } from "react"
import { 
    Plus, 
    Minus, 
    TrendingUp, 
    TrendingDown, 
    Search, 
    Loader2, 
    Landmark, 
    Wallet, 
    Building2, 
    Calendar, 
    RefreshCw, 
    FileText, 
    ArrowUpDown, 
    DollarSign,
    SlidersHorizontal,
    CheckCircle2
} from "lucide-react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogDescription,
    DialogFooter,
} from "@/components/ui/dialog"
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import { 
    XAxis, 
    YAxis, 
    CartesianGrid, 
    Tooltip, 
    ResponsiveContainer, 
    AreaChart, 
    Area,
    Legend
} from "recharts"
import { TreasuryEntry, TreasuryBalances } from "@/types/treasury"
import { Branch } from "@/types/branch"
import apiClient from "@/lib/api-client"
import { toast } from "sonner"

export default function TreasuryPage() {
    // Balances & entries state
    const [balances, setBalances] = useState<TreasuryBalances | null>(null)
    const [entries, setEntries] = useState<TreasuryEntry[]>([])
    const [branches, setBranches] = useState<Branch[]>([])
    
    // Loading states
    const [isLoadingBalances, setIsLoadingBalances] = useState(true)
    const [isLoadingEntries, setIsLoadingEntries] = useState(true)
    const [isLoadingBranches, setIsLoadingBranches] = useState(true)
    const [isSaving, setIsSaving] = useState(false)

    // Filters state
    const [searchTerm, setSearchTerm] = useState("")
    const [branchFilter, setBranchFilter] = useState("all")
    const [paymentMethodFilter, setPaymentMethodFilter] = useState("all")
    const [entryTypeFilter, setEntryTypeFilter] = useState("all")
    const [fromDateFilter, setFromDateFilter] = useState("")
    const [toDateFilter, setToDateFilter] = useState("")
    
    // Pagination state
    const [currentPage, setCurrentPage] = useState(1)
    const [totalEntries, setTotalEntries] = useState(0)
    const pageSize = 10

    // Dialog state for Recording Deposit/Withdrawal
    const [isDialogOpen, setIsDialogOpen] = useState(false)
    const [dialogType, setDialogType] = useState<"deposit" | "withdrawal">("deposit")
    const [dialogAmount, setDialogAmount] = useState("")
    const [dialogIsBank, setDialogIsBank] = useState(false)
    const [dialogBranchId, setDialogBranchId] = useState("")
    const [dialogDescription, setDialogDescription] = useState("")
    const [dialogDate, setDialogDate] = useState("")
    const [dialogEntryType, setDialogEntryType] = useState("Deposit")

    // Fetch branches for filter and dropdown list
    const fetchBranches = async () => {
        setIsLoadingBranches(true)
        try {
            const response = await apiClient.get<Branch[]>("/branches")
            if (Array.isArray(response.data)) {
                setBranches(response.data)
            }
        } catch (error: any) {
            console.error("Failed to fetch branches", error)
        } finally {
            setIsLoadingBranches(false)
        }
    }

    // Fetch balances and trend
    const fetchBalances = async () => {
        setIsLoadingBalances(true)
        try {
            const queryParams = new URLSearchParams()
            if (branchFilter !== "all") {
                queryParams.append("branchId", branchFilter)
            }
            const response = await apiClient.get<TreasuryBalances>(`/treasury/balances?${queryParams.toString()}`)
            if (response.data) {
                setBalances(response.data)
            }
        } catch (error: any) {
            console.error("Failed to fetch balances", error)
            toast.error("Failed to fetch treasury metrics")
        } finally {
            setIsLoadingBalances(false)
        }
    }

    // Fetch entries with pagination and filters
    const fetchEntries = async () => {
        setIsLoadingEntries(true)
        try {
            const queryParams = new URLSearchParams()
            queryParams.append("page", currentPage.toString())
            queryParams.append("pageSize", pageSize.toString())

            if (searchTerm.trim()) {
                queryParams.append("searchTerm", searchTerm)
            }
            if (branchFilter !== "all") {
                queryParams.append("branchId", branchFilter)
            }
            if (paymentMethodFilter !== "all") {
                queryParams.append("paymentMethod", paymentMethodFilter)
            }
            if (entryTypeFilter !== "all") {
                queryParams.append("type", entryTypeFilter)
            }
            if (fromDateFilter) {
                queryParams.append("fromDate", new Date(fromDateFilter).toISOString())
            }
            if (toDateFilter) {
                queryParams.append("toDate", new Date(toDateFilter).toISOString())
            }

            const response = await apiClient.get<{ items: TreasuryEntry[], total: number }>(
                `/treasury/entries?${queryParams.toString()}`
            )
            if (response.data) {
                setEntries(response.data.items || [])
                setTotalEntries(response.data.total || 0)
            }
        } catch (error: any) {
            console.error("Failed to fetch entries", error)
            toast.error("Failed to load cash ledger entries")
        } finally {
            setIsLoadingEntries(false)
        }
    }

    // Initialize & Fetch on filters/pagination change
    useEffect(() => {
        fetchBranches()
    }, [])

    useEffect(() => {
        fetchBalances()
    }, [branchFilter])

    useEffect(() => {
        fetchEntries()
    }, [currentPage, branchFilter, paymentMethodFilter, entryTypeFilter, fromDateFilter, toDateFilter])

    // Trigger searches on button click or Enter key
    const handleSearchTrigger = (e: React.FormEvent) => {
        e.preventDefault()
        setCurrentPage(1)
        fetchEntries()
    }

    // Reset filters
    const handleResetFilters = () => {
        setSearchTerm("")
        setBranchFilter("all")
        setPaymentMethodFilter("all")
        setEntryTypeFilter("all")
        setFromDateFilter("")
        setToDateFilter("")
        setCurrentPage(1)
    }

    // Open transaction logger
    const handleOpenDialog = (type: "deposit" | "withdrawal") => {
        setDialogType(type)
        setDialogAmount("")
        setDialogIsBank(false)
        setDialogBranchId(branches[0]?.id || "")
        setDialogDescription("")
        setDialogDate(new Date().toISOString().substring(0, 16)) // Format: YYYY-MM-DDTHH:mm
        setDialogEntryType(type === "deposit" ? "Deposit" : "Withdrawal")
        setIsDialogOpen(true)
    }

    // Handle form submit for deposit/withdrawal
    const handleSaveTransaction = async (e: React.FormEvent) => {
        e.preventDefault()
        const amountNum = parseFloat(dialogAmount)
        if (isNaN(amountNum) || amountNum <= 0) {
            toast.error("Please enter a valid amount greater than zero")
            return
        }

        setIsSaving(true)
        try {
            const requestPayload = {
                amount: amountNum,
                isBank: dialogIsBank,
                entryType: dialogEntryType,
                description: dialogDescription,
                branchId: dialogBranchId || null,
                date: dialogDate ? new Date(dialogDate).toISOString() : new Date().toISOString()
            }

            await apiClient.post("/treasury/entries", requestPayload)
            toast.success(`${dialogType === "deposit" ? "Deposit" : "Withdrawal"} logged successfully`)
            setIsDialogOpen(false)
            
            // Reload live data
            fetchBalances()
            fetchEntries()
        } catch (error: any) {
            console.error("Failed to save transaction", error)
            const errorMsg = error.response?.data || error.message || "Failed to record transaction"
            toast.error(typeof errorMsg === "string" ? errorMsg : errorMsg.message || "Failed to record transaction")
        } finally {
            setIsSaving(false)
        }
    }

    // Total Liquidity calculation
    const totalLiquidity = useMemo(() => {
        if (!balances) return 0
        return (balances.totalCash || 0) + (balances.totalBank || 0)
    }, [balances])

    return (
        <div className="space-y-8 p-1 md:p-6 transition-all duration-300">
            {/* Header section with HSL gradient accent */}
            <div className="flex flex-col md:flex-row md:items-center justify-between gap-4 border-b border-zinc-200/50 dark:border-zinc-800/50 pb-6">
                <div>
                    <h1 className="text-3xl font-extrabold tracking-tight bg-gradient-to-r from-emerald-500 via-teal-500 to-cyan-500 bg-clip-text text-transparent">
                        Treasury & Cash Ledger
                    </h1>
                    <p className="text-muted-foreground mt-1">
                        Monitor live balances, track cash book movements, and log new deposits or withdrawals.
                    </p>
                </div>
                <div className="flex items-center gap-3">
                    <Button 
                        onClick={() => handleOpenDialog("deposit")} 
                        className="bg-emerald-600 hover:bg-emerald-700 text-white font-medium shadow-lg shadow-emerald-600/20"
                    >
                        <Plus className="mr-2 h-4 w-4" />
                        Log Deposit
                    </Button>
                    <Button 
                        onClick={() => handleOpenDialog("withdrawal")} 
                        variant="destructive"
                        className="bg-rose-600 hover:bg-rose-700 text-white font-medium shadow-lg shadow-rose-600/20"
                    >
                        <Minus className="mr-2 h-4 w-4" />
                        Log Withdrawal
                    </Button>
                    <Button 
                        variant="outline" 
                        size="icon" 
                        onClick={() => { fetchBalances(); fetchEntries(); }} 
                        title="Reload Data"
                        className="border-zinc-200 dark:border-zinc-800"
                    >
                        <RefreshCw className="h-4 w-4" />
                    </Button>
                </div>
            </div>

            {/* Telemetry Glassmorphism Cards Section */}
            <div className="grid gap-6 md:grid-cols-3">
                {/* Cash Box Card */}
                <Card className="relative overflow-hidden border border-emerald-500/20 dark:border-emerald-500/10 bg-gradient-to-br from-white/30 via-emerald-50/5 to-white/10 dark:from-zinc-950/40 dark:via-emerald-950/5 dark:to-zinc-950/20 backdrop-blur-lg shadow-xl shadow-emerald-500/5">
                    <div className="absolute top-0 right-0 w-32 h-32 bg-emerald-500/10 rounded-full blur-3xl -mr-8 -mt-8" />
                    <CardHeader className="flex flex-row items-center justify-between pb-2">
                        <div className="space-y-0.5">
                            <CardTitle className="text-sm font-semibold tracking-wide text-emerald-600 dark:text-emerald-400 uppercase">
                                Cash Box (Safe)
                            </CardTitle>
                            <CardDescription className="text-xs">Physical branch cash balances</CardDescription>
                        </div>
                        <div className="p-3 bg-emerald-500/15 text-emerald-600 dark:text-emerald-400 rounded-2xl">
                            <Wallet className="h-6 w-6" />
                        </div>
                    </CardHeader>
                    <CardContent className="pt-2">
                        {isLoadingBalances ? (
                            <div className="h-10 flex items-center">
                                <Loader2 className="h-5 w-5 animate-spin text-emerald-500" />
                            </div>
                        ) : (
                            <div className="text-3xl font-extrabold tracking-tight text-emerald-700 dark:text-emerald-300">
                                ${balances?.totalCash?.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 }) || "0.00"}
                            </div>
                        )}
                        <div className="flex items-center gap-1 mt-2 text-xs text-emerald-600/80 dark:text-emerald-400/80">
                            <TrendingUp className="h-3 w-3" />
                            <span>Safely secured liquid cash</span>
                        </div>
                    </CardContent>
                </Card>

                {/* Bank Account Card */}
                <Card className="relative overflow-hidden border border-blue-500/20 dark:border-blue-500/10 bg-gradient-to-br from-white/30 via-blue-50/5 to-white/10 dark:from-zinc-950/40 dark:via-blue-950/5 dark:to-zinc-950/20 backdrop-blur-lg shadow-xl shadow-blue-500/5">
                    <div className="absolute top-0 right-0 w-32 h-32 bg-blue-500/10 rounded-full blur-3xl -mr-8 -mt-8" />
                    <CardHeader className="flex flex-row items-center justify-between pb-2">
                        <div className="space-y-0.5">
                            <CardTitle className="text-sm font-semibold tracking-wide text-blue-600 dark:text-blue-400 uppercase">
                                Bank Accounts
                            </CardTitle>
                            <CardDescription className="text-xs">Digital, Visa & Wire settlements</CardDescription>
                        </div>
                        <div className="p-3 bg-blue-500/15 text-blue-600 dark:text-blue-400 rounded-2xl">
                            <Landmark className="h-6 w-6" />
                        </div>
                    </CardHeader>
                    <CardContent className="pt-2">
                        {isLoadingBalances ? (
                            <div className="h-10 flex items-center">
                                <Loader2 className="h-5 w-5 animate-spin text-blue-500" />
                            </div>
                        ) : (
                            <div className="text-3xl font-extrabold tracking-tight text-blue-700 dark:text-blue-300">
                                ${balances?.totalBank?.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 }) || "0.00"}
                            </div>
                        )}
                        <div className="flex items-center gap-1 mt-2 text-xs text-blue-600/80 dark:text-blue-400/80">
                            <CheckCircle2 className="h-3 w-3" />
                            <span>Reconciled online assets</span>
                        </div>
                    </CardContent>
                </Card>

                {/* Total Liquidity Card */}
                <Card className="relative overflow-hidden border border-purple-500/20 dark:border-purple-500/10 bg-gradient-to-br from-white/30 via-purple-50/5 to-white/10 dark:from-zinc-950/40 dark:via-purple-950/5 dark:to-zinc-950/20 backdrop-blur-lg shadow-xl shadow-purple-500/5">
                    <div className="absolute top-0 right-0 w-32 h-32 bg-purple-500/10 rounded-full blur-3xl -mr-8 -mt-8" />
                    <CardHeader className="flex flex-row items-center justify-between pb-2">
                        <div className="space-y-0.5">
                            <CardTitle className="text-sm font-semibold tracking-wide text-purple-600 dark:text-purple-400 uppercase">
                                Consolidated Balance
                            </CardTitle>
                            <CardDescription className="text-xs">Total cash & bank liquidity</CardDescription>
                        </div>
                        <div className="p-3 bg-purple-500/15 text-purple-600 dark:text-purple-400 rounded-2xl">
                            <DollarSign className="h-6 w-6" />
                        </div>
                    </CardHeader>
                    <CardContent className="pt-2">
                        {isLoadingBalances ? (
                            <div className="h-10 flex items-center">
                                <Loader2 className="h-5 w-5 animate-spin text-purple-500" />
                            </div>
                        ) : (
                            <div className="text-3xl font-extrabold tracking-tight text-purple-700 dark:text-purple-300">
                                ${totalLiquidity.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                            </div>
                        )}
                        <div className="flex items-center gap-1 mt-2 text-xs text-purple-600/80 dark:text-purple-400/80">
                            <TrendingUp className="h-3 w-3" />
                            <span>Consolidated asset pool</span>
                        </div>
                    </CardContent>
                </Card>
            </div>

            {/* Line Charts of Net Cash Flow */}
            <Card className="border border-zinc-200/60 dark:border-zinc-800/60 shadow-lg bg-gradient-to-br from-card to-background">
                <CardHeader className="flex flex-col md:flex-row md:items-center justify-between pb-4">
                    <div className="space-y-1">
                        <CardTitle className="text-lg md:text-xl font-bold flex items-center gap-2">
                            <TrendingUp className="h-5 w-5 text-emerald-500" />
                            Net Cash Flow Analytics
                        </CardTitle>
                        <CardDescription className="text-xs md:text-sm">
                            30-day view of deposits, withdrawals, sales, and total net flow
                        </CardDescription>
                    </div>
                    {balances?.netFlowChart && balances.netFlowChart.length > 0 && (
                        <div className="flex items-center gap-2 mt-2 md:mt-0 text-sm font-semibold text-emerald-500 bg-emerald-500/10 px-3 py-1 rounded-full w-fit">
                            <TrendingUp className="h-4 w-4" />
                            <span>Live Net Trends</span>
                        </div>
                    )}
                </CardHeader>
                <CardContent className="pl-0 pr-4">
                    <div className="h-[320px] w-full">
                        {isLoadingBalances ? (
                            <div className="h-full flex items-center justify-center text-muted-foreground text-sm">
                                <Loader2 className="h-8 w-8 animate-spin text-primary mr-2" />
                                Loading cash flow telemetry...
                            </div>
                        ) : !balances?.netFlowChart || balances.netFlowChart.length === 0 ? (
                            <div className="h-full flex items-center justify-center text-muted-foreground text-sm">
                                No ledger transactions in the last 30 days to build flow charts.
                            </div>
                        ) : (
                            <ResponsiveContainer width="100%" height="100%">
                                <AreaChart data={balances.netFlowChart} margin={{ top: 10, right: 10, left: 10, bottom: 0 }}>
                                    <defs>
                                        <linearGradient id="netFlowGrad" x1="0" y1="0" x2="0" y2="1">
                                            <stop offset="5%" stopColor="hsl(var(--primary))" stopOpacity={0.3} />
                                            <stop offset="95%" stopColor="hsl(var(--primary))" stopOpacity={0.0} />
                                        </linearGradient>
                                        <linearGradient id="cashFlowGrad" x1="0" y1="0" x2="0" y2="1">
                                            <stop offset="5%" stopColor="#10b981" stopOpacity={0.2} />
                                            <stop offset="95%" stopColor="#10b981" stopOpacity={0.0} />
                                        </linearGradient>
                                        <linearGradient id="bankFlowGrad" x1="0" y1="0" x2="0" y2="1">
                                            <stop offset="5%" stopColor="#3b82f6" stopOpacity={0.2} />
                                            <stop offset="95%" stopColor="#3b82f6" stopOpacity={0.0} />
                                        </linearGradient>
                                    </defs>
                                    <XAxis
                                        dataKey="date"
                                        stroke="#888888"
                                        fontSize={11}
                                        tickLine={false}
                                        axisLine={false}
                                        tickFormatter={(str) => {
                                            try {
                                                const d = new Date(str);
                                                return d.toLocaleDateString("en-US", { month: "short", day: "numeric" });
                                            } catch {
                                                return str;
                                            }
                                        }}
                                    />
                                    <YAxis
                                        stroke="#888888"
                                        fontSize={11}
                                        tickLine={false}
                                        axisLine={false}
                                        tickFormatter={(val) => `$${val.toLocaleString()}`}
                                    />
                                    <CartesianGrid strokeDasharray="3 3" vertical={false} className="stroke-zinc-200 dark:stroke-zinc-800" />
                                    <Tooltip
                                        formatter={(val: any) => [`$${parseFloat(val).toLocaleString(undefined, { minimumFractionDigits: 2 })}`]}
                                        contentStyle={{
                                            backgroundColor: 'hsl(var(--popover))',
                                            borderColor: 'hsl(var(--border))',
                                            borderRadius: 'var(--radius)',
                                            color: 'hsl(var(--popover-foreground))',
                                            boxShadow: '0 10px 15px -3px rgba(0, 0, 0, 0.1)'
                                        }}
                                    />
                                    <Legend verticalAlign="top" height={36} iconType="circle" />
                                    <Area
                                        type="monotone"
                                        dataKey="netFlow"
                                        name="Total Net Flow"
                                        stroke="hsl(var(--primary))"
                                        fillOpacity={1}
                                        fill="url(#netFlowGrad)"
                                        strokeWidth={2.5}
                                    />
                                    <Area
                                        type="monotone"
                                        dataKey="cashFlow"
                                        name="Cash Box Flow"
                                        stroke="#10b981"
                                        fillOpacity={1}
                                        fill="url(#cashFlowGrad)"
                                        strokeWidth={1.5}
                                    />
                                    <Area
                                        type="monotone"
                                        dataKey="bankFlow"
                                        name="Bank Flow"
                                        stroke="#3b82f6"
                                        fillOpacity={1}
                                        fill="url(#bankFlowGrad)"
                                        strokeWidth={1.5}
                                    />
                                </AreaChart>
                            </ResponsiveContainer>
                        )}
                    </div>
                </CardContent>
            </Card>

            {/* Ledger Movements, Search & Filter Controls */}
            <Card className="border border-zinc-200/60 dark:border-zinc-800/60 shadow-lg">
                <CardHeader>
                    <CardTitle className="text-xl font-bold flex items-center gap-2">
                        <FileText className="h-5 w-5 text-primary" />
                        Cash Ledger Movements
                    </CardTitle>
                    <CardDescription>
                        Historical journal entries of cash book and bank entries
                    </CardDescription>
                </CardHeader>
                <CardContent className="space-y-6">
                    {/* Search & Filter Form */}
                    <form onSubmit={handleSearchTrigger} className="space-y-4">
                        <div className="grid gap-4 sm:grid-cols-2 md:grid-cols-4 lg:grid-cols-6">
                            {/* Search Query */}
                            <div className="relative col-span-1 sm:col-span-2">
                                <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                                <Input
                                    placeholder="Search description, employee..."
                                    value={searchTerm}
                                    onChange={(e) => setSearchTerm(e.target.value)}
                                    className="pl-10"
                                />
                            </div>

                            {/* Branch Filter */}
                            <div>
                                <Select value={branchFilter} onValueChange={(val) => { setBranchFilter(val); setCurrentPage(1); }}>
                                    <SelectTrigger className="w-full">
                                        <div className="flex items-center gap-2">
                                            <Building2 className="h-4 w-4 text-muted-foreground shrink-0" />
                                            <span className="truncate">
                                                {branchFilter === "all" ? "All Branches" : branches.find(b => b.id === branchFilter)?.name || "Branch"}
                                            </span>
                                        </div>
                                    </SelectTrigger>
                                    <SelectContent>
                                        <SelectItem value="all">All Branches</SelectItem>
                                        {isLoadingBranches ? (
                                            <SelectItem value="loading" disabled>Loading...</SelectItem>
                                        ) : (
                                            branches.map((b) => (
                                                <SelectItem key={b.id} value={b.id}>{b.name}</SelectItem>
                                            ))
                                        )}
                                    </SelectContent>
                                </Select>
                            </div>

                            {/* Type Filter */}
                            <div>
                                <Select value={entryTypeFilter} onValueChange={(val) => { setEntryTypeFilter(val); setCurrentPage(1); }}>
                                    <SelectTrigger>
                                        <SelectValue placeholder="All Movement Types" />
                                    </SelectTrigger>
                                    <SelectContent>
                                        <SelectItem value="all">All Movement Types</SelectItem>
                                        <SelectItem value="Deposit">Deposits</SelectItem>
                                        <SelectItem value="Withdrawal">Withdrawals</SelectItem>
                                        <SelectItem value="عمليات مبيعات">Sales Operations</SelectItem>
                                        <SelectItem value="مبيعات آجل">Deferred Sales</SelectItem>
                                        <SelectItem value="سحب من راتب موظف">Salary Deduct</SelectItem>
                                        <SelectItem value="دفع قيمة من آجل">Paid Deferred</SelectItem>
                                    </SelectContent>
                                </Select>
                            </div>

                            {/* Payment Method Filter */}
                            <div>
                                <Select value={paymentMethodFilter} onValueChange={(val) => { setPaymentMethodFilter(val); setCurrentPage(1); }}>
                                    <SelectTrigger>
                                        <SelectValue placeholder="All Assets" />
                                    </SelectTrigger>
                                    <SelectContent>
                                        <SelectItem value="all">All Assets</SelectItem>
                                        <SelectItem value="cash">Cash Only</SelectItem>
                                        <SelectItem value="bank">Bank Only</SelectItem>
                                    </SelectContent>
                                </Select>
                            </div>

                            {/* Action Buttons inside filter row */}
                            <div className="flex gap-2">
                                <Button type="submit" variant="secondary" className="flex-1">
                                    Apply
                                </Button>
                                <Button 
                                    type="button" 
                                    variant="ghost" 
                                    onClick={handleResetFilters}
                                    title="Reset Filters"
                                    className="px-2"
                                >
                                    <SlidersHorizontal className="h-4 w-4" />
                                </Button>
                            </div>
                        </div>

                        {/* Date Filters Row */}
                        <div className="flex flex-wrap items-center gap-4 text-sm border-t border-zinc-100 dark:border-zinc-800 pt-3">
                            <div className="flex items-center gap-2">
                                <Calendar className="h-4 w-4 text-muted-foreground" />
                                <span className="text-muted-foreground text-xs font-medium">From:</span>
                                <Input
                                    type="date"
                                    value={fromDateFilter}
                                    onChange={(e) => { setFromDateFilter(e.target.value); setCurrentPage(1); }}
                                    className="h-8 w-[140px] text-xs py-0.5"
                                />
                            </div>
                            <div className="flex items-center gap-2">
                                <Calendar className="h-4 w-4 text-muted-foreground" />
                                <span className="text-muted-foreground text-xs font-medium">To:</span>
                                <Input
                                    type="date"
                                    value={toDateFilter}
                                    onChange={(e) => { setToDateFilter(e.target.value); setCurrentPage(1); }}
                                    className="h-8 w-[140px] text-xs py-0.5"
                                />
                            </div>
                        </div>
                    </form>

                    {/* Table of movements */}
                    {isLoadingEntries ? (
                        <div className="h-64 flex flex-col items-center justify-center gap-2 text-muted-foreground">
                            <Loader2 className="h-8 w-8 animate-spin text-primary" />
                            <span>Retrieving ledger transactions...</span>
                        </div>
                    ) : entries.length === 0 ? (
                        <div className="h-48 flex flex-col items-center justify-center gap-1 border border-dashed rounded-lg border-zinc-200 dark:border-zinc-800 text-muted-foreground text-sm">
                            <span>No ledger transactions found matching filters.</span>
                            <Button variant="link" onClick={handleResetFilters} className="text-xs text-primary">
                                Clear all filters
                            </Button>
                        </div>
                    ) : (
                        <div className="border rounded-md overflow-x-auto border-zinc-200 dark:border-zinc-800">
                            <Table>
                                <TableHeader>
                                    <TableRow className="bg-zinc-50 dark:bg-zinc-950/40">
                                        <TableHead>Date / Time</TableHead>
                                        <TableHead>Branch</TableHead>
                                        <TableHead>Movement Type</TableHead>
                                        <TableHead>Asset</TableHead>
                                        <TableHead>Description</TableHead>
                                        <TableHead>Actor</TableHead>
                                        <TableHead className="text-right">Amount</TableHead>
                                    </TableRow>
                                </TableHeader>
                                <TableBody>
                                    {entries.map((entry) => {
                                        const isNegative = entry.amount < 0
                                        const isDeposit = entry.entryType.toLowerCase().includes("deposit") || entry.entryType.includes("إيداع")
                                        const isWithdrawal = entry.entryType.toLowerCase().includes("withdraw") || entry.entryType.includes("سحب")
                                        
                                        // Badge styling
                                        let badgeVariant: "default" | "secondary" | "destructive" | "outline" = "outline"
                                        if (isDeposit || entry.entryType === "عمليات مبيعات" || entry.entryType === "دفع قيمة من آجل") {
                                            badgeVariant = "default"
                                        } else if (isWithdrawal || isNegative) {
                                            badgeVariant = "destructive"
                                        }

                                        return (
                                            <TableRow key={entry.id} className="hover:bg-zinc-50/50 dark:hover:bg-zinc-900/30">
                                                <TableCell className="whitespace-nowrap text-xs font-mono">
                                                    {new Date(entry.date).toLocaleString("en-US", {
                                                        year: 'numeric',
                                                        month: 'short',
                                                        day: '2-digit',
                                                        hour: '2-digit',
                                                        minute: '2-digit',
                                                        hour12: true
                                                    })}
                                                </TableCell>
                                                <TableCell className="font-medium text-xs">
                                                    <span className="flex items-center gap-1.5">
                                                        <Building2 className="h-3.5 w-3.5 text-muted-foreground shrink-0" />
                                                        {entry.branchName || "Main Office"}
                                                    </span>
                                                </TableCell>
                                                <TableCell>
                                                    <Badge variant={badgeVariant} className="text-[10px] py-0 px-2 font-medium">
                                                        {entry.entryType}
                                                    </Badge>
                                                </TableCell>
                                                <TableCell>
                                                    <Badge variant="secondary" className="text-[10px] py-0 px-2 font-medium flex items-center gap-1 w-fit bg-zinc-100 dark:bg-zinc-800 text-zinc-800 dark:text-zinc-300">
                                                        {entry.isBank ? (
                                                            <>
                                                                <Landmark className="h-3 w-3 text-blue-500 shrink-0" />
                                                                <span>Bank</span>
                                                            </>
                                                        ) : (
                                                            <>
                                                                <Wallet className="h-3 w-3 text-emerald-500 shrink-0" />
                                                                <span>Cash</span>
                                                            </>
                                                        )}
                                                    </Badge>
                                                </TableCell>
                                                <TableCell className="max-w-[200px] truncate text-xs text-muted-foreground" title={entry.description || ""}>
                                                    {entry.description || "—"}
                                                </TableCell>
                                                <TableCell className="text-xs whitespace-nowrap text-muted-foreground">
                                                    {entry.employeeName}
                                                </TableCell>
                                                <TableCell className={`text-right font-bold text-xs whitespace-nowrap ${isNegative ? "text-rose-600 dark:text-rose-400" : "text-emerald-600 dark:text-emerald-400"}`}>
                                                    {isNegative ? "" : "+"}
                                                    ${entry.amount.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                                                </TableCell>
                                            </TableRow>
                                        )
                                    })}
                                </TableBody>
                            </Table>
                        </div>
                    )}

                    {/* Pagination */}
                    {totalEntries > pageSize && (
                        <div className="flex items-center justify-between border-t border-zinc-100 dark:border-zinc-800 pt-4 text-sm text-muted-foreground">
                            <div>
                                Showing <span className="font-semibold text-foreground">{((currentPage - 1) * pageSize) + 1}</span> to <span className="font-semibold text-foreground">{Math.min(currentPage * pageSize, totalEntries)}</span> of <span className="font-semibold text-foreground">{totalEntries}</span> ledger movements
                            </div>
                            <div className="flex items-center gap-2">
                                <Button
                                    variant="outline"
                                    size="sm"
                                    onClick={() => setCurrentPage(prev => Math.max(prev - 1, 1))}
                                    disabled={currentPage === 1 || isLoadingEntries}
                                >
                                    Previous
                                </Button>
                                <div className="text-xs font-medium">
                                    Page {currentPage} of {Math.ceil(totalEntries / pageSize)}
                                </div>
                                <Button
                                    variant="outline"
                                    size="sm"
                                    onClick={() => setCurrentPage(prev => Math.min(prev + 1, Math.ceil(totalEntries / pageSize)))}
                                    disabled={currentPage >= Math.ceil(totalEntries / pageSize) || isLoadingEntries}
                                >
                                    Next
                                </Button>
                            </div>
                        </div>
                    )}
                </CardContent>
            </Card>

            {/* Unified Modal Dialog for Deposits & Withdrawals */}
            <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
                <DialogContent className="sm:max-w-[480px]">
                    <DialogHeader>
                        <DialogTitle className="text-xl font-bold flex items-center gap-2">
                            {dialogType === "deposit" ? (
                                <>
                                    <Plus className="h-5 w-5 text-emerald-500" />
                                    Record Safe Deposit
                                </>
                            ) : (
                                <>
                                    <Minus className="h-5 w-5 text-rose-500" />
                                    Record Safe Withdrawal
                                </>
                            )}
                        </DialogTitle>
                        <DialogDescription>
                            Log safe balance adjustments. Ensure proper descriptions are provided for bookkeeping audits.
                        </DialogDescription>
                    </DialogHeader>
                    <form onSubmit={handleSaveTransaction} className="space-y-4 py-2">
                        {/* Amount */}
                        <div className="space-y-2">
                            <label className="text-sm font-semibold text-foreground">Amount ($)</label>
                            <div className="relative">
                                <span className="absolute left-3 top-1/2 -translate-y-1/2 text-muted-foreground font-semibold">$</span>
                                <Input
                                    type="number"
                                    step="0.01"
                                    min="0.01"
                                    required
                                    placeholder="0.00"
                                    value={dialogAmount}
                                    onChange={(e) => setDialogAmount(e.target.value)}
                                    className="pl-8 text-lg font-bold"
                                />
                            </div>
                        </div>

                        {/* Safe Target Asset */}
                        <div className="space-y-2">
                            <label className="text-sm font-semibold text-foreground">Asset Account</label>
                            <div className="grid grid-cols-2 gap-3">
                                <Button
                                    type="button"
                                    variant={!dialogIsBank ? "default" : "outline"}
                                    onClick={() => setDialogIsBank(false)}
                                    className={`w-full flex items-center justify-center gap-2 ${!dialogIsBank ? "bg-emerald-600 hover:bg-emerald-600/90" : ""}`}
                                >
                                    <Wallet className="h-4 w-4" />
                                    Cash Safe
                                </Button>
                                <Button
                                    type="button"
                                    variant={dialogIsBank ? "default" : "outline"}
                                    onClick={() => setDialogIsBank(true)}
                                    className={`w-full flex items-center justify-center gap-2 ${dialogIsBank ? "bg-blue-600 hover:bg-blue-600/90" : ""}`}
                                >
                                    <Landmark className="h-4 w-4" />
                                    Bank Account
                                </Button>
                            </div>
                        </div>

                        {/* Branch Selection */}
                        <div className="space-y-2">
                            <label className="text-sm font-semibold text-foreground">Associated Branch</label>
                            <Select value={dialogBranchId} onValueChange={setDialogBranchId} required>
                                <SelectTrigger>
                                    <SelectValue placeholder="Select Branch" />
                                </SelectTrigger>
                                <SelectContent>
                                    {branches.map((b) => (
                                        <SelectItem key={b.id} value={b.id}>
                                            {b.name}
                                        </SelectItem>
                                    ))}
                                </SelectContent>
                            </Select>
                        </div>

                        {/* Entry Type details (e.g. Deposit -> Capital Deposit, Transfer, Cash Adjustment) */}
                        <div className="space-y-2">
                            <label className="text-sm font-semibold text-foreground">Ledger Label (Entry Type)</label>
                            <Select value={dialogEntryType} onValueChange={setDialogEntryType} required>
                                <SelectTrigger>
                                    <SelectValue placeholder="Select Category" />
                                </SelectTrigger>
                                <SelectContent>
                                    {dialogType === "deposit" ? (
                                        <>
                                            <SelectItem value="Deposit">Manual Cash Deposit</SelectItem>
                                            <SelectItem value="إيداع رأسمالي">Capital Injection / Owner Deposit</SelectItem>
                                            <SelectItem value="تسوية رصيد زائد">Safe Balance Surplus Adjustment</SelectItem>
                                            <SelectItem value="تحويل خارجي">External Bank Inflow</SelectItem>
                                        </>
                                    ) : (
                                        <>
                                            <SelectItem value="Withdrawal">Manual Cash Withdrawal</SelectItem>
                                            <SelectItem value="مصروفات نثرية">Petty Cash / Expenses Inflow</SelectItem>
                                            <SelectItem value="تسوية رصيد ناقص">Safe Balance Deficit Adjustment</SelectItem>
                                            <SelectItem value="رواتب موظفين">Staff Salaries Advance</SelectItem>
                                        </>
                                    )}
                                </SelectContent>
                            </Select>
                        </div>

                        {/* Date (defaults to now) */}
                        <div className="space-y-2">
                            <label className="text-sm font-semibold text-foreground">Transaction Time</label>
                            <Input
                                type="datetime-local"
                                value={dialogDate}
                                onChange={(e) => setDialogDate(e.target.value)}
                                className="w-full"
                            />
                        </div>

                        {/* Description */}
                        <div className="space-y-2">
                            <label className="text-sm font-semibold text-foreground">Description / Notes</label>
                            <Input
                                placeholder="e.g. Daily cash box deposit from retail sales cashier session"
                                value={dialogDescription}
                                onChange={(e) => setDialogDescription(e.target.value)}
                            />
                        </div>

                        <DialogFooter className="pt-4 border-t border-zinc-100 dark:border-zinc-800 gap-2 sm:gap-0">
                            <Button
                                type="button"
                                variant="outline"
                                onClick={() => setIsDialogOpen(false)}
                                disabled={isSaving}
                            >
                                Cancel
                            </Button>
                            <Button
                                type="submit"
                                className={dialogType === "deposit" ? "bg-emerald-600 hover:bg-emerald-700 text-white" : "bg-rose-600 hover:bg-rose-700 text-white"}
                                disabled={isSaving}
                            >
                                {isSaving && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
                                Confirm {dialogType === "deposit" ? "Deposit" : "Withdrawal"}
                            </Button>
                        </DialogFooter>
                    </form>
                </DialogContent>
            </Dialog>
        </div>
    )
}
