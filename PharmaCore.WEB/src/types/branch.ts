export enum BranchStatus {
    ACTIVE = "ACTIVE",
    INACTIVE = "INACTIVE",
    PENDING = "PENDING",
}

export interface QualityCertificate {
    id: string
    name: string
    issuedBy: string
    issuedDate: string
    expiryDate?: string
    certificateUrl?: string
}

export interface BranchManager {
    id: string
    name: string
    phone: string
    email?: string
}

export interface Branch {
    id: string
    code: string
    name: string
    nameAr?: string

    // Contact Information
    address: string
    city: string
    phones: string[] // Multiple phone numbers
    email?: string

    // Manager
    manager?: BranchManager

    // Location
    latitude?: number
    longitude?: number

    // Quality & Certificates
    qualityCertificates?: QualityCertificate[]

    // Additional Info
    description?: string
    descriptionAr?: string
    openingHours?: string
    establishedDate?: string
    totalArea?: number // in square meters

    // Status & Metrics
    status: BranchStatus
    employeeCount?: number

    // Timestamps
    createdAt: string
    updatedAt: string
}

export interface CreateBranchRequest {
    code: string
    name: string
    nameAr?: string
    address: string
    city: string
    phones: string[]
    email?: string
    managerId?: string
    latitude?: number
    longitude?: number
    description?: string
    descriptionAr?: string
    openingHours?: string
    establishedDate?: string
    totalArea?: number
    status: BranchStatus
}

export interface UpdateBranchRequest extends Partial<CreateBranchRequest> { }
