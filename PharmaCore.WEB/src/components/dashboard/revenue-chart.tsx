"use client"

import {
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    ResponsiveContainer,
    AreaChart,
    Area,
} from "recharts"
import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import { TrendingUp, BarChart3 } from "lucide-react"
import { useLanguage } from "@/components/providers/language-provider"

interface RevenueDataPoint {
    month: string
    revenue: number
    orders: number
}

interface RevenueChartProps {
    data: RevenueDataPoint[]
}

export function RevenueChart({ data = [] }: RevenueChartProps) {
    const { language, t } = useLanguage()
    const isRtl = language === "ar"

    return (
        <Card className="col-span-1 lg:col-span-4 transition-all duration-200 bg-gradient-to-br from-card to-background">
            <CardHeader className="flex flex-row items-center justify-between pb-4">
                <div className="space-y-0.5">
                    <CardTitle className="text-lg md:text-xl font-bold flex items-center gap-2">
                        <BarChart3 className="h-5 w-5 text-primary" />
                        {t("dashboard.revenue_trend_title")}
                    </CardTitle>
                    <CardDescription className="text-xs md:text-sm">
                        {t("dashboard.revenue_trend_desc")}
                    </CardDescription>
                </div>
                <div className="flex items-center gap-1.5 text-sm text-green-500 font-medium bg-green-500/10 px-2.5 py-1 rounded-full">
                    <TrendingUp className="h-4 w-4" />
                    <span>{t("dashboard.live_telemetry")}</span>
                </div>
            </CardHeader>
            <CardContent className={isRtl ? "pl-4 pr-0" : "pl-0 pr-4"}>
                <div className="h-[300px] w-full">
                    {data.length === 0 ? (
                        <div className="h-full flex items-center justify-center text-muted-foreground text-sm">
                            {t("dashboard.no_revenue_history")}
                        </div>
                    ) : (
                        <ResponsiveContainer width="100%" height="100%">
                            <AreaChart data={data} margin={{ top: 10, right: 10, left: 10, bottom: 0 }}>
                                <defs>
                                    <linearGradient id="revenueGradient" x1="0" y1="0" x2="0" y2="1">
                                        <stop offset="5%" stopColor="hsl(var(--primary))" stopOpacity={0.4} />
                                        <stop offset="95%" stopColor="hsl(var(--primary))" stopOpacity={0.0} />
                                    </linearGradient>
                                </defs>
                                <XAxis
                                    dataKey="month"
                                    stroke="#888888"
                                    fontSize={11}
                                    tickLine={false}
                                    axisLine={false}
                                />
                                <YAxis
                                    stroke="#888888"
                                    fontSize={11}
                                    tickLine={false}
                                    axisLine={false}
                                    tickFormatter={(value) => `${isRtl ? "ج.م" : "EGP"} ${value.toLocaleString()}`}
                                    orientation={isRtl ? "right" : "left"}
                                />
                                <CartesianGrid strokeDasharray="3 3" vertical={false} className="stroke-muted" />
                                <Tooltip
                                    formatter={(value: any) => [`${isRtl ? "ج.م" : "EGP"} ${parseFloat(value).toLocaleString()}`, t("dashboard.revenue_axis_label")]}
                                    contentStyle={{
                                        backgroundColor: 'hsl(var(--popover))',
                                        borderColor: 'hsl(var(--border))',
                                        borderRadius: 'var(--radius)',
                                        color: 'hsl(var(--popover-foreground))',
                                        textAlign: isRtl ? 'right' : 'left'
                                    }}
                                />
                                <Area
                                    type="monotone"
                                    dataKey="revenue"
                                    name={t("dashboard.revenue_axis_label")}
                                    stroke="hsl(var(--primary))"
                                    fillOpacity={1}
                                    fill="url(#revenueGradient)"
                                    strokeWidth={2.5}
                                />
                            </AreaChart>
                        </ResponsiveContainer>
                    )}
                </div>
            </CardContent>
        </Card>
    )
}
