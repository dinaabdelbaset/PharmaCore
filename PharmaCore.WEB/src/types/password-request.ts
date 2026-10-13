export enum PasswordRequestStatus {
    PENDING = "PENDING",
    APPROVED = "APPROVED",
    REJECTED = "REJECTED",
}

export interface PasswordRequest {
    id: string
    targetUserId: string
    targetUserName: string
    targetUserCode: string
    requestedById: string
    requestedByName: string
    requestedByCode: string
    reason: string
    newPassword?: string // Set when approved
    status: PasswordRequestStatus
    createdAt: string
    updatedAt: string
    reviewedById?: string
    reviewedByName?: string
    reviewedAt?: string
    reviewNotes?: string
}

export interface CreatePasswordRequestInput {
    targetUserId: string
    reason: string
}

export interface ReviewPasswordRequestInput {
    requestId: string
    approved: boolean
    newPassword?: string // Required if approved
    reviewNotes?: string
}
