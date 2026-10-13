"use client"

import { useState, useEffect, useMemo } from "react"
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
} from "@/components/ui/dialog"
import { Input } from "@/components/ui/input"
import { ScrollArea } from "@/components/ui/scroll-area"
import { Avatar, AvatarFallback } from "@/components/ui/avatar"
import { Building2, Megaphone, User, Search, RefreshCw } from "lucide-react"
import type { SearchResult, Conversation } from "@/types/message"
import apiClient from "@/lib/api-client"
import { toast } from "sonner"

interface NewMessageDialogProps {
    open: boolean
    onClose: () => void
    onStartConversation: (conversation: Conversation) => void
}

export function NewMessageDialog({ open, onClose, onStartConversation }: NewMessageDialogProps) {
    const [searchQuery, setSearchQuery] = useState("")
    const [targets, setTargets] = useState<SearchResult[]>([])
    const [isLoading, setIsLoading] = useState(false)

    useEffect(() => {
        if (open) {
            loadTargets()
        }
    }, [open])

    const loadTargets = async () => {
        setIsLoading(true)
        try {
            const [empRes, branchRes] = await Promise.all([
                apiClient.get<any[]>("/employees"),
                apiClient.get<any[]>("/branches")
            ])

            const list: SearchResult[] = []

            // Global Notice option
            list.push({
                id: "all",
                name: "إعلان عام للمنظومة (Global Notice)",
                type: "all",
                subtitle: "إعلان عام لكافة الفروع والموظفين"
            })

            // Branches
            if (Array.isArray(branchRes.data)) {
                branchRes.data.forEach(b => {
                    list.push({
                        id: b.id,
                        name: `فرع: ${b.name}`,
                        type: "branch",
                        subtitle: b.address || "عنوان الفرع"
                    })
                })
            }

            // Employees
            if (Array.isArray(empRes.data)) {
                empRes.data.forEach(e => {
                    list.push({
                        id: e.id,
                        name: e.fullName,
                        type: "employee",
                        subtitle: `${e.role} | ${e.jobTitle || "كود: " + e.employeeCode}`
                    })
                })
            }

            setTargets(list)
        } catch (err) {
            toast.error("فشل تحميل قائمة الجهات المتصلة")
        } finally {
            setIsLoading(false)
        }
    }

    const filteredResults = useMemo(() => {
        if (!searchQuery) return targets

        const q = searchQuery.toLowerCase()
        return targets.filter(item =>
            item.name.toLowerCase().includes(q) ||
            (item.subtitle && item.subtitle.toLowerCase().includes(q))
        )
    }, [searchQuery, targets])

    const handleSelect = (result: SearchResult) => {
        // Construct conversation format matching API groupings
        const newConversation: Conversation = {
            id: result.type === "all" ? "all" : `${result.type}-${result.id}`,
            type: result.type,
            targetId: result.id,
            targetName: result.name,
            targetType: result.subtitle || "",
            unreadCount: 0,
            participants: [],
            createdAt: new Date(),
            updatedAt: new Date()
        }

        onStartConversation(newConversation)
        onClose()
        setSearchQuery("")
    }

    const getIcon = (type: string) => {
        switch (type) {
            case "all":
                return <Megaphone className="h-5 w-5 text-amber-500" />
            case "branch":
                return <Building2 className="h-5 w-5 text-sky-500" />
            default:
                return <User className="h-5 w-5 text-indigo-500" />
        }
    }

    const getTypeBadge = (type: string) => {
        switch (type) {
            case "all":
                return <span className="text-[10px] bg-amber-500/10 text-amber-400 px-2 py-0.5 rounded border border-amber-500/20">إعلان عام</span>
            case "branch":
                return <span className="text-[10px] bg-sky-500/10 text-sky-400 px-2 py-0.5 rounded border border-sky-500/20">فرع</span>
            default:
                return <span className="text-[10px] bg-indigo-500/10 text-indigo-400 px-2 py-0.5 rounded border border-indigo-500/20">موظف</span>
        }
    }

    return (
        <Dialog open={open} onOpenChange={onClose}>
            <DialogContent className="sm:max-w-[500px]" dir="rtl">
                <DialogHeader className="text-right">
                    <DialogTitle>رسالة جديدة</DialogTitle>
                    <DialogDescription>
                        ابحث عن الموظفين أو الفروع لبدء محادثة داخلية أو بث إعلان.
                    </DialogDescription>
                </DialogHeader>

                <div className="space-y-4 pt-2">
                    <div className="relative">
                        <Search className="absolute right-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-muted-foreground" />
                        <Input
                            placeholder="ابحث بالاسم، المسمى الوظيفي أو الفرع..."
                            value={searchQuery}
                            onChange={(e) => setSearchQuery(e.target.value)}
                            className="pr-9 pl-4"
                            autoFocus
                        />
                    </div>

                    <ScrollArea className="h-[350px] border rounded-lg p-2">
                        {isLoading ? (
                            <div className="flex justify-center items-center py-20 text-muted-foreground text-sm">
                                <RefreshCw className="animate-spin ml-2 h-4 w-4" />
                                جاري تحميل قائمة الاتصال...
                            </div>
                        ) : (
                            <div className="space-y-1">
                                {filteredResults.map((result) => (
                                    <button
                                        key={result.id + "-" + result.type}
                                        onClick={() => handleSelect(result)}
                                        className="w-full p-3 flex items-center gap-3 hover:bg-muted/50 rounded-lg transition-colors text-right"
                                    >
                                        <Avatar className="h-10 w-10 border bg-muted/20">
                                            <AvatarFallback className="bg-transparent">
                                                {getIcon(result.type)}
                                            </AvatarFallback>
                                        </Avatar>

                                        <div className="flex-1 min-w-0">
                                            <div className="flex items-center justify-between">
                                                <h4 className="font-semibold text-sm truncate">
                                                    {result.name}
                                                </h4>
                                                {getTypeBadge(result.type)}
                                            </div>
                                            {result.subtitle && (
                                                <p className="text-xs text-muted-foreground truncate mt-0.5">
                                                    {result.subtitle}
                                                </p>
                                            )}
                                        </div>
                                    </button>
                                ))}

                                {filteredResults.length === 0 && (
                                    <div className="p-8 text-center text-muted-foreground text-sm">
                                        لا توجد نتائج مطابقة لبحثك
                                    </div>
                                )}
                            </div>
                        )}
                    </ScrollArea>
                </div>
            </DialogContent>
        </Dialog>
    )
}
