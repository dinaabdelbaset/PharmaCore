"use client"

import {
    BarChart,
    Bar,
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    ResponsiveContainer,
    Cell,
} from "recharts"
import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import { Award } from "lucide-react"
import { useLanguage } from "@/components/providers/language-provider"

const COLORS = [
    '#3b82f6', // blue
    '#10b981', // green
    '#f59e0b', // amber
    '#ef4444', // red
    '#8b5cf6', // violet
    '#ec4899', // pink
    '#14b8a6', // teal
    '#6366f1', // indigo
    '#f97316', // orange
    '#84cc16'  // lime
]

interface BranchPerformanceData {
    id: string
    name: string
    revenue: number
    orders: number
}

interface BranchPerformanceProps {
    data: BranchPerformanceData[]
}

export function BranchPerformance({ data = [] }: BranchPerformanceProps) {
    const { language, t } = useLanguage()
    const isRtl = language === "ar"

    return (
        <Card className="col-span-1 lg:col-span-3 transition-all duration-200 bg-gradient-to-br from-card to-background">
            <CardHeader className="pb-4">
                <CardTitle className="text-lg md:text-xl font-bold flex items-center gap-2">
                    <Award className="h-5 w-5 text-amber-500" />
                    {t("dashboard.branch_performance_rankings")}
                </CardTitle>
                <CardDescription className="text-xs md:text-sm">
                    {t("dashboard.branch_performance_subtitle")}
                </CardDescription>
            </CardHeader>
            <CardContent>
                <div className="h-[300px] w-full">
                    {data.length === 0 ? (
                        <div className="h-full flex items-center justify-center text-muted-foreground text-sm">
                            {t("dashboard.no_performance_rankings")}
                        </div>
                    ) : (
                        <ResponsiveContainer width="100%" height="100%">
                            <BarChart data={data} layout="vertical" margin={isRtl ? { right: -10, left: 10 } : { left: -10, right: 10 }}>
                                <CartesianGrid strokeDasharray="3 3" horizontal={false} className="stroke-muted" />
                                <XAxis type="number" hide />
                                <YAxis
                                    dataKey="name"
                                    type="category"
                                    width={90}
                                    tick={{ fontSize: 11 }}
                                    tickLine={false}
                                    axisLine={false}
                                    orientation={isRtl ? "right" : "left"}
                                />
                                <Tooltip
                                    cursor={{ fill: 'transparent' }}
                                    formatter={(value: any) => [`${isRtl ? "ج.م" : "EGP"} ${parseFloat(value).toLocaleString()}`, t("dashboard.revenue_axis_label")]}
                                    contentStyle={{
                                        backgroundColor: 'hsl(var(--popover))',
                                        borderColor: 'hsl(var(--border))',
                                        borderRadius: 'var(--radius)',
                                        color: 'hsl(var(--popover-foreground))',
                                        textAlign: isRtl ? 'right' : 'left'
                                    }}
                                />
                                <Bar dataKey="revenue" radius={isRtl ? [4, 0, 0, 4] : [0, 4, 4, 0]} barSize={20}>
                                    {data.map((entry, index) => (
                                        <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                                    ))}
                                </Bar>
                            </BarChart>
                        </ResponsiveContainer>
                    )}
                </div>
            </CardContent>
        </Card>
    )
}
