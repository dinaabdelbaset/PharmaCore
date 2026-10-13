export interface Message {
    id: string
    conversationId: string
    senderId: string
    senderName: string
    senderRole?: string
    content: string
    type: 'text' | 'image' | 'file'
    imageUrl?: string
    fileUrl?: string
    fileName?: string
    timestamp: Date
    read: boolean
}

export interface Conversation {
    id: string
    type: 'branch' | 'department' | 'user' | 'employee' | 'all'
    targetId: string
    targetName: string
    targetType?: string
    lastMessage?: Message
    unreadCount: number
    participants: Participant[]
    createdAt: Date
    updatedAt: Date
}

export interface Participant {
    id: string
    name: string
    role: string
    branchId?: string
    branchName?: string
    lastSeen?: Date
}

export interface Department {
    id: string
    name: string
    description: string
    icon: string
}

export interface SearchResult {
    id: string
    name: string
    type: 'branch' | 'department' | 'user' | 'employee' | 'all'
    subtitle?: string
}
