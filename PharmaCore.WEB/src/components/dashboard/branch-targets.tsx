"use client"

import { useState, useMemo, useEffect } from "react"
import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import {
    DropdownMenu,
    DropdownMenuCheckboxItem,
    DropdownMenuContent,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { Button } from "@/components/ui/button"
import { Maximize2, Minimize2, ChevronDown, TrendingUp, TrendingDown, Minus, Loader2 } from "lucide-react"
import { Input } from "@/components/ui/input"
import { Progress } from "@/components/ui/progress"
import apiClient from "@/lib/api-client"
import { Branch, BranchStatus } from "@/types/branch"
import { toast } from "sonner"
import { useLanguage } from "@/components/providers/language-provider"

const ALL_BRANCHES = [
    { id: "1", name: "Main Branch (Cairo)", nameAr: "فرع القاهرة - وسط البلد" },
    { id: "2", name: "Alexandria Branch", nameAr: "فرع الإسكندرية - سموحة" },
    { id: "3", name: "Giza Branch", nameAr: "فرع الجيزة - شارع الهرم" },
    { id: "4", name: "Maadi Branch", nameAr: "فرع المعادي" },
    { id: "5", name: "Nasr City Branch", nameAr: "فرع مدينة نصر" },
    { id: "6", name: "Heliopolis Branch", nameAr: "فرع مصر الجديدة" },
    { id: "7", name: "Zamalek Branch", nameAr: "فرع الزمالك" },
    { id: "8", name: "Dokki Branch", nameAr: "فرع الدقي" },
    { id: "9", name: "Mohandessin Branch", nameAr: "فرع المهندسين" },
    { id: "10", name: "New Cairo Branch", nameAr: "فرع التجمع الخامس" },
]

// Deterministic pseudo-random number generator
const pseudoRandom = (seed: number) => {
    const x = Math.sin(seed++) * 10000
    return x - Math.floor(x)
}

// String to numeric seed generator
const stringToSeed = (str: string) => {
    let hash = 0
    for (let i = 0; i < str.length; i++) {
        hash = (hash << 5) - hash + str.charCodeAt(i)
        hash |= 0 // Convert to 32bit integer
    }
    return Math.abs(hash)
}

// Mock data generator for branch targets
const generateTargetsData = (branchesList: any[], branchIds: string[], period: string) => {
    return branchIds.map(branchId => {
        const branch = branchesList.find(b => b.id === branchId)
        if (!branch) return null

        // Use branch ID and period as seed for deterministic values
        const seed = stringToSeed(branchId) + (period.length * 100)

        const target = Math.floor(pseudoRandom(seed) * 50000) + 30000
        const achieved = Math.floor(pseudoRandom(seed + 1) * target * 1.2) // Can exceed or fall short
        const percentage = Math.round((achieved / target) * 100)

        return {
            id: branch.id,
            name: branch.name,
            nameAr: branch.nameAr || branch.nameArAr || branch.name,
            target,
            achieved,
            percentage,
            status: percentage >= 100 ? 'exceeded' : percentage >= 80 ? 'on-track' : 'behind'
        }
    }).filter((item): item is NonNullable<typeof item> => item !== null)
}

interface BranchTargetsProps {
    isSimulated?: boolean
}

export function BranchTargets({ isSimulated = false }: BranchTargetsProps) {
    const { language } = useLanguage()
    const isRtl = language === "ar"

    const [branches, setBranches] = useState<Branch[]>([])
    const [isLoading, setIsLoading] = useState(false)
    const [selectedBranches, setSelectedBranches] = useState<string[]>([])
    const [searchTerm, setSearchTerm] = useState("")
    const [periodType, setPeriodType] = useState("month")
    const [specificPeriod, setSpecificPeriod] = useState("current")
    const [viewMode, setViewMode] = useState<'default' | 'fullWidth' | 'fullScreen'>('default')

    useEffect(() => {
        if (isSimulated) {
            setBranches([])
            setSelectedBranches(["1", "2", "3", "4", "5"])
        } else {
            const fetchBranches = async () => {
                setIsLoading(true)
                try {
                    const response = await apiClient.get<Branch[]>("/branches")
                    if (Array.isArray(response.data)) {
                        setBranches(response.data)
                        // Select active branches by default
                        const activeBranchIds = response.data
                            .filter(b => b.status === BranchStatus.ACTIVE)
                            .map(b => b.id)
                        setSelectedBranches(activeBranchIds)
                    } else {
                        setBranches([])
                        setSelectedBranches([])
                    }
                } catch (error: any) {
                    console.error("Failed to fetch branches in BranchTargets", error)
                    const errorMsg = error.response?.data?.message || error.message || (isRtl ? "فشل تحميل الفروع" : "Failed to load branches")
                    toast.error(errorMsg)
                    setBranches([])
                    setSelectedBranches([])
                } finally {
                    setIsLoading(false)
                }
            }
            fetchBranches()
        }
    }, [isSimulated, isRtl])

    const activeBranchesList = useMemo(() => {
        return isSimulated ? ALL_BRANCHES : branches
    }, [isSimulated, branches])

    const targetsData = useMemo(() => {
        return generateTargetsData(activeBranchesList, selectedBranches, specificPeriod)
    }, [activeBranchesList, selectedBranches, specificPeriod])

    const filteredBranches = useMemo(() => {
        return activeBranchesList.filter(branch => {
            const name = isRtl ? (branch.nameAr || branch.name) : branch.name
            return name.toLowerCase().includes(searchTerm.toLowerCase())
        })
    }, [activeBranchesList, searchTerm, isRtl])

    const toggleBranch = (id: string) => {
        setSelectedBranches(prev =>
            prev.includes(id)
                ? prev.filter(b => b !== id)
                : [...prev, id]
        )
    }

    const getCardClasses = () => {
        switch (viewMode) {
            case 'fullScreen':
                return "fixed inset-0 z-50 h-screen w-screen rounded-none overflow-auto"
            case 'fullWidth':
                return "col-span-full transition-all duration-200"
            default:
                return "col-span-1 lg:col-span-4 transition-all duration-200"
        }
    }

    const getStatusColor = (status: string) => {
        switch (status) {
            case 'exceeded':
                return 'text-green-600 dark:text-green-400'
            case 'on-track':
                return 'text-blue-600 dark:text-blue-400'
            case 'behind':
                return 'text-red-600 dark:text-red-400'
            default:
                return 'text-muted-foreground'
        }
    }

    const getStatusLabel = (status: string) => {
        if (isRtl) {
            switch (status) {
                case 'exceeded': return "تجاوز المستهدف"
                case 'on-track': return "على المسار"
                case 'behind': return "متأخر عن الخطة"
                default: return ""
            }
        }
        return status.toUpperCase()
    }

    const getStatusIcon = (status: string) => {
        switch (status) {
            case 'exceeded':
                return <TrendingUp className="h-4 w-4 text-green-600" />
            case 'on-track':
                return <Minus className="h-4 w-4 text-blue-600" />
            case 'behind':
                return <TrendingDown className="h-4 w-4 text-red-600" />
            default:
                return null
        }
    }

    const getProgressColor = (percentage: number) => {
        if (percentage >= 100) return 'bg-green-500'
        if (percentage >= 80) return 'bg-blue-500'
        return 'bg-red-500'
    }

    return (
        <>
            {viewMode === 'fullScreen' && (
                <div className="fixed inset-0 bg-background/80 backdrop-blur-sm z-40" onClick={() => setViewMode('default')} />
            )}
            <Card className={getCardClasses()} dir={isRtl ? "rtl" : "ltr"}>
                <CardHeader className="relative">
                    {/* View toggle buttons */}
                    <div className={`absolute top-4 ${isRtl ? "left-6" : "right-6"} flex gap-1`}>
                        <Button
                            variant="ghost"
                            size="icon"
                            onClick={() => setViewMode(viewMode === 'fullWidth' ? 'default' : 'fullWidth')}
                            title={isRtl ? "كامل العرض" : "Toggle Full Width"}
                        >
                            {viewMode === 'fullWidth' ? <Minimize2 className="h-4 w-4" /> : <Maximize2 className="h-4 w-4 rotate-45" />}
                        </Button>
                        <Button
                            variant="ghost"
                            size="icon"
                            onClick={() => setViewMode(viewMode === 'fullScreen' ? 'default' : 'fullScreen')}
                            title={isRtl ? "كامل الشاشة" : "Toggle Full Screen"}
                        >
                            {viewMode === 'fullScreen' ? <Minimize2 className="h-4 w-4" /> : <Maximize2 className="h-4 w-4" />}
                        </Button>
                    </div>

                    {/* Title and description */}
                    <div className={isRtl ? "pl-16 md:pl-20" : "pr-16 md:pr-20"}>
                        <CardTitle className="text-base md:text-lg">
                            {isRtl ? "أهداف ومبيعات الفروع" : "Branch Targets"}
                        </CardTitle>
                        <CardDescription className="text-xs md:text-sm">
                            {isRtl ? "متابعة ومقارنة أداء الفروع المختلفة مقابل الأهداف البيعية المحددة" : "Track and compare branch performance against targets"}
                        </CardDescription>
                    </div>

                    {/* Filters row */}
                    <div className="flex gap-2 items-center pt-3 md:pt-4 flex-wrap">
                        <DropdownMenu>
                            <DropdownMenuTrigger asChild>
                                <Button variant="outline" className="w-[200px] justify-between" disabled={isLoading}>
                                    <span>{isRtl ? "اختر الفروع" : "Select Branches"}</span>
                                    <ChevronDown className="ml-2 h-4 w-4 shrink-0" />
                                </Button>
                            </DropdownMenuTrigger>
                            <DropdownMenuContent align={isRtl ? "end" : "start"} className="w-[200px]">
                                <div className="p-2">
                                    <Input
                                        placeholder={isRtl ? "بحث..." : "Search..."}
                                        value={searchTerm}
                                        onChange={(e) => setSearchTerm(e.target.value)}
                                        className="h-8"
                                        onKeyDown={(e) => e.stopPropagation()}
                                    />
                                </div>
                                <DropdownMenuSeparator />
                                <div className="max-h-[300px] overflow-y-auto">
                                    {filteredBranches.map((branch) => (
                                        <DropdownMenuCheckboxItem
                                            key={branch.id}
                                            checked={selectedBranches.includes(branch.id)}
                                            onCheckedChange={() => toggleBranch(branch.id)}
                                            onSelect={(e) => e.preventDefault()}
                                        >
                                            {isRtl ? (branch.nameAr || branch.name) : branch.name}
                                        </DropdownMenuCheckboxItem>
                                    ))}
                                    {filteredBranches.length === 0 && (
                                        <div className="p-2 text-sm text-muted-foreground text-center">
                                            {isRtl ? "لا توجد فروع" : "No branches found"}
                                        </div>
                                    )}
                                </div>
                              </DropdownMenuContent>
                        </DropdownMenu>

                        <Select value={periodType} onValueChange={setPeriodType}>
                            <SelectTrigger className="w-[140px]" disabled={isLoading}>
                                <SelectValue placeholder={isRtl ? "نوع الفترة" : "Period Type"} />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value="month">{isRtl ? "شهرياً" : "Monthly"}</SelectItem>
                                <SelectItem value="quarter">{isRtl ? "ربع سنوي" : "Quarterly"}</SelectItem>
                                <SelectItem value="year">{isRtl ? "سنوياً" : "Yearly"}</SelectItem>
                                <SelectItem value="custom">{isRtl ? "فترة مخصصة" : "Custom Range"}</SelectItem>
                            </SelectContent>
                        </Select>

                        <Select value={specificPeriod} onValueChange={setSpecificPeriod}>
                            <SelectTrigger className="w-[160px]" disabled={isLoading}>
                                <SelectValue placeholder={isRtl ? "اختر الفترة" : "Select Period"} />
                            </SelectTrigger>
                            <SelectContent>
                                {periodType === "month" && (
                                    <>
                                        <SelectItem value="current">{isRtl ? "الشهر الحالي" : "Current Month"}</SelectItem>
                                        <SelectItem value="last">{isRtl ? "الشهر السابق" : "Last Month"}</SelectItem>
                                        <SelectItem value="jan">{isRtl ? "يناير 2026" : "January 2026"}</SelectItem>
                                        <SelectItem value="feb">{isRtl ? "فبراير 2026" : "February 2026"}</SelectItem>
                                        <SelectItem value="mar">{isRtl ? "مارس 2026" : "March 2026"}</SelectItem>
                                    </>
                                )}
                                {periodType === "quarter" && (
                                    <>
                                        <SelectItem value="q1">{isRtl ? "الربع الأول 2026" : "Q1 2026"}</SelectItem>
                                        <SelectItem value="q2">{isRtl ? "الربع الثاني 2026" : "Q2 2026"}</SelectItem>
                                        <SelectItem value="q3">{isRtl ? "الربع الثالث 2026" : "Q3 2026"}</SelectItem>
                                        <SelectItem value="q4">{isRtl ? "الربع الرابع 2026" : "Q4 2026"}</SelectItem>
                                    </>
                                )}
                                {periodType === "year" && (
                                    <>
                                        <SelectItem value="2026">2026</SelectItem>
                                        <SelectItem value="2025">2025</SelectItem>
                                        <SelectItem value="2024">2024</SelectItem>
                                    </>
                                )}
                                {periodType === "custom" && (
                                    <SelectItem value="custom">{isRtl ? "نطاق مخصص" : "Custom Range"}</SelectItem>
                                )}
                            </SelectContent>
                        </Select>
                    </div>
                </CardHeader>
                <CardContent>
                    {isLoading ? (
                        <div className="flex flex-col items-center justify-center py-12 text-muted-foreground gap-2">
                            <Loader2 className="h-8 w-8 animate-spin text-primary" />
                            <span className="text-sm">{isRtl ? "جاري تحميل الأهداف والنسب..." : "Loading branch targets..."}</span>
                        </div>
                    ) : (
                        <div className="space-y-4">
                            {targetsData.map((item: any) => (
                                <div key={item.id} className="border rounded-lg p-4 hover:bg-accent/50 transition-colors">
                                    <div className="flex items-center justify-between mb-3">
                                        <div className="flex items-center gap-3">
                                            <h3 className="font-semibold">{isRtl ? (item.nameAr || item.name) : item.name}</h3>
                                            <div className="flex items-center gap-1">
                                                {getStatusIcon(item.status)}
                                                <span className={`text-[10px] font-bold ${getStatusColor(item.status)}`}>
                                                    {getStatusLabel(item.status)}
                                                </span>
                                            </div>
                                        </div>
                                        <div className={`text-sm font-bold font-mono ${getStatusColor(item.status)}`}>
                                            {item.percentage}%
                                        </div>
                                    </div>

                                    <div className="grid grid-cols-3 gap-4 mb-3 text-xs md:text-sm">
                                        <div>
                                            <p className="text-muted-foreground">{isRtl ? "المستهدف" : "Target"}</p>
                                            <p className="font-bold font-mono text-zinc-900 dark:text-zinc-100">
                                                {item.target.toLocaleString()} {isRtl ? "ج.م" : "EGP"}
                                            </p>
                                        </div>
                                        <div>
                                            <p className="text-muted-foreground">{isRtl ? "المحقق" : "Achieved"}</p>
                                            <p className="font-bold font-mono text-zinc-900 dark:text-zinc-100">
                                                {item.achieved.toLocaleString()} {isRtl ? "ج.م" : "EGP"}
                                            </p>
                                        </div>
                                        <div>
                                            <p className="text-muted-foreground">{isRtl ? "المتبقي" : "Remaining"}</p>
                                            <p className="font-bold font-mono text-zinc-900 dark:text-zinc-100">
                                                {Math.abs(item.target - item.achieved).toLocaleString()} {isRtl ? "ج.م" : "EGP"}
                                                {item.achieved >= item.target && (isRtl ? " (مكتمل)" : " (Exceeded)")}
                                            </p>
                                        </div>
                                    </div>

                                    <div className="relative">
                                        <Progress value={Math.min(item.percentage, 100)} className="h-2" />
                                        <div
                                            className={`absolute top-0 ${isRtl ? "right-0" : "left-0"} h-2 rounded-full transition-all ${getProgressColor(item.percentage)}`}
                                            style={{ width: `${Math.min(item.percentage, 100)}%` }}
                                        />
                                    </div>
                                </div>
                            ))}

                            {targetsData.length === 0 && (
                                <div className="text-center py-8 text-muted-foreground text-xs">
                                    {isSimulated 
                                        ? (isRtl ? "لم يتم تحديد أي فروع. يرجى اختيار فروع لعرض أهدافها." : "No branches selected. Please select branches to view targets.") 
                                        : (isRtl ? "لا توجد أهداف مسجلة في قاعدة البيانات حالياً." : "No live targets data registered.")
                                    }
                                </div>
                            )}
                        </div>
                    )}
                </CardContent>
            </Card>
        </>
    )
}
