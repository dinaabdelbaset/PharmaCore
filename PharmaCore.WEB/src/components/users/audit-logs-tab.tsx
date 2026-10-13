"use client"

import { useState, useEffect } from "react"
import { Search, Filter, RefreshCw, Calendar, Terminal, ShieldAlert } from "lucide-react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table"
import { Badge } from "@/components/ui/badge"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import apiClient from "@/lib/api-client"
import { toast } from "sonner"

interface AuditEvent {
    id: string
    branchId?: string | null
    employeeId?: string | null
    employeeCode?: string | null
    employeeName?: string | null
    eventType: string
    category: string
    screenName?: string | null
    targetTable?: string | null
    targetId?: string | null
    description: string
    severity: number
    ipAddress?: string | null
    deviceName?: string | null
    createdAt: string
}

interface PagedResult {
    items: AuditEvent[]
    totalCount: number
    page: number
    pageSize: number
}

export function AuditLogsTab() {
    const [events, setEvents] = useState<AuditEvent[]>([])
    const [totalCount, setTotalCount] = useState(0)
    const [page, setPage] = useState(1)
    const [pageSize] = useState(25)
    
    // Filters
    const [searchTerm, setSearchTerm] = useState("")
    const [categoryFilter, setCategoryFilter] = useState("all")
    const [severityFilter, setSeverityFilter] = useState("all")
    const [categories, setCategories] = useState<string[]>([])
    
    const [isLoading, setIsLoading] = useState(false)

    useEffect(() => {
        loadStats()
    }, [])

    useEffect(() => {
        loadLogs()
    }, [page, categoryFilter, severityFilter])

    const loadStats = async () => {
        try {
            const res = await apiClient.get<{ byCategory: { category: string; count: number }[] }>("/audit-logs/stats")
            const cats = res.data.byCategory.map(c => c.category).filter(Boolean)
            setCategories(cats)
        } catch (err) {
            console.error("Error loading audit stats", err)
        }
    }

    const loadLogs = async () => {
        setIsLoading(true)
        try {
            const params: Record<string, any> = {
                page,
                size: pageSize,
            }
            if (searchTerm) params.search = searchTerm
            if (categoryFilter !== "all") params.category = categoryFilter
            if (severityFilter !== "all") params.severity = Number(severityFilter)

            const res = await apiClient.get<PagedResult>("/audit-logs", { params })
            setEvents(res.data.items)
            setTotalCount(res.data.totalCount)
        } catch (err) {
            toast.error("فشل تحميل سجل الرقابة والنشاط")
        } finally {
            setIsLoading(false)
        }
    }

    const handleSearch = (e: React.FormEvent) => {
        e.preventDefault()
        setPage(1)
        loadLogs()
    }

    const getSeverityBadge = (severity: number) => {
        switch (severity) {
            case 0:
                return <Badge variant="outline" className="text-muted-foreground border-muted-foreground bg-muted/20">عادي</Badge>
            case 1:
                return <Badge variant="outline" className="text-sky-500 border-sky-500 bg-sky-500/10">متوسط</Badge>
            case 2:
                return <Badge variant="outline" className="text-amber-500 border-amber-500 bg-amber-500/10">هام</Badge>
            case 3:
                return <Badge variant="destructive">حرج جداً</Badge>
            default:
                return <Badge variant="outline">غير محدد</Badge>
        }
    }

    return (
        <div className="space-y-6">
            {/* Search and Filters row */}
            <form onSubmit={handleSearch} className="flex flex-wrap gap-3 items-center bg-card p-4 rounded-lg border">
                <div className="relative flex-1 min-w-[240px]">
                    <Search className="absolute right-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                    <Input
                        placeholder="ابحث بوصف العملية، اسم الموظف..."
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        className="pr-10 pl-4"
                    />
                </div>

                <div className="w-[180px]">
                    <Select value={categoryFilter} onValueChange={setCategoryFilter}>
                        <SelectTrigger>
                            <SelectValue placeholder="فئة العمليات" />
                        </SelectTrigger>
                        <SelectContent>
                            <SelectItem value="all">كل الفئات</SelectItem>
                            {categories.map((cat) => (
                                <SelectItem key={cat} value={cat}>{cat}</SelectItem>
                            ))}
                            {categories.length === 0 && (
                                <>
                                    <SelectItem value="Auth">تسجيل الدخول</SelectItem>
                                    <SelectItem value="Sales">المبيعات</SelectItem>
                                    <SelectItem value="Settings">الإعدادات</SelectItem>
                                </>
                            )}
                        </SelectContent>
                    </Select>
                </div>

                <div className="w-[180px]">
                    <Select value={severityFilter} onValueChange={setSeverityFilter}>
                        <SelectTrigger>
                            <SelectValue placeholder="الأهمية والخطورة" />
                        </SelectTrigger>
                        <SelectContent>
                            <SelectItem value="all">كل المستويات</SelectItem>
                            <SelectItem value="0">عادي (Normal)</SelectItem>
                            <SelectItem value="1">متوسط (Warning)</SelectItem>
                            <SelectItem value="2">هام (Alert)</SelectItem>
                            <SelectItem value="3">حرج جداً (Critical)</SelectItem>
                        </SelectContent>
                    </Select>
                </div>

                <div className="flex gap-2">
                    <Button type="submit" disabled={isLoading}>بحث</Button>
                    <Button 
                        type="button" 
                        variant="outline" 
                        size="icon" 
                        onClick={() => {
                            setSearchTerm("")
                            setCategoryFilter("all")
                            setSeverityFilter("all")
                            setPage(1)
                            loadLogs()
                        }}
                        disabled={isLoading}
                    >
                        <RefreshCw className={`h-4 w-4 ${isLoading ? "animate-spin" : ""}`} />
                    </Button>
                </div>
            </form>

            {/* Logs Table */}
            <div className="border rounded-lg bg-card overflow-hidden">
                <Table>
                    <TableHeader className="bg-muted/40">
                        <TableRow>
                            <TableHead className="text-right w-[180px]">التاريخ والوقت</TableHead>
                            <TableHead className="text-right w-[150px]">الموظف المسؤول</TableHead>
                            <TableHead className="text-right w-[120px]">فئة الحركة</TableHead>
                            <TableHead className="text-right">تفاصيل الحركة والبيان</TableHead>
                            <TableHead className="text-right w-[100px]">الأهمية</TableHead>
                            <TableHead className="text-right w-[140px]">الجهاز والـ IP</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {isLoading ? (
                            <TableRow>
                                <TableCell colSpan={6} className="text-center py-20 text-muted-foreground text-sm">
                                    <RefreshCw className="animate-spin ml-2 h-5 w-5 inline" />
                                    جاري تحميل سجلات الرقابة...
                                </TableCell>
                            </TableRow>
                        ) : events.length === 0 ? (
                            <TableRow>
                                <TableCell colSpan={6} className="text-center py-20 text-muted-foreground text-sm">
                                    لا توجد سجلات مطابقة للفلاتر المحددة
                                </TableCell>
                            </TableRow>
                        ) : (
                            events.map((ev) => (
                                <TableRow key={ev.id} className="hover:bg-muted/10">
                                    <TableCell className="text-xs font-mono text-muted-foreground">
                                        {new Date(ev.createdAt).toLocaleString("ar-EG")}
                                    </TableCell>
                                    <TableCell className="font-medium text-xs">
                                        <div className="flex flex-col">
                                            <span>{ev.employeeName || "مدير النظام"}</span>
                                            {ev.employeeCode && <span className="text-[10px] text-muted-foreground font-mono">({ev.employeeCode})</span>}
                                        </div>
                                    </TableCell>
                                    <TableCell>
                                        <Badge variant="secondary" className="text-[10px]">{ev.category}</Badge>
                                    </TableCell>
                                    <TableCell className="text-xs text-right leading-relaxed font-semibold">
                                        {ev.description}
                                    </TableCell>
                                    <TableCell>
                                        {getSeverityBadge(ev.severity)}
                                    </TableCell>
                                    <TableCell className="text-[10px] font-mono text-muted-foreground">
                                        <div className="flex flex-col">
                                            <span>{ev.ipAddress || "127.0.0.1"}</span>
                                            {ev.deviceName && <span className="truncate max-w-[120px]">{ev.deviceName}</span>}
                                        </div>
                                    </TableCell>
                                </TableRow>
                            ))
                        )}
                    </TableBody>
                </Table>
            </div>

            {/* Pagination */}
            {totalCount > pageSize && (
                <div className="flex justify-between items-center bg-card p-4 rounded-lg border text-sm">
                    <span className="text-muted-foreground">إجمالي الحركات: {totalCount}</span>
                    <div className="flex gap-2">
                        <Button 
                            variant="outline" 
                            size="sm" 
                            onClick={() => setPage(prev => Math.max(prev - 1, 1))}
                            disabled={page === 1 || isLoading}
                        >
                            الصفحة السابقة
                        </Button>
                        <span className="flex items-center px-4 font-mono font-medium">الصفحة {page}</span>
                        <Button 
                            variant="outline" 
                            size="sm" 
                            onClick={() => setPage(prev => (prev * pageSize < totalCount ? prev + 1 : prev))}
                            disabled={page * pageSize >= totalCount || isLoading}
                        >
                            الصفحة التالية
                        </Button>
                    </div>
                </div>
            )}
        </div>
    )
}
