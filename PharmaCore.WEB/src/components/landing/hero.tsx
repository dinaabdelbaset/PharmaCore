"use client"

import React, { useState, useEffect } from "react"
import Link from "next/link"
import { useLanguage } from "@/components/providers/language-provider"
import { 
    Sparkles, 
    ArrowRight, 
    Play, 
    CheckCircle2, 
    TrendingUp, 
    Building2, 
    Activity, 
    Search, 
    Plus, 
    Minus, 
    Trash2, 
    CreditCard, 
    Check, 
    Brain, 
    ShoppingCart,
    Clock,
    RefreshCw,
    Maximize2,
    Lock,
    AlertCircle,
    CheckCircle,
    User,
    Wifi
} from "lucide-react"

// Product list for interactive cashier POS mockup
const mockProducts = [
    { id: 1, name: "Amoxicillin 500mg", nameAr: "أموكسيسيلين 500 ملغ", price: 12.50, code: "RX-4821", category: "Antibiotic", categoryAr: "مضاد حيوي" },
    { id: 2, name: "Panadol Extra", nameAr: "بنادول إكسترا", price: 6.20, code: "OTC-9012", category: "Analgesic", categoryAr: "مسكن آلام" },
    { id: 3, name: "Vitamin C 1000mg", nameAr: "فيتامين سي 1000 ملغ", price: 8.00, code: "SUP-0492", category: "Supplement", categoryAr: "مكمل غذائي" },
    { id: 4, name: "Aspirin Cardio 100mg", nameAr: "أسبرين كارديو 100 ملغ", price: 4.80, code: "RX-5512", category: "Cardio", categoryAr: "أدوية قلب" },
    { id: 5, name: "Flonase Allergy Spray", nameAr: "بخاخ فلونيز للحساسية", price: 15.90, code: "OTC-7741", category: "Allergy", categoryAr: "حساسية" },
    { id: 6, name: "Lipitor 20mg", nameAr: "ليبيتور 20 ملغ", price: 24.50, code: "RX-8849", category: "Cholesterol", categoryAr: "مخفض الكوليسترول" }
]

export interface HeroProps {
    sessionActive?: boolean
}

export default function Hero({ sessionActive }: HeroProps = {}) {
    const { t, language } = useLanguage()
    const isAr = language === "ar"
    const [mounted, setMounted] = useState(false)
    const [isAuthenticated, setIsAuthenticated] = useState(false)
    
    // Interactive state for dashboard mockup tabs
    const [activeTab, setActiveTab] = useState<"pos" | "analytics" | "ai">("pos")

    // Interactive state for Cashier POS Mockup
    const [cart, setCart] = useState<Array<{ product: typeof mockProducts[0], qty: number }>>([
        { product: mockProducts[0], qty: 1 },
        { product: mockProducts[1], qty: 2 },
        { product: mockProducts[2], qty: 1 }
    ])
    const [isCheckingOut, setIsCheckingOut] = useState(false)
    const [invoiceCreated, setInvoiceCreated] = useState(false)
    const [invoiceNumber, setInvoiceNumber] = useState("")

    // Interactive state for AI Tab
    const [approvedOrder, setApprovedOrder] = useState<string | null>(null)
    const [isApproving, setIsApproving] = useState(false)

    // Chart telemetry hover interaction
    const [hoveredPoint, setHoveredPoint] = useState<number | null>(null)

    useEffect(() => {
        setMounted(true)
        if (sessionActive !== undefined) {
            setIsAuthenticated(sessionActive)
        } else {
            const token = localStorage.getItem("token")
            setIsAuthenticated(!!token)
        }
    }, [sessionActive])

    // POS Interactions
    const addToCart = (product: typeof mockProducts[0]) => {
        setCart(prev => {
            const existing = prev.find(item => item.product.id === product.id)
            if (existing) {
                return prev.map(item => item.product.id === product.id ? { ...item, qty: item.qty + 1 } : item)
            }
            return [...prev, { product, qty: 1 }]
        })
    }

    const updateQty = (id: number, delta: number) => {
        setCart(prev => {
            return prev.map(item => {
                if (item.product.id === id) {
                    const newQty = item.qty + delta
                    return newQty > 0 ? { ...item, qty: newQty } : null
                }
                return item
            }).filter(Boolean) as typeof cart
        })
    }

    const removeFromCart = (id: number) => {
        setCart(prev => prev.filter(item => item.product.id !== id))
    }

    const handleCheckout = () => {
        if (cart.length === 0) return
        setIsCheckingOut(true)
        setTimeout(() => {
            setIsCheckingOut(false)
            setInvoiceCreated(true)
            setInvoiceNumber(`INV-2026-${Math.floor(1000 + Math.random() * 9000)}`)
        }, 1200)
    }

    const resetPOS = () => {
        setCart([
            { product: mockProducts[0], qty: 1 },
            { product: mockProducts[1], qty: 2 },
            { product: mockProducts[2], qty: 1 }
        ])
        setInvoiceCreated(false)
        setInvoiceNumber("")
    }

    // AI Order Trigger
    const handleApproveOrder = () => {
        setIsApproving(true)
        setTimeout(() => {
            setIsApproving(false)
            setApprovedOrder(`PO-AI-${Math.floor(10000 + Math.random() * 90000)}`)
        }, 1000)
    }

    if (!mounted) {
        return null // avoid hydration flashes
    }

    const ctaLink = isAuthenticated ? "/dashboard" : "/register"
    const ctaText = isAuthenticated ? t("hero.cta_dashboard") : t("hero.cta_get_started")

    const cartTotal = cart.reduce((acc, item) => acc + item.product.price * item.qty, 0)
    const cartCount = cart.reduce((acc, item) => acc + item.qty, 0)

    // Chart dimensions & sales dataset
    const salesData = [
        { time: "08:00", sales: 420, tx: 12 },
        { time: "10:00", sales: 850, tx: 25 },
        { time: "12:00", sales: 1640, tx: 48 },
        { time: "14:00", sales: 1200, tx: 35 },
        { time: "16:00", sales: 1950, tx: 55 },
        { time: "18:00", sales: 2450, tx: 72 },
        { time: "20:00", sales: 1800, tx: 50 },
        { time: "22:00", sales: 950, tx: 28 }
    ]

    return (
        <section className="relative overflow-hidden bg-background py-20 lg:py-28">
            {/* Vibrant gradient glowing backgrounds */}
            <div className="absolute -top-40 left-1/4 h-[400px] w-[600px] -translate-x-1/2 rounded-full bg-emerald-500/10 dark:bg-emerald-500/20 blur-[120px] pointer-events-none" />
            <div className="absolute -bottom-40 right-1/4 h-[400px] w-[600px] translate-x-1/2 rounded-full bg-indigo-500/10 dark:bg-indigo-500/20 blur-[120px] pointer-events-none" />
            
            <div className="container relative z-10 mx-auto px-4 sm:px-6 lg:px-8">
                {/* Badge text */}
                <div className="flex justify-center mb-6">
                    <div className="inline-flex items-center gap-2 px-4 py-1.5 rounded-full text-xs font-semibold bg-emerald-500/10 dark:bg-emerald-500/15 text-emerald-600 dark:text-emerald-400 border border-emerald-500/25 dark:border-emerald-500/20 animate-fade-in shadow-sm">
                        <Sparkles className="h-3.5 w-3.5 text-emerald-500 animate-pulse" />
                        <span>{t("hero.badge")}</span>
                    </div>
                </div>

                {/* Primary marketing headline */}
                <div className="text-center max-w-4xl mx-auto">
                    <h1 className="text-4xl font-extrabold tracking-tight text-foreground sm:text-6xl lg:text-7xl leading-tight opacity-0 animate-fade-in [animation-delay:150ms]">
                        {t("hero.title_part1")}{" "}
                        <span className="bg-gradient-to-r from-emerald-500 via-teal-500 to-indigo-500 bg-clip-text text-transparent drop-shadow-sm">
                            {t("hero.title_part2")}
                        </span>
                    </h1>

                    {/* Subheading / Description */}
                    <p className="mt-6 text-lg sm:text-xl text-muted-foreground leading-relaxed max-w-3xl mx-auto opacity-0 animate-fade-in [animation-delay:300ms]">
                        {t("hero.subtitle")}
                    </p>

                    {/* CTAs */}
                    <div className="mt-10 flex flex-wrap items-center justify-center gap-4 opacity-0 animate-fade-in [animation-delay:450ms]">
                        <Link 
                            href={ctaLink} 
                            className="w-full sm:w-auto rounded-xl bg-gradient-to-r from-emerald-500 to-teal-600 px-8 py-4 text-base font-semibold text-white shadow-lg shadow-emerald-500/20 hover:shadow-emerald-500/30 dark:shadow-emerald-500/10 hover:opacity-95 transform active:scale-95 transition-all duration-200 flex items-center justify-center gap-2 group"
                        >
                            {ctaText}
                            <ArrowRight className="h-5 w-5 rtl:rotate-180 transition-transform duration-200 group-hover:translate-x-1 rtl:group-hover:-translate-x-1" />
                        </Link>
                        <a 
                            href="#interactive-mockup" 
                            className="w-full sm:w-auto rounded-xl border border-border bg-background/50 hover:bg-muted/80 backdrop-blur px-8 py-4 text-base font-semibold text-foreground transition duration-200 flex items-center justify-center gap-2"
                        >
                            <Play className="h-4 w-4 text-emerald-500 fill-emerald-500/25" />
                            {t("hero.cta_learn_more")}
                        </a>
                    </div>
                </div>

                {/* Telemetry Stats counters */}
                <div className="grid grid-cols-1 sm:grid-cols-3 gap-6 max-w-4xl mx-auto mt-16 border-t border-border/60 pt-10 text-center opacity-0 animate-fade-in [animation-delay:600ms]">
                    {/* Stat 1: Pharmacies */}
                    <div className="flex flex-col items-center p-4 rounded-2xl bg-slate-500/5 dark:bg-slate-400/5 border border-border/40 backdrop-blur-sm">
                        <div className="flex items-center gap-2 mb-1">
                            <Building2 className="h-5 w-5 text-emerald-500" />
                            <span className="text-3xl font-extrabold text-foreground tracking-tight">
                                {t("hero.stat_pharmacies")}
                            </span>
                        </div>
                        <span className="text-sm text-muted-foreground font-medium">
                            {t("hero.stat_pharmacies_label")}
                        </span>
                    </div>

                    {/* Stat 2: Transactions */}
                    <div className="flex flex-col items-center p-4 rounded-2xl bg-slate-500/5 dark:bg-slate-400/5 border border-border/40 backdrop-blur-sm">
                        <div className="flex items-center gap-2 mb-1">
                            <TrendingUp className="h-5 w-5 text-teal-500" />
                            <span className="text-3xl font-extrabold text-foreground tracking-tight">
                                {t("hero.stat_transactions")}
                            </span>
                        </div>
                        <span className="text-sm text-muted-foreground font-medium">
                            {t("hero.stat_transactions_label")}
                        </span>
                    </div>

                    {/* Stat 3: Uptime */}
                    <div className="flex flex-col items-center p-4 rounded-2xl bg-slate-500/5 dark:bg-slate-400/5 border border-border/40 backdrop-blur-sm">
                        <div className="flex items-center gap-2 mb-1">
                            <Activity className="h-5 w-5 text-indigo-500" />
                            <span className="text-3xl font-extrabold text-foreground tracking-tight">
                                {t("hero.stat_uptime")}
                            </span>
                        </div>
                        <span className="text-sm text-muted-foreground font-medium">
                            {t("hero.stat_uptime_label")}
                        </span>
                    </div>
                </div>

                {/* Dashboard Mockup Tabs Controller */}
                <div id="interactive-mockup" className="mt-20 scroll-mt-24 opacity-0 animate-fade-in [animation-delay:750ms]">
                    <div className="flex flex-wrap items-center justify-center gap-2 mb-6 border-b border-border/60 pb-5 max-w-xl mx-auto">
                        {/* Tab 1: POS Cashier */}
                        <button 
                            onClick={() => setActiveTab("pos")} 
                            className={`px-4 py-2.5 rounded-xl text-xs sm:text-sm font-semibold transition-all duration-200 flex items-center gap-2 cursor-pointer ${
                                activeTab === "pos" 
                                    ? "bg-emerald-500/10 dark:bg-emerald-500/20 text-emerald-600 dark:text-emerald-400 border border-emerald-500/30" 
                                    : "text-muted-foreground hover:text-foreground hover:bg-muted"
                            }`}
                        >
                            <ShoppingCart className="h-4 w-4" />
                            <span>{isAr ? "كاشير الصيدلية (POS)" : "Cashier Terminal"}</span>
                        </button>

                        {/* Tab 2: Analytics */}
                        <button 
                            onClick={() => setActiveTab("analytics")} 
                            className={`px-4 py-2.5 rounded-xl text-xs sm:text-sm font-semibold transition-all duration-200 flex items-center gap-2 cursor-pointer ${
                                activeTab === "analytics" 
                                    ? "bg-teal-500/10 dark:bg-teal-500/20 text-teal-600 dark:text-teal-400 border border-teal-500/30" 
                                    : "text-muted-foreground hover:text-foreground hover:bg-muted"
                            }`}
                        >
                            <TrendingUp className="h-4 w-4" />
                            <span>{isAr ? "تحليلات الفروع" : "Branch Analytics"}</span>
                        </button>

                        {/* Tab 3: AI Forecast */}
                        <button 
                            onClick={() => setActiveTab("ai")} 
                            className={`px-4 py-2.5 rounded-xl text-xs sm:text-sm font-semibold transition-all duration-200 flex items-center gap-2 cursor-pointer ${
                                activeTab === "ai" 
                                    ? "bg-indigo-500/10 dark:bg-indigo-500/20 text-indigo-600 dark:text-indigo-400 border border-indigo-500/30" 
                                    : "text-muted-foreground hover:text-foreground hover:bg-muted"
                            }`}
                        >
                            <Brain className="h-4 w-4" />
                            <span>{isAr ? "التنبؤ الذكي (AI)" : "AI Predictor"}</span>
                        </button>
                    </div>

                    {/* Dashboard Preview Mockup frame */}
                    <div className="relative mx-auto max-w-6xl rounded-2xl border border-border/80 bg-slate-900/5 dark:bg-slate-950/60 p-2 backdrop-blur shadow-2xl hover:shadow-emerald-500/5 dark:hover:shadow-emerald-500/10 transition-all duration-300">
                        {/* Browser Chrome Header */}
                        <div className="flex items-center justify-between px-4 py-3 border-b border-border/80 bg-muted/60 dark:bg-slate-900/80 rounded-t-xl">
                            {/* Window Dots */}
                            <div className="flex items-center gap-1.5">
                                <span className="w-3 h-3 rounded-full bg-rose-500" />
                                <span className="w-3 h-3 rounded-full bg-amber-500" />
                                <span className="w-3 h-3 rounded-full bg-emerald-500" />
                            </div>
                            {/* Mock Address Bar */}
                            <div className="hidden sm:flex items-center gap-2 px-3 py-1 rounded-md bg-background/80 border border-border/40 text-[11px] text-muted-foreground w-1/3 justify-center select-none font-mono">
                                <Lock className="h-3 w-3 text-emerald-500" />
                                <span>app.pharmacore.com/{activeTab}</span>
                            </div>
                            {/* Status Indicator */}
                            <div className="flex items-center gap-2">
                                <span className="w-2 h-2 rounded-full bg-emerald-500 animate-ping" />
                                <span className="text-xs font-semibold text-emerald-600 dark:text-emerald-400 flex items-center gap-1">
                                    <Wifi className="h-3 w-3" />
                                    {isAr ? "متصل" : "Live"}
                                </span>
                            </div>
                        </div>

                        {/* Interactive App Window Workspace */}
                        <div className="flex min-h-[500px] flex-col lg:flex-row bg-background dark:bg-slate-950 text-foreground overflow-hidden rounded-b-xl">
                            {/* App Navigation Sidebar Mock */}
                            <aside className="w-full lg:w-16 flex lg:flex-col items-center justify-between p-3 lg:py-6 border-e border-border/50 bg-muted/20 dark:bg-slate-900/30 gap-4">
                                <div className="flex lg:flex-col items-center gap-4 w-full lg:w-auto">
                                    {/* Logo */}
                                    <div className="w-9 h-9 rounded-xl bg-gradient-to-tr from-emerald-500 to-teal-500 flex items-center justify-center font-bold text-white shadow-md text-sm">
                                        PC
                                    </div>
                                    <span className="lg:hidden text-sm font-bold text-foreground">PharmaCore</span>
                                </div>
                                <div className="flex lg:flex-col items-center gap-4">
                                    <div className={`p-2 rounded-lg transition-colors ${activeTab === "pos" ? "bg-emerald-500/15 text-emerald-500" : "text-muted-foreground hover:bg-muted hover:text-foreground"}`}>
                                        <ShoppingCart className="h-4 w-4" />
                                    </div>
                                    <div className={`p-2 rounded-lg transition-colors ${activeTab === "analytics" ? "bg-teal-500/15 text-teal-500" : "text-muted-foreground hover:bg-muted hover:text-foreground"}`}>
                                        <TrendingUp className="h-4 w-4" />
                                    </div>
                                    <div className={`p-2 rounded-lg transition-colors ${activeTab === "ai" ? "bg-indigo-500/15 text-indigo-500" : "text-muted-foreground hover:bg-muted hover:text-foreground"}`}>
                                        <Brain className="h-4 w-4" />
                                    </div>
                                </div>
                                <div className="hidden lg:flex items-center justify-center w-8 h-8 rounded-full bg-slate-200 dark:bg-slate-800 text-foreground text-xs font-bold">
                                    <User className="h-4 w-4 text-muted-foreground" />
                                </div>
                            </aside>

                            {/* Main Content Workspace depending on selected tab */}
                            <main className="flex-1 p-4 sm:p-6 overflow-hidden">
                                {activeTab === "pos" && (
                                    <div className="grid grid-cols-1 xl:grid-cols-12 gap-6 h-full transition-all duration-300">
                                        {/* POS Catalog */}
                                        <div className="xl:col-span-7 flex flex-col gap-4">
                                            <div className="flex items-center justify-between border-b border-border/40 pb-3">
                                                <h3 className="font-bold text-base flex items-center gap-2">
                                                    <ShoppingCart className="h-5 w-5 text-emerald-500" />
                                                    {isAr ? "كتالوج الأدوية المتاحة" : "Medicine Catalogue"}
                                                </h3>
                                                <span className="text-xs text-muted-foreground font-medium">
                                                    {mockProducts.length} {isAr ? "عناصر متوفرة" : "items available"}
                                                </span>
                                            </div>
                                            
                                            {/* Products Grid */}
                                            <div className="grid grid-cols-1 sm:grid-cols-2 gap-3 max-h-[380px] overflow-y-auto pe-1">
                                                {mockProducts.map((p) => (
                                                    <div key={p.id} className="p-3.5 rounded-xl border border-border/60 bg-muted/10 hover:border-emerald-500/30 hover:bg-emerald-500/5 transition-all duration-200 group flex flex-col justify-between gap-3 text-start">
                                                        <div>
                                                            <div className="flex items-center justify-between gap-2">
                                                                <span className="font-bold text-sm text-foreground group-hover:text-emerald-500 transition-colors">
                                                                    {isAr ? p.nameAr : p.name}
                                                                </span>
                                                                <span className="text-[10px] px-2 py-0.5 rounded-full bg-slate-100 dark:bg-slate-800 text-muted-foreground border border-border/30">
                                                                    {isAr ? p.categoryAr : p.category}
                                                                </span>
                                                            </div>
                                                            <p className="text-[11px] text-muted-foreground font-mono mt-1">{p.code}</p>
                                                        </div>
                                                        <div className="flex items-center justify-between mt-2">
                                                            <span className="font-extrabold text-sm text-foreground">
                                                                ${p.price.toFixed(2)}
                                                            </span>
                                                            <button 
                                                                onClick={() => addToCart(p)}
                                                                className="px-2.5 py-1.5 rounded-lg bg-emerald-500 text-white hover:bg-emerald-600 text-[11px] font-semibold flex items-center gap-1 transform active:scale-95 transition cursor-pointer"
                                                            >
                                                                <Plus className="h-3 w-3" />
                                                                {isAr ? "إضافة" : "Add"}
                                                            </button>
                                                        </div>
                                                    </div>
                                                ))}
                                            </div>
                                        </div>

                                        {/* POS Cart Panel */}
                                        <div className="xl:col-span-5 flex flex-col rounded-xl border border-border/80 bg-muted/20 dark:bg-slate-900/40 p-4 relative overflow-hidden">
                                            {invoiceCreated ? (
                                                /* Success Checkout View */
                                                <div className="flex flex-col items-center justify-center text-center py-12 h-full animate-fade-in">
                                                    <div className="w-16 h-16 rounded-full bg-emerald-500/10 text-emerald-500 flex items-center justify-center mb-4">
                                                        <CheckCircle className="h-10 w-10 animate-bounce" />
                                                    </div>
                                                    <h4 className="font-bold text-lg text-foreground">
                                                        {isAr ? "تم إنشاء الفاتورة بنجاح!" : "Invoice Created Successfully!"}
                                                    </h4>
                                                    <p className="text-xs text-muted-foreground mt-2 max-w-[280px]">
                                                        {isAr 
                                                            ? `رقم الفاتورة: ${invoiceNumber} تم حفظها ومزامنتها مع الخادم الرئيسي.` 
                                                            : `Invoice ${invoiceNumber} has been verified and synced with central server.`
                                                        }
                                                    </p>
                                                    <div className="mt-8 flex flex-col gap-2 w-full">
                                                        <div className="flex justify-between text-xs py-1.5 border-b border-border/40 font-mono">
                                                            <span className="text-muted-foreground">{isAr ? "القيمة الإجمالية" : "Total Amount"}:</span>
                                                            <span className="font-bold">${cartTotal.toFixed(2)}</span>
                                                        </div>
                                                        <div className="flex justify-between text-xs py-1.5 border-b border-border/40 font-mono">
                                                            <span className="text-muted-foreground">{isAr ? "طريقة الدفع" : "Payment Method"}:</span>
                                                            <span className="font-semibold text-emerald-500">{isAr ? "نقدي (كاش)" : "Cash (Synced)"}</span>
                                                        </div>
                                                    </div>
                                                    <button 
                                                        onClick={resetPOS} 
                                                        className="mt-8 w-full py-2.5 rounded-lg bg-emerald-500 text-white font-semibold hover:bg-emerald-600 text-xs transition duration-200 cursor-pointer"
                                                    >
                                                        {isAr ? "بدء معاملة جديدة" : "New Transaction"}
                                                    </button>
                                                </div>
                                            ) : (
                                                /* Standard Cart View */
                                                <div className="flex flex-col justify-between h-full min-h-[360px]">
                                                    <div>
                                                        <div className="flex items-center justify-between border-b border-border/40 pb-3 mb-3">
                                                            <h4 className="font-bold text-sm flex items-center gap-2">
                                                                <ShoppingCart className="h-4 w-4 text-emerald-500" />
                                                                {isAr ? "سلة المشتريات" : "Current Cart"}
                                                            </h4>
                                                            <span className="text-xs px-2.5 py-0.5 rounded-full bg-emerald-500/10 text-emerald-500 font-bold">
                                                                {cartCount} {isAr ? "منتجات" : "items"}
                                                            </span>
                                                        </div>

                                                        {/* Cart List */}
                                                        <div className="flex flex-col gap-2.5 max-h-[220px] overflow-y-auto pe-1">
                                                            {cart.length === 0 ? (
                                                                <div className="text-center py-12 text-muted-foreground text-xs">
                                                                    {isAr ? "السلة فارغة. أضف بعض الأدوية!" : "Cart is empty. Add some medicines!"}
                                                                </div>
                                                            ) : (
                                                                cart.map((item) => (
                                                                    <div key={item.product.id} className="flex items-center justify-between gap-2 p-2 rounded-lg bg-background border border-border/40 text-xs">
                                                                        <div className="flex-1 text-start">
                                                                            <p className="font-semibold">{isAr ? item.product.nameAr : item.product.name}</p>
                                                                            <p className="text-[10px] text-muted-foreground">${item.product.price.toFixed(2)}</p>
                                                                        </div>
                                                                        <div className="flex items-center gap-2">
                                                                            <button 
                                                                                onClick={() => updateQty(item.product.id, -1)}
                                                                                className="p-1 rounded bg-muted hover:bg-slate-200 dark:hover:bg-slate-800 transition"
                                                                            >
                                                                                <Minus className="h-3 w-3" />
                                                                            </button>
                                                                            <span className="font-bold w-4 text-center">{item.qty}</span>
                                                                            <button 
                                                                                onClick={() => updateQty(item.product.id, 1)}
                                                                                className="p-1 rounded bg-muted hover:bg-slate-200 dark:hover:bg-slate-800 transition"
                                                                            >
                                                                                <Plus className="h-3 w-3" />
                                                                            </button>
                                                                            <button 
                                                                                onClick={() => removeFromCart(item.product.id)}
                                                                                className="p-1 text-rose-500 hover:bg-rose-500/10 rounded transition ms-1"
                                                                            >
                                                                                <Trash2 className="h-3.5 w-3.5" />
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                ))
                                                            )}
                                                        </div>
                                                    </div>

                                                    {/* Cart Summary */}
                                                    <div className="border-t border-border/60 pt-3 mt-4">
                                                        <div className="flex justify-between items-center text-xs mb-1.5 font-medium">
                                                            <span className="text-muted-foreground">{isAr ? "المجموع الفرعي" : "Subtotal"}:</span>
                                                            <span>${cartTotal.toFixed(2)}</span>
                                                        </div>
                                                        <div className="flex justify-between items-center text-xs mb-3 font-medium">
                                                            <span className="text-muted-foreground">{isAr ? "الضريبة المضافة (15%)" : "Tax (15%)"}:</span>
                                                            <span>${(cartTotal * 0.15).toFixed(2)}</span>
                                                        </div>
                                                        <div className="flex justify-between items-center border-t border-dashed border-border/80 pt-2 mb-4">
                                                            <span className="font-bold text-sm">{isAr ? "المجموع الكلي" : "Total Amount"}:</span>
                                                            <span className="font-extrabold text-sm text-emerald-600 dark:text-emerald-400">
                                                                ${(cartTotal * 1.15).toFixed(2)}
                                                            </span>
                                                        </div>
                                                        <button 
                                                            disabled={cart.length === 0 || isCheckingOut}
                                                            onClick={handleCheckout}
                                                            className="w-full py-3 rounded-lg bg-gradient-to-r from-emerald-500 to-teal-600 text-white font-bold hover:opacity-95 text-xs transition duration-200 flex items-center justify-center gap-2 disabled:opacity-50 disabled:cursor-not-allowed cursor-pointer"
                                                        >
                                                            {isCheckingOut ? (
                                                                <>
                                                                    <RefreshCw className="h-4 w-4 animate-spin" />
                                                                    {isAr ? "جاري المعالجة..." : "Processing transaction..."}
                                                                </>
                                                            ) : (
                                                                <>
                                                                    <CreditCard className="h-4 w-4" />
                                                                    {isAr ? "تأكيد البيع وطباعة الفاتورة" : "Verify Sale & Sync"}
                                                                </>
                                                            )}
                                                        </button>
                                                    </div>
                                                </div>
                                            )}
                                        </div>
                                    </div>
                                )}

                                {activeTab === "analytics" && (
                                    <div className="grid grid-cols-1 xl:grid-cols-12 gap-6 h-full transition-all duration-300 text-start">
                                        {/* Analytics Left Panel: Revenue Chart */}
                                        <div className="xl:col-span-8 flex flex-col gap-4">
                                            <div className="flex items-center justify-between border-b border-border/40 pb-3">
                                                <div>
                                                    <h3 className="font-bold text-base text-foreground">
                                                        {isAr ? "مراقبة الإيرادات اللحظية" : "Real-time Telemetry Analytics"}
                                                    </h3>
                                                    <p className="text-[11px] text-muted-foreground mt-0.5">
                                                        {isAr ? "إجمالي مبيعات فروع الصيدلية للـ 24 ساعة الماضية" : "Consolidated sales performance across active pharmacies"}
                                                    </p>
                                                </div>
                                                <div className="flex items-center gap-2">
                                                    <span className="w-2.5 h-2.5 rounded-full bg-teal-500 animate-pulse" />
                                                    <span className="text-xs text-muted-foreground font-mono">Synced 3s ago</span>
                                                </div>
                                            </div>

                                            {/* Custom CSS/SVG Chart */}
                                            <div className="relative p-4 rounded-xl border border-border/60 bg-muted/10 h-[240px] flex flex-col justify-between">
                                                {/* Chart values tooltips */}
                                                {hoveredPoint !== null && (
                                                    <div 
                                                        className="absolute p-2.5 rounded-lg border border-border bg-background shadow-lg z-20 text-[11px] font-mono pointer-events-none"
                                                        style={{ 
                                                            left: `${(hoveredPoint / (salesData.length - 1)) * 80 + 10}%`,
                                                            top: "20px" 
                                                        }}
                                                    >
                                                        <p className="font-bold text-foreground">{salesData[hoveredPoint].time}</p>
                                                        <p className="text-teal-500">{isAr ? "المبيعات" : "Sales"}: ${salesData[hoveredPoint].sales}</p>
                                                        <p className="text-muted-foreground">{isAr ? "العمليات" : "Tx Count"}: {salesData[hoveredPoint].tx}</p>
                                                    </div>
                                                )}

                                                {/* Chart Canvas Area */}
                                                <div className="w-full h-4/5 relative mt-4 select-none">
                                                    {/* Background Grid Lines */}
                                                    <div className="absolute inset-0 flex flex-col justify-between opacity-20 pointer-events-none">
                                                        <hr className="border-t border-border" />
                                                        <hr className="border-t border-border" />
                                                        <hr className="border-t border-border" />
                                                        <hr className="border-t border-border" />
                                                    </div>

                                                    {/* SVG Area and Line Graph */}
                                                    <svg className="w-full h-full" viewBox="0 0 700 160" preserveAspectRatio="none">
                                                        <defs>
                                                            <linearGradient id="chartGradient" x1="0" y1="0" x2="0" y2="1">
                                                                <stop offset="0%" stopColor="#14b8a6" stopOpacity="0.45" />
                                                                <stop offset="100%" stopColor="#14b8a6" stopOpacity="0.0" />
                                                            </linearGradient>
                                                        </defs>
                                                        {/* Area Fill */}
                                                        <path 
                                                            d="M 0 160 L 0 130 Q 100 80, 200 65 T 400 30 T 600 20 L 700 80 L 700 160 Z" 
                                                            fill="url(#chartGradient)" 
                                                            className="transition-all duration-500"
                                                        />
                                                        {/* Line Curve */}
                                                        <path 
                                                            d="M 0 130 Q 100 80, 200 65 T 400 30 T 600 20 L 700 80" 
                                                            fill="none" 
                                                            stroke="#14b8a6" 
                                                            strokeWidth="3" 
                                                            className="transition-all duration-500"
                                                        />
                                                        {/* Active Interactive Dots */}
                                                        {salesData.map((d, index) => {
                                                            // Approximate horizontal coordinates
                                                            const x = (index / (salesData.length - 1)) * 680 + 10
                                                            // Map sales value (range 400 to 2500) to height (range 150 to 10)
                                                            const y = 150 - ((d.sales - 400) / 2100) * 130
                                                            return (
                                                                <circle 
                                                                    key={index} 
                                                                    cx={x} 
                                                                    cy={y} 
                                                                    r={hoveredPoint === index ? "6" : "4"} 
                                                                    className={`fill-background stroke-teal-500 stroke-[3px] transition-all cursor-pointer ${
                                                                        hoveredPoint === index ? "scale-125" : ""
                                                                    }`}
                                                                    onMouseEnter={() => setHoveredPoint(index)}
                                                                    onMouseLeave={() => setHoveredPoint(null)}
                                                                />
                                                            )
                                                        })}
                                                    </svg>
                                                </div>

                                                {/* X Axis Labels */}
                                                <div className="flex items-center justify-between text-[10px] text-muted-foreground font-mono border-t border-border/40 pt-2">
                                                    {salesData.map((d, i) => (
                                                        <span key={i} className="cursor-pointer hover:text-teal-500 font-bold" onMouseEnter={() => setHoveredPoint(i)} onMouseLeave={() => setHoveredPoint(null)}>
                                                            {d.time}
                                                        </span>
                                                    ))}
                                                </div>
                                            </div>
                                        </div>

                                        {/* Analytics Right Panel: Branches list */}
                                        <div className="xl:col-span-4 flex flex-col gap-4">
                                            <h4 className="font-bold text-sm text-foreground flex items-center gap-2 border-b border-border/40 pb-3">
                                                <Building2 className="h-4 w-4 text-teal-500" />
                                                {isAr ? "حالة الأجهزة والمزامنة" : "Branch Status & POS Sync"}
                                            </h4>
                                            
                                            <div className="flex flex-col gap-2.5">
                                                {/* Branch 1 */}
                                                <div className="p-2.5 rounded-lg border border-border/50 bg-background flex flex-col gap-1 text-xs">
                                                    <div className="flex items-center justify-between">
                                                        <span className="font-bold">{isAr ? "الفرع الرئيسي - وسط المدينة" : "Downtown Branch (HQ)"}</span>
                                                        <span className="flex items-center gap-1 text-[10px] text-emerald-500 font-semibold bg-emerald-500/5 px-2 py-0.5 rounded-full border border-emerald-500/10">
                                                            <span className="w-1.5 h-1.5 rounded-full bg-emerald-500 animate-pulse" />
                                                            {isAr ? "نشط" : "Online"}
                                                        </span>
                                                    </div>
                                                    <div className="flex justify-between text-[10px] text-muted-foreground mt-1">
                                                        <span>{isAr ? "المبيعات اليومية" : "Today Sales"}: $14,250</span>
                                                        <span>88% {isAr ? "من المستهدف" : "Target"}</span>
                                                    </div>
                                                    <div className="w-full bg-muted h-1 rounded-full overflow-hidden mt-1">
                                                        <div className="bg-emerald-500 h-full rounded-full" style={{ width: "88%" }} />
                                                    </div>
                                                </div>

                                                {/* Branch 2 */}
                                                <div className="p-2.5 rounded-lg border border-border/50 bg-background flex flex-col gap-1 text-xs">
                                                    <div className="flex items-center justify-between">
                                                        <span className="font-bold">{isAr ? "فرع المطار الدولي" : "Airport Terminal Branch"}</span>
                                                        <span className="flex items-center gap-1 text-[10px] text-emerald-500 font-semibold bg-emerald-500/5 px-2 py-0.5 rounded-full border border-emerald-500/10">
                                                            <span className="w-1.5 h-1.5 rounded-full bg-emerald-500 animate-pulse" />
                                                            {isAr ? "نشط" : "Online"}
                                                        </span>
                                                    </div>
                                                    <div className="flex justify-between text-[10px] text-muted-foreground mt-1">
                                                        <span>{isAr ? "المبيعات اليومية" : "Today Sales"}: $22,100</span>
                                                        <span>95% {isAr ? "من المستهدف" : "Target"}</span>
                                                    </div>
                                                    <div className="w-full bg-muted h-1 rounded-full overflow-hidden mt-1">
                                                        <div className="bg-teal-500 h-full rounded-full" style={{ width: "95%" }} />
                                                    </div>
                                                </div>

                                                {/* Branch 3 */}
                                                <div className="p-2.5 rounded-lg border border-border/50 bg-background flex flex-col gap-1 text-xs">
                                                    <div className="flex items-center justify-between">
                                                        <span className="font-bold">{isAr ? "صيدلية المنطقة الشمالية" : "Northside Medical Clinic"}</span>
                                                        <span className="flex items-center gap-1 text-[10px] text-amber-500 font-semibold bg-amber-500/5 px-2 py-0.5 rounded-full border border-amber-500/10">
                                                            <span className="w-1.5 h-1.5 rounded-full bg-amber-500 animate-pulse" />
                                                            {isAr ? "حركة كثيفة" : "High Traffic"}
                                                        </span>
                                                    </div>
                                                    <div className="flex justify-between text-[10px] text-muted-foreground mt-1">
                                                        <span>{isAr ? "المبيعات اليومية" : "Today Sales"}: $9,400</span>
                                                        <span>60% {isAr ? "من المستهدف" : "Target"}</span>
                                                    </div>
                                                    <div className="w-full bg-muted h-1 rounded-full overflow-hidden mt-1">
                                                        <div className="bg-amber-500 h-full rounded-full" style={{ width: "60%" }} />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                )}

                                {activeTab === "ai" && (
                                    <div className="grid grid-cols-1 xl:grid-cols-12 gap-6 h-full transition-all duration-300 text-start">
                                        {/* AI Forecast Main */}
                                        <div className="xl:col-span-7 flex flex-col gap-4">
                                            <div className="flex items-center justify-between border-b border-border/40 pb-3">
                                                <h3 className="font-bold text-base flex items-center gap-2">
                                                    <Brain className="h-5 w-5 text-indigo-500" />
                                                    {isAr ? "تنبؤات العرض والطلب بالذكاء الاصطناعي" : "AI Stock Demand Predictor"}
                                                </h3>
                                                <span className="text-[10px] px-2 py-0.5 rounded-full bg-indigo-500/10 text-indigo-500 border border-indigo-500/20 font-semibold">
                                                    PharmaCore AI v2.4
                                                </span>
                                            </div>

                                            {/* AI predictions list */}
                                            <div className="flex flex-col gap-3">
                                                {/* Prediction 1 */}
                                                <div className="p-3.5 rounded-xl border border-border/50 bg-indigo-500/5 dark:bg-indigo-500/10 flex items-start gap-3">
                                                    <div className="p-2 rounded-lg bg-indigo-500/20 text-indigo-500 mt-0.5">
                                                        <TrendingUp className="h-4 w-4" />
                                                    </div>
                                                    <div className="flex-1 text-xs">
                                                        <div className="flex items-center justify-between">
                                                            <span className="font-bold text-foreground">{isAr ? "زيادة موسمية متوقعة: أدوية الحساسية" : "Seasonal Allergy Spike Forecasted"}</span>
                                                            <span className="text-[10px] text-indigo-500 font-extrabold bg-indigo-500/10 px-2 py-0.5 rounded-full">+240%</span>
                                                        </div>
                                                        <p className="text-muted-foreground mt-1 leading-relaxed">
                                                            {isAr 
                                                                ? "بناءً على التغيرات الجوية وبيانات حبوب اللقاح التاريخية، من المتوقع زيادة الطلب على مضادات الهستامين بمعدل 2.4 ضعف خلال الـ 14 يوماً القادمة."
                                                                : "Analysis of localized allergy indexes predicts a 2.4x spike in Antihistamines starting in the next 3-5 days. Suggesting restock."
                                                            }
                                                        </p>
                                                    </div>
                                                </div>

                                                {/* Prediction 2 */}
                                                <div className="p-3.5 rounded-xl border border-border/50 bg-amber-500/5 dark:bg-amber-500/10 flex items-start gap-3">
                                                    <div className="p-2 rounded-lg bg-amber-500/20 text-amber-500 mt-0.5">
                                                        <AlertCircle className="h-4 w-4" />
                                                    </div>
                                                    <div className="flex-1 text-xs">
                                                        <div className="flex items-center justify-between">
                                                            <span className="font-bold text-foreground">{isAr ? "تحذير: نفاذ وشيك لمخزون الأموكسيسيلين" : "Imminent Stock-out Warning: Amoxicillin"}</span>
                                                            <span className="text-[10px] text-amber-500 font-extrabold bg-amber-500/10 px-2 py-0.5 rounded-full">4 Days Left</span>
                                                        </div>
                                                        <p className="text-muted-foreground mt-1 leading-relaxed">
                                                            {isAr 
                                                                ? "مخزون أموكسيسيلين 500 ملغ في فرع المطار ينخفض بشكل أسرع من المتوقع. الطلبات التلقائية الموصى بها مسبقاً جاهزة للإرسال."
                                                                : "Amoxicillin 500mg stock at the Airport terminal branch is depleted faster than normal run rate. Action suggested."
                                                            }
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        {/* AI Restock Interactive Panel */}
                                        <div className="xl:col-span-5 flex flex-col justify-between rounded-xl border border-border/80 bg-muted/20 dark:bg-slate-900/40 p-4 min-h-[340px]">
                                            <div>
                                                <div className="flex items-center justify-between border-b border-border/40 pb-3 mb-4">
                                                    <h4 className="font-bold text-sm text-foreground flex items-center gap-2">
                                                        <Brain className="h-4 w-4 text-indigo-500" />
                                                        {isAr ? "محرك الطلبات الذكي" : "Smart Restock Engine"}
                                                    </h4>
                                                </div>

                                                {approvedOrder ? (
                                                    <div className="flex flex-col items-center justify-center text-center py-6 h-full animate-fade-in">
                                                        <div className="w-12 h-12 rounded-full bg-emerald-500/15 text-emerald-500 flex items-center justify-center mb-3">
                                                            <Check className="h-6 w-6" />
                                                        </div>
                                                        <h5 className="font-bold text-sm text-foreground">{isAr ? "تم إرسال أمر التوريد!" : "Auto-Order Dispatched!"}</h5>
                                                        <p className="text-[11px] text-muted-foreground mt-1">
                                                            {isAr 
                                                                ? `تم إرسال أمر الشراء الموصى به (${approvedOrder}) مباشرة إلى المورد المعتمد.`
                                                                : `Purchase order ${approvedOrder} was auto-dispatched to default verified supplier.`
                                                            }
                                                        </p>
                                                        <button 
                                                            onClick={() => setApprovedOrder(null)} 
                                                            className="mt-6 px-4 py-1.5 rounded-lg border border-border text-[11px] font-semibold hover:bg-muted text-foreground transition duration-200 cursor-pointer"
                                                        >
                                                            {isAr ? "إعادة تعيين المحاكاة" : "Reset Simulation"}
                                                        </button>
                                                    </div>
                                                ) : (
                                                    <div className="flex flex-col gap-3 text-xs">
                                                        <p className="text-muted-foreground leading-relaxed">
                                                            {isAr 
                                                                ? "يقوم محرك الذكاء الاصطناعي بتشغيل دورة تدقيق يومية لتحديد فجوات الإمداد وصياغة أوامر شراء ذكية مسبقاً لتوفير التكاليف وضمان الخدمة."
                                                                : "The AI platform drafts purchase recommendations automatically based on historical consumption, lead time, and safety buffers."
                                                            }
                                                        </p>
                                                        
                                                        {/* Recommendation Details */}
                                                        <div className="p-3 rounded-lg bg-background border border-border/40 font-mono text-[11px] mt-2">
                                                            <div className="flex justify-between py-1">
                                                                <span className="text-muted-foreground">{isAr ? "المنتج المقترح" : "Item"}:</span>
                                                                <span className="font-bold text-foreground">Flonase Allergy Spray</span>
                                                            </div>
                                                            <div className="flex justify-between py-1">
                                                                <span className="text-muted-foreground">{isAr ? "الكمية الموصى بها" : "Qty Drafted"}:</span>
                                                                <span className="font-bold text-indigo-500">+150 units</span>
                                                            </div>
                                                            <div className="flex justify-between py-1">
                                                                <span className="text-muted-foreground">{isAr ? "فرع الصيدلية وجهة التسليم" : "Destination"}:</span>
                                                                <span className="font-semibold">{isAr ? "فرع المطار الدولي" : "Airport Branch"}</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                )}
                                            </div>

                                            {!approvedOrder && (
                                                <button
                                                    disabled={isApproving}
                                                    onClick={handleApproveOrder}
                                                    className="w-full mt-4 py-3 rounded-lg bg-gradient-to-r from-indigo-500 to-purple-600 text-white font-bold hover:opacity-95 text-xs transition duration-200 flex items-center justify-center gap-2 cursor-pointer"
                                                >
                                                    {isApproving ? (
                                                        <>
                                                            <RefreshCw className="h-4 w-4 animate-spin" />
                                                            {isAr ? "جاري الإرسال والمزامنة..." : "Dispatching order..."}
                                                        </>
                                                    ) : (
                                                        <>
                                                            <Brain className="h-4 w-4" />
                                                            {isAr ? "الموافقة وإرسال طلب التوريد تلقائياً" : "Approve & Dispatch Auto-Order"}
                                                        </>
                                                    )}
                                                </button>
                                            )}
                                        </div>
                                    </div>
                                )}
                            </main>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    )
}
