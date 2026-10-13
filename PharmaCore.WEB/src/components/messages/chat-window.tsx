"use client"

import { useState, useRef, useEffect } from "react"
import { ScrollArea } from "@/components/ui/scroll-area"
import { Input } from "@/components/ui/input"
import { Button } from "@/components/ui/button"
import { Avatar, AvatarFallback } from "@/components/ui/avatar"
import { Send, Building2, Megaphone, User } from "lucide-react"
import { cn } from "@/lib/utils"
import type { Conversation, Message } from "@/types/message"
import apiClient from "@/lib/api-client"
import { toast } from "sonner"

interface ChatWindowProps {
    conversation: Conversation | null
    onMessageSent?: () => void
}

export function ChatWindow({ conversation, onMessageSent }: ChatWindowProps) {
    const [messages, setMessages] = useState<Message[]>([])
    const [inputText, setInputText] = useState("")
    const [isLoading, setIsLoading] = useState(false)
    const [myId, setMyId] = useState<string>("")
    const scrollRef = useRef<HTMLDivElement>(null)

    // Decode token to get employee ID
    useEffect(() => {
        if (typeof window !== "undefined") {
            const token = localStorage.getItem("token")
            if (token) {
                try {
                    const payload = token.split(".")[1]
                    const decoded = JSON.parse(atob(payload))
                    setMyId(decoded.employee_id || decoded.user_id || "")
                } catch (e) {
                    console.error("JWT decoding failed", e)
                }
            }
        }
    }, [])

    // Load messages and start polling when conversation changes
    useEffect(() => {
        if (conversation) {
            loadMessages()
            const pollInterval = setInterval(() => {
                loadMessages(true) // Silent reload
            }, 4000)

            return () => clearInterval(pollInterval)
        } else {
            setMessages([])
        }
    }, [conversation])

    // Auto scroll when messages change
    useEffect(() => {
        scrollToBottom()
    }, [messages])

    const loadMessages = async (silent = false) => {
        if (!conversation) return
        if (!silent) setIsLoading(true)
        try {
            const res = await apiClient.get<any[]>(`/messages/conversations/${conversation.targetId}`, {
                params: { type: conversation.type }
            })

            const list: Message[] = res.data.map(m => ({
                id: m.id,
                conversationId: conversation.id,
                senderId: m.senderId,
                senderName: m.senderName,
                senderRole: m.senderRole,
                content: m.content,
                type: "text",
                timestamp: new Date(m.timestamp),
                read: m.read
            }))

            setMessages(list)
        } catch (err) {
            console.error("Error loading chat messages", err)
        } finally {
            if (!silent) setIsLoading(false)
        }
    }

    const handleSend = async (e: React.FormEvent) => {
        e.preventDefault()
        if (!inputText.trim() || !conversation) return

        const text = inputText.trim()
        setInputText("") // Clear immediately for UX

        try {
            await apiClient.post("/messages", {
                targetId: conversation.targetId,
                targetType: conversation.type,
                content: text
            })
            
            // Reload messages instantly
            await loadMessages(true)
            if (onMessageSent) onMessageSent()
        } catch (err) {
            toast.error("فشل إرسال الرسالة")
        }
    }

    const scrollToBottom = () => {
        if (scrollRef.current) {
            const scrollContainer = scrollRef.current.querySelector("[data-radix-scroll-area-viewport]")
            if (scrollContainer) {
                scrollContainer.scrollTop = scrollContainer.scrollHeight
            }
        }
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

    const formatMessageDate = (date: Date) => {
        return date.toLocaleDateString("ar-EG", { weekday: 'long', month: 'short', day: 'numeric' })
    }

    const formatMessageTime = (date: Date) => {
        return date.toLocaleTimeString("ar-EG", { hour: '2-digit', minute: '2-digit' })
    }

    if (!conversation) {
        return (
            <div className="h-full flex items-center justify-center bg-muted/10 text-muted-foreground text-sm" dir="rtl">
                <div className="text-center space-y-2">
                    <Megaphone className="h-10 w-10 mx-auto text-muted-foreground/40" />
                    <p className="font-semibold">لم يتم تحديد محادثة</p>
                    <p className="text-xs">اختر موظفاً أو فرعاً من القائمة الجانبية لبدء المراسلة</p>
                </div>
            </div>
        )
    }

    return (
        <div className="h-full flex flex-col bg-slate-50/50 dark:bg-slate-900/10" dir="rtl">
            {/* Header */}
            <div className="p-4 border-b bg-card flex items-center gap-3">
                <Avatar className="h-10 w-10 border bg-muted/20">
                    <AvatarFallback className="bg-transparent">
                        {getIcon(conversation.type)}
                    </AvatarFallback>
                </Avatar>
                <div>
                    <h3 className="font-bold text-sm text-foreground">{conversation.targetName}</h3>
                    <p className="text-[10px] text-muted-foreground mt-0.5">{conversation.targetType}</p>
                </div>
            </div>

            {/* Messages Area */}
            <ScrollArea className="flex-1 p-4" ref={scrollRef}>
                {isLoading && messages.length === 0 ? (
                    <div className="h-full flex items-center justify-center text-muted-foreground text-xs py-20">
                        جاري تحميل المحادثة...
                    </div>
                ) : (
                    <div className="space-y-4">
                        {messages.map((message, index) => {
                            const showDate = index === 0 ||
                                formatMessageDate(messages[index - 1].timestamp) !== formatMessageDate(message.timestamp)

                            const isOwn = message.senderId.toString().toLowerCase() === myId.toLowerCase()

                            return (
                                <div key={message.id}>
                                    {showDate && (
                                        <div className="flex justify-center my-4">
                                            <span className="text-[10px] text-muted-foreground bg-muted/60 px-3 py-1 rounded-full border">
                                                {formatMessageDate(message.timestamp)}
                                            </span>
                                        </div>
                                    )}

                                    <div className={cn(
                                        "flex gap-2.5 max-w-[75%]",
                                        isOwn ? "mr-auto flex-row-reverse text-left" : "ml-auto"
                                    )}>
                                        <Avatar className="h-8 w-8 border bg-muted/40">
                                            <AvatarFallback className="text-[10px] bg-transparent font-bold">
                                                {message.senderName.split(" ").map(n => n[0]).slice(0, 2).join("")}
                                            </AvatarFallback>
                                        </Avatar>

                                        <div className="space-y-1">
                                            {/* Sender identity */}
                                            <div className="flex items-center gap-2">
                                                <span className="text-[10px] font-bold text-slate-700 dark:text-slate-300">
                                                    {isOwn ? "أنت" : message.senderName}
                                                </span>
                                                <span className="text-[9px] text-muted-foreground">({message.senderRole})</span>
                                            </div>

                                            {/* Chat Bubble */}
                                            <div className={cn(
                                                "p-3 rounded-2xl text-xs leading-relaxed shadow-sm",
                                                isOwn 
                                                    ? "bg-primary text-primary-foreground rounded-tl-none text-right" 
                                                    : "bg-card text-foreground rounded-tr-none text-right border"
                                            )}>
                                                {message.content}
                                            </div>

                                            {/* Time label */}
                                            <div className="text-[9px] text-muted-foreground px-1 font-mono text-left">
                                                {formatMessageTime(message.timestamp)}
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            )
                        })}
                    </div>
                )}
            </ScrollArea>

            {/* Input Row */}
            <form onSubmit={handleSend} className="p-4 border-t bg-card flex gap-2 items-center">
                <Input
                    placeholder="اكتب رسالتك هنا..."
                    value={inputText}
                    onChange={(e) => setInputText(e.target.value)}
                    className="flex-1 text-xs"
                />
                <Button type="submit" size="icon" disabled={!inputText.trim()} className="rounded-full">
                    <Send className="h-4 w-4 rotate-180" />
                </Button>
            </form>
        </div>
    )
}
