"use client"

import React from "react"
import { useLanguage } from "@/components/providers/language-provider"
import { Badge } from "@/components/ui/badge"
import { 
    Network, 
    Cpu, 
    ShieldCheck, 
    WifiOff, 
    AlertTriangle, 
    BarChart3,
    ArrowUpRight,
    CheckCircle2
} from "lucide-react"

export default function FeaturesSection() {
    const { language, t } = useLanguage()
    const isAr = language === "ar"

    // Configuration for the 6 core system capabilities
    const featuresList = [
        {
            key: "branch_sync",
            icon: Network,
            colorClass: "emerald",
            glowClass: "group-hover:border-emerald-500/30 group-hover:bg-emerald-500/[0.02] group-hover:shadow-emerald-500/5",
            iconBg: "bg-emerald-500/10 text-emerald-600 dark:text-emerald-400 dark:bg-emerald-500/15",
            details: isAr 
                ? ["مزامنة فورية للمخازن", "تحديث تلقائي للبيانات", "مراقبة الفروع من لوحة واحدة"] 
                : ["Instant stock syncing", "Automatic data updates", "Monitor branches from one hub"]
        },
        {
            key: "pos",
            icon: Cpu,
            colorClass: "cyan",
            glowClass: "group-hover:border-cyan-500/30 group-hover:bg-cyan-500/[0.02] group-hover:shadow-cyan-500/5",
            iconBg: "bg-cyan-500/10 text-cyan-600 dark:text-cyan-400 dark:bg-cyan-500/15",
            details: isAr 
                ? ["دعم الباركود والبحث الذكي", "معالجة المعاملات بكسور الثانية", "إيصالات إلكترونية وفواتير حرارية"] 
                : ["Barcode & smart search support", "Sub-second transaction processing", "E-receipts & thermal invoicing"]
        },
        {
            key: "treasury",
            icon: ShieldCheck,
            colorClass: "violet",
            glowClass: "group-hover:border-violet-500/30 group-hover:bg-violet-500/[0.02] group-hover:shadow-violet-500/5",
            iconBg: "bg-violet-500/10 text-violet-600 dark:text-violet-400 dark:bg-violet-500/15",
            details: isAr 
                ? ["إقفال يومي ومطابقة النقدية", "تسجيل فوري للمصروفات والعهد", "أذونات صرف وموافقة إدارية"] 
                : ["Daily ledger matching & closing", "Instant expense & safe tracking", "Multi-level safe authorizations"]
        },
        {
            key: "offline",
            icon: WifiOff,
            colorClass: "amber",
            glowClass: "group-hover:border-amber-500/30 group-hover:bg-amber-500/[0.02] group-hover:shadow-amber-500/5",
            iconBg: "bg-amber-500/10 text-amber-600 dark:text-amber-400 dark:bg-amber-500/15",
            details: isAr 
                ? ["مواصلة المبيعات بدون إنترنت", "تخزين محلي مشفر وآمن", "مزامنة تلقائية صامتة فور الاتصال"] 
                : ["Continue sales without internet", "Encrypted & secure local storage", "Silent auto-sync upon reconnection"]
        },
        {
            key: "expiry",
            icon: AlertTriangle,
            colorClass: "rose",
            glowClass: "group-hover:border-rose-500/30 group-hover:bg-rose-500/[0.02] group-hover:shadow-rose-500/5",
            iconBg: "bg-rose-500/10 text-rose-600 dark:text-rose-400 dark:bg-rose-500/15",
            details: isAr 
                ? ["تنبيهات الصلاحيات المنتهية", "تحديد نواقص الأدوية والكميات الحادة", "إشعارات فورية وإرسال مباشر"] 
                : ["Near-expiry batch alerts", "Low-stock & out-of-stock indicators", "Push notifications & direct messaging"]
        },
        {
            key: "analytics",
            icon: BarChart3,
            colorClass: "blue",
            glowClass: "group-hover:border-blue-500/30 group-hover:bg-blue-500/[0.02] group-hover:shadow-blue-500/5",
            iconBg: "bg-blue-500/10 text-blue-600 dark:text-blue-400 dark:bg-blue-500/15",
            details: isAr 
                ? ["كشوفات الأرباح والخسائر", "مراقبة متوسط قيمة السلة الشرائية", "مقارنة أداء المبيعات بين الفروع"] 
                : ["Profit & loss statements", "Average cart value tracking", "Compare sales across branches"]
        }
    ]

    return (
        <section id="features" className="relative py-20 px-4 md:px-8 bg-background overflow-hidden border-t border-border/20">
            {/* Ambient Background Glows */}
            <div className="absolute top-1/3 left-1/4 -translate-x-1/2 w-[400px] h-[400px] bg-emerald-500/5 dark:bg-emerald-500/10 rounded-full blur-3xl pointer-events-none" />
            <div className="absolute bottom-1/3 right-1/4 translate-x-1/2 w-[400px] h-[400px] bg-indigo-500/5 dark:bg-indigo-500/10 rounded-full blur-3xl pointer-events-none" />

            <div className="max-w-7xl mx-auto relative z-10">
                {/* Header Info */}
                <div className="text-center max-w-3xl mx-auto space-y-4 mb-16">
                    <Badge variant="outline" className="px-3 py-1 border-emerald-500/30 bg-emerald-500/10 text-emerald-600 dark:text-emerald-400 font-semibold tracking-wider uppercase text-xs">
                        {t("features.badge")}
                    </Badge>
                    <h2 className="text-3xl md:text-5xl font-extrabold tracking-tight text-foreground leading-tight">
                        {t("features.title")}
                    </h2>
                    <p className="text-base sm:text-lg text-muted-foreground leading-relaxed">
                        {t("features.subtitle")}
                    </p>
                </div>

                {/* 3-Column Responsive Features Grid */}
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
                    {featuresList.map((feat) => {
                        const IconComponent = feat.icon
                        
                        return (
                            <div 
                                key={feat.key}
                                className={`group relative flex flex-col justify-between p-8 rounded-2xl border border-border/50 bg-card/60 dark:bg-zinc-900/40 backdrop-blur-xl hover:scale-[1.03] hover:border-border transition-all duration-350 shadow-md ${feat.glowClass}`}
                            >
                                <div>
                                    {/* Top Card Bar with Custom Icon & Badge */}
                                    <div className="flex items-center justify-between mb-6">
                                        <div className={`w-12 h-12 rounded-xl flex items-center justify-center transition-transform duration-300 group-hover:scale-110 group-hover:rotate-3 ${feat.iconBg}`}>
                                            <IconComponent className="w-6 h-6" />
                                        </div>
                                        <Badge 
                                            variant="secondary" 
                                            className="px-2.5 py-0.5 text-[10px] font-bold tracking-wide rounded-full opacity-80 group-hover:opacity-100 transition-opacity bg-muted text-muted-foreground"
                                        >
                                            {t(`features.${feat.key}.badge`)}
                                        </Badge>
                                    </div>

                                    {/* Feature Title */}
                                    <h3 className="text-xl font-bold text-foreground mb-3 flex items-center gap-1.5 text-start font-sans">
                                        {t(`features.${feat.key}.title`)}
                                        <ArrowUpRight className="w-4 h-4 text-muted-foreground opacity-0 group-hover:opacity-100 group-hover:translate-x-0.5 group-hover:-translate-y-0.5 transition-all duration-300" />
                                    </h3>

                                    {/* Feature Description */}
                                    <p className="text-sm text-muted-foreground leading-relaxed text-start mb-6">
                                        {t(`features.${feat.key}.desc`)}
                                    </p>
                                </div>

                                {/* Dynamic Highlights list */}
                                <div className="border-t border-border/40 pt-5 mt-auto">
                                    <ul className="space-y-2.5">
                                        {feat.details.map((detail, idx) => (
                                            <li key={idx} className="flex items-center gap-2.5 text-xs text-muted-foreground/90 font-medium text-start">
                                                <CheckCircle2 className="w-4 h-4 text-emerald-500 flex-shrink-0" />
                                                <span>{detail}</span>
                                            </li>
                                        ))}
                                    </ul>
                                </div>
                            </div>
                        )
                    })}
                </div>
            </div>
        </section>
    )
}
