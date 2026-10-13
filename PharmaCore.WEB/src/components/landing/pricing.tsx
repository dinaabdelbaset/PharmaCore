"use client"

import React, { useState } from "react"
import { useLanguage } from "@/components/providers/language-provider"
import { Button } from "@/components/ui/button"
import { Badge } from "@/components/ui/badge"
import { Card, CardHeader, CardTitle, CardDescription, CardContent, CardFooter } from "@/components/ui/card"
import { Dialog, DialogContent, DialogClose } from "@/components/ui/dialog"
import { Check, Sparkles, Lock, CreditCard, Building2, Mail, User, ShieldCheck } from "lucide-react"
import { toast } from "sonner"

export default function PricingSection() {
    const { language } = useLanguage()
    const isAr = language === "ar"

    // Arabic dictionary
    const arText = {
        badge: "حاسبة الأسعار الذكية",
        title: "خطط أسعار مرنة تفاعلية",
        subtitle: "حدد متطلبات صيدليتك بدقة واحسب تكلفتك المخصصة. قم بتفعيل الميزات التي تحتاجها فقط.",
        billingMonthly: "دفع شهري",
        billingYearly: "دفع سنوي",
        save20: "وفر 20%",
        branchesLabel: "عدد الفروع النشطة",
        devicesLabel: "أجهزة الكاشير ونقاط البيع",
        addonsLabel: "الميزات المتقدمة والربط",
        summaryTitle: "تفاصيل باقتك المخصصة",
        summaryDesc: "بناءً على خياراتك المحددة:",
        basePriceLabel: "الباقة الأساسية (فرع + جهاز واحد)",
        branchesCostLabel: "تكلفة الفروع الإضافية",
        devicesCostLabel: "تكلفة الأجهزة الإضافية",
        yearlyDiscountLabel: "خصم الدفع السنوي (20%-) ",
        totalLabel: "إجمالي الاشتراك الشهري",
        checkoutBtn: "اشترك الآن بالخطة المخصصة",
        contactSalesBtn: "طلب عرض سعر خاص للشبكات الضخمة",
        billingFrequencyYearly: "سنوياً",
        billingFrequencyMonthly: "شهرياً",
        featureAi: "توقعات الطلب بالذكاء الاصطناعي وتنبيهات ذكية",
        featureAiDesc: "تحليلات تنبؤية متقدمة لحساب نواقص الأدوية والطلبيات المستقبلية.",
        featureSync: "مزامنة لحظية متقدمة بين الفروع",
        featureSyncDesc: "مزامنة لحظية للمخزون وحركة الأدوية والكميات بين جميع الفروع مباشرة.",
        featureApi: "ربط وتكاملات مخصصة (API/EHR)",
        featureApiDesc: "تكامل كامل مع أنظمة المستشفيات والتطبيقات الخارجية والملف الطبي الموحد.",
        featureDb: "خادم قاعدة بيانات سحابي مخصص",
        featureDbDesc: "قاعدة بيانات معزولة تماماً لضمان السرعة القصوى وحماية فائقة للبيانات.",
        featureSupport: "دعم ذو أولوية على مدار الساعة",
        featureSupportDesc: "قناة دعم فني مباشر مخصصة لك لحل أي مشكلات في أي وقت.",
        featureInsurance: "ربط التأمين الطبي الموحد (نفيس/وصيل)",
        featureInsuranceDesc: "تكامل فوري لرفع المطالبات الطبية للشركات والموافقة عليها لحظياً وتقليل نسبة المرفوضات.",
        featureAlerts: "تنبيهات الواتساب والرسائل القصيرة",
        featureAlertsDesc: "إرسال رسائل تلقائية للعملاء لتذكيرهم بمواعيد صرف الأدوية المزمنة أو الفواتير الإلكترونية.",
        featureDelivery: "الربط مع تطبيقات التوصيل السريع",
        featureDeliveryDesc: "ربط مباشر مع منصات التوصيل السريع لجدولة طلبات التوصيل وتتبع السائقين لحظياً.",
        featureLoyalty: "برنامج الولاء ونقاط العملاء",
        featureLoyaltyDesc: "تفعيل نظام النقاط، الكاش باك، والكوبونات المخصصة لزيادة ولاء العملاء وتكرار زياراتهم.",
        featureEcommerce: "المتجر الإلكتروني المتكامل للصيدلية",
        featureEcommerceDesc: "موقع وتطبيق بيع أونلاين صيدلاني خاص بك متصل لحظياً بمخزون الفروع الفعلي.",
        featureOffline: "العمل دون اتصال بالإنترنت الذكي",
        featureOfflineDesc: "تمكين الكاشير من البيع وإصدار الفواتير وطباعتها عند انقطاع الإنترنت بالكامل ومزامنتها لاحقاً.",
        featureBackup: "النسخ الاحتياطي السحابي التلقائي",
        featureBackupDesc: "أخذ نسخ احتياطية مشفرة لقاعدة البيانات كل 6 ساعات وحفظها في خوادم سحابية منفصلة.",
        branchUnit: "فرع",
        deviceUnit: "جهاز",
        monthlySuffix: "/ شهرياً",
        modalCheckoutTitle: "تأكيد الاشتراك المخصص",
        modalCheckoutDesc: "قم بإدخال بيانات الدفع لإتمام اشتراكك المخصص بمبلغ",
        successMsg: "تم الاشتراك بنجاح بالخطة المخصصة! مرحباً بك في فارماكور.",
        validationMsg: "يرجى ملء جميع الحقول المطلوبة",
        checkoutFormEmail: "البريد الإلكتروني للعمل",
        checkoutFormName: "اسم صاحب البطاقة",
        checkoutFormNumber: "رقم البطاقة",
        checkoutFormExpiry: "تاريخ الانتهاء",
        checkoutFormCvv: "الرمز السري",
        secureLabel: "دفع آمن ومشفر 100% (SSL)",
        cancelBtn: "إلغاء",
        loadingBtn: "جاري التحميل...",
        payNowBtn: "ادفع الآن",
        billedAnnually: "* يتم الفوترة سنوياً",
        selectedLabel: "محددة",
        yr: "سنة",
        mo: "شهر",
        checkoutFormNamePlaceholder: "محمد أحمد",
        enterpriseContactMsg: "سيتم التواصل معك فوراً من قطاع مبيعات المؤسسات."
    }

    // English dictionary
    const enText = {
        badge: "Smart Pricing Calculator",
        title: "Flexible Interactive Pricing",
        subtitle: "Configure your exact pharmacy requirements and calculate your custom cost. Only pay for what you actually use.",
        billingMonthly: "Monthly Billing",
        billingYearly: "Yearly Billing",
        save20: "Save 20%",
        branchesLabel: "Number of Active Branches",
        devicesLabel: "Cashier Terminals & POS Devices",
        addonsLabel: "Advanced Feature Add-ons",
        summaryTitle: "Your Custom Plan Summary",
        summaryDesc: "Based on your selected configurations:",
        basePriceLabel: "Base Plan (1 Branch + 1 Device)",
        branchesCostLabel: "Extra Branches Cost",
        devicesCostLabel: "Extra Devices Cost",
        yearlyDiscountLabel: "Yearly Billing Discount (20% off)",
        totalLabel: "Total Monthly Price",
        checkoutBtn: "Subscribe Now to Custom Plan",
        contactSalesBtn: "Contact Sales for Large Chains",
        billingFrequencyYearly: "yearly",
        billingFrequencyMonthly: "monthly",
        featureAi: "AI Demand Forecasting & Smart Alerts",
        featureAiDesc: "Predictive intelligence to optimize medicine stock, order cycles, and detect expiry risks.",
        featureSync: "Real-time Multi-Branch Synchronization",
        featureSyncDesc: "Synchronize stock movement, transfers, and inventory across all locations automatically.",
        featureApi: "Custom API & EHR/Hospital Integrations",
        featureApiDesc: "Direct connection with hospital EHRs, delivery apps, or proprietary legacy software.",
        featureDb: "Dedicated Cloud Database Instance",
        featureDbDesc: "Isolated cloud DB resources for maximum transaction speed and enterprise security.",
        featureSupport: "Priority 24/7 Phone & Email Support",
        featureSupportDesc: "Direct VIP helpline support from senior engineers with guaranteed SLA response times.",
        featureInsurance: "Insurance Claims Integration (Nphies/Waseel)",
        featureInsuranceDesc: "Direct integration with insurance portals (Nphies/Waseel) to process claims instantly and minimize rejection rates.",
        featureAlerts: "WhatsApp & SMS Automated Alerts",
        featureAlertsDesc: "Send automated reminders for chronic refills, e-receipts, or patient notifications directly to mobile.",
        featureDelivery: "Delivery Gateways Integration",
        featureDeliveryDesc: "Direct connection to express delivery providers for automated order dispatch and real-time courier tracking.",
        featureLoyalty: "CRM & Loyalty Reward Program",
        featureLoyaltyDesc: "Reward points, cashback systems, and customer loyalty tiers to maximize retention and sales.",
        featureEcommerce: "E-commerce Pharmacy Storefront",
        featureEcommerceDesc: "Your own online pharmacy website and app synced in real-time with physical branch inventory.",
        featureOffline: "Smart Offline Mode & Local Sync",
        featureOfflineDesc: "Continue processing sales and printing receipts during internet outages. Data syncs automatically once online.",
        featureBackup: "Automated Cloud Backups (6h Cycle)",
        featureBackupDesc: "Encrypted database backups captured every 6 hours and stored in isolated cloud server buckets.",
        branchUnit: "branch(es)",
        deviceUnit: "device(s)",
        monthlySuffix: "/ month",
        modalCheckoutTitle: "Confirm Custom Subscription",
        modalCheckoutDesc: "Enter your payment details to complete your custom subscription for",
        successMsg: "Subscription completed successfully! Welcome to PharmaCore.",
        validationMsg: "Please fill in all required fields",
        checkoutFormEmail: "Work Email Address",
        checkoutFormName: "Cardholder Name",
        checkoutFormNumber: "Card Number",
        checkoutFormExpiry: "Expiry Date (MM/YY)",
        checkoutFormCvv: "CVV",
        secureLabel: "100% Secure SSL Encrypted Payment",
        cancelBtn: "Cancel",
        loadingBtn: "Loading...",
        payNowBtn: "Pay Now",
        billedAnnually: "* Billed Annually",
        selectedLabel: "selected",
        yr: "yr",
        mo: "mo",
        checkoutFormNamePlaceholder: "Mohammed Al-Fulan",
        enterpriseContactMsg: "Our Enterprise sales desk will contact you shortly."
    }

    const labels = isAr ? arText : enText

    // State variables for configuration
    const [billingCycle, setBillingCycle] = useState<"monthly" | "yearly">("yearly")
    const [branches, setBranches] = useState<number>(1)
    const [devices, setDevices] = useState<number>(1)

    // Toggles for features
    const [featureAi, setFeatureAi] = useState<boolean>(true)
    const [featureSync, setFeatureSync] = useState<boolean>(true)
    const [featureApi, setFeatureApi] = useState<boolean>(false)
    const [featureDb, setFeatureDb] = useState<boolean>(false)
    const [featureSupport, setFeatureSupport] = useState<boolean>(false)
    const [featureInsurance, setFeatureInsurance] = useState<boolean>(false)
    const [featureAlerts, setFeatureAlerts] = useState<boolean>(false)
    const [featureDelivery, setFeatureDelivery] = useState<boolean>(false)
    const [featureLoyalty, setFeatureLoyalty] = useState<boolean>(false)
    const [featureEcommerce, setFeatureEcommerce] = useState<boolean>(false)
    const [featureOffline, setFeatureOffline] = useState<boolean>(false)
    const [featureBackup, setFeatureBackup] = useState<boolean>(false)

    // Price components
    const BASE_PRICE = 19
    const BRANCH_PRICE = 10
    const DEVICE_PRICE = 5

    const addonPrices = {
        ai: 29,
        sync: 39,
        api: 49,
        db: 79,
        support: 19,
        insurance: 59,
        alerts: 19,
        delivery: 29,
        loyalty: 15,
        ecommerce: 69,
        offline: 25,
        backup: 12
    }

    // Dynamic cost calculations
    const baseCost = BASE_PRICE
    const branchesCost = (branches - 1) * BRANCH_PRICE
    const devicesCost = (devices - 1) * DEVICE_PRICE
    const addonsCost = 
        (featureAi ? addonPrices.ai : 0) +
        (featureSync ? addonPrices.sync : 0) +
        (featureApi ? addonPrices.api : 0) +
        (featureDb ? addonPrices.db : 0) +
        (featureSupport ? addonPrices.support : 0) +
        (featureInsurance ? addonPrices.insurance : 0) +
        (featureAlerts ? addonPrices.alerts : 0) +
        (featureDelivery ? addonPrices.delivery : 0) +
        (featureLoyalty ? addonPrices.loyalty : 0) +
        (featureEcommerce ? addonPrices.ecommerce : 0) +
        (featureOffline ? addonPrices.offline : 0) +
        (featureBackup ? addonPrices.backup : 0)

    const selectedAddonsCount = 
        (featureAi ? 1 : 0) +
        (featureSync ? 1 : 0) +
        (featureApi ? 1 : 0) +
        (featureDb ? 1 : 0) +
        (featureSupport ? 1 : 0) +
        (featureInsurance ? 1 : 0) +
        (featureAlerts ? 1 : 0) +
        (featureDelivery ? 1 : 0) +
        (featureLoyalty ? 1 : 0) +
        (featureEcommerce ? 1 : 0) +
        (featureOffline ? 1 : 0) +
        (featureBackup ? 1 : 0)

    const monthlySubtotal = baseCost + branchesCost + devicesCost + addonsCost
    const yearlyDiscount = billingCycle === "yearly" ? Math.round(monthlySubtotal * 0.20) : 0
    const finalMonthlyPrice = monthlySubtotal - yearlyDiscount

    // State for checkout modal
    const [checkoutOpen, setCheckoutOpen] = useState(false)
    const [isSubmitting, setIsSubmitting] = useState(false)
    const [checkoutForm, setCheckoutForm] = useState({
        name: "",
        email: "",
        cardNumber: "",
        expiry: "",
        cvv: "",
    })

    const handleCheckoutSubmit = (e: React.FormEvent) => {
        e.preventDefault()
        if (!checkoutForm.name || !checkoutForm.email || !checkoutForm.cardNumber || !checkoutForm.expiry || !checkoutForm.cvv) {
            toast.error(labels.validationMsg)
            return
        }

        setIsSubmitting(true)
        setTimeout(() => {
            setIsSubmitting(false)
            setCheckoutOpen(false)
            setCheckoutForm({ name: "", email: "", cardNumber: "", expiry: "", cvv: "" })
            toast.success(labels.successMsg)
        }, 1500)
    }

    // Formatter helpers
    const formatCardNumber = (value: string) => {
        const v = value.replace(/\\s+/g, "").replace(/[^0-9]/gi, "")
        const matches = v.match(/\\d{4,16}/g)
        const match = (matches && matches[0]) || ""
        const parts = []

        for (let i = 0, len = match.length; i < len; i += 4) {
            parts.push(match.substring(i, i + 4))
        }

        return parts.length > 0 ? parts.join(" ") : v
    }

    const formatExpiry = (value: string) => {
        const v = value.replace(/\\s+/g, "").replace(/[^0-9]/gi, "")
        if (v.length >= 2) {
            return `${v.slice(0, 2)}/${v.slice(2, 4)}`
        }
        return v
    }

    const featuresList = [
        {
            id: "ai",
            name: labels.featureAi,
            desc: labels.featureAiDesc,
            price: addonPrices.ai,
            state: featureAi,
            setState: setFeatureAi,
            hasSparkles: true
        },
        {
            id: "sync",
            name: labels.featureSync,
            desc: labels.featureSyncDesc,
            price: addonPrices.sync,
            state: featureSync,
            setState: setFeatureSync
        },
        {
            id: "api",
            name: labels.featureApi,
            desc: labels.featureApiDesc,
            price: addonPrices.api,
            state: featureApi,
            setState: setFeatureApi
        },
        {
            id: "db",
            name: labels.featureDb,
            desc: labels.featureDbDesc,
            price: addonPrices.db,
            state: featureDb,
            setState: setFeatureDb
        },
        {
            id: "support",
            name: labels.featureSupport,
            desc: labels.featureSupportDesc,
            price: addonPrices.support,
            state: featureSupport,
            setState: setFeatureSupport
        },
        {
            id: "insurance",
            name: labels.featureInsurance,
            desc: labels.featureInsuranceDesc,
            price: addonPrices.insurance,
            state: featureInsurance,
            setState: setFeatureInsurance
        },
        {
            id: "alerts",
            name: labels.featureAlerts,
            desc: labels.featureAlertsDesc,
            price: addonPrices.alerts,
            state: featureAlerts,
            setState: setFeatureAlerts
        },
        {
            id: "delivery",
            name: labels.featureDelivery,
            desc: labels.featureDeliveryDesc,
            price: addonPrices.delivery,
            state: featureDelivery,
            setState: setFeatureDelivery
        },
        {
            id: "loyalty",
            name: labels.featureLoyalty,
            desc: labels.featureLoyaltyDesc,
            price: addonPrices.loyalty,
            state: featureLoyalty,
            setState: setFeatureLoyalty
        },
        {
            id: "ecommerce",
            name: labels.featureEcommerce,
            desc: labels.featureEcommerceDesc,
            price: addonPrices.ecommerce,
            state: featureEcommerce,
            setState: setFeatureEcommerce
        },
        {
            id: "offline",
            name: labels.featureOffline,
            desc: labels.featureOfflineDesc,
            price: addonPrices.offline,
            state: featureOffline,
            setState: setFeatureOffline
        },
        {
            id: "backup",
            name: labels.featureBackup,
            desc: labels.featureBackupDesc,
            price: addonPrices.backup,
            state: featureBackup,
            setState: setFeatureBackup
        }
    ]

    return (
        <section className="relative py-20 px-4 md:px-8 bg-zinc-50 dark:bg-zinc-950/40 overflow-hidden" dir={isAr ? "rtl" : "ltr"}>
            {/* Ambient gradients */}
            <div className="absolute top-1/4 left-1/2 -translate-x-1/2 w-[500px] h-[500px] bg-emerald-500/10 dark:bg-emerald-500/5 rounded-full blur-3xl pointer-events-none" />
            <div className="absolute bottom-10 left-10 w-[300px] h-[300px] bg-blue-500/10 dark:bg-blue-500/5 rounded-full blur-3xl pointer-events-none" />

            <div className="max-w-7xl mx-auto relative z-10">
                {/* Header Section */}
                <div className="text-center max-w-3xl mx-auto space-y-4 mb-12">
                    <Badge variant="outline" className="px-3 py-1 border-emerald-500/30 bg-emerald-500/10 text-emerald-600 dark:text-emerald-400 font-semibold tracking-wider uppercase text-xs">
                        {labels.badge}
                    </Badge>
                    <h2 className="text-3xl md:text-5xl font-extrabold tracking-tight text-zinc-900 dark:text-white">
                        {labels.title}
                    </h2>
                    <p className="text-lg text-zinc-600 dark:text-zinc-400 leading-relaxed">
                        {labels.subtitle}
                    </p>
                </div>

                {/* Billing Cycle Switch */}
                <div className="flex justify-center mb-12">
                    <div className="inline-flex p-1 bg-white dark:bg-zinc-900 shadow-md rounded-full border border-zinc-200/50 dark:border-zinc-800/80">
                        <button
                            onClick={() => setBillingCycle("monthly")}
                            className={`px-6 py-2 rounded-full text-sm font-semibold transition-all duration-300 ${
                                billingCycle === "monthly"
                                    ? "bg-zinc-900 dark:bg-zinc-100 text-white dark:text-zinc-900 shadow"
                                    : "text-zinc-600 dark:text-zinc-400 hover:text-zinc-900 dark:hover:text-white"
                            }`}
                        >
                            {labels.billingMonthly}
                        </button>
                        <button
                            onClick={() => setBillingCycle("yearly")}
                            className={`px-6 py-2 rounded-full text-sm font-semibold transition-all duration-300 flex items-center gap-2 ${
                                billingCycle === "yearly"
                                    ? "bg-zinc-900 dark:bg-zinc-100 text-white dark:text-zinc-900 shadow"
                                    : "text-zinc-600 dark:text-zinc-400 hover:text-zinc-900 dark:hover:text-white"
                            }`}
                        >
                            {labels.billingYearly}
                            <span className="px-2 py-0.5 text-[10px] font-extrabold bg-emerald-500/20 text-emerald-600 dark:text-emerald-400 rounded-full border border-emerald-500/30">
                                {labels.save20}
                            </span>
                        </button>
                    </div>
                </div>

                {/* Main Interactive Grid */}
                <div className="grid grid-cols-1 lg:grid-cols-12 gap-8 items-start max-w-6xl mx-auto">
                    
                    {/* Left Panel: Sliders & Checkboxes */}
                    <div className="lg:col-span-7 space-y-8">
                        <Card className="bg-white/60 dark:bg-zinc-900/60 backdrop-blur-xl border border-zinc-200/50 dark:border-zinc-800/80 shadow-lg rounded-2xl p-6 md:p-8 space-y-8">
                            
                            {/* Sliders Block */}
                            <div className="space-y-6">
                                {/* Branches Slider */}
                                <div className="space-y-3">
                                    <div className="flex justify-between items-center">
                                        <label className="text-sm md:text-base font-bold text-zinc-800 dark:text-zinc-200">
                                            {labels.branchesLabel}
                                        </label>
                                        <Badge className="px-3 py-1 bg-emerald-500 text-white font-extrabold text-sm rounded-lg">
                                            {branches} {labels.branchUnit}
                                        </Badge>
                                    </div>
                                    <input
                                        type="range"
                                        min="1"
                                        max="50"
                                        value={branches}
                                        onChange={(e) => setBranches(parseInt(e.target.value))}
                                        className="w-full h-2 bg-zinc-200 dark:bg-zinc-800 rounded-lg appearance-none cursor-pointer accent-emerald-500"
                                    />
                                    <div className="flex justify-between text-xs text-zinc-400 font-semibold px-1">
                                        <span>1</span>
                                        <span>10</span>
                                        <span>20</span>
                                        <span>30</span>
                                        <span>40</span>
                                        <span>50</span>
                                    </div>
                                </div>

                                {/* Devices Slider */}
                                <div className="space-y-3">
                                    <div className="flex justify-between items-center">
                                        <label className="text-sm md:text-base font-bold text-zinc-800 dark:text-zinc-200">
                                            {labels.devicesLabel}
                                        </label>
                                        <Badge className="px-3 py-1 bg-emerald-500 text-white font-extrabold text-sm rounded-lg">
                                            {devices} {labels.deviceUnit}
                                        </Badge>
                                    </div>
                                    <input
                                        type="range"
                                        min="1"
                                        max="100"
                                        value={devices}
                                        onChange={(e) => setDevices(parseInt(e.target.value))}
                                        className="w-full h-2 bg-zinc-200 dark:bg-zinc-800 rounded-lg appearance-none cursor-pointer accent-emerald-500"
                                    />
                                    <div className="flex justify-between text-xs text-zinc-400 font-semibold px-1">
                                        <span>1</span>
                                        <span>20</span>
                                        <span>40</span>
                                        <span>60</span>
                                        <span>80</span>
                                        <span>100</span>
                                    </div>
                                </div>
                            </div>

                            <hr className="border-zinc-200/50 dark:border-zinc-800" />

                            {/* Feature Toggles */}
                            <div className="space-y-4">
                                <h3 className="text-base font-bold text-zinc-800 dark:text-zinc-200 mb-2">
                                    {labels.addonsLabel}
                                </h3>

                                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                    {featuresList.map((item) => (
                                        <div 
                                            key={item.id}
                                            onClick={() => item.setState(!item.state)}
                                            className={`p-3.5 border rounded-xl cursor-pointer transition-all duration-300 flex items-start gap-3.5 ${
                                                item.state 
                                                    ? "border-emerald-500/50 bg-emerald-500/5 dark:bg-emerald-500/10 shadow-md shadow-emerald-500/5" 
                                                    : "border-zinc-200 dark:border-zinc-800/80 bg-zinc-50/50 dark:bg-zinc-900/30 hover:border-zinc-300 dark:hover:border-zinc-700"
                                            }`}
                                        >
                                            <div className="mt-0.5 flex-shrink-0">
                                                <div className={`w-5 h-5 rounded border flex items-center justify-center transition-colors ${item.state ? "bg-emerald-500 border-emerald-500 text-white" : "border-zinc-300 dark:border-zinc-700"}`}>
                                                    {item.state && <Check className="w-3.5 h-3.5 stroke-[3]" />}
                                                </div>
                                            </div>
                                            <div className="flex-grow space-y-0.5">
                                                <div className="flex justify-between items-start gap-2">
                                                    <span className="text-xs md:text-sm font-bold text-zinc-850 dark:text-zinc-150 flex items-center gap-1.5 leading-snug">
                                                        {item.name}
                                                        {item.hasSparkles && <Sparkles className="w-3.5 h-3.5 text-emerald-500 animate-pulse flex-shrink-0" />}
                                                    </span>
                                                    <Badge variant="outline" className="border-emerald-500/30 text-emerald-500 font-extrabold text-[10px] md:text-[11px] px-1.5 py-0 whitespace-nowrap flex-shrink-0">
                                                        +${item.price}{labels.monthlySuffix}
                                                    </Badge>
                                                </div>
                                                <p className="text-[11px] md:text-xs text-zinc-500 dark:text-zinc-400 leading-normal">
                                                    {item.desc}
                                                </p>
                                            </div>
                                        </div>
                                    ))}
                                </div>
                            </div>
                        </Card>
                    </div>

                    {/* Right Panel: Cost Summary Sticky Block */}
                    <div className="lg:col-span-5 lg:sticky lg:top-28">
                        <Card className="bg-white dark:bg-zinc-900 border-2 border-emerald-500 shadow-2xl rounded-2xl overflow-hidden">
                            <CardHeader className="p-6 md:p-8 bg-zinc-50/50 dark:bg-zinc-950/20 border-b border-zinc-200/50 dark:border-zinc-800">
                                <CardTitle className="text-lg md:text-xl font-black text-zinc-900 dark:text-white flex items-center gap-2">
                                    <Building2 className="w-5 h-5 text-emerald-500" />
                                    {labels.summaryTitle}
                                </CardTitle>
                                <CardDescription className="text-xs text-zinc-500 dark:text-zinc-400 mt-1">
                                    {labels.summaryDesc}
                                </CardDescription>
                            </CardHeader>
                            <CardContent className="p-6 md:p-8 space-y-6">
                                {/* Pricing Details Breakdown */}
                                <div className="space-y-3.5">
                                    {/* Base Price line */}
                                    <div className="flex justify-between items-center text-sm">
                                        <span className="text-zinc-500 dark:text-zinc-400 font-medium">
                                            {labels.basePriceLabel}
                                        </span>
                                        <span className="text-zinc-800 dark:text-zinc-200 font-bold">
                                            ${baseCost}
                                        </span>
                                    </div>

                                    {/* Branches Cost line */}
                                    {branches > 1 && (
                                        <div className="flex justify-between items-center text-sm">
                                            <span className="text-zinc-500 dark:text-zinc-400 font-medium">
                                                {labels.branchesCostLabel} ({branches - 1} × ${BRANCH_PRICE})
                                            </span>
                                            <span className="text-zinc-800 dark:text-zinc-200 font-bold">
                                                +${branchesCost}
                                            </span>
                                        </div>
                                    )}

                                    {/* Devices Cost line */}
                                    {devices > 1 && (
                                        <div className="flex justify-between items-center text-sm">
                                            <span className="text-zinc-500 dark:text-zinc-400 font-medium">
                                                {labels.devicesCostLabel} ({devices - 1} × ${DEVICE_PRICE})
                                            </span>
                                            <span className="text-zinc-800 dark:text-zinc-200 font-bold">
                                                +${devicesCost}
                                            </span>
                                        </div>
                                    )}

                                    {/* Addons summary cost line */}
                                    {addonsCost > 0 && (
                                        <div className="flex justify-between items-center text-sm">
                                            <span className="text-zinc-500 dark:text-zinc-400 font-medium">
                                                {labels.addonsLabel} ({selectedAddonsCount} {labels.selectedLabel})
                                            </span>
                                            <span className="text-zinc-800 dark:text-zinc-200 font-bold">
                                                +${addonsCost}
                                            </span>
                                        </div>
                                    )}

                                    {/* Yearly Discount Line */}
                                    {billingCycle === "yearly" && yearlyDiscount > 0 && (
                                        <div className="flex justify-between items-center text-sm">
                                            <span className="text-emerald-500 font-semibold">
                                                {labels.yearlyDiscountLabel}
                                            </span>
                                            <span className="text-emerald-500 font-extrabold">
                                                -${yearlyDiscount}
                                            </span>
                                        </div>
                                    )}
                                </div>

                                <div className="w-full h-px bg-zinc-200 dark:bg-zinc-800" />

                                {/* Total Price Block */}
                                <div className="space-y-1 text-center py-2">
                                    <p className="text-xs text-zinc-400 font-bold uppercase tracking-widest">
                                        {labels.totalLabel}
                                    </p>
                                    <div className="flex items-baseline justify-center gap-1.5 mt-2">
                                        <span className="text-5xl md:text-6xl font-black tracking-tight text-zinc-900 dark:text-white">
                                            ${finalMonthlyPrice}
                                        </span>
                                        <span className="text-sm font-semibold text-zinc-500 dark:text-zinc-400">
                                            {labels.monthlySuffix}
                                        </span>
                                    </div>
                                    {billingCycle === "yearly" && (
                                        <p className="text-[11px] text-emerald-500 font-extrabold tracking-wide uppercase mt-1">
                                            {labels.billedAnnually} (${finalMonthlyPrice * 12}/{labels.yr})
                                        </p>
                                    )}
                                </div>
                            </CardContent>
                            <CardFooter className="p-6 md:p-8 pt-0 flex flex-col gap-4">
                                <Button 
                                    onClick={() => setCheckoutOpen(true)}
                                    className="w-full py-7 text-base font-bold bg-emerald-500 hover:bg-emerald-600 text-white rounded-xl shadow-xl shadow-emerald-500/20 dark:shadow-emerald-500/10 hover:shadow-2xl hover:scale-[1.01] transition-all duration-300"
                                >
                                    {labels.checkoutBtn}
                                </Button>
                                
                                {branches > 40 && (
                                    <Button 
                                        variant="outline"
                                        onClick={() => {
                                            toast.info(labels.enterpriseContactMsg)
                                        }}
                                        className="w-full py-5 rounded-xl font-semibold border-zinc-300 dark:border-zinc-750 hover:bg-zinc-50 dark:hover:bg-zinc-800 transition-all text-xs"
                                    >
                                        {labels.contactSalesBtn}
                                    </Button>
                                )}

                                <div className="flex items-center gap-2 justify-center text-[10px] text-zinc-400">
                                    <Lock className="w-3.5 h-3.5 text-zinc-400" />
                                    <span>{labels.secureLabel}</span>
                                </div>
                            </CardFooter>
                        </Card>
                    </div>
                </div>
            </div>

            {/* Checkout Dialog Modal */}
            <Dialog open={checkoutOpen} onOpenChange={setCheckoutOpen}>
                <DialogContent className="sm:max-w-[480px] rounded-2xl overflow-hidden border border-zinc-200/50 dark:border-zinc-800 bg-white dark:bg-zinc-900 shadow-2xl p-6" dir={isAr ? "rtl" : "ltr"}>
                    <div className="space-y-4">
                        <div className="text-center sm:text-start space-y-1">
                            <h3 className="text-xl font-extrabold text-zinc-900 dark:text-white flex items-center gap-2 justify-center sm:justify-start">
                                <CreditCard className="w-5 h-5 text-emerald-500" />
                                {labels.modalCheckoutTitle}
                            </h3>
                            <p className="text-xs text-zinc-500 dark:text-zinc-400">
                                {labels.modalCheckoutDesc}{" "}
                                <span className="font-extrabold text-emerald-500">${finalMonthlyPrice}/{billingCycle === "yearly" ? labels.billingFrequencyYearly : labels.billingFrequencyMonthly}</span>.
                            </p>
                        </div>

                        {/* Order breakdown summary */}
                        <div className="p-4 rounded-xl bg-zinc-50 dark:bg-zinc-950 border border-zinc-200/50 dark:border-zinc-800/80 space-y-1.5 text-xs text-zinc-500">
                            <div className="flex justify-between">
                                <span>{labels.branchesLabel}:</span>
                                <span className="font-bold text-zinc-700 dark:text-zinc-300">{branches} {labels.branchUnit}</span>
                            </div>
                            <div className="flex justify-between">
                                <span>{labels.devicesLabel}:</span>
                                <span className="font-bold text-zinc-700 dark:text-zinc-300">{devices} {labels.deviceUnit}</span>
                            </div>
                            <div className="flex justify-between">
                                <span>{labels.addonsLabel}:</span>
                                <span className="font-bold text-zinc-700 dark:text-zinc-300">
                                    {selectedAddonsCount} {labels.selectedLabel}
                                </span>
                            </div>
                        </div>

                        <form onSubmit={handleCheckoutSubmit} className="space-y-4">
                            {/* Email Address */}
                            <div className="space-y-1">
                                <label className="text-[11px] font-bold text-zinc-500 flex items-center gap-1">
                                    <Mail className="w-3 h-3" />
                                    {labels.checkoutFormEmail}
                                </label>
                                <input
                                    type="email"
                                    required
                                    value={checkoutForm.email}
                                    onChange={(e) => setCheckoutForm({ ...checkoutForm, email: e.target.value })}
                                    className="w-full px-3 py-2 text-sm border rounded-lg bg-zinc-50 dark:bg-zinc-950 border-zinc-200 dark:border-zinc-800 focus:outline-none focus:ring-2 focus:ring-emerald-500/50 text-zinc-900 dark:text-zinc-100"
                                    placeholder="name@pharmacy.com"
                                />
                            </div>

                            {/* Cardholder Name */}
                            <div className="space-y-1">
                                <label className="text-[11px] font-bold text-zinc-500 flex items-center gap-1">
                                    <User className="w-3 h-3" />
                                    {labels.checkoutFormName}
                                </label>
                                <input
                                    type="text"
                                    required
                                    value={checkoutForm.name}
                                    onChange={(e) => setCheckoutForm({ ...checkoutForm, name: e.target.value })}
                                    className="w-full px-3 py-2 text-sm border rounded-lg bg-zinc-50 dark:bg-zinc-950 border-zinc-200 dark:border-zinc-800 focus:outline-none focus:ring-2 focus:ring-emerald-500/50 text-zinc-900 dark:text-zinc-100"
                                    placeholder={labels.checkoutFormNamePlaceholder}
                                />
                            </div>

                            {/* Card Number */}
                            <div className="space-y-1">
                                <label className="text-[11px] font-bold text-zinc-500 flex items-center gap-1">
                                    <CreditCard className="w-3 h-3" />
                                    {labels.checkoutFormNumber}
                                </label>
                                <input
                                    type="text"
                                    required
                                    maxLength={19}
                                    value={checkoutForm.cardNumber}
                                    onChange={(e) => setCheckoutForm({ ...checkoutForm, cardNumber: formatCardNumber(e.target.value) })}
                                    className="w-full px-3 py-2 text-sm border rounded-lg bg-zinc-50 dark:bg-zinc-950 border-zinc-200 dark:border-zinc-800 focus:outline-none focus:ring-2 focus:ring-emerald-500/50 text-zinc-900 dark:text-zinc-100 font-mono tracking-wider text-left"
                                    placeholder="0000 0000 0000 0000"
                                    dir="ltr"
                                />
                            </div>

                            {/* Expiry & CVV */}
                            <div className="grid grid-cols-2 gap-4">
                                <div className="space-y-1">
                                    <label className="text-[11px] font-bold text-zinc-500">
                                        {labels.checkoutFormExpiry}
                                    </label>
                                    <input
                                        type="text"
                                        required
                                        maxLength={5}
                                        value={checkoutForm.expiry}
                                        onChange={(e) => setCheckoutForm({ ...checkoutForm, expiry: formatExpiry(e.target.value) })}
                                        className="w-full px-3 py-2 text-sm border rounded-lg bg-zinc-50 dark:bg-zinc-950 border-zinc-200 dark:border-zinc-800 focus:outline-none focus:ring-2 focus:ring-emerald-500/50 text-zinc-900 dark:text-zinc-100 font-mono text-left"
                                        placeholder="MM/YY"
                                        dir="ltr"
                                    />
                                </div>
                                <div className="space-y-1">
                                    <label className="text-[11px] font-bold text-zinc-500">
                                        {labels.checkoutFormCvv}
                                    </label>
                                    <input
                                        type="password"
                                        required
                                        maxLength={4}
                                        value={checkoutForm.cvv}
                                        onChange={(e) => setCheckoutForm({ ...checkoutForm, cvv: e.target.value.replace(/[^0-9]/g, "") })}
                                        className="w-full px-3 py-2 text-sm border rounded-lg bg-zinc-50 dark:bg-zinc-950 border-zinc-200 dark:border-zinc-800 focus:outline-none focus:ring-2 focus:ring-emerald-500/50 text-zinc-900 dark:text-zinc-100 font-mono text-left"
                                        placeholder="•••"
                                        dir="ltr"
                                    />
                                </div>
                            </div>

                            {/* Secure Indicator */}
                            <div className="flex items-center gap-1.5 text-[10px] text-zinc-400 justify-center py-2 bg-zinc-50 dark:bg-zinc-950 rounded-lg border border-dashed border-zinc-200/50 dark:border-zinc-850">
                                <ShieldCheck className="w-3.5 h-3.5 text-emerald-500" />
                                <span>{labels.secureLabel}</span>
                            </div>

                            {/* Actions */}
                            <div className="flex gap-3 pt-2">
                                <DialogClose asChild>
                                    <Button type="button" variant="outline" className="w-1/3 py-5 rounded-lg border-zinc-300 dark:border-zinc-750">
                                        {labels.cancelBtn}
                                    </Button>
                                </DialogClose>
                                <Button
                                    type="submit"
                                    disabled={isSubmitting}
                                    className="w-2/3 py-5 font-bold bg-emerald-500 hover:bg-emerald-600 text-white rounded-lg shadow-lg flex items-center justify-center gap-1.5"
                                >
                                    {isSubmitting ? (
                                        <>
                                            <span className="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin" />
                                            {labels.loadingBtn}
                                        </>
                                    ) : (
                                        <>
                                            <Lock className="w-4 h-4" />
                                            {labels.payNowBtn}
                                        </>
                                    )}
                                </Button>
                            </div>
                        </form>
                    </div>
                </DialogContent>
            </Dialog>
        </section>
    )
}
