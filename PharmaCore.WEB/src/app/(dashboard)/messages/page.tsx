"use client"

import { useState } from "react"
import { Card } from "@/components/ui/card"
import { ConversationList } from "@/components/messages/conversation-list"
import { ChatWindow } from "@/components/messages/chat-window"
import { NewMessageDialog } from "@/components/messages/new-message-dialog"
import { Button } from "@/components/ui/button"
import { MessageSquarePlus, ArrowLeft } from "lucide-react"
import type { Conversation } from "@/types/message"

export default function MessagesPage() {
    const [selectedConversation, setSelectedConversation] = useState<Conversation | null>(null)
    const [showNewMessage, setShowNewMessage] = useState(false)
    const [refreshTrigger, setRefreshTrigger] = useState(0)

    return (
        <div className="flex-1 p-3 md:p-6 lg:p-8 pt-3 md:pt-6" dir="rtl">
            <div className="flex items-center justify-between mb-4 md:mb-6">
                <div className="text-right">
                    <h2 className="text-2xl md:text-3xl font-bold tracking-tight">الرسائل والإعلانات</h2>
                    <p className="text-sm text-muted-foreground hidden sm:block">نظام التواصل الداخلي للموظفين والفروع</p>
                </div>
                <Button onClick={() => setShowNewMessage(true)} size="sm" className="md:size-default">
                    <MessageSquarePlus className="ml-2 h-4 w-4" />
                    <span>رسالة جديدة</span>
                </Button>
            </div>

            <div className="grid grid-cols-1 lg:grid-cols-3 gap-3 md:gap-4 h-[calc(100vh-180px)] md:h-[calc(100vh-200px)]">
                {/* Conversations List - Hidden on mobile when conversation is selected */}
                <Card className={`lg:col-span-1 overflow-hidden ${selectedConversation ? 'hidden lg:block' : 'block'}`}>
                    <ConversationList
                        selectedConversation={selectedConversation}
                        onSelectConversation={setSelectedConversation}
                        refreshTrigger={refreshTrigger}
                    />
                </Card>

                {/* Chat Window - Full width on mobile when conversation is selected */}
                <Card className={`lg:col-span-2 overflow-hidden ${!selectedConversation ? 'hidden lg:block' : 'block'}`}>
                    {selectedConversation && (
                        <div className="lg:hidden p-3 border-b flex items-center gap-2" dir="rtl">
                            <Button
                                variant="ghost"
                                size="icon"
                                onClick={() => setSelectedConversation(null)}
                            >
                                <ArrowLeft className="h-5 w-5" />
                            </Button>
                            <span className="font-semibold text-sm">العودة للمحادثات</span>
                        </div>
                    )}
                    <ChatWindow 
                        conversation={selectedConversation} 
                        onMessageSent={() => setRefreshTrigger(prev => prev + 1)}
                    />
                </Card>
            </div>

            <NewMessageDialog
                open={showNewMessage}
                onClose={() => setShowNewMessage(false)}
                onStartConversation={(conv) => {
                    setSelectedConversation(conv)
                    setRefreshTrigger(prev => prev + 1)
                }}
            />
        </div>
    )
}
