export enum ActivationCodeStatus {
    ACTIVE = "ACTIVE",
    USED = "USED",
    EXPIRED = "EXPIRED",
    REVOKED = "REVOKED",
}

export interface ActivationCode {
    id: string
    code: string
    userId?: string
    userName?: string
    deviceId?: string
    deviceName?: string
    expiresAt: string
    usedAt?: string
    status: ActivationCodeStatus
    createdById: string
    createdByName: string
    createdAt: string
    updatedAt: string
}

export interface GenerateActivationCodeRequest {
    userId?: string
    expiresAt: string
}
