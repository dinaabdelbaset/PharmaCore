"use client"

import { AlertTriangle, Calendar, Package, ArrowRight, ShieldAlert } from "lucide-react"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { Badge } from "@/components/ui/badge"
import { useLanguage } from "@/components/providers/language-provider"

export interface ExpiryRisk {
    barcode: string
    productName: string
    branchName: string
    stockQuantity: number
    expiryDate: string
    daysRemaining: number
}

export interface StockRisk {
    barcode: string
    productName: string
    branchName: string
    stockQuantity: number
    minQtyAlert: number
}

interface AiSmartAlertsProps {
    expiryRisks: ExpiryRisk[]
    stockRisks: StockRisk[]
}

export function AiSmartAlerts({ expiryRisks = [], stockRisks = [] }: AiSmartAlertsProps) {
    const { language } = useLanguage()
    const isRtl = language === "ar"

    return (
        <Card className="col-span-1 lg:col-span-3 transition-all duration-200 border-indigo-500/20 bg-gradient-to-br from-card to-indigo-950/5 text-right rtl:text-right" dir={isRtl ? "rtl" : "ltr"}>
            <CardHeader className="pb-3">
                <div className="flex items-center gap-2">
                    <div className="p-1.5 rounded-lg bg-indigo-500/10 text-indigo-500 animate-pulse">
                        <ShieldAlert className="h-5 w-5" />
                    </div>
                    <div>
                        <CardTitle className="text-base md:text-lg flex items-center gap-2">
                            {isRtl ? "تنبيهات المخزون الذكية" : "AI-Driven Smart Alerts"}
                        </CardTitle>
                        <CardDescription className="text-xs md:text-sm">
                            {isRtl ? "تحذيرات وتنبؤات حية لحالة المخزون والأدوية" : "Real-time inventory predictive warnings"}
                        </CardDescription>
                    </div>
                </div>
            </CardHeader>
            <CardContent>
                <Tabs defaultValue="expiry" className="w-full">
                    <TabsList className="grid w-full grid-cols-2 h-9 mb-3 bg-muted/60">
                        <TabsTrigger value="expiry" className="text-xs">
                            {isRtl ? `مخاطر الصلاحية (${expiryRisks.length})` : `Expiry Risk (${expiryRisks.length})`}
                        </TabsTrigger>
                        <TabsTrigger value="stock" className="text-xs">
                            {isRtl ? `نواقص المخزون (${stockRisks.length})` : `Low Stock (${stockRisks.length})`}
                        </TabsTrigger>
                    </TabsList>

                    {/* Expiry Risk Alerts */}
                    <TabsContent value="expiry" className="space-y-3 focus-visible:outline-none">
                        <div className="max-h-[300px] overflow-y-auto pr-1 pl-1 space-y-2.5">
                            {expiryRisks.length === 0 ? (
                                <div className="text-center py-12 text-muted-foreground text-sm flex flex-col items-center gap-2">
                                    <Calendar className="h-8 w-8 opacity-40 text-green-500" />
                                    <span>{isRtl ? "جميع التشغيلات في الحدود الآمنة للصلاحية." : "All batches have safe expiry horizons."}</span>
                                </div>
                            ) : (
                                expiryRisks.map((risk, index) => {
                                    const isCritical = risk.daysRemaining <= 30
                                    return (
                                        <div
                                            key={`${risk.barcode}-${index}`}
                                            className="flex flex-col p-3 border rounded-lg hover:border-indigo-500/30 bg-card/50 hover:bg-indigo-500/5 transition-all duration-200 group"
                                        >
                                            <div className="flex items-start justify-between gap-2 mb-1.5">
                                                <div className="min-w-0 text-left rtl:text-right">
                                                    <h4 className="font-semibold text-sm truncate text-foreground group-hover:text-indigo-500 transition-colors">
                                                        {risk.productName}
                                                    </h4>
                                                    <p className="text-xs font-mono text-muted-foreground">
                                                        {isRtl ? "الباركود:" : "Barcode:"} {risk.barcode}
                                                    </p>
                                                </div>
                                                <Badge
                                                    variant="outline"
                                                    className={
                                                        isCritical
                                                            ? "bg-red-500/10 text-red-500 border-red-500/20"
                                                            : "bg-yellow-500/10 text-yellow-600 border-yellow-500/20"
                                                    }
                                                >
                                                    {isRtl ? `متبقي ${risk.daysRemaining} يوم` : `${risk.daysRemaining} days left`}
                                                </Badge>
                                            </div>

                                            <div className="flex items-center justify-between text-xs text-muted-foreground mt-1">
                                                <span className="bg-muted px-2 py-0.5 rounded text-[10px]">
                                                    {risk.branchName}
                                                </span>
                                                <span className="flex items-center gap-1">
                                                    {isRtl ? "الرصيد الحالي:" : "Stock:"} <strong className="text-foreground font-mono">{risk.stockQuantity}</strong> {isRtl ? "وحدة" : "units"}
                                                </span>
                                            </div>
                                        </div>
                                    )
                                })
                            )}
                        </div>
                    </TabsContent>

                    {/* Low Stock Alerts */}
                    <TabsContent value="stock" className="space-y-3 focus-visible:outline-none">
                        <div className="max-h-[300px] overflow-y-auto pr-1 pl-1 space-y-2.5">
                            {stockRisks.length === 0 ? (
                                <div className="text-center py-12 text-muted-foreground text-sm flex flex-col items-center gap-2">
                                    <Package className="h-8 w-8 opacity-40 text-green-500" />
                                    <span>{isRtl ? "جميع الفروع لديها كميات كافية وآمنة من المخزون." : "All branches have optimal stock levels."}</span>
                                </div>
                            ) : (
                                stockRisks.map((risk, index) => {
                                    const ratio = risk.stockQuantity / (risk.minQtyAlert || 10)
                                    const isCritical = ratio <= 0.3
                                    return (
                                        <div
                                            key={`${risk.barcode}-${index}`}
                                            className="flex flex-col p-3 border rounded-lg hover:border-amber-500/30 bg-card/50 hover:bg-amber-500/5 transition-all duration-200 group"
                                        >
                                            <div className="flex items-start justify-between gap-2 mb-1.5">
                                                <div className="min-w-0 text-left rtl:text-right">
                                                    <h4 className="font-semibold text-sm truncate text-foreground group-hover:text-amber-500 transition-colors">
                                                        {risk.productName}
                                                    </h4>
                                                    <p className="text-xs font-mono text-muted-foreground">
                                                        {isRtl ? "الباركود:" : "Barcode:"} {risk.barcode}
                                                    </p>
                                                </div>
                                                <Badge
                                                    variant="outline"
                                                    className={
                                                        isCritical
                                                            ? "bg-red-500/10 text-red-500 border-red-500/20"
                                                            : "bg-amber-500/10 text-amber-600 border-amber-500/20 animate-pulse"
                                                    }
                                                >
                                                    {isCritical 
                                                        ? (isRtl ? "نقص حرج" : "Critical Low") 
                                                        : (isRtl ? "تحت حد الأمان" : "Restock")
                                                    }
                                                </Badge>
                                            </div>

                                            <div className="flex items-center justify-between text-xs text-muted-foreground mt-1">
                                                <span className="bg-muted px-2 py-0.5 rounded text-[10px]">
                                                    {risk.branchName}
                                                </span>
                                                <span className="flex items-center gap-1.5">
                                                    {isRtl ? "المخزون الحالي:" : "Stock:"} <strong className={`font-mono ${isCritical ? "text-red-500 font-bold" : "text-foreground"}`}>{risk.stockQuantity}</strong> / {risk.minQtyAlert} {isRtl ? "وحدة" : "units"}
                                                </span>
                                            </div>
                                        </div>
                                    )
                                })
                            )}
                        </div>
                    </TabsContent>
                </Tabs>
            </CardContent>
        </Card>
    )
}
