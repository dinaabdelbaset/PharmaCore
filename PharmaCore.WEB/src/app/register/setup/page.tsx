"use client"

import { useState, useEffect } from "react"
import { useRouter } from "next/navigation"
import { useLanguage } from "@/components/providers/language-provider"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { toast } from "sonner"
import { 
    Loader2, 
    Building2, 
    Smartphone, 
    Globe2, 
    Network, 
    Check, 
    BrainCircuit, 
    RefreshCw, 
    Lock, 
    CreditCard,
    ArrowRight,
    ArrowLeft,
    Sparkles,
    ShieldCheck,
    Cpu,
    Zap,
    Plus,
    Trash2,
    Home
} from "lucide-react"
import apiClient from "@/lib/api-client"

interface BranchNode {
    id: string;
    name: string;
    devices: number;
}

interface RegionNode {
    id: string;
    name: string;
    branches: BranchNode[];
}

interface TenantNode {
    id: string;
    name: string;
    regions: RegionNode[];
}

export default function RegisterSetupPage() {
    const router = useRouter()
    const { language, t } = useLanguage()
    const dir = language === "ar" ? "rtl" : "ltr"
    const isRTL = dir === "rtl"
    
    // Tree State
    const [orgName, setOrgName] = useState("My Pharmacy")
    const [tree, setTree] = useState<TenantNode[]>([
        {
            id: "tenant-1",
            name: "Main Tenant",
            regions: [
                {
                    id: "region-1",
                    name: "Cairo Region",
                    branches: [
                        {
                            id: "branch-1",
                            name: "Downtown Branch",
                            devices: 1
                        }
                    ]
                }
            ]
        }
    ])

    // Sync default localized names based on language on mount
    useEffect(() => {
        if (tree.length === 1 && tree[0].name === "Main Tenant") {
            setTree([
                {
                    id: "tenant-1",
                    name: language === "ar" ? "المقر الرئيسي" : "Main Tenant",
                    regions: [
                        {
                            id: "region-1",
                            name: language === "ar" ? "منطقة القاهرة" : "Cairo Region",
                            branches: [
                                {
                                    id: "branch-1",
                                    name: language === "ar" ? "فرع وسط البلد" : "Downtown Branch",
                                    devices: 1
                                }
                            ]
                        }
                    ]
                }
            ]);
            setOrgName(language === "ar" ? "صيدلية العزبي" : "My Pharmacy");
        }
    }, [language]);

    // Calculate aggregated scales from tree
    const totalTenants = tree.length;
    const totalRegions = tree.reduce((acc, tenant) => acc + tenant.regions.length, 0);
    const totalBranches = tree.reduce((acc, tenant) => 
        acc + tenant.regions.reduce((rAcc, region) => rAcc + region.branches.length, 0), 0);
    const totalDevices = tree.reduce((acc, tenant) => 
        acc + tenant.regions.reduce((rAcc, region) => 
            region.branches.reduce((bAcc, branch) => bAcc + branch.devices, 0) + rAcc, 0), 0);

    // Addons
    const [addons, setAddons] = useState({
        aiForecast: false,
        multiBranchSync: false,
        customApi: false,
        dedicatedDb: false,
        prioritySupport: false
    })

    // Billing cycle
    const [isYearly, setIsYearly] = useState(false)
    const [isProcessing, setIsProcessing] = useState(false)

    // Card Details
    const [cardName, setCardName] = useState("")
    const [cardNumber, setCardNumber] = useState("")
    const [cardExpiry, setCardExpiry] = useState("")
    const [cardCvv, setCardCvv] = useState("")
    const [isFlipped, setIsFlipped] = useState(false)

    // Calculate Price dynamically based on aggregated scales
    const basePrice = 19 // Includes 1 Tenant, 1 Region, 1 Branch, 1 Device
    const tenantPrice = Math.max(0, totalTenants - 1) * 15
    const regionPrice = Math.max(0, totalRegions - 1) * 10
    const branchPrice = Math.max(0, totalBranches - 1) * 20
    const devicePrice = Math.max(0, totalDevices - 1) * 5

    const addonPrice = 
        (addons.aiForecast ? 29 : 0) +
        (addons.multiBranchSync ? 39 : 0) +
        (addons.customApi ? 49 : 0) +
        (addons.dedicatedDb ? 79 : 0) +
        (addons.prioritySupport ? 19 : 0)

    const subtotal = basePrice + tenantPrice + regionPrice + branchPrice + devicePrice + addonPrice
    const discount = isYearly ? subtotal * 0.2 : 0
    const total = subtotal - discount

    // Check if user is logged in
    useEffect(() => {
        const token = localStorage.getItem("token")
        if (!token) {
            router.push("/register")
        }
    }, [router])

    // Tree State Operations
    const handleAddTenant = () => {
        if (totalTenants >= 5) {
            toast.error(language === "ar" ? "تم الوصول للحد الأقصى للمؤسسات الفرعية (5)" : "Maximum tenants limit reached (5)");
            return;
        }
        const newTenant: TenantNode = {
            id: `tenant-${Date.now()}`,
            name: `${language === "ar" ? "مؤسسة فرعية جديدة" : "New Tenant"}`,
            regions: [
                {
                    id: `region-${Date.now()}`,
                    name: `${language === "ar" ? "منطقة جديدة" : "New Region"}`,
                    branches: [
                        {
                            id: `branch-${Date.now()}`,
                            name: `${language === "ar" ? "فرع جديد" : "New Branch"}`,
                            devices: 1
                        }
                    ]
                }
            ]
        };
        setTree(prev => [...prev, newTenant]);
    };

    const handleDeleteTenant = (tenantId: string) => {
        if (tree.length <= 1) {
            toast.error(language === "ar" ? "يجب أن تحتوي المؤسسة على مؤسسة فرعية واحدة على الأقل" : "Organization must have at least one tenant");
            return;
        }
        setTree(prev => prev.filter(t => t.id !== tenantId));
    };

    const handleRenameTenant = (tenantId: string, name: string) => {
        setTree(prev => prev.map(t => t.id === tenantId ? { ...t, name } : t));
    };

    const handleAddRegion = (tenantId: string) => {
        if (totalRegions >= 10) {
            toast.error(language === "ar" ? "تم الوصول للحد الأقصى للمناطق (10)" : "Maximum regions limit reached (10)");
            return;
        }
        const newRegion: RegionNode = {
            id: `region-${Date.now()}`,
            name: `${language === "ar" ? "منطقة جديدة" : "New Region"}`,
            branches: [
                {
                    id: `branch-${Date.now()}`,
                    name: `${language === "ar" ? "فرع جديد" : "New Branch"}`,
                    devices: 1
                }
            ]
        };
        setTree(prev => prev.map(t => {
            if (t.id === tenantId) {
                return {
                    ...t,
                    regions: [...t.regions, newRegion]
                };
            }
            return t;
        }));
    };

    const handleDeleteRegion = (tenantId: string, regionId: string) => {
        const tenant = tree.find(t => t.id === tenantId);
        if (tenant && tenant.regions.length <= 1) {
            toast.error(language === "ar" ? "يجب أن تحتوي كل مؤسسة فرعية على منطقة واحدة على الأقل" : "Each tenant must have at least one region");
            return;
        }
        setTree(prev => prev.map(t => {
            if (t.id === tenantId) {
                return {
                    ...t,
                    regions: t.regions.filter(r => r.id !== regionId)
                };
            }
            return t;
        }));
    };

    const handleRenameRegion = (tenantId: string, regionId: string, name: string) => {
        setTree(prev => prev.map(t => {
            if (t.id === tenantId) {
                return {
                    ...t,
                    regions: t.regions.map(r => r.id === regionId ? { ...r, name } : r)
                };
            }
            return t;
        }));
    };

    const handleAddBranch = (tenantId: string, regionId: string) => {
        if (totalBranches >= 20) {
            toast.error(language === "ar" ? "تم الوصول للحد الأقصى للفروع (20)" : "Maximum branches limit reached (20)");
            return;
        }
        const newBranch: BranchNode = {
            id: `branch-${Date.now()}`,
            name: `${language === "ar" ? "فرع جديد" : "New Branch"}`,
            devices: 1
        };
        setTree(prev => prev.map(t => {
            if (t.id === tenantId) {
                return {
                    ...t,
                    regions: t.regions.map(r => {
                        if (r.id === regionId) {
                            return {
                                ...r,
                                branches: [...r.branches, newBranch]
                            };
                        }
                        return r;
                    })
                };
            }
            return t;
        }));
    };

    const handleDeleteBranch = (tenantId: string, regionId: string, branchId: string) => {
        const tenant = tree.find(t => t.id === tenantId);
        const region = tenant?.regions.find(r => r.id === regionId);
        if (region && region.branches.length <= 1) {
            toast.error(language === "ar" ? "يجب أن تحتوي كل منطقة على فرع واحد على الأقل" : "Each region must have at least one branch");
            return;
        }
        setTree(prev => prev.map(t => {
            if (t.id === tenantId) {
                return {
                    ...t,
                    regions: t.regions.map(r => {
                        if (r.id === regionId) {
                            return {
                                ...r,
                                branches: r.branches.filter(b => b.id !== branchId)
                            };
                        }
                        return r;
                    })
                };
            }
            return t;
        }));
    };

    const handleRenameBranch = (tenantId: string, regionId: string, branchId: string, name: string) => {
        setTree(prev => prev.map(t => {
            if (t.id === tenantId) {
                return {
                    ...t,
                    regions: t.regions.map(r => {
                        if (r.id === regionId) {
                            return {
                                ...r,
                                branches: r.branches.map(b => b.id === branchId ? { ...b, name } : b)
                            };
                        }
                        return r;
                    })
                };
            }
            return t;
        }));
    };

    const handleUpdateBranchDevices = (tenantId: string, regionId: string, branchId: string, increment: boolean) => {
        const tenant = tree.find(t => t.id === tenantId);
        const region = tenant?.regions.find(r => r.id === regionId);
        const branch = region?.branches.find(b => b.id === branchId);
        if (!branch) return;

        if (increment) {
            if (totalDevices >= 50) {
                toast.error(language === "ar" ? "تم الوصول للحد الأقصى للأجهزة (50)" : "Maximum devices limit reached (50)");
                return;
            }
        } else {
            if (branch.devices <= 1) {
                toast.error(language === "ar" ? "يجب أن يحتوي كل فرع على جهاز واحد على الأقل" : "Each branch must have at least one device");
                return;
            }
        }

        setTree(prev => prev.map(t => {
            if (t.id === tenantId) {
                return {
                    ...t,
                    regions: t.regions.map(r => {
                        if (r.id === regionId) {
                            return {
                                ...r,
                                branches: r.branches.map(b => {
                                    if (b.id === branchId) {
                                        return {
                                            ...b,
                                            devices: increment ? b.devices + 1 : b.devices - 1
                                        };
                                    }
                                    return b;
                                })
                            };
                        }
                        return r;
                    })
                };
            }
            return t;
        }));
    };

    const handleCheckoutSubmit = async (e: React.FormEvent) => {
        e.preventDefault()

        if (!cardName.trim() || !cardNumber.trim() || !cardExpiry.trim() || !cardCvv.trim()) {
            toast.error(language === "ar" ? "برجاء ملء جميع تفاصيل الدفع" : "Please fill in all payment details")
            return
        }

        setIsProcessing(true)

        try {
            const payload = {
                tenants: Number(totalTenants),
                regions: Number(totalRegions),
                branches: Number(totalBranches),
                devices: Number(totalDevices),
                addons: addons
            }

            await apiClient.post("/auth/setup", payload)

            localStorage.setItem("setup_completed", "true")
            localStorage.setItem("demoMode", "false") // Live mode activated
            toast.success(t("setup.success"))
            router.push("/dashboard")
        } catch (error: unknown) {
            console.error("Setup API Error:", error)
            let errorMessage = language === "ar" 
                ? "فشل إعداد الحساب. يرجى المحاولة مرة أخرى." 
                : "Setup failed. Please try again."

            const apiError = error as { response?: { data?: string | { message?: string; error?: string } }; message?: string };

            if (apiError.response?.data) {
                if (typeof apiError.response.data === "string") {
                    errorMessage = apiError.response.data
                } else if (apiError.response.data.message) {
                    errorMessage = apiError.response.data.message
                } else if (apiError.response.data.error) {
                    errorMessage = apiError.response.data.error
                }
            } else if (apiError.message) {
                errorMessage = apiError.message
            }

            toast.error(errorMessage)
        } finally {
            setIsProcessing(false)
        }
    }

    const toggleAddon = (key: keyof typeof addons) => {
        setAddons(prev => ({ ...prev, [key]: !prev[key] }))
    }

    // Helper functions for formatting payment inputs
    const formatCardNumber = (value: string) => {
        const v = value.replace(/\s+/g, "").replace(/[^0-9]/gi, "")
        const parts = []
        for (let i = 0; i < v.length; i += 4) {
            parts.push(v.substring(i, i + 4))
        }
        return parts.join(" ")
    }

    const getFormattedCardNumberForDisplay = (num: string) => {
        const clean = num.replace(/\s+/g, "").replace(/[^0-9]/g, "")
        const padded = clean.padEnd(16, "•")
        const chunks = []
        for (let i = 0; i < 16; i += 4) {
            chunks.push(padded.substring(i, i + 4))
        }
        return chunks.join(" ")
    }

    const formatExpiry = (value: string) => {
        const v = value.replace(/\s+/g, "").replace(/[^0-9]/gi, "")
        
        // Auto-prefix month with 0 if first digit is > 1
        if (v.length === 1 && v !== '0' && v !== '1') {
            return `0${v}/`
        }
        
        if (v.length > 2) {
            return `${v.slice(0, 2)}/${v.slice(2, 4)}`
        }
        return v
    }

    const handleCardNumberChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const formatted = formatCardNumber(e.target.value)
        setCardNumber(formatted.slice(0, 19))
    }

    const handleExpiryChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const raw = e.target.value.replace("/", "")
        const formatted = formatExpiry(raw)
        setCardExpiry(formatted.slice(0, 5))
    }

    const handleCvvChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const raw = e.target.value.replace(/[^0-9]/g, "")
        setCardCvv(raw.slice(0, 4))
    }

    // Detect card type (Visa / Mastercard)
    const getCardType = () => {
        if (cardNumber.startsWith("4")) return "visa"
        if (cardNumber.startsWith("5")) return "mastercard"
        return "generic"
    }

    return (
        <div dir={dir} className="min-h-screen bg-neutral-950 text-neutral-100 flex flex-col items-center py-10 px-4 sm:px-6 lg:px-8 relative overflow-hidden">
            {/* Ambient Background Blur Blobs */}
            <div className="absolute top-[-10%] left-[-10%] w-[50vw] h-[50vw] bg-emerald-500/10 rounded-full blur-[120px] pointer-events-none animate-pulse duration-[10000ms]" />
            <div className="absolute bottom-[-10%] right-[-10%] w-[50vw] h-[50vw] bg-teal-500/10 rounded-full blur-[120px] pointer-events-none animate-pulse duration-[8000ms]" />
            
            {/* Grid Pattern overlay */}
            <div className="absolute inset-0 bg-[linear-gradient(to_right,#1f2937_1px,transparent_1px),linear-gradient(to_bottom,#1f2937_1px,transparent_1px)] bg-[size:4rem_4rem] [mask-image:radial-gradient(ellipse_60%_50%_at_50%_40%,#000_70%,transparent_100%)] opacity-[0.15] pointer-events-none" />

            <div className="w-full max-w-6xl space-y-10 z-10">
                {/* Heading */}
                <div className="text-center space-y-4">
                    <div className="inline-flex items-center gap-1.5 px-3.5 py-1 rounded-full bg-emerald-500/10 text-emerald-400 border border-emerald-500/20 text-xs font-semibold shadow-[0_0_15px_rgba(16,185,129,0.08)]">
                        <Sparkles className="h-3.5 w-3.5" />
                        <span>{language === "ar" ? "الخطوة الأخيرة: تهيئة الاشتراك" : "Final Step: Subscription Setup"}</span>
                    </div>
                    <h1 className="text-3xl sm:text-4xl lg:text-5xl font-extrabold tracking-tight bg-gradient-to-r from-emerald-400 via-teal-400 to-cyan-400 bg-clip-text text-transparent py-1 leading-tight">
                        {t("setup.title")}
                    </h1>
                    <p className="text-sm sm:text-base text-neutral-400 max-w-2xl mx-auto px-4">
                        {t("setup.subtitle")}
                    </p>
                </div>

                {/* Top Stepper Progress */}
                <div className="w-full max-w-3xl mx-auto px-4">
                    <div className="flex items-start justify-between relative">
                        {/* Stepper progress connector line */}
                        <div className="absolute left-10 right-10 top-5 -translate-y-1/2 h-[3px] bg-neutral-800/60 rounded-full z-0" />
                        <div 
                            className="absolute top-5 -translate-y-1/2 h-[3px] bg-gradient-to-r from-emerald-500 via-teal-500 to-cyan-500 transition-all duration-500 z-0 rounded-full shadow-[0_0_10px_rgba(16,185,129,0.3)]" 
                            style={{
                                width: "calc(50% - 40px)",
                                [dir === "rtl" ? "right" : "left"]: "40px"
                            }}
                        />

                        {/* Step 1 */}
                        <div className="flex flex-col items-center z-10 w-20 group select-none">
                            <div className="w-10 h-10 rounded-full flex items-center justify-center bg-emerald-500 border border-emerald-400/30 text-neutral-950 shadow-[0_0_15px_rgba(16,185,129,0.3)] group-hover:scale-105 transition-all duration-300 cursor-default">
                                <Check className="h-5 w-5 stroke-[2.5]" />
                            </div>
                            <span className="text-[10px] sm:text-xs mt-2.5 font-bold text-emerald-400 text-center leading-tight transition-colors group-hover:text-emerald-300">
                                {t("setup.step1")}
                            </span>
                        </div>

                        {/* Step 2 */}
                        <div className="flex flex-col items-center z-10 w-20 group select-none">
                            <div className="relative flex items-center justify-center">
                                {/* Ambient Glow */}
                                <div className="absolute inset-0 rounded-full bg-emerald-500/20 blur-md animate-pulse pointer-events-none" />
                                <div className="w-10 h-10 rounded-full flex items-center justify-center bg-neutral-950 border-2 border-emerald-500 text-emerald-400 shadow-[0_0_20px_rgba(16,185,129,0.4)] scale-110 z-10 transition-all duration-300 cursor-default">
                                    <Cpu className="h-5 w-5 animate-pulse" />
                                </div>
                            </div>
                            <span className="text-[10px] sm:text-xs mt-2.5 font-bold text-neutral-200 text-center leading-tight transition-colors group-hover:text-white">
                                {t("setup.step2")}
                            </span>
                        </div>

                        {/* Step 3 */}
                        <div className="flex flex-col items-center z-10 w-20 group select-none">
                            <div className="w-10 h-10 rounded-full flex items-center justify-center bg-neutral-950 border border-neutral-800 text-neutral-600 transition-all duration-300 group-hover:border-neutral-700/50 group-hover:text-neutral-400 cursor-default">
                                <Zap className="h-5 w-5" />
                            </div>
                            <span className="text-[10px] sm:text-xs mt-2.5 font-medium text-neutral-500 text-center leading-tight transition-colors group-hover:text-neutral-400">
                                {t("setup.step3")}
                            </span>
                        </div>
                    </div>
                </div>

                {/* Main Content Grid */}
                <div className="grid grid-cols-1 lg:grid-cols-12 gap-8 items-start">
                    
                    {/* Left Panel: Tree Hierarchy & Addons (7 Cols) */}
                    <div className="lg:col-span-7 space-y-6">
                        
                        {/* Tree Hierarchy Card */}
                        <Card className="border-neutral-900 bg-neutral-900/20 backdrop-blur-xl shadow-2xl rounded-3xl overflow-hidden relative group">
                            <div className="absolute top-0 left-0 w-full h-[1px] bg-gradient-to-r from-transparent via-emerald-500/20 to-transparent" />
                            <CardHeader className="pb-5 border-b border-neutral-900/60">
                                <div className="flex items-center gap-2">
                                    <div className="p-2 rounded-xl bg-emerald-500/10 text-emerald-400">
                                        <Network className="h-5 w-5" />
                                    </div>
                                    <div>
                                        <CardTitle className="text-lg font-bold text-neutral-100">
                                            {language === "ar" ? "هيكل فروع وموارد الصيدلية" : "Pharmacy Tree & Resource Hierarchy"}
                                        </CardTitle>
                                        <CardDescription className="text-xs text-neutral-400">
                                            {language === "ar" ? "قم بتعديل هيكل المؤسسة، الفروع، والمناطق ووزع الأجهزة كما ترغب" : "Customize organization, region, and branch structure and allot device allocations"}
                                        </CardDescription>
                                    </div>
                                </div>
                            </CardHeader>
                            <CardContent className="p-6 space-y-6">
                                {/* Tree limits indicator badges */}
                                <div className="grid grid-cols-2 sm:grid-cols-4 gap-2.5 bg-neutral-950/40 p-4 rounded-2xl border border-neutral-900">
                                    <div className="flex flex-col items-center justify-center p-2 rounded-xl bg-neutral-900/40 border border-neutral-800/80">
                                        <span className="text-[10px] text-neutral-500 font-semibold uppercase">{language === "ar" ? "المؤسسات" : "Tenants"}</span>
                                        <span className={`text-sm font-bold font-mono ${totalTenants >= 5 ? "text-amber-400" : "text-emerald-400"}`}>{totalTenants}/5</span>
                                    </div>
                                    <div className="flex flex-col items-center justify-center p-2 rounded-xl bg-neutral-900/40 border border-neutral-800/80">
                                        <span className="text-[10px] text-neutral-500 font-semibold uppercase">{language === "ar" ? "المناطق" : "Regions"}</span>
                                        <span className={`text-sm font-bold font-mono ${totalRegions >= 10 ? "text-amber-400" : "text-emerald-400"}`}>{totalRegions}/10</span>
                                    </div>
                                    <div className="flex flex-col items-center justify-center p-2 rounded-xl bg-neutral-900/40 border border-neutral-800/80">
                                        <span className="text-[10px] text-neutral-500 font-semibold uppercase">{language === "ar" ? "الفروع" : "Branches"}</span>
                                        <span className={`text-sm font-bold font-mono ${totalBranches >= 20 ? "text-amber-400" : "text-emerald-400"}`}>{totalBranches}/20</span>
                                    </div>
                                    <div className="flex flex-col items-center justify-center p-2 rounded-xl bg-neutral-900/40 border border-neutral-800/80">
                                        <span className="text-[10px] text-neutral-500 font-semibold uppercase">{language === "ar" ? "الأجهزة" : "Devices"}</span>
                                        <span className={`text-sm font-bold font-mono ${totalDevices >= 50 ? "text-amber-400" : "text-emerald-400"}`}>{totalDevices}/50</span>
                                    </div>
                                </div>

                                {/* Tree Structure */}
                                <div className="space-y-6 pt-2">
                                    {/* Root Organization Node */}
                                    <div className="relative p-5 rounded-2xl bg-gradient-to-br from-emerald-500/10 to-emerald-950/20 border border-emerald-500/30 shadow-lg">
                                        <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
                                            <div className="flex items-center gap-3 flex-1">
                                                <div className="p-2.5 rounded-xl bg-emerald-500/20 text-emerald-400 border border-emerald-500/30">
                                                    <Home className="h-5 w-5" />
                                                </div>
                                                <div className="flex-1 space-y-1">
                                                    <label className="text-[10px] uppercase tracking-wider text-emerald-400 font-bold block">
                                                        {language === "ar" ? "المؤسسة الرئيسية" : "Main Organization"}
                                                    </label>
                                                    <Input
                                                        type="text"
                                                        value={orgName}
                                                        onChange={(e) => setOrgName(e.target.value)}
                                                        className="h-9 font-bold text-neutral-100 bg-neutral-950/40 border-neutral-850 focus-visible:ring-emerald-500"
                                                    />
                                                </div>
                                            </div>
                                            <div className="flex flex-col items-end gap-1.5 shrink-0">
                                                <Button
                                                    type="button"
                                                    size="sm"
                                                    disabled={totalTenants >= 5}
                                                    onClick={handleAddTenant}
                                                    className="w-full sm:w-auto bg-emerald-500/15 hover:bg-emerald-500/25 text-emerald-400 border border-emerald-500/30 h-9 px-3.5 text-xs font-bold rounded-xl flex items-center justify-center gap-1.5 transition-colors"
                                                >
                                                    <Plus className="h-3.5 w-3.5" />
                                                    <span>{language === "ar" ? "إضافة مؤسسة فرعية" : "Add Tenant"}</span>
                                                </Button>
                                                {totalTenants >= 5 && (
                                                    <span className="text-[9px] text-amber-500 font-medium">
                                                        {language === "ar" ? "الحد الأقصى للمؤسسات الفرعية هو 5" : "Max limit of 5 Tenants reached"}
                                                    </span>
                                                )}
                                            </div>
                                        </div>
                                    </div>

                                    {/* Tenant Node list */}
                                    <div className={`space-y-6 relative ${isRTL ? "pr-6 border-r" : "pl-6 border-l"} border-emerald-500/15`}>
                                        {tree.map((tenant, tenantIndex) => {
                                            const isLastTenant = tenantIndex === tree.length - 1;
                                            return (
                                                <div key={tenant.id} className="relative pt-2">
                                                    {/* Connection Line */}
                                                    <div 
                                                        className={`absolute top-0 bottom-0 ${isRTL ? "-right-[25px] border-r" : "-left-[25px] border-l"} border-emerald-500/20`} 
                                                        style={{ bottom: isLastTenant ? "calc(100% - 28px)" : "0px" }}
                                                    />
                                                    <div 
                                                        className={`absolute top-[28px] -translate-y-1/2 ${isRTL ? "-right-[25px] w-[25px]" : "-left-[25px] w-[25px]"} h-[2px] bg-emerald-500/20`} 
                                                    />

                                                    {/* Tenant Card */}
                                                    <div className="p-4 rounded-xl bg-teal-950/10 border border-teal-500/20 hover:border-teal-500/35 shadow-md transition-all duration-300">
                                                        <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
                                                            <div className="flex items-center gap-2.5 flex-1">
                                                                <div className="p-2 rounded-lg bg-teal-500/10 text-teal-400 border border-teal-500/20">
                                                                    <Network className="h-4 w-4" />
                                                                </div>
                                                                <div className="flex-1 space-y-1">
                                                                    <label className="text-[9px] uppercase tracking-wider text-teal-400 font-semibold block">
                                                                        {language === "ar" ? "المؤسسة الفرعية" : "Tenant"} #{tenantIndex + 1}
                                                                    </label>
                                                                    <Input
                                                                        type="text"
                                                                        value={tenant.name}
                                                                        onChange={(e) => handleRenameTenant(tenant.id, e.target.value)}
                                                                        className="h-8 text-sm text-neutral-100 bg-neutral-950/30 border-neutral-850 focus-visible:ring-teal-500"
                                                                    />
                                                                </div>
                                                            </div>

                                                            <div className="flex items-center gap-2 shrink-0">
                                                                <Button
                                                                    type="button"
                                                                    size="sm"
                                                                    disabled={totalRegions >= 10}
                                                                    onClick={() => handleAddRegion(tenant.id)}
                                                                    className="bg-teal-500/15 hover:bg-teal-500/25 text-teal-400 border border-teal-500/20 h-8 px-2.5 text-xs font-semibold rounded-lg flex items-center gap-1 transition-colors"
                                                                >
                                                                    <Plus className="h-3 w-3" />
                                                                    <span>{language === "ar" ? "منطقة" : "Region"}</span>
                                                                </Button>
                                                                
                                                                <Button
                                                                    type="button"
                                                                    size="icon"
                                                                    disabled={tree.length <= 1}
                                                                    onClick={() => handleDeleteTenant(tenant.id)}
                                                                    className="h-8 w-8 text-neutral-500 hover:text-red-400 hover:bg-red-500/10 border border-neutral-800 hover:border-red-500/30 rounded-lg transition-all"
                                                                >
                                                                    <Trash2 className="h-3.5 w-3.5" />
                                                                </Button>
                                                            </div>
                                                        </div>
                                                        
                                                        {/* Limit Helper Text */}
                                                        {totalRegions >= 10 && (
                                                            <div className="mt-1.5 text-right rtl:text-left">
                                                                <span className="text-[9px] text-amber-500 font-medium">
                                                                    {language === "ar" ? "تم الوصول للحد الأقصى للمناطق (10)" : "Max regions limit reached (10)"}
                                                                </span>
                                                            </div>
                                                        )}

                                                        {/* Region Nodes under this Tenant */}
                                                        <div className={`mt-4 space-y-5 relative ${isRTL ? "pr-5 border-r" : "pl-5 border-l"} border-teal-500/15`}>
                                                            {tenant.regions.map((region, regionIndex) => {
                                                                const isLastRegion = regionIndex === tenant.regions.length - 1;
                                                                return (
                                                                    <div key={region.id} className="relative pt-1">
                                                                        {/* Connection Line */}
                                                                        <div 
                                                                            className={`absolute top-0 bottom-0 ${isRTL ? "-right-[21px] border-r" : "-left-[21px] border-l"} border-teal-500/20`} 
                                                                            style={{ bottom: isLastRegion ? "calc(100% - 26px)" : "0px" }}
                                                                        />
                                                                        <div 
                                                                            className={`absolute top-[26px] -translate-y-1/2 ${isRTL ? "-right-[21px] w-[21px]" : "-left-[21px] w-[21px]"} h-[2px] bg-teal-500/20`} 
                                                                        />

                                                                        {/* Region Card */}
                                                                        <div className="p-3.5 rounded-lg bg-cyan-950/10 border border-cyan-500/20 hover:border-cyan-500/35 shadow-sm transition-all duration-300">
                                                                            <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-3">
                                                                                <div className="flex items-center gap-2 flex-1">
                                                                                    <div className="p-1.5 rounded bg-cyan-500/10 text-cyan-400 border border-cyan-500/20">
                                                                                        <Globe2 className="h-3.5 w-3.5" />
                                                                                    </div>
                                                                                    <div className="flex-1 space-y-1">
                                                                                        <label className="text-[8px] uppercase tracking-wider text-cyan-400 font-semibold block">
                                                                                            {language === "ar" ? "المنطقة" : "Region"} #{regionIndex + 1}
                                                                                        </label>
                                                                                        <Input
                                                                                            type="text"
                                                                                            value={region.name}
                                                                                            onChange={(e) => handleRenameRegion(tenant.id, region.id, e.target.value)}
                                                                                            className="h-7 text-xs text-neutral-100 bg-neutral-950/30 border-neutral-855 focus-visible:ring-cyan-500"
                                                                                        />
                                                                                    </div>
                                                                                </div>

                                                                                <div className="flex items-center gap-1.5 shrink-0">
                                                                                    <Button
                                                                                        type="button"
                                                                                        size="sm"
                                                                                        disabled={totalBranches >= 20}
                                                                                        onClick={() => handleAddBranch(tenant.id, region.id)}
                                                                                        className="bg-cyan-500/15 hover:bg-cyan-500/25 text-cyan-400 border border-cyan-500/20 h-7 px-2 text-[10px] font-semibold rounded flex items-center gap-0.5 transition-colors"
                                                                                    >
                                                                                        <Plus className="h-2.5 w-2.5" />
                                                                                        <span>{language === "ar" ? "فرع" : "Branch"}</span>
                                                                                    </Button>
                                                                                    
                                                                                    <Button
                                                                                        type="button"
                                                                                        size="icon"
                                                                                        disabled={tenant.regions.length <= 1}
                                                                                        onClick={() => handleDeleteRegion(tenant.id, region.id)}
                                                                                        className="h-7 w-7 text-neutral-500 hover:text-red-400 hover:bg-red-500/10 border border-neutral-800 hover:border-red-500/30 rounded transition-all"
                                                                                    >
                                                                                        <Trash2 className="h-3 w-3" />
                                                                                    </Button>
                                                                                </div>
                                                                            </div>

                                                                            {/* Limit Helper Text */}
                                                                            {totalBranches >= 20 && (
                                                                                <div className="mt-1 text-right rtl:text-left">
                                                                                    <span className="text-[8px] text-amber-500 font-medium">
                                                                                        {language === "ar" ? "تم الوصول للحد الأقصى للفروع (20)" : "Max branches limit reached (20)"}
                                                                                    </span>
                                                                                </div>
                                                                            )}

                                                                            {/* Branch Nodes under this Region */}
                                                                            <div className={`mt-3.5 space-y-4 relative ${isRTL ? "pr-5 border-r" : "pl-5 border-l"} border-cyan-500/15`}>
                                                                                {region.branches.map((branch, branchIndex) => {
                                                                                    const isLastBranch = branchIndex === region.branches.length - 1;
                                                                                    return (
                                                                                        <div key={branch.id} className="relative pt-0.5">
                                                                                            {/* Connection Line */}
                                                                                            <div 
                                                                                                className={`absolute top-0 bottom-0 ${isRTL ? "-right-[21px] border-r" : "-left-[21px] border-l"} border-cyan-500/20`} 
                                                                                                style={{ bottom: isLastBranch ? "calc(100% - 24px)" : "0px" }}
                                                                                            />
                                                                                            <div 
                                                                                                className={`absolute top-[24px] -translate-y-1/2 ${isRTL ? "-right-[21px] w-[21px]" : "-left-[21px] w-[21px]"} h-[2px] bg-cyan-500/20`} 
                                                                                            />

                                                                                            {/* Branch Card */}
                                                                                            <div className="p-3 rounded-lg bg-indigo-950/10 border border-indigo-500/20 hover:border-indigo-500/35 shadow-sm transition-all duration-300">
                                                                                                <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-3">
                                                                                                    <div className="flex items-center gap-2 flex-1">
                                                                                                        <div className="p-1 rounded bg-indigo-500/10 text-indigo-400 border border-indigo-500/20">
                                                                                                            <Building2 className="h-3 w-3" />
                                                                                                        </div>
                                                                                                        <div className="flex-1 space-y-1">
                                                                                                            <label className="text-[8px] uppercase tracking-wider text-indigo-400 font-semibold block">
                                                                                                                {language === "ar" ? "الفرع" : "Branch"} #{branchIndex + 1}
                                                                                                            </label>
                                                                                                            <Input
                                                                                                                type="text"
                                                                                                                value={branch.name}
                                                                                                                onChange={(e) => handleRenameBranch(tenant.id, region.id, branch.id, e.target.value)}
                                                                                                                className="h-7 text-xs text-neutral-100 bg-neutral-950/30 border-neutral-800 focus-visible:ring-indigo-500"
                                                                                                            />
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <div className="flex items-center justify-between sm:justify-end gap-3 shrink-0">
                                                                                                        {/* POS Devices Counter */}
                                                                                                        <div className="flex items-center gap-1.5 bg-neutral-950/60 border border-neutral-800/80 px-2 py-0.5 rounded-md">
                                                                                                            <span className="text-[8px] uppercase tracking-wider text-neutral-400 font-medium">
                                                                                                                {language === "ar" ? "أجهزة" : "Devices"}
                                                                                                            </span>
                                                                                                            
                                                                                                            <button
                                                                                                                type="button"
                                                                                                                disabled={branch.devices <= 1}
                                                                                                                onClick={() => handleUpdateBranchDevices(tenant.id, region.id, branch.id, false)}
                                                                                                                className="w-5 h-5 flex items-center justify-center rounded bg-neutral-900 border border-neutral-800 text-neutral-400 hover:text-indigo-400 hover:border-indigo-500/30 disabled:opacity-30 disabled:hover:text-neutral-400 disabled:hover:border-neutral-800 transition-all text-xs font-bold"
                                                                                                            >
                                                                                                                -
                                                                                                            </button>
                                                                                                            
                                                                                                            <span className="text-xs font-bold text-indigo-400 font-mono w-4 text-center">
                                                                                                                {branch.devices}
                                                                                                            </span>
                                                                                                            
                                                                                                            <button
                                                                                                                type="button"
                                                                                                                disabled={totalDevices >= 50}
                                                                                                                onClick={() => handleUpdateBranchDevices(tenant.id, region.id, branch.id, true)}
                                                                                                                className="w-5 h-5 flex items-center justify-center rounded bg-neutral-900 border border-neutral-800 text-neutral-400 hover:text-indigo-400 hover:border-indigo-500/30 disabled:opacity-30 disabled:hover:text-neutral-400 disabled:hover:border-neutral-800 transition-all text-xs font-bold"
                                                                                                            >
                                                                                                                +
                                                                                                            </button>
                                                                                                        </div>

                                                                                                        <Button
                                                                                                            type="button"
                                                                                                            size="icon"
                                                                                                            disabled={region.branches.length <= 1}
                                                                                                            onClick={() => handleDeleteBranch(tenant.id, region.id, branch.id)}
                                                                                                            className="h-7 w-7 text-neutral-500 hover:text-red-400 hover:bg-red-500/10 border border-neutral-800 hover:border-red-500/30 rounded transition-all"
                                                                                                        >
                                                                                                            <Trash2 className="h-3.5 w-3.5" />
                                                                                                        </Button>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    );
                                                                                })}
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                );
                                                            })}
                                                        </div>
                                                    </div>
                                                </div>
                                            );
                                        })}
                                    </div>
                                </div>
                            </CardContent>
                        </Card>

                        {/* Addons Card */}
                        <Card className="border-neutral-900 bg-neutral-900/20 backdrop-blur-xl shadow-2xl rounded-3xl overflow-hidden relative">
                            <div className="absolute top-0 left-0 w-full h-[1px] bg-gradient-to-r from-transparent via-teal-500/20 to-transparent" />
                            <CardHeader className="pb-5 border-b border-neutral-900/60">
                                <div className="flex items-center gap-2">
                                    <div className="p-2 rounded-xl bg-teal-500/10 text-teal-400">
                                        <BrainCircuit className="h-5 w-5" />
                                    </div>
                                    <div>
                                        <CardTitle className="text-lg font-bold text-neutral-100">
                                            {t("setup.addons")}
                                        </CardTitle>
                                        <CardDescription className="text-xs text-neutral-400">
                                            {language === "ar" ? "اختر الميزات المتقدمة لتدعيم أعمالك بالذكاء الاصطناعي" : "Add cognitive modules and enterprise tools to your subscription"}
                                        </CardDescription>
                                    </div>
                                </div>
                            </CardHeader>
                            <CardContent className="p-6 space-y-4">
                                {/* AI Forecast */}
                                <div 
                                    onClick={() => toggleAddon("aiForecast")}
                                    className={`group flex items-start gap-4 p-4 border rounded-2xl cursor-pointer transition-all duration-300 select-none ${
                                        addons.aiForecast 
                                            ? "border-teal-500 bg-teal-950/20 shadow-[0_0_22px_rgba(20,184,166,0.15),inset_0_1px_1px_rgba(255,255,255,0.05)] ring-1 ring-teal-500/20" 
                                            : "border-neutral-800 bg-neutral-900/30 hover:border-neutral-700/60 hover:bg-neutral-900/50"
                                    }`}
                                >
                                    <div className={`p-2.5 rounded-xl transition-all duration-300 ${
                                        addons.aiForecast 
                                            ? "bg-teal-500/20 text-teal-400 border border-teal-500/35 shadow-[0_0_12px_rgba(20,184,166,0.2)]" 
                                            : "bg-neutral-950 text-neutral-400 border border-neutral-800 group-hover:border-neutral-700 group-hover:text-neutral-300"
                                    }`}>
                                        <BrainCircuit className="h-5 w-5" />
                                    </div>
                                    <div className="flex-1 space-y-2">
                                        <div className="flex flex-wrap items-center gap-2">
                                            <h4 className="text-sm font-bold text-neutral-200 group-hover:text-white transition-colors">
                                                {language === "ar" ? "التنبؤ بالطلب بالذكاء الاصطناعي" : "AI Demand Forecasting"}
                                            </h4>
                                            <span className={`text-[10px] font-bold font-mono px-2 py-0.5 rounded-full border transition-all duration-300 ${
                                                addons.aiForecast
                                                    ? "bg-teal-500/25 border-teal-500/40 text-teal-300 shadow-[0_2px_8px_rgba(20,184,166,0.15)]"
                                                    : "bg-neutral-950 border-neutral-800 text-neutral-400 group-hover:border-neutral-700 group-hover:text-neutral-300"
                                            }`}>
                                                +$29/{language === "ar" ? "شهرياً" : "mo"}
                                            </span>
                                        </div>
                                        <p className="text-xs text-neutral-400 group-hover:text-neutral-300 leading-relaxed transition-colors">
                                            {language === "ar" ? "تحليل المخزون والتنبؤ المتقدم بالاحتياجات المستقبلية وحركة الصلاحيات" : "Predict medicine expiry patterns and optimize purchase cycles based on machine learning"}
                                        </p>
                                    </div>
                                    <div className={`w-5 h-5 rounded-md border flex items-center justify-center transition-all duration-300 shrink-0 self-start mt-1 ${
                                        addons.aiForecast 
                                            ? "bg-teal-500 border-teal-500 text-neutral-950 scale-100 shadow-[0_0_12px_rgba(20,184,166,0.4)]" 
                                            : "border-neutral-800 bg-neutral-950/60 opacity-60 scale-95 group-hover:opacity-100 group-hover:scale-100 group-hover:border-neutral-700"
                                    }`}>
                                        {addons.aiForecast && <Check className="h-3 w-3 stroke-[3.5]" />}
                                    </div>
                                </div>

                                {/* Multi-Branch Sync */}
                                <div 
                                    onClick={() => toggleAddon("multiBranchSync")}
                                    className={`group flex items-start gap-4 p-4 border rounded-2xl cursor-pointer transition-all duration-300 select-none ${
                                        addons.multiBranchSync 
                                            ? "border-teal-500 bg-teal-950/20 shadow-[0_0_22px_rgba(20,184,166,0.15),inset_0_1px_1px_rgba(255,255,255,0.05)] ring-1 ring-teal-500/20" 
                                            : "border-neutral-800 bg-neutral-900/30 hover:border-neutral-700/60 hover:bg-neutral-900/50"
                                    }`}
                                >
                                    <div className={`p-2.5 rounded-xl transition-all duration-300 ${
                                        addons.multiBranchSync 
                                            ? "bg-teal-500/20 text-teal-400 border border-teal-500/35 shadow-[0_0_12px_rgba(20,184,166,0.2)]" 
                                            : "bg-neutral-950 text-neutral-400 border border-neutral-800 group-hover:border-neutral-700 group-hover:text-neutral-300"
                                    }`}>
                                        <RefreshCw className="h-5 w-5" />
                                    </div>
                                    <div className="flex-1 space-y-2">
                                        <div className="flex flex-wrap items-center gap-2">
                                            <h4 className="text-sm font-bold text-neutral-200 group-hover:text-white transition-colors">
                                                {language === "ar" ? "مزامنة الفروع اللحظية" : "Real-time Multi-Branch Sync"}
                                            </h4>
                                            <span className={`text-[10px] font-bold font-mono px-2 py-0.5 rounded-full border transition-all duration-300 ${
                                                addons.multiBranchSync
                                                    ? "bg-teal-500/25 border-teal-500/40 text-teal-300 shadow-[0_2px_8px_rgba(20,184,166,0.15)]"
                                                    : "bg-neutral-950 border-neutral-800 text-neutral-400 group-hover:border-neutral-700 group-hover:text-neutral-300"
                                            }`}>
                                                +$39/{language === "ar" ? "شهرياً" : "mo"}
                                            </span>
                                        </div>
                                        <p className="text-xs text-neutral-400 group-hover:text-neutral-300 leading-relaxed transition-colors">
                                            {language === "ar" ? "مشاركة بيانات المخزون والمبيعات لحظياً وتلقائياً بين جميع فروع المؤسسة" : "Instantly sync inventory, staff transfer logs, and transaction ledgers cross-regionally"}
                                        </p>
                                    </div>
                                    <div className={`w-5 h-5 rounded-md border flex items-center justify-center transition-all duration-300 shrink-0 self-start mt-1 ${
                                        addons.multiBranchSync 
                                            ? "bg-teal-500 border-teal-500 text-neutral-950 scale-100 shadow-[0_0_12px_rgba(20,184,166,0.4)]" 
                                            : "border-neutral-800 bg-neutral-950/60 opacity-60 scale-95 group-hover:opacity-100 group-hover:scale-100 group-hover:border-neutral-700"
                                    }`}>
                                        {addons.multiBranchSync && <Check className="h-3 w-3 stroke-[3.5]" />}
                                    </div>
                                </div>

                                {/* Custom API */}
                                <div 
                                    onClick={() => toggleAddon("customApi")}
                                    className={`group flex items-start gap-4 p-4 border rounded-2xl cursor-pointer transition-all duration-300 select-none ${
                                        addons.customApi 
                                            ? "border-teal-500 bg-teal-950/20 shadow-[0_0_22px_rgba(20,184,166,0.15),inset_0_1px_1px_rgba(255,255,255,0.05)] ring-1 ring-teal-500/20" 
                                            : "border-neutral-800 bg-neutral-900/30 hover:border-neutral-700/60 hover:bg-neutral-900/50"
                                    }`}
                                >
                                    <div className={`p-2.5 rounded-xl transition-all duration-300 ${
                                        addons.customApi 
                                            ? "bg-teal-500/20 text-teal-400 border border-teal-500/35 shadow-[0_0_12px_rgba(20,184,166,0.2)]" 
                                            : "bg-neutral-950 text-neutral-400 border border-neutral-800 group-hover:border-neutral-700 group-hover:text-neutral-300"
                                    }`}>
                                        <Network className="h-5 w-5" />
                                    </div>
                                    <div className="flex-1 space-y-2">
                                        <div className="flex flex-wrap items-center gap-2">
                                            <h4 className="text-sm font-bold text-neutral-200 group-hover:text-white transition-colors">
                                                {language === "ar" ? "ربط مخصص (API/EHR)" : "Custom API & EHR Integrations"}
                                            </h4>
                                            <span className={`text-[10px] font-bold font-mono px-2 py-0.5 rounded-full border transition-all duration-300 ${
                                                addons.customApi
                                                    ? "bg-teal-500/25 border-teal-500/40 text-teal-300 shadow-[0_2px_8px_rgba(20,184,166,0.15)]"
                                                    : "bg-neutral-950 border-neutral-800 text-neutral-400 group-hover:border-neutral-700 group-hover:text-neutral-300"
                                            }`}>
                                                +$49/{language === "ar" ? "شهرياً" : "mo"}
                                            </span>
                                        </div>
                                        <p className="text-xs text-neutral-400 group-hover:text-neutral-300 leading-relaxed transition-colors">
                                            {language === "ar" ? "الربط مع بوابات المستشفيات والـ API الخارجية والتنبيهات المخصصة" : "Integrate local government health portals, custom EHR APIs, and external webhooks"}
                                        </p>
                                    </div>
                                    <div className={`w-5 h-5 rounded-md border flex items-center justify-center transition-all duration-300 shrink-0 self-start mt-1 ${
                                        addons.customApi 
                                            ? "bg-teal-500 border-teal-500 text-neutral-950 scale-100 shadow-[0_0_12px_rgba(20,184,166,0.4)]" 
                                            : "border-neutral-800 bg-neutral-950/60 opacity-60 scale-95 group-hover:opacity-100 group-hover:scale-100 group-hover:border-neutral-700"
                                    }`}>
                                        {addons.customApi && <Check className="h-3 w-3 stroke-[3.5]" />}
                                    </div>
                                </div>

                                {/* Dedicated DB */}
                                <div 
                                    onClick={() => toggleAddon("dedicatedDb")}
                                    className={`group flex items-start gap-4 p-4 border rounded-2xl cursor-pointer transition-all duration-300 select-none ${
                                        addons.dedicatedDb 
                                            ? "border-teal-500 bg-teal-950/20 shadow-[0_0_22px_rgba(20,184,166,0.15),inset_0_1px_1px_rgba(255,255,255,0.05)] ring-1 ring-teal-500/20" 
                                            : "border-neutral-800 bg-neutral-900/30 hover:border-neutral-700/60 hover:bg-neutral-900/50"
                                    }`}
                                >
                                    <div className={`p-2.5 rounded-xl transition-all duration-300 ${
                                        addons.dedicatedDb 
                                            ? "bg-teal-500/20 text-teal-400 border border-teal-500/35 shadow-[0_0_12px_rgba(20,184,166,0.2)]" 
                                            : "bg-neutral-950 text-neutral-400 border border-neutral-800 group-hover:border-neutral-700 group-hover:text-neutral-300"
                                    }`}>
                                        <Building2 className="h-5 w-5" />
                                    </div>
                                    <div className="flex-1 space-y-2">
                                        <div className="flex flex-wrap items-center gap-2">
                                            <h4 className="text-sm font-bold text-neutral-200 group-hover:text-white transition-colors">
                                                {language === "ar" ? "خادم قاعدة بيانات مخصص" : "Dedicated Database Instance"}
                                            </h4>
                                            <span className={`text-[10px] font-bold font-mono px-2 py-0.5 rounded-full border transition-all duration-300 ${
                                                addons.dedicatedDb
                                                    ? "bg-teal-500/25 border-teal-500/40 text-teal-300 shadow-[0_2px_8px_rgba(20,184,166,0.15)]"
                                                    : "bg-neutral-950 border-neutral-850 text-neutral-400 group-hover:border-neutral-700 group-hover:text-neutral-300"
                                            }`}>
                                                +$79/{language === "ar" ? "شهرياً" : "mo"}
                                            </span>
                                        </div>
                                        <p className="text-xs text-neutral-400 group-hover:text-neutral-300 leading-relaxed transition-colors">
                                            {language === "ar" ? "قاعدة بيانات معزولة وخاصة بمؤسستك فقط لضمان السرعة والامتثال للأمان" : "Isolated PostgreSQL database instance for high throughput compliance"}
                                        </p>
                                    </div>
                                    <div className={`w-5 h-5 rounded-md border flex items-center justify-center transition-all duration-300 shrink-0 self-start mt-1 ${
                                        addons.dedicatedDb 
                                            ? "bg-teal-500 border-teal-500 text-neutral-950 scale-100 shadow-[0_0_12px_rgba(20,184,166,0.4)]" 
                                            : "border-neutral-800 bg-neutral-950/60 opacity-60 scale-95 group-hover:opacity-100 group-hover:scale-100 group-hover:border-neutral-700"
                                    }`}>
                                        {addons.dedicatedDb && <Check className="h-3 w-3 stroke-[3.5]" />}
                                    </div>
                                </div>

                                {/* Priority Support */}
                                <div 
                                    onClick={() => toggleAddon("prioritySupport")}
                                    className={`group flex items-start gap-4 p-4 border rounded-2xl cursor-pointer transition-all duration-300 select-none ${
                                        addons.prioritySupport 
                                            ? "border-teal-500 bg-teal-950/20 shadow-[0_0_22px_rgba(20,184,166,0.15),inset_0_1px_1px_rgba(255,255,255,0.05)] ring-1 ring-teal-500/20" 
                                            : "border-neutral-800 bg-neutral-900/30 hover:border-neutral-700/60 hover:bg-neutral-900/50"
                                    }`}
                                >
                                    <div className={`p-2.5 rounded-xl transition-all duration-300 ${
                                        addons.prioritySupport 
                                            ? "bg-teal-500/20 text-teal-400 border border-teal-500/35 shadow-[0_0_12px_rgba(20,184,166,0.2)]" 
                                            : "bg-neutral-950 text-neutral-400 border border-neutral-800 group-hover:border-neutral-700 group-hover:text-neutral-300"
                                    }`}>
                                        <Smartphone className="h-5 w-5" />
                                    </div>
                                    <div className="flex-1 space-y-2">
                                        <div className="flex flex-wrap items-center gap-2">
                                            <h4 className="text-sm font-bold text-neutral-200 group-hover:text-white transition-colors">
                                                {language === "ar" ? "دعم فني خاص على مدار الساعة" : "Priority 24/7 Support"}
                                            </h4>
                                            <span className={`text-[10px] font-bold font-mono px-2 py-0.5 rounded-full border transition-all duration-300 ${
                                                addons.prioritySupport
                                                    ? "bg-teal-500/25 border-teal-500/40 text-teal-300 shadow-[0_2px_8px_rgba(20,184,166,0.15)]"
                                                    : "bg-neutral-950 border-neutral-850 text-neutral-400 group-hover:border-neutral-700 group-hover:text-neutral-300"
                                            }`}>
                                                +$19/{language === "ar" ? "شهرياً" : "mo"}
                                            </span>
                                        </div>
                                        <p className="text-xs text-neutral-400 group-hover:text-neutral-300 leading-relaxed transition-colors">
                                            {language === "ar" ? "مدير حساب مخصص، ودعم فني هاتفي مباشر لمساعدة الصيادلة في أي وقت" : "Dedicated account support representative with direct phone and Slack hotlines"}
                                        </p>
                                    </div>
                                    <div className={`w-5 h-5 rounded-md border flex items-center justify-center transition-all duration-300 shrink-0 self-start mt-1 ${
                                        addons.prioritySupport 
                                            ? "bg-teal-500 border-teal-500 text-neutral-950 scale-100 shadow-[0_0_12px_rgba(20,184,166,0.4)]" 
                                            : "border-neutral-800 bg-neutral-950/60 opacity-60 scale-95 group-hover:opacity-100 group-hover:scale-100 group-hover:border-neutral-700"
                                    }`}>
                                        {addons.prioritySupport && <Check className="h-3 w-3 stroke-[3.5]" />}
                                    </div>
                                </div>
                            </CardContent>
                        </Card>
                    </div>

                    {/* Right Panel: Summary & Payment (5 Cols) */}
                    <div className="lg:col-span-5 space-y-6 lg:sticky lg:top-6">
                        
                        {/* Plan Summary Card */}
                        <Card className="border-neutral-900 bg-neutral-900/20 backdrop-blur-xl shadow-2xl rounded-3xl overflow-hidden relative">
                            <div className="absolute top-0 left-0 w-full h-[1px] bg-gradient-to-r from-transparent via-emerald-500/25 to-transparent" />
                            <div className="bg-neutral-900/40 p-5 border-b border-neutral-900/80 flex justify-between items-center">
                                <span className="text-xs font-bold text-emerald-400 tracking-wider uppercase">{t("setup.summary")}</span>
                                <div className="flex items-center gap-2">
                                    <span className="text-xs text-neutral-400">{t("setup.billing.monthly")}</span>
                                    <button 
                                        type="button"
                                        onClick={() => setIsYearly(!isYearly)}
                                        className={`relative inline-flex h-6 w-11 shrink-0 cursor-pointer items-center rounded-full transition-colors duration-300 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-emerald-500/50 ${isYearly ? "bg-emerald-500" : "bg-neutral-800"}`}
                                    >
                                        <span className={`pointer-events-none block h-5 w-5 rounded-full bg-neutral-950 shadow-md transition-transform duration-300 ${
                                            isYearly 
                                                ? (dir === "rtl" ? "-translate-x-5" : "translate-x-5") 
                                                : (dir === "rtl" ? "-translate-x-1" : "translate-x-1")
                                        }`} />
                                    </button>
                                    <span className="text-xs font-semibold text-emerald-400 flex items-center gap-1.5">
                                        {t("setup.billing.yearly")}
                                        <span className="text-[9px] font-bold bg-emerald-500/10 border border-emerald-500/30 text-emerald-400 px-1.5 py-0.5 rounded-md">20%</span>
                                    </span>
                                </div>
                            </div>
                            <CardContent className="p-6 space-y-5">
                                <div className="space-y-3.5 text-xs">
                                    <div className="flex justify-between items-center text-neutral-300">
                                        <span className="text-neutral-400">{language === "ar" ? "الاشتراك الأساسي (يشمل مؤسسة، منطقة، فرع وجهاز)" : "Base Subscription (Includes 1 Tenant, Region, Branch, Device)"}</span>
                                        <span className="font-mono font-medium">${basePrice}.00</span>
                                    </div>

                                    {tenantPrice > 0 && (
                                        <div className="flex justify-between items-center text-neutral-300">
                                            <span className="text-neutral-400 flex items-center gap-1.5">
                                                <Network className="h-3.5 w-3.5 text-emerald-400" />
                                                {totalTenants - 1} {language === "ar" ? "مؤسسات فرعية إضافية" : "Extra Tenants"}
                                            </span>
                                            <span className="font-mono font-medium">${tenantPrice}.00</span>
                                        </div>
                                    )}
                                    {regionPrice > 0 && (
                                        <div className="flex justify-between items-center text-neutral-300">
                                            <span className="text-neutral-400 flex items-center gap-1.5">
                                                <Globe2 className="h-3.5 w-3.5 text-emerald-400" />
                                                {totalRegions - 1} {language === "ar" ? "أقاليم إضافية" : "Extra Regions"}
                                            </span>
                                            <span className="font-mono font-medium">${regionPrice}.00</span>
                                        </div>
                                    )}
                                    {branchPrice > 0 && (
                                        <div className="flex justify-between items-center text-neutral-300">
                                            <span className="text-neutral-400 flex items-center gap-1.5">
                                                <Building2 className="h-3.5 w-3.5 text-emerald-400" />
                                                {totalBranches - 1} {language === "ar" ? "فروع إضافية" : "Extra Branches"}
                                            </span>
                                            <span className="font-mono font-medium">${branchPrice}.00</span>
                                        </div>
                                    )}
                                    {devicePrice > 0 && (
                                        <div className="flex justify-between items-center text-neutral-300">
                                            <span className="text-neutral-400 flex items-center gap-1.5">
                                                <Smartphone className="h-3.5 w-3.5 text-emerald-400" />
                                                {totalDevices - 1} {language === "ar" ? "أجهزة نقاط بيع إضافية" : "Extra POS Devices"}
                                            </span>
                                            <span className="font-mono font-medium">${devicePrice}.00</span>
                                        </div>
                                    )}
                                    {addonPrice > 0 && (
                                        <div className="flex justify-between items-center border-t border-neutral-900/60 pt-3 text-neutral-300">
                                            <span className="text-neutral-400">{t("setup.addons")}</span>
                                            <span className="font-mono font-medium">${addonPrice}.00</span>
                                        </div>
                                    )}

                                    {isYearly && (
                                        <>
                                            <div className="flex justify-between items-center border-t border-neutral-900/60 pt-3 text-neutral-300">
                                                <span className="text-neutral-400">{language === "ar" ? "المجموع الفرعي" : "Subtotal"}</span>
                                                <span className="font-mono font-medium">${subtotal.toFixed(2)}</span>
                                            </div>
                                            <div className="flex justify-between items-center text-emerald-400 font-semibold">
                                                <span className="flex items-center gap-1">
                                                    <Sparkles className="h-3.5 w-3.5" />
                                                    {language === "ar" ? "خصم الدفع السنوي (20%-)" : "Yearly Discount (20% Off)"}
                                                </span>
                                                <span className="font-mono">-${discount.toFixed(2)}</span>
                                            </div>
                                        </>
                                    )}
                                </div>

                                <div className="border-t border-neutral-900/60 pt-5 flex flex-col items-center justify-center space-y-2">
                                    <div className="flex items-baseline gap-1 text-4xl font-extrabold text-white">
                                        <span className="font-mono">${total.toFixed(2)}</span>
                                        <span className="text-xs text-neutral-500 font-normal">/{language === "ar" ? "شهر" : "mo"}</span>
                                    </div>
                                    {isYearly && (
                                        <div className="flex flex-col items-center gap-1.5">
                                            <span className="text-[10px] text-emerald-400 font-bold bg-emerald-500/10 px-2.5 py-0.5 rounded-full border border-emerald-500/25 flex items-center gap-1 shadow-[0_0_15px_rgba(16,185,129,0.06)] animate-pulse">
                                                <Sparkles className="h-3 w-3" />
                                                {language === "ar" ? `وفرت $${(discount * 12).toFixed(2)} سنوياً` : `Saved $${(discount * 12).toFixed(2)} / year`}
                                            </span>
                                            <span className="text-[11px] text-neutral-400 font-medium tracking-wide">
                                                {language === "ar" 
                                                    ? `فاتورة سنوية إجمالية بقيمة $${(total * 12).toFixed(2)} / عام` 
                                                    : `Billed annually: $${(total * 12).toFixed(2)} / year`}
                                            </span>
                                        </div>
                                    )}
                                </div>
                            </CardContent>
                        </Card>

                        {/* Secure Checkout Card */}
                        <Card className="border-neutral-900 bg-neutral-900/20 backdrop-blur-xl shadow-2xl rounded-3xl overflow-hidden relative">
                            <div className="absolute top-0 left-0 w-full h-[1px] bg-gradient-to-r from-transparent via-cyan-500/20 to-transparent" />
                            <CardHeader className="pb-4 border-b border-neutral-900/60 flex flex-row items-center justify-between">
                                <div className="flex items-center gap-2">
                                    <Lock className="h-4 w-4 text-emerald-400" />
                                    <CardTitle className="text-sm font-bold text-neutral-200">
                                        {t("setup.checkout")}
                                    </CardTitle>
                                </div>
                                <div className="flex items-center gap-1 text-[10px] text-emerald-400 font-bold bg-emerald-500/10 px-2 py-0.5 rounded-md border border-emerald-500/20">
                                    <ShieldCheck className="h-3.5 w-3.5" />
                                    <span>TEST MODE</span>
                                </div>
                            </CardHeader>
                            <CardContent className="p-6 space-y-5">
                                
                                {/* Glassmorphic Virtual Card Container with 3D Flip */}
                                <div className="w-full h-48 perspective-1000 my-2">
                                    <div className={`relative w-full h-full duration-700 transform-style preserve-3d transition-transform ${isFlipped ? "rotate-y-180" : ""}`}>
                                        
                                        {/* Front of Card */}
                                        <div className="absolute inset-0 w-full h-full rounded-2xl bg-gradient-to-br from-neutral-800/40 via-neutral-900/40 to-neutral-950/70 backface-hidden border border-white/10 p-6 shadow-[0_20px_50px_rgba(0,0,0,0.5)] overflow-hidden flex flex-col justify-between group select-none" dir="ltr">
                                            {/* Glossy light reflection effect */}
                                            <div className="absolute inset-0 bg-gradient-to-tr from-transparent via-white/[0.02] to-white/[0.08] pointer-events-none" />
                                            <div className="absolute -top-12 -left-12 w-36 h-36 rounded-full bg-emerald-500/10 blur-2xl group-hover:bg-emerald-500/20 transition-all pointer-events-none" />
                                            <div className="absolute -bottom-12 -right-12 w-36 h-36 rounded-full bg-teal-500/10 blur-2xl group-hover:bg-teal-500/20 transition-all pointer-events-none" />
                                            
                                            {/* Top Row: Network / Branding and Smart Chip */}
                                            <div className="flex justify-between items-start z-10">
                                                <div className="space-y-0.5">
                                                    <span className="text-[9px] tracking-widest text-emerald-400 font-bold uppercase">PharmaCore Premium</span>
                                                    <h3 className="text-[11px] font-extrabold text-neutral-300 tracking-wider">ENTERPRISE NODE</h3>
                                                </div>
                                                {/* Chip and Contactless symbol */}
                                                <div className="flex items-center gap-3">
                                                    {/* Contactless waves SVG */}
                                                    <svg className="w-4 h-4 text-neutral-400 opacity-60 rotate-90" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.5" strokeLinecap="round">
                                                        <path d="M5 12a10 10 0 0 1 5-8.66" />
                                                        <path d="M5 12a6 6 0 0 1 3-5.19" />
                                                        <path d="M5 12a2 2 0 0 1 1-1.73" />
                                                    </svg>

                                                    {/* Premium Smart EMV Microchip */}
                                                    <div className="w-9 h-7 rounded-md bg-gradient-to-br from-yellow-300 via-amber-400 to-yellow-600 p-[1px] relative overflow-hidden shadow-md">
                                                        <div className="absolute inset-0 bg-neutral-950/10 rounded-md" />
                                                        <div className="w-full h-full relative">
                                                            {/* Horizontal divider */}
                                                            <div className="absolute top-1/2 left-0 right-0 h-[1px] bg-neutral-950/40" />
                                                            {/* Vertical dividers */}
                                                            <div className="absolute left-1/3 top-0 bottom-0 w-[1px] bg-neutral-950/40" />
                                                            <div className="absolute right-1/3 top-0 bottom-0 w-[1px] bg-neutral-950/40" />
                                                            
                                                            {/* Center cutout shape */}
                                                            <div className="absolute top-[20%] bottom-[20%] left-[25%] right-[25%] rounded bg-gradient-to-tl from-yellow-400 to-yellow-200 border border-neutral-950/30 shadow-inner flex items-center justify-center">
                                                                <div className="w-1.5 h-1.5 rounded-full bg-neutral-950/20" />
                                                            </div>
                                                            
                                                            {/* Detail circuit lines */}
                                                            <div className="absolute left-[15%] top-1/2 -translate-y-1/2 w-[2px] h-3 bg-neutral-950/40 rounded-sm" />
                                                            <div className="absolute right-[15%] top-1/2 -translate-y-1/2 w-[2px] h-3 bg-neutral-950/40 rounded-sm" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            {/* Middle Row: Card Number */}
                                            <div className="my-3 z-10">
                                                <p className="text-xl font-bold font-mono tracking-[0.2em] text-neutral-100 transition-all select-none drop-shadow-[0_2px_4px_rgba(0,0,0,0.8)]">
                                                    {getFormattedCardNumberForDisplay(cardNumber)}
                                                </p>
                                            </div>

                                            {/* Bottom Row: Name, Expiry */}
                                            <div className="flex justify-between items-end z-10">
                                                <div className="space-y-0.5">
                                                    <span className="text-[8px] text-neutral-500 uppercase font-bold tracking-wider">{t("setup.card_name")}</span>
                                                    <p className="text-[11px] font-mono tracking-wider font-semibold text-neutral-200 uppercase truncate max-w-[220px]">
                                                        {cardName.trim() ? cardName.toUpperCase() : "CARDHOLDER NAME"}
                                                    </p>
                                                </div>
                                                <div className="flex items-center gap-4">
                                                    <div className="space-y-0.5">
                                                        <span className="text-[8px] text-neutral-500 uppercase font-bold tracking-wider">{language === "ar" ? "الانتهاء" : "Expiry"}</span>
                                                        <p className="text-xs font-mono font-semibold text-neutral-200">{cardExpiry || "MM/YY"}</p>
                                                    </div>
                                                    {/* Visual indicators for card type */}
                                                    <div className="h-6 flex items-center justify-end">
                                                        {getCardType() === "visa" && (
                                                            <span className="text-[10px] font-extrabold text-blue-400 font-sans italic bg-blue-500/10 px-2 py-0.5 rounded border border-blue-500/20 shadow-sm">VISA</span>
                                                        )}
                                                        {getCardType() === "mastercard" && (
                                                            <span className="text-[10px] font-extrabold text-orange-400 font-sans italic bg-orange-500/10 px-2 py-0.5 rounded border border-orange-500/20 shadow-sm">MC</span>
                                                        )}
                                                        {getCardType() === "generic" && (
                                                            <span className="text-[8px] font-extrabold text-emerald-400 font-sans tracking-widest bg-emerald-500/10 px-2 py-0.5 rounded border border-emerald-500/20 shadow-sm">SECURE</span>
                                                        )}
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        {/* Back of Card */}
                                        <div className="absolute inset-0 w-full h-full rounded-2xl bg-gradient-to-br from-neutral-800/40 via-neutral-900/40 to-neutral-950/70 backface-hidden rotate-y-180 border border-white/10 py-6 shadow-[0_20px_50px_rgba(0,0,0,0.5)] flex flex-col justify-between overflow-hidden" dir="ltr">
                                            {/* Black magnetic strip */}
                                            <div className="w-full h-10 bg-neutral-950 shadow-inner mt-2" />
                                            
                                            {/* Signature strip and CVV */}
                                            <div className="px-6 flex items-center gap-4 w-full my-3">
                                                <div className="flex-1 h-9 bg-neutral-100/90 rounded-sm relative overflow-hidden flex items-center justify-start pl-3 select-none">
                                                    {/* Signature simulation line */}
                                                    <span className="text-[10px] font-sans italic text-neutral-400 font-semibold">Authorized Signature</span>
                                                    <div className="absolute inset-y-0 right-0 w-16 bg-white border-l border-neutral-300 flex items-center justify-center font-mono text-neutral-900 font-bold text-xs tracking-wider">
                                                        {cardCvv || "•••"}
                                                    </div>
                                                </div>
                                                <div className="text-[8px] text-neutral-400 space-y-0.5 w-16 text-right">
                                                    <span className="font-bold block uppercase tracking-wider">SECURE CODE</span>
                                                    <span className="text-[6px] text-neutral-500 block">DO NOT SHARE</span>
                                                </div>
                                            </div>

                                            {/* Premium holographic stickers and instructions */}
                                            <div className="px-6 flex justify-between items-center z-10 text-[7px] text-neutral-500 leading-tight select-none">
                                                <div>
                                                    <p>PharmaCore Network Node: Node-774A</p>
                                                    <p>System activation requires authorization signature.</p>
                                                </div>
                                                {/* Holographic sticker */}
                                                <div className="w-8 h-8 rounded-full bg-gradient-to-tr from-cyan-400 via-pink-400 to-yellow-300 opacity-60 blur-[1px] animate-pulse" />
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <form onSubmit={handleCheckoutSubmit} className="space-y-4">
                                    
                                    {/* Cardholder Name */}
                                    <div className="space-y-1.5">
                                        <Label className="text-xs font-semibold text-neutral-400">{t("setup.card_name")}</Label>
                                        <div className="relative flex items-center">
                                            <CreditCard className="absolute left-3.5 rtl:left-auto rtl:right-3.5 h-4 w-4 text-neutral-500" />
                                            <Input
                                                type="text" required placeholder={language === "ar" ? "مثال: خالد الرويضي" : "e.g., John Doe"}
                                                value={cardName} onChange={(e) => setCardName(e.target.value)}
                                                className="pl-10 pr-4 rtl:pl-4 rtl:pr-10 h-10 text-neutral-100 border-neutral-800 bg-neutral-950/60 focus-visible:ring-1 focus-visible:ring-emerald-500"
                                            />
                                        </div>
                                    </div>

                                    {/* Card Number */}
                                    <div className="space-y-1.5">
                                        <Label className="text-xs font-semibold text-neutral-400">{t("setup.card_number")}</Label>
                                        <div className="relative flex items-center">
                                            <CreditCard className="absolute left-3.5 rtl:left-auto rtl:right-3.5 h-4 w-4 text-neutral-500" />
                                            <Input
                                                type="text" required placeholder="4242 4242 4242 4242" maxLength={19}
                                                value={cardNumber} onChange={handleCardNumberChange}
                                                className="pl-10 pr-4 rtl:pl-4 rtl:pr-10 h-10 text-neutral-100 border-neutral-800 bg-neutral-950/60 focus-visible:ring-1 focus-visible:ring-emerald-500 text-left"
                                                dir="ltr"
                                            />
                                            {/* Visual card type indicator */}
                                            <div className="absolute right-3 rtl:right-auto rtl:left-3 flex items-center">
                                                {getCardType() === "visa" && (
                                                    <span className="text-[10px] font-extrabold text-blue-400 font-sans italic bg-blue-500/10 px-1.5 py-0.5 rounded border border-blue-500/20">VISA</span>
                                                )}
                                                {getCardType() === "mastercard" && (
                                                    <span className="text-[10px] font-extrabold text-orange-400 font-sans italic bg-orange-500/10 px-1.5 py-0.5 rounded border border-orange-500/20">MC</span>
                                                )}
                                            </div>
                                        </div>
                                    </div>

                                    {/* Expiry and CVV Row */}
                                    <div className="grid grid-cols-2 gap-4">
                                        <div className="space-y-1.5">
                                            <Label className="text-xs font-semibold text-neutral-400">{t("setup.expiry")}</Label>
                                            <Input
                                                type="text" required placeholder="12/28" maxLength={5}
                                                value={cardExpiry} onChange={handleExpiryChange}
                                                className="h-10 text-neutral-100 border-neutral-800 bg-neutral-950/60 focus-visible:ring-1 focus-visible:ring-emerald-500 text-center"
                                                dir="ltr"
                                            />
                                        </div>
                                        <div className="space-y-1.5">
                                            <Label className="text-xs font-semibold text-neutral-400">{t("setup.cvv")}</Label>
                                            <Input
                                                type="password" required placeholder="•••" maxLength={4}
                                                value={cardCvv} onChange={handleCvvChange}
                                                onFocus={() => setIsFlipped(true)}
                                                onBlur={() => setIsFlipped(false)}
                                                className="h-10 text-neutral-100 border-neutral-800 bg-neutral-950/60 focus-visible:ring-1 focus-visible:ring-emerald-500 text-center"
                                                dir="ltr"
                                            />
                                        </div>
                                    </div>

                                    <Button 
                                        type="submit" disabled={isProcessing}
                                        className="w-full h-11 bg-gradient-to-r from-emerald-600 via-teal-600 to-cyan-600 hover:from-emerald-500 hover:via-teal-500 hover:to-cyan-500 text-white font-bold rounded-xl mt-4 shadow-lg flex items-center justify-center gap-2 group transition-all duration-300"
                                    >
                                        {isProcessing ? (
                                            <>
                                                <Loader2 className="h-5 w-5 animate-spin" />
                                                <span className="text-xs">{t("setup.processing")}</span>
                                            </>
                                        ) : (
                                            <>
                                                <Lock className="h-4 w-4" />
                                                <span>{t("setup.btn_pay")}</span>
                                                {language === "ar" ? (
                                                    <ArrowLeft className="h-4 w-4 transition-transform duration-200 group-hover:-translate-x-1" />
                                                ) : (
                                                    <ArrowRight className="h-4 w-4 transition-transform duration-200 group-hover:translate-x-1" />
                                                )}
                                            </>
                                        )}
                                    </Button>
                                </form>

                                <div className="flex items-center justify-center gap-2 text-[10px] text-neutral-500 mt-4 border-t border-neutral-900 pt-4">
                                    <ShieldCheck className="h-3.5 w-3.5 text-emerald-500" />
                                    <span>{language === "ar" ? "تشفير آمن بمستوى البنوك (256-bit SSL)" : "Bank-level 256-bit SSL Secure Checkout"}</span>
                                </div>
                            </CardContent>
                        </Card>
                    </div>

                </div>
            </div>
            
            <style jsx>{`
                .perspective-1000 {
                    perspective: 1000px;
                }
                .transform-style {
                    transform-style: preserve-3d;
                }
                .backface-hidden {
                    backface-visibility: hidden;
                    -webkit-backface-visibility: hidden;
                }
                .rotate-y-180 {
                    transform: rotateY(180deg);
                }
            `}</style>
        </div>
    )
}
