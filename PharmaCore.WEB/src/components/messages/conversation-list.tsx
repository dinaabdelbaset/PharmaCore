"use client"

import { useState, useEffect, useMemo } from "react"
import { Input } from "@/components/ui/input"
import { ScrollArea } from "@/components/ui/scroll-area"
import { Avatar, AvatarFallback } from "@/components/ui/avatar"
import { Badge } from "@/components/ui/badge"
import { Search, Building2, Megaphone, User, RefreshCw } from "lucide-react"
import { cn } from "@/lib/utils"
import type { Conversation } from "@/types/message"
import apiClient from "@/lib/api-client"

interface ConversationListProps {
    selectedConversation: Conversation | null
    onSelectConversation: (conversation: Conversation) => void
    refreshTrigger: number
}

export function ConversationList({ selectedConversation, onSelectConversation, refreshTrigger }: ConversationListProps) {
    const [searchQuery, setSearchQuery] = useState("")
    const [conversations, setConversations] = useState<Conversation[]>([])
    const [isLoading, setIsLoading] = useState(false)

    useEffect(() => {
        loadConversations()
    }, [refreshTrigger])

    const loadConversations = async () => {
        setIsLoading(true)
        try {
            const res = await apiClient.get<any[]>("/messages/conversations")
            
            // Format to match Conversation schema
            const list: Conversation[] = res.data.map(c => ({
                id: c.id,
                type: c.type,
                targetId: c.targetId,
                targetName: c.targetName,
                targetType: c.targetType,
                unreadCount: c.unreadCount,
                participants: [],
                lastMessage: c.lastMessage ? {
                    id: c.lastMessage.id,
                    conversationId: c.id,
                    senderId: c.lastMessage.senderId,
                    senderName: c.lastMessage.senderName,
                    senderRole: c.lastMessage.senderRole,
                    content: c.lastMessage.content,
                    type: "text",
                    timestamp: new Date(c.lastMessage.timestamp),
                    read: c.lastMessage.read
                } : undefined,
                createdAt: new Date(),
                updatedAt: new Date()
            }))
            
            setConversations(list)
        } catch (err) {
            console.error("Error loading conversations", err)
        } finally {
            setIsLoading(false)
        }
    }

    const filteredConversations = useMemo(() => {
        if (!searchQuery) return conversations

        const q = searchQuery.toLowerCase()
        return conversations.filter(conv =>
            conv.targetName.toLowerCase().includes(q) ||
            (conv.targetType && conv.targetType.toLowerCase().includes(q))
        )
    }, [searchQuery, conversations])

    const getIcon = (type: string) => {
        switch (type) {
            case "all":
                return <Megaphone className="h-4 w-4 text-amber-500" />
            case "branch":
                return <Building2 className="h-4 w-4 text-sky-500" />
            default:
                return <User className="h-4 w-4 text-indigo-500" />
        }
    }

    const formatTime = (date: Date) => {
        const now = new Date()
        const diff = now.getTime() - date.getTime()
        const minutes = Math.floor(diff / 1000 / 60)
        const hours = Math.floor(minutes / 60)
        const days = Math.floor(hours / 24)

        if (minutes < 1) return "الآن"
        if (minutes < 60) return `منذ ${minutes} د`
        if (hours < 24) return `منذ ${hours} س`
        if (days < 7) return `منذ ${days} ي`
        return date.toLocaleDateString("ar-EG")
    }

    return (
        <div className="h-full flex flex-col" dir="rtl">
            <div className="p-4 border-b flex gap-2 items-center">
                <div className="relative flex-1">
                    <Search className="absolute right-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-muted-foreground" />
                    <Input
                        placeholder="ابحث في المحادثات..."
                        value={searchQuery}
                        onChange={(e) => setSearchQuery(e.target.value)}
                        className="pr-9 pl-4"
                    />
                </div>
                <button 
                    onClick={loadConversations} 
                    className="p-2 border rounded-md hover:bg-muted/50 transition-colors"
                    disabled={isLoading}
                    title="تحديث"
                >
                    <RefreshCw className={cn("h-4 w-4 text-muted-foreground", isLoading && "animate-spin")} />
                </button>
            </div>

            <ScrollArea className="flex-1">
                <div className="divide-y">
                    {filteredConversations.map((conversation) => (
                        <button
                            key={conversation.id}
                            onClick={() => onSelectConversation(conversation)}
                            className={cn(
                                "w-full p-4 flex gap-3 hover:bg-muted/40 transition-colors text-right",
                                selectedConversation?.id === conversation.id && "bg-muted/60"
                            )}
                        >
                            <Avatar className="h-10 w-10 border bg-muted/20">
                                <AvatarFallback className="bg-transparent">
                                    {getIcon(conversation.type)}
                                </AvatarFallback>
                            </Avatar>

                            <div className="flex-1 min-w-0">
                                <div className="flex items-center justify-between mb-1">
                                    <h4 className="font-semibold text-sm truncate">
                                        {conversation.targetName}
                                    </h4>
                                    {conversation.lastMessage && (
                                        <span className="text-[10px] text-muted-foreground font-mono">
                                            {formatTime(conversation.lastMessage.timestamp)}
                                        </span>
                                    )}
                                </div>

                                {conversation.lastMessage && (
                                    <div className="flex items-center justify-between mt-0.5">
                                        <p className={cn(
                                            "text-xs truncate max-w-[200px]",
                                            conversation.unreadCount > 0
                                                ? "font-bold text-foreground"
                                                : "text-muted-foreground"
                                        )}>
                                            <span className="font-medium text-slate-500 ml-1">
                                                {conversation.lastMessage.senderName}:
                                            </span>
                                            {conversation.lastMessage.content}
                                        </p>
                                        {conversation.unreadCount > 0 && (
                                            <Badge variant="destructive" className="h-5 min-w-[20px] flex items-center justify-center text-[10px] px-1 rounded-full">
                                                {conversation.unreadCount}
                                            </Badge>
                                        )}
                                    </div>
                                )}
                            </div>
                        </button>
                    ))}

                    {filteredConversations.length === 0 && (
                        <div className="p-8 text-center text-muted-foreground text-sm">
                            لا توجد محادثات نشطة
                        </div>
                    )}
                </div>
            </ScrollArea>
        </div>
    )
}
