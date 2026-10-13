"use client"

import { useState, useEffect } from "react"
import { Card, CardContent, CardHeader, CardTitle, CardDescription } from "@/components/ui/card"
import { RevenueChart } from "@/components/dashboard/revenue-chart"
import { BranchPerformance } from "@/components/dashboard/branch-performance"
import { BranchTargets } from "@/components/dashboard/branch-targets"
import { AiSmartAlerts, ExpiryRisk, StockRisk } from "@/components/dashboard/ai-smart-alerts"
import { AiDemandForecast } from "@/components/dashboard/ai-demand-forecast"
import { Label } from "@/components/ui/label"
import { useLanguage } from "@/components/providers/language-provider"
import { 
    Loader2, 
    DollarSign, 
    CreditCard, 
    Activity, 
    ShoppingBag, 
    Brain, 
    TrendingDown, 
    ArrowUpRight, 
    ArrowDownRight, 
    Percent, 
    Layers, 
    Coins 
} from "lucide-react"
import apiClient from "@/lib/api-client"
import { toast } from "sonner"
import { useRouter } from "next/navigation"

interface KpiMetric {
    value: number
    changePercent: number
}

interface TopProduct {
    name: string
    quantity: number
    revenue: number
}

interface BranchBalance {
    branchId: string
    branchName: string
    cashBalance: number
    bankBalance: number
    totalBalance: number
}

interface DashboardStats {
    totalRevenue: KpiMetric
    salesCount: KpiMetric
    activeCashiers: number
    revenueChart: any[]
    branchPerformance: any[]
    expiryRisks: ExpiryRisk[]
    stockRisks: StockRisk[]
    topProducts: TopProduct[]
    totalExpenses: number
    branchBalances: BranchBalance[]
}

// High-fidelity enterprise demonstration data (SAP Grade)
const simulatedStats: DashboardStats = {
    totalRevenue: { value: 1248349.50, changePercent: 14.2 },
    salesCount: { value: 84230, changePercent: 11.8 },
    activeCashiers: 42,
    revenueChart: [
        { month: "Jan 2026", revenue: 180000, orders: 12000 },
        { month: "Feb 2026", revenue: 198000, orders: 13500 },
        { month: "Mar 2026", revenue: 215000, orders: 14200 },
        { month: "Apr 2026", revenue: 230000, orders: 15100 },
        { month: "May 2026", revenue: 245000, orders: 16000 },
        { month: "Jun 2026", revenue: 270000, orders: 17200 }
    ],
    branchPerformance: [
        { id: "1", name: "Main Branch (Cairo)", revenue: 450000, orders: 31000 },
        { id: "2", name: "Alexandria Branch", revenue: 380000, orders: 25000 },
        { id: "3", name: "Giza Branch", revenue: 320000, orders: 21000 },
        { id: "4", name: "Heliopolis Branch", revenue: 280000, orders: 19000 },
        { id: "5", name: "Zamalek Branch", revenue: 250000, orders: 16000 }
    ],
    expiryRisks: [
        { barcode: "622100300124", productName: "Panadol Cold & Flu", branchName: "Main Branch", stockQuantity: 450, expiryDate: new Date(Date.now() + 1000 * 60 * 60 * 24 * 12).toISOString(), daysRemaining: 12 },
        { barcode: "622201004521", productName: "Cataflam 50mg Tablets", branchName: "Alexandria Branch", stockQuantity: 180, expiryDate: new Date(Date.now() + 1000 * 60 * 60 * 24 * 24).toISOString(), daysRemaining: 24 },
        { barcode: "622400508930", productName: "Brufen 400mg Syrup", branchName: "Giza Branch", stockQuantity: 290, expiryDate: new Date(Date.now() + 1000 * 60 * 60 * 24 * 48).toISOString(), daysRemaining: 48 },
        { barcode: "622104005912", productName: "Concor 5mg Tablets", branchName: "Heliopolis Branch", stockQuantity: 320, expiryDate: new Date(Date.now() + 1000 * 60 * 60 * 24 * 72).toISOString(), daysRemaining: 72 }
    ],
    stockRisks: [
        { barcode: "622300104921", productName: "Augmentin 1g Tablets", branchName: "Main Branch", stockQuantity: 3, minQtyAlert: 15 },
        { barcode: "622201004902", productName: "Panadol Extra 500mg", branchName: "Zamalek Branch", stockQuantity: 5, minQtyAlert: 30 },
        { barcode: "622104005988", productName: "Otrivin Pediatric Nasal Spray", branchName: "Giza Branch", stockQuantity: 2, minQtyAlert: 10 },
        { barcode: "622400508212", productName: "Gliclazide 60mg (Diamicron)", branchName: "Alexandria Branch", stockQuantity: 4, minQtyAlert: 20 }
    ],
    topProducts: [
        { name: "Panadol Extra 500mg", quantity: 1840, revenue: 40480.00 },
        { name: "Augmentin 1g Tablets", quantity: 420, revenue: 37380.00 },
        { name: "Cataflam 50mg Tablets", quantity: 980, revenue: 35280.00 },
        { name: "Concor 5mg Tablets", quantity: 720, revenue: 30600.00 },
        { name: "Otrivin Pediatric Spray", quantity: 850, revenue: 17000.00 }
    ],
    totalExpenses: 28450.00,
    branchBalances: [
        { branchId: "1", branchName: "Cairo Main Branch", cashBalance: 45000.00, bankBalance: 125000.00, totalBalance: 170000.00 },
        { branchId: "2", branchName: "Alexandria Branch", cashBalance: 32000.00, bankBalance: 88000.00, totalBalance: 120000.00 },
        { branchId: "3", branchName: "Giza Branch", cashBalance: 18000.00, bankBalance: 62000.00, totalBalance: 80000.00 }
    ]
}

export default function DashboardPage() {
    const router = useRouter()
    const { language, t } = useLanguage()
    const isRtl = language === "ar"
    const [liveStats, setLiveStats] = useState<DashboardStats | null>(null)
    const [isSimulated, setIsSimulated] = useState(false)
    const [isLoading, setIsLoading] = useState(true)

    const fetchLiveStats = async () => {
        setIsLoading(true)
        try {
            const response = await apiClient.get<DashboardStats>("/dashboard/stats")
            if (response.data) {
                setLiveStats(response.data)
            }
        } catch (error: any) {
            console.error("Failed to fetch dashboard stats", error)
            const isDemo = typeof window !== "undefined" ? localStorage.getItem("demoMode") === "true" : false
            setIsSimulated(isDemo)
        } finally {
            setIsLoading(false)
        }
    }

    useEffect(() => {
        if (typeof window !== "undefined") {
            const setupDone = localStorage.getItem("setup_completed") === "true"
            if (!setupDone) {
                router.push("/register/setup")
                return
            }
            const isDemo = localStorage.getItem("demoMode") === "true"
            setIsSimulated(isDemo)
        }
        fetchLiveStats()
    }, [router])

    if (isLoading) {
        return (
            <div className="h-screen w-full flex flex-col items-center justify-center gap-3 text-muted-foreground">
                <Loader2 className="h-10 w-10 animate-spin text-primary" />
                <span className="text-sm font-medium">Aggregating live ERP telemetry...</span>
            </div>
        )
    }

    const emptyStats: DashboardStats = {
        totalRevenue: { value: 0, changePercent: 0 },
        salesCount: { value: 0, changePercent: 0 },
        activeCashiers: 0,
        revenueChart: [],
        branchPerformance: [],
        expiryRisks: [],
        stockRisks: [],
        topProducts: [],
        totalExpenses: 0,
        branchBalances: []
    }

    const activeStats = isSimulated ? simulatedStats : (liveStats ?? emptyStats)

    const totalRev = activeStats.totalRevenue?.value ?? 0
    const totalRevChange = activeStats.totalRevenue?.changePercent ?? 0
    const salesVol = activeStats.salesCount?.value ?? 0
    const salesVolChange = activeStats.salesCount?.changePercent ?? 0
    const activeCashiers = activeStats.activeCashiers ?? 0
    const avgOrderVal = salesVol > 0 ? totalRev / salesVol : 0
    
    // Net profit calculations
    const totalExp = activeStats.totalExpenses ?? 0
    const netProfit = totalRev - totalExp
    const expensesPercent = totalRev > 0 ? (totalExp / totalRev) * 100 : 0
    const profitPercent = totalRev > 0 ? (netProfit / totalRev) * 100 : 0

    return (
        <div className="flex-1 space-y-6 p-4 md:p-6 lg:p-8 pt-4 md:pt-6" dir={isRtl ? "rtl" : "ltr"}>
            {/* Header / Control Bar */}
            <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
                <div>
                    <h2 className="text-2xl md:text-3xl font-bold tracking-tight">
                        {t("dashboard.operations_center")}
                    </h2>
                    <p className="text-sm text-muted-foreground">
                        {t("dashboard.telemetry_subtitle")}
                    </p>
                </div>
                
                {/* Simulation Switch */}
                <div className="flex items-center gap-3 bg-muted/50 border p-2.5 rounded-lg max-w-fit shadow-sm">
                    <div className="flex items-center gap-1.5 text-xs font-semibold text-violet-500">
                        <Brain className="h-4 w-4 animate-bounce" />
                        <span>{t("dashboard.simulated_mode")}</span>
                    </div>
                    <button
                        role="switch"
                        id="demo-mode"
                        aria-checked={isSimulated}
                        onClick={() => {
                            const nextVal = !isSimulated;
                            setIsSimulated(nextVal);
                            localStorage.setItem("demoMode", nextVal ? "true" : "false");
                            window.dispatchEvent(new Event("demoModeChanged"));
                            if (!nextVal && (!liveStats || liveStats.totalRevenue.value === 0)) {
                                toast.info(isRtl ? "عرض حالة قاعدة البيانات الحية الفارغة." : "Showing empty live database state (no transaction history registered).")
                            } else {
                                toast.success(nextVal 
                                    ? (isRtl ? "تم تفعيل نمط المحاكاة" : "Enterprise mode activated") 
                                    : (isRtl ? "تم تفعيل مزامنة البيانات الحية" : "Live data sync active")
                                )
                            }
                        }}
                        className={`relative inline-flex h-5 w-9 shrink-0 cursor-pointer items-center rounded-full transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 focus-visible:ring-offset-background ${isSimulated ? "bg-violet-600" : "bg-zinc-600"}`}
                    >
                        <span
                            className={`pointer-events-none block h-4 w-4 rounded-full bg-white shadow-lg ring-0 transition-transform ${
                                isSimulated 
                                    ? (isRtl ? "-translate-x-[18px]" : "translate-x-[18px]") 
                                    : "translate-x-0.5"
                            }`}
                        />
                    </button>
                    <Label htmlFor="demo-mode" className="text-xs text-muted-foreground cursor-pointer select-none">
                        {isSimulated ? t("dashboard.demo_active") : t("dashboard.live_data")}
                    </Label>
                </div>
            </div>

            {/* Glowing warning banner if in Sandbox Mode */}
            {isSimulated && (
                <div className="p-4 border border-violet-500/30 rounded-xl bg-gradient-to-r from-violet-950/20 to-indigo-950/20 shadow-md flex items-center justify-between gap-4 animate-fade-in">
                    <div className="text-sm text-zinc-300">
                        {t("dashboard.sandbox_banner")}
                    </div>
                </div>
            )}

            {/* KPI Cards Row */}
            <div className="grid gap-3 md:gap-4 grid-cols-1 sm:grid-cols-2 lg:grid-cols-4">
                {/* Total Revenue */}
                <Card className="hover:border-primary/20 transition-all duration-200 shadow-sm relative overflow-hidden group">
                    <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                        <CardTitle className="text-xs md:text-sm font-medium text-muted-foreground">
                            {t("dashboard.total_revenue")}
                        </CardTitle>
                        <div className="p-1.5 rounded-lg bg-green-500/10 text-green-500">
                            <DollarSign className="h-4 w-4" />
                        </div>
                    </CardHeader>
                    <CardContent>
                        <div className="text-xl md:text-2xl font-bold">
                            {totalRev.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })} {isRtl ? "ج.م" : "EGP"}
                        </div>
                        <p className={`text-xs mt-1 flex items-center gap-1 ${totalRevChange >= 0 ? "text-green-500" : "text-red-500"}`}>
                            {totalRevChange >= 0 ? <ArrowUpRight className="h-3 w-3" /> : <ArrowDownRight className="h-3 w-3" />}
                            <span>{totalRevChange >= 0 ? "+" : ""}{totalRevChange.toFixed(1)}% {t("dashboard.from_last_month")}</span>
                        </p>
                    </CardContent>
                </Card>

                {/* Sales Volume */}
                <Card className="hover:border-primary/20 transition-all duration-200 shadow-sm">
                    <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                        <CardTitle className="text-xs md:text-sm font-medium text-muted-foreground">
                            {t("dashboard.sales_transactions")}
                        </CardTitle>
                        <div className="p-1.5 rounded-lg bg-blue-500/10 text-blue-500">
                            <ShoppingBag className="h-4 w-4" />
                        </div>
                    </CardHeader>
                    <CardContent>
                        <div className="text-xl md:text-2xl font-bold">+{salesVol.toLocaleString()}</div>
                        <p className={`text-xs mt-1 flex items-center gap-1 ${salesVolChange >= 0 ? "text-green-500" : "text-red-500"}`}>
                            {salesVolChange >= 0 ? <ArrowUpRight className="h-3 w-3" /> : <ArrowDownRight className="h-3 w-3" />}
                            <span>{salesVolChange >= 0 ? "+" : ""}{salesVolChange.toFixed(1)}% {t("dashboard.from_last_month")}</span>
                        </p>
                    </CardContent>
                </Card>

                {/* Active Cashiers */}
                <Card className="hover:border-primary/20 transition-all duration-200 shadow-sm">
                    <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                        <CardTitle className="text-xs md:text-sm font-medium text-muted-foreground">
                            {t("dashboard.active_cashiers")}
                        </CardTitle>
                        <div className="p-1.5 rounded-lg bg-indigo-500/10 text-indigo-500">
                            <Activity className="h-4 w-4 animate-pulse" />
                        </div>
                    </CardHeader>
                    <CardContent>
                        <div className="text-xl md:text-2xl font-bold">{activeCashiers} POS</div>
                        <p className="text-xs text-muted-foreground mt-1">
                            {t("dashboard.active_cashiers_desc")}
                        </p>
                    </CardContent>
                </Card>

                {/* Average Ticket Size */}
                <Card className="hover:border-primary/20 transition-all duration-200 shadow-sm">
                    <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                        <CardTitle className="text-xs md:text-sm font-medium text-muted-foreground">
                            {t("dashboard.avg_ticket_value")}
                        </CardTitle>
                        <div className="p-1.5 rounded-lg bg-amber-500/10 text-amber-500">
                            <CreditCard className="h-4 w-4" />
                        </div>
                    </CardHeader>
                    <CardContent>
                        <div className="text-xl md:text-2xl font-bold">
                            {avgOrderVal.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })} {isRtl ? "ج.م" : "EGP"}
                        </div>
                        <p className="text-xs text-muted-foreground mt-1">
                            {t("dashboard.avg_ticket_desc")}
                        </p>
                    </CardContent>
                </Card>
            </div>

            {/* Charts and AI Optimizations Grid */}
            <div className="grid gap-6 grid-cols-1 lg:grid-cols-7">
                {/* Row 1: Charts */}
                <RevenueChart data={activeStats.revenueChart} />
                <BranchPerformance data={activeStats.branchPerformance} />

                {/* Row 2: AI & Stock Alarms */}
                <AiDemandForecast isSimulated={isSimulated} />
                <AiSmartAlerts
                    expiryRisks={activeStats.expiryRisks}
                    stockRisks={activeStats.stockRisks}
                />

                {/* Row 3: New Panels (Top Products & Balances + Expenses Tracker) */}
                
                {/* Left side: Top Products & Branch balances */}
                <div className="col-span-full lg:col-span-4 space-y-6">
                    {/* Top Selling Products */}
                    <Card className="shadow-sm border border-border/50 bg-gradient-to-br from-card to-background">
                        <CardHeader className="pb-3">
                            <CardTitle className="text-lg md:text-xl font-bold flex items-center gap-2">
                                <Layers className="h-5 w-5 text-emerald-500" />
                                {t("dashboard.top_products_title")}
                            </CardTitle>
                            <CardDescription className="text-xs">
                                {t("dashboard.top_products_desc")}
                            </CardDescription>
                        </CardHeader>
                        <CardContent>
                            <div className="overflow-x-auto">
                                <table className="w-full text-sm text-left rtl:text-right">
                                    <thead>
                                        <tr className="border-b border-border/50 text-xs text-muted-foreground">
                                            <th className="py-2.5 font-semibold">{t("dashboard.product_name")}</th>
                                            <th className="py-2.5 font-semibold text-center">{t("dashboard.quantity")}</th>
                                            <th className="py-2.5 font-semibold text-right rtl:text-left">{t("dashboard.revenue")}</th>
                                        </tr>
                                    </thead>
                                    <tbody className="divide-y divide-border/30">
                                        {activeStats.topProducts && activeStats.topProducts.length > 0 ? (
                                            activeStats.topProducts.map((p, idx) => (
                                                <tr key={idx} className="hover:bg-muted/30 transition-colors">
                                                    <td className="py-3 font-medium text-zinc-950 dark:text-zinc-100 flex items-center gap-2.5">
                                                        <span className="w-5 h-5 rounded-full bg-emerald-500/10 text-emerald-500 font-bold flex items-center justify-center text-[10px]">
                                                            {idx + 1}
                                                        </span>
                                                        {p.name}
                                                    </td>
                                                    <td className="py-3 text-center font-mono font-medium text-zinc-600 dark:text-zinc-400">
                                                        {p.quantity.toLocaleString()}
                                                    </td>
                                                    <td className="py-3 text-right rtl:text-left font-mono font-bold text-emerald-500">
                                                        {p.revenue.toLocaleString(undefined, { minimumFractionDigits: 2 })} {isRtl ? "ج.م" : "EGP"}
                                                    </td>
                                                </tr>
                                            ))
                                        ) : (
                                            <tr>
                                                <td colSpan={3} className="py-6 text-center text-muted-foreground text-xs">
                                                    {t("common.no_results")}
                                                </td>
                                            </tr>
                                        )}
                                    </tbody>
                                </table>
                            </div>
                        </CardContent>
                    </Card>

                    {/* Branch Treasury Balances */}
                    <Card className="shadow-sm border border-border/50 bg-gradient-to-br from-card to-background">
                        <CardHeader className="pb-3">
                            <CardTitle className="text-lg md:text-xl font-bold flex items-center gap-2">
                                <Coins className="h-5 w-5 text-indigo-500" />
                                {t("dashboard.treasury_status_title")}
                            </CardTitle>
                            <CardDescription className="text-xs">
                                {t("dashboard.treasury_status_desc")}
                            </CardDescription>
                        </CardHeader>
                        <CardContent>
                            <div className="overflow-x-auto">
                                <table className="w-full text-sm text-left rtl:text-right">
                                    <thead>
                                        <tr className="border-b border-border/50 text-xs text-muted-foreground">
                                            <th className="py-2.5 font-semibold">{t("dashboard.branch")}</th>
                                            <th className="py-2.5 font-semibold text-center">{t("dashboard.cash_balance")}</th>
                                            <th className="py-2.5 font-semibold text-center">{t("dashboard.bank_balance")}</th>
                                            <th className="py-2.5 font-semibold text-right rtl:text-left">{t("dashboard.total_balance")}</th>
                                        </tr>
                                    </thead>
                                    <tbody className="divide-y divide-border/30">
                                        {activeStats.branchBalances && activeStats.branchBalances.length > 0 ? (
                                            activeStats.branchBalances.map((b, idx) => (
                                                <tr key={idx} className="hover:bg-muted/30 transition-colors">
                                                    <td className="py-3 font-semibold text-zinc-800 dark:text-zinc-200">
                                                        {b.branchName}
                                                    </td>
                                                    <td className="py-3 text-center font-mono text-xs font-semibold text-amber-500">
                                                        {b.cashBalance.toLocaleString(undefined, { minimumFractionDigits: 2 })} {isRtl ? "ج.م" : "EGP"}
                                                    </td>
                                                    <td className="py-3 text-center font-mono text-xs font-semibold text-blue-500">
                                                        {b.bankBalance.toLocaleString(undefined, { minimumFractionDigits: 2 })} {isRtl ? "ج.م" : "EGP"}
                                                    </td>
                                                    <td className="py-3 text-right rtl:text-left font-mono font-bold text-zinc-900 dark:text-white">
                                                        {b.totalBalance.toLocaleString(undefined, { minimumFractionDigits: 2 })} {isRtl ? "ج.م" : "EGP"}
                                                    </td>
                                                </tr>
                                            ))
                                        ) : (
                                            <tr>
                                                <td colSpan={4} className="py-6 text-center text-muted-foreground text-xs">
                                                    {t("common.no_results")}
                                                </td>
                                            </tr>
                                        )}
                                    </tbody>
                                </table>
                            </div>
                        </CardContent>
                    </Card>
                </div>

                {/* Right side: Expenses & profit tracker (3 cols) */}
                <div className="col-span-full lg:col-span-3">
                    <Card className="h-full shadow-sm border border-border/50 bg-gradient-to-br from-card to-background flex flex-col">
                        <CardHeader className="pb-4">
                            <CardTitle className="text-lg md:text-xl font-bold flex items-center gap-2">
                                <TrendingDown className="h-5 w-5 text-rose-500" />
                                {t("dashboard.expenses_profit_title")}
                            </CardTitle>
                            <CardDescription className="text-xs">
                                {t("dashboard.expenses_profit_desc")}
                            </CardDescription>
                        </CardHeader>
                        <CardContent className="flex-1 flex flex-col justify-around gap-6">
                            
                            {/* KPI Metrics Breakdown */}
                            <div className="space-y-4">
                                {/* Revenue */}
                                <div className="p-3.5 rounded-xl bg-green-500/5 border border-green-500/10">
                                    <span className="text-xs text-muted-foreground font-medium block">{t("dashboard.total_revenue")}</span>
                                    <span className="text-xl font-black text-green-500 mt-1 block">
                                        EGP {totalRev.toLocaleString(undefined, { minimumFractionDigits: 2 })}
                                    </span>
                                </div>

                                {/* Expenses */}
                                <div className="p-3.5 rounded-xl bg-rose-500/5 border border-rose-500/10">
                                    <span className="text-xs text-muted-foreground font-medium block">{t("dashboard.expenses")}</span>
                                    <span className="text-xl font-black text-rose-500 mt-1 block">
                                        EGP {totalExp.toLocaleString(undefined, { minimumFractionDigits: 2 })}
                                    </span>
                                </div>

                                {/* Net Profit */}
                                <div className={`p-3.5 rounded-xl border ${netProfit >= 0 ? "bg-emerald-500/5 border-emerald-500/10 text-emerald-500" : "bg-red-500/5 border-red-500/10 text-red-500"}`}>
                                    <span className="text-xs text-muted-foreground font-medium block">{t("dashboard.net_profit")}</span>
                                    <span className="text-xl font-black mt-1 block">
                                        EGP {netProfit.toLocaleString(undefined, { minimumFractionDigits: 2 })}
                                    </span>
                                </div>
                            </div>

                            {/* Ratio progress bars */}
                            <div className="space-y-5 py-2">
                                {/* Expenses Ratio */}
                                <div className="space-y-1.5">
                                    <div className="flex justify-between text-xs font-bold">
                                        <span className="text-muted-foreground">{t("dashboard.expenses_ratio")}</span>
                                        <span className="text-rose-500">{expensesPercent.toFixed(1)}%</span>
                                    </div>
                                    <div className="w-full h-2 rounded-full bg-muted overflow-hidden">
                                        <div 
                                            className="h-full bg-rose-500 rounded-full transition-all duration-500" 
                                            style={{ width: `${Math.min(expensesPercent, 100)}%` }}
                                        />
                                    </div>
                                </div>

                                {/* Profit Ratio */}
                                <div className="space-y-1.5">
                                    <div className="flex justify-between text-xs font-bold">
                                        <span className="text-muted-foreground">{t("dashboard.net_profit_ratio")}</span>
                                        <span className="text-emerald-500">{profitPercent.toFixed(1)}%</span>
                                    </div>
                                    <div className="w-full h-2 rounded-full bg-muted overflow-hidden">
                                        <div 
                                            className="h-full bg-emerald-500 rounded-full transition-all duration-500" 
                                            style={{ width: `${Math.max(0, Math.min(profitPercent, 100))}%` }}
                                        />
                                    </div>
                                </div>
                            </div>

                        </CardContent>
                    </Card>
                </div>

                {/* Row 4: Target Tracking (Full Width) */}
                <div className="col-span-full">
                    <BranchTargets isSimulated={isSimulated} />
                </div>
            </div>
        </div>
    )
}
