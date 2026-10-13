export enum DeviceStatus {
    ACTIVE = "ACTIVE",
    INACTIVE = "INACTIVE",
    PENDING = "PENDING",
    BLOCKED = "BLOCKED",
}

export interface Device {
    id: string
    deviceId: string
    deviceName: string
    deviceModel?: string
    userId?: string
    userName?: string
    branchId?: string
    branchName?: string
    lastLogin?: string
    ipAddress?: string
    status: DeviceStatus
    createdAt: string
    updatedAt: string
}

export interface RegisterDeviceRequest {
    deviceId: string
    deviceName: string
    deviceModel?: string
    userId?: string
    branchId?: string
    status: DeviceStatus
}
