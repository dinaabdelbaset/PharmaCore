"use client"

import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { Badge } from "@/components/ui/badge"
import { Progress } from "@/components/ui/progress"
import { TrendingUp, TrendingDown, Wallet, BrainCircuit, ArrowRightLeft, UserCheck } from "lucide-react"
import { useLanguage } from "@/components/providers/language-provider"

interface ForecastItem {
    productName: string
    productNameAr?: string
    changePercent: number
    confidence: number
    action: string
    actionAr?: string
    recommendedQty: number
}

interface CustodyItem {
    employeeName: string
    branchName: string
    branchNameAr?: string
    cashAmount: number
    status: "Open" | "Closed"
}

interface AiDemandForecastProps {
    isSimulated?: boolean
}

export function AiDemandForecast({ isSimulated = false }: AiDemandForecastProps) {
    const { language } = useLanguage()
    const isRtl = language === "ar"

    const fastMoving: ForecastItem[] = isSimulated ? [
        { productName: "Panadol Extra 500mg", productNameAr: "بنادول اكسترا 500 مجم", changePercent: 24, confidence: 96, action: "Restock Immediately", actionAr: "إعادة طلب فورية", recommendedQty: 150 },
        { productName: "Augmentin 1g Tablets", productNameAr: "أوجمنتين 1 جرام أقراص", changePercent: 18, confidence: 91, action: "Restock", actionAr: "إعادة طلب", recommendedQty: 60 },
        { productName: "Concor 5mg Tablets", productNameAr: "كونكور 5 مجم أقراص", changePercent: 8, confidence: 88, action: "Normal Restock", actionAr: "إعادة طلب اعتيادية", recommendedQty: 40 },
        { productName: "Gliclazide 60mg (Diamicron)", productNameAr: "دياميكرون 60 مجم", changePercent: 12, confidence: 85, action: "Restock", actionAr: "إعادة طلب", recommendedQty: 50 }
    ] : []

    const slowMoving: ForecastItem[] = isSimulated ? [
        { productName: "Cataflam 50mg Tablets", productNameAr: "كاتافلام 50 مجم أقراص", changePercent: -15, confidence: 78, action: "Transfer Excess to Heliopolis", actionAr: "نقل الزائد لفرع مصر الجديدة", recommendedQty: -30 },
        { productName: "Brufen 400mg Syrup", productNameAr: "بروفين شراب 400 مجم", changePercent: -28, confidence: 82, action: "Hold Purchases", actionAr: "إيقاف مؤقت للشراء", recommendedQty: 0 },
        { productName: "Claritin 10mg Tablets", productNameAr: "كلاريتين 10 مجم أقراص", changePercent: -10, confidence: 75, action: "Liquidate Batches", actionAr: "تصريف التشغيلات الحالية", recommendedQty: -15 }
    ] : []

    const cashCustody: CustodyItem[] = isSimulated ? [
        { employeeName: "أحمد حسن", branchName: "Main Branch", branchNameAr: "فرع القاهرة الرئيسي", cashAmount: 1250.00, status: "Open" },
        { employeeName: "سارة محمد", branchName: "Alexandria Branch", branchNameAr: "فرع الإسكندرية", cashAmount: 840.00, status: "Closed" },
        { employeeName: "محمود علي", branchName: "Giza Branch", branchNameAr: "فرع الجيزة", cashAmount: 430.00, status: "Open" },
        { employeeName: "حسن إبراهيم", branchName: "Heliopolis Branch", branchNameAr: "فرع مصر الجديدة", cashAmount: 1680.00, status: "Open" }
    ] : []

    return (
        <Card className="col-span-1 lg:col-span-4 transition-all duration-200 border-violet-500/20 bg-gradient-to-br from-card to-violet-950/5 text-right rtl:text-right" dir={isRtl ? "rtl" : "ltr"}>
            <CardHeader className="pb-3">
                <div className="flex items-center gap-2">
                    <div className="p-1.5 rounded-lg bg-violet-500/10 text-violet-500">
                        <BrainCircuit className="h-5 w-5" />
                    </div>
                    <div>
                        <CardTitle className="text-base md:text-lg">
                            {isRtl ? "تحسينات المخزون والعهد بالذكاء الاصطناعي" : "AI Stock & Custody Optimizations"}
                        </CardTitle>
                        <CardDescription className="text-xs md:text-sm">
                            {isRtl ? "توصيات ذكية لإعادة الطلب ومراقبة عهد الكاشيرات النشطة للفروع" : "Predictive inventory restocking recommendations & cashier safe box custody (العهد)"}
                        </CardDescription>
                    </div>
                </div>
            </CardHeader>
            <CardContent>
                <Tabs defaultValue="demand" className="w-full">
                    <TabsList className="grid w-full grid-cols-3 h-9 mb-3 bg-muted/60">
                        <TabsTrigger value="demand" className="text-xs">
                            {isRtl ? "مبيعات متسارعة (+%)" : "High Demand (+%)"}
                        </TabsTrigger>
                        <TabsTrigger value="excess" className="text-xs">
                            {isRtl ? "مخزون راكد (-%)" : "Excess Stock (-%)"}
                        </TabsTrigger>
                        <TabsTrigger value="custody" className="text-xs">
                            {isRtl ? "عهد الكاشيرات (الخزينة)" : "Cashier Custody (العهد)"}
                        </TabsTrigger>
                    </TabsList>

                    {/* High Demand Restock */}
                    <TabsContent value="demand" className="space-y-3 focus-visible:outline-none">
                        <div className="max-h-[300px] overflow-y-auto pr-1 pl-1 space-y-3">
                            {fastMoving.length === 0 ? (
                                <div className="text-center py-12 text-muted-foreground text-sm flex flex-col items-center justify-center gap-2">
                                    <TrendingUp className="h-8 w-8 opacity-40 text-violet-500 animate-pulse" />
                                    <span>{isRtl ? "لا توجد توقعات زيادة طلب بعد. قم بمزامنة مبيعات الكاشير لتفعيل الذكاء الاصطناعي." : "No high demand trends predicted yet. Sync sales transactions to enable AI predictions."}</span>
                                </div>
                            ) : (
                                fastMoving.map((item, idx) => (
                                    <div
                                        key={idx}
                                        className="p-3 border rounded-lg hover:border-violet-500/30 bg-card/50 hover:bg-violet-500/5 transition-all duration-200"
                                    >
                                        <div className="flex items-start justify-between gap-2 mb-2">
                                            <div className="text-left rtl:text-right">
                                                <h4 className="font-semibold text-sm text-foreground">
                                                    {isRtl ? (item.productNameAr || item.productName) : item.productName}
                                                </h4>
                                                <div className="flex items-center gap-1.5 text-xs text-green-500 font-medium mt-0.5">
                                                    <TrendingUp className="h-3.5 w-3.5 animate-pulse" />
                                                    <span>{isRtl ? `ارتفاع موسمي متوقع +${item.changePercent}%` : `+${item.changePercent}% seasonal spike forecast`}</span>
                                                </div>
                                            </div>
                                            <Badge className="bg-green-500/15 text-green-500 hover:bg-green-500/25 border-none font-mono">
                                                +{item.recommendedQty} {isRtl ? "عبوة" : "packs"}
                                            </Badge>
                                        </div>
                                        <div className="space-y-1.5">
                                            <div className="flex items-center justify-between text-xs text-muted-foreground">
                                                <span>{isRtl ? "التوصية:" : "Recommendation:"} <strong className="text-foreground">{isRtl ? (item.actionAr || item.action) : item.action}</strong></span>
                                                <span>{isRtl ? "دقة التوقع:" : "AI Confidence:"} {item.confidence}%</span>
                                            </div>
                                            <Progress value={item.confidence} className="h-1 bg-muted" />
                                        </div>
                                    </div>
                                ))
                            )}
                        </div>
                    </TabsContent>

                    {/* Excess Stock & Transfers */}
                    <TabsContent value="excess" className="space-y-3 focus-visible:outline-none">
                        <div className="max-h-[300px] overflow-y-auto pr-1 pl-1 space-y-3">
                            {slowMoving.length === 0 ? (
                                <div className="text-center py-12 text-muted-foreground text-sm flex flex-col items-center justify-center gap-2">
                                    <TrendingDown className="h-8 w-8 opacity-40 text-amber-500 animate-pulse" />
                                    <span>{isRtl ? "لا توجد أي تحذيرات مخزون راكد زائد حالياً." : "No excess stock warnings predicted yet."}</span>
                                </div>
                            ) : (
                                slowMoving.map((item, idx) => (
                                    <div
                                        key={idx}
                                        className="p-3 border rounded-lg hover:border-amber-500/30 bg-card/50 hover:bg-amber-500/5 transition-all duration-200"
                                    >
                                        <div className="flex items-start justify-between gap-2 mb-2">
                                            <div className="text-left rtl:text-right">
                                                <h4 className="font-semibold text-sm text-foreground">
                                                    {isRtl ? (item.productNameAr || item.productName) : item.productName}
                                                </h4>
                                                <div className="flex items-center gap-1.5 text-xs text-amber-500 font-medium mt-0.5">
                                                    <TrendingDown className="h-3.5 w-3.5" />
                                                    <span>{isRtl ? `تباطؤ في حركة البيع متوقع ${item.changePercent}%` : `${item.changePercent}% lower velocity forecast`}</span>
                                                </div>
                                            </div>
                                            {item.recommendedQty !== 0 ? (
                                                <Badge className="bg-amber-500/15 text-amber-600 hover:bg-amber-500/25 border-none font-mono">
                                                    {isRtl ? `نقل ${Math.abs(item.recommendedQty)} عبوة` : `Transfer ${Math.abs(item.recommendedQty)} packs`}
                                                </Badge>
                                            ) : (
                                                <Badge variant="outline" className="text-muted-foreground text-xs">
                                                    {isRtl ? "إيقاف الشراء" : "Hold Purchases"}
                                                </Badge>
                                            )}
                                        </div>
                                        <div className="space-y-1.5">
                                            <div className="flex items-center justify-between text-xs text-muted-foreground">
                                                <span className="flex items-center gap-1">
                                                    <ArrowRightLeft className="h-3 w-3 text-primary animate-pulse" />
                                                    <span>{isRtl ? "الإجراء:" : "Action:"} <strong className="text-foreground">{isRtl ? (item.actionAr || item.action) : item.action}</strong></span>
                                                </span>
                                                <span>{isRtl ? "دقة التوقع:" : "AI Confidence:"} {item.confidence}%</span>
                                            </div>
                                            <Progress value={item.confidence} className="h-1 bg-muted" />
                                        </div>
                                    </div>
                                ))
                            )}
                        </div>
                    </TabsContent>

                    {/* Cashier Custody (العهد) */}
                    <TabsContent value="custody" className="space-y-3 focus-visible:outline-none">
                        <div className="max-h-[300px] overflow-y-auto pr-1 pl-1 space-y-3">
                            {cashCustody.length === 0 ? (
                                <div className="text-center py-12 text-muted-foreground text-sm flex flex-col items-center justify-center gap-2">
                                    <Wallet className="h-8 w-8 opacity-40 text-indigo-500 animate-pulse" />
                                    <span>{isRtl ? "لا توجد أي فترات عهدة نشطة حالياً للفروع." : "No active shifts or custody logs registered."}</span>
                                </div>
                            ) : (
                                cashCustody.map((cust, idx) => (
                                    <div
                                        key={idx}
                                        className="flex items-center justify-between p-3 border rounded-lg hover:border-indigo-500/30 bg-card/50 hover:bg-indigo-500/5 transition-all duration-200"
                                    >
                                        <div className="flex items-center gap-2.5">
                                            <div className="p-2 rounded-full bg-indigo-500/10 text-indigo-500">
                                                <UserCheck className="h-4 w-4" />
                                            </div>
                                            <div className="text-left rtl:text-right">
                                                <h4 className="font-semibold text-sm text-foreground">{cust.employeeName}</h4>
                                                <p className="text-xs text-muted-foreground">{isRtl ? (cust.branchNameAr || cust.branchName) : cust.branchName}</p>
                                            </div>
                                        </div>
                                        <div className="text-right rtl:text-left">
                                            <div className="flex items-center gap-1 text-sm font-bold text-foreground font-mono">
                                                <Wallet className="h-3.5 w-3.5 text-green-500" />
                                                {cust.cashAmount.toLocaleString(undefined, { minimumFractionDigits: 2 })} {isRtl ? "ج.م" : "EGP"}
                                            </div>
                                            <span className={`text-[9px] font-bold ${cust.status === "Open" ? "text-green-500" : "text-muted-foreground"}`}>
                                                {cust.status === "Open" 
                                                    ? (isRtl ? "● وردية مفتوحة" : "● Active Shift") 
                                                    : (isRtl ? "مغلقة (تمت التسوية)" : "Closed (Balanced)")
                                                }
                                            </span>
                                        </div>
                                    </div>
                                ))
                            )}
                        </div>
                    </TabsContent>
                </Tabs>
            </CardContent>
        </Card>
    )
}
