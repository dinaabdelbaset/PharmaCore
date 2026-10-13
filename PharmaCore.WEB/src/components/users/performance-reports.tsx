"use client"

import { useState, useEffect } from "react"
import { BarChart3, TrendingUp, DollarSign, Award, RefreshCw, ShoppingCart } from "lucide-react"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table"
import apiClient from "@/lib/api-client"
import { toast } from "sonner"

interface PerformanceStat {
    employeeId: string | null
    employeeName: string
    employeeCode: string
    totalRevenue: number
    salesCount: number
    averageTicketValue: number
}

export function PerformanceReports() {
    const [stats, setStats] = useState<PerformanceStat[]>([])
    const [isLoading, setIsLoading] = useState(false)

    useEffect(() => {
        loadStats()
    }, [])

    const loadStats = async () => {
        setIsLoading(true)
        try {
            const res = await apiClient.get<PerformanceStat[]>("/employees/performance-stats")
            setStats(res.data)
        } catch (err) {
            toast.error("فشل تحميل إحصائيات تقييم الكاشير")
        } finally {
            setIsLoading(false)
        }
    }

    // Calculated fields
    const totalSalesSum = stats.reduce((sum, item) => sum + item.totalRevenue, 0)
    const totalInvoicesSum = stats.reduce((sum, item) => sum + item.salesCount, 0)
    const topCashier = stats.length > 0 ? stats[0] : null
    const overallAvgTicket = totalInvoicesSum > 0 ? totalSalesSum / totalInvoicesSum : 0

    return (
        <div className="space-y-6" dir="rtl">
            <div className="flex justify-between items-center bg-card p-4 rounded-lg border">
                <div>
                    <h3 className="text-lg font-bold">لوحة أداء المبيعات والكاشير</h3>
                    <p className="text-xs text-muted-foreground">تقييم مبيعات وإيرادات الكواشير والصيدلة بشكل تراكمي</p>
                </div>
                <Button variant="outline" size="icon" onClick={loadStats} disabled={isLoading}>
                    <RefreshCw className={`h-4 w-4 ${isLoading ? "animate-spin" : ""}`} />
                </Button>
            </div>

            {/* Metrics Row */}
            <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
                <Card>
                    <CardHeader className="flex flex-row items-center justify-between pb-2 space-y-0">
                        <CardTitle className="text-xs font-bold">أعلى كاشير مبيعاً</CardTitle>
                        <Award className="h-4 w-4 text-amber-500" />
                    </CardHeader>
                    <CardContent>
                        <div className="text-base font-bold truncate">
                            {topCashier ? topCashier.employeeName : "لا يوجد"}
                        </div>
                        <p className="text-[10px] text-muted-foreground mt-1">
                            {topCashier ? `إيراد: ${topCashier.totalRevenue.toLocaleString("ar-EG")} ج.م` : "-"}
                        </p>
                    </CardContent>
                </Card>

                <Card>
                    <CardHeader className="flex flex-row items-center justify-between pb-2 space-y-0">
                        <CardTitle className="text-xs font-bold">إجمالي مبيعات الموظفين</CardTitle>
                        <DollarSign className="h-4 w-4 text-emerald-500" />
                    </CardHeader>
                    <CardContent>
                        <div className="text-base font-bold text-emerald-500 font-mono">
                            {totalSalesSum.toLocaleString("ar-EG")} ج.م
                        </div>
                        <p className="text-[10px] text-muted-foreground mt-1">حجم المعاملات المكتملة</p>
                    </CardContent>
                </Card>

                <Card>
                    <CardHeader className="flex flex-row items-center justify-between pb-2 space-y-0">
                        <CardTitle className="text-xs font-bold">إجمالي فواتير البيع</CardTitle>
                        <ShoppingCart className="h-4 w-4 text-sky-500" />
                    </CardHeader>
                    <CardContent>
                        <div className="text-base font-bold font-mono">
                            {totalInvoicesSum.toLocaleString("ar-EG")} فاتورة
                        </div>
                        <p className="text-[10px] text-muted-foreground mt-1">معالجة فورية بنقاط البيع</p>
                    </CardContent>
                </Card>

                <Card>
                    <CardHeader className="flex flex-row items-center justify-between pb-2 space-y-0">
                        <CardTitle className="text-xs font-bold">متوسط الفاتورة العام</CardTitle>
                        <TrendingUp className="h-4 w-4 text-indigo-500" />
                    </CardHeader>
                    <CardContent>
                        <div className="text-base font-bold font-mono">
                            {overallAvgTicket.toLocaleString("ar-EG", { maximumFractionDigits: 1 })} ج.م
                        </div>
                        <p className="text-[10px] text-muted-foreground mt-1">لكافة فواتير المبيعات</p>
                    </CardContent>
                </Card>
            </div>

            {/* Main Leaderboard Table */}
            <Card>
                <CardHeader className="pb-3">
                    <CardTitle className="text-sm font-bold flex items-center gap-2">
                        <BarChart3 className="h-4 w-4 text-sky-500" />
                        ترتيب الكواشير والصيادلة حسب المبيعات
                    </CardTitle>
                    <CardDescription>عرض تفصيلي لإجمالي فواتير الإيراد، وعدد الفواتير، ونسبة المبيعات</CardDescription>
                </CardHeader>
                <CardContent className="p-0">
                    <Table>
                        <TableHeader className="bg-muted/40">
                            <TableRow>
                                <TableHead className="text-right w-[80px]">الترتيب</TableHead>
                                <TableHead className="text-right">الموظف</TableHead>
                                <TableHead className="text-right w-[150px]">عدد الفواتير</TableHead>
                                <TableHead className="text-right w-[180px]">إجمالي المبيعات</TableHead>
                                <TableHead className="text-right w-[180px]">متوسط قيمة الفاتورة</TableHead>
                                <TableHead className="text-right">مساهمة المبيعات</TableHead>
                            </TableRow>
                        </TableHeader>
                        <TableBody>
                            {isLoading ? (
                                <TableRow>
                                    <TableCell colSpan={6} className="text-center py-20 text-muted-foreground text-sm">
                                        <RefreshCw className="animate-spin ml-2 h-5 w-5 inline" />
                                        جاري سحب تقارير الكفاءة...
                                    </TableCell>
                                </TableRow>
                            ) : stats.length === 0 ? (
                                <TableRow>
                                    <TableCell colSpan={6} className="text-center py-20 text-muted-foreground text-sm">
                                        لا توجد مبيعات مسجلة للموظفين بعد
                                    </TableCell>
                                </TableRow>
                            ) : (
                                stats.map((item, index) => {
                                    const percent = totalSalesSum > 0 ? (item.totalRevenue / totalSalesSum) * 100 : 0
                                    
                                    return (
                                        <TableRow key={item.employeeId || item.employeeName} className="hover:bg-muted/10">
                                            <TableCell className="font-bold text-center">
                                                {index + 1}#
                                            </TableCell>
                                            <TableCell>
                                                <div className="flex flex-col">
                                                    <span className="font-semibold text-xs">{item.employeeName}</span>
                                                    {item.employeeCode && <span className="text-[10px] text-muted-foreground font-mono">({item.employeeCode})</span>}
                                                </div>
                                            </TableCell>
                                            <TableCell className="font-mono text-xs">
                                                {item.salesCount} فاتورة
                                            </TableCell>
                                            <TableCell className="font-mono text-xs font-bold text-emerald-500">
                                                {item.totalRevenue.toLocaleString("ar-EG")} ج.م
                                            </TableCell>
                                            <TableCell className="font-mono text-xs text-muted-foreground">
                                                {item.averageTicketValue.toLocaleString("ar-EG", { maximumFractionDigits: 1 })} ج.م
                                            </TableCell>
                                            <TableCell>
                                                <div className="flex items-center gap-3">
                                                    <div className="flex-1 bg-muted rounded-full h-2 overflow-hidden max-w-[120px]">
                                                        <div 
                                                            className="bg-sky-500 h-full rounded-full" 
                                                            style={{ width: `${percent}%` }}
                                                        />
                                                    </div>
                                                    <span className="text-[10px] font-mono font-bold text-slate-500">
                                                        {percent.toFixed(1)}%
                                                    </span>
                                                </div>
                                            </TableCell>
                                        </TableRow>
                                    )
                                })
                            )}
                        </TableBody>
                    </Table>
                </CardContent>
            </Card>
        </div>
    )
}
