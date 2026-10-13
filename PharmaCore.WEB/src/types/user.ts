export enum UserRole {
    ORG_OWNER = "ORG_OWNER",
    ORG_ADMIN = "ORG_ADMIN",
    HR = "HR",
    MANAGER = "MANAGER",
    SUPERVISOR = "SUPERVISOR",
    EMPLOYEE = "EMPLOYEE",
}

export enum UserStatus {
    ACTIVE = "ACTIVE",
    INACTIVE = "INACTIVE",
    PENDING = "PENDING",
}

export interface UserCertificate {
    id: string
    name: string
    issuedBy: string
    issuedDate: string
    expiryDate?: string
    certificateUrl?: string
}

export interface User {
    id: string
    code: string
    name: string
    nameAr?: string
    email: string
    role: UserRole
    status: UserStatus

    // Personal Information
    dateOfBirth?: string
    nationalId?: string
    address?: string
    city?: string

    // Contact Information
    phones: string[] // Multiple phone numbers
    emergencyContact?: {
        name: string
        phone: string
        relation: string
    }

    // Employment Details
    branchId?: string
    branchName?: string
    position?: string
    department?: string
    hireDate?: string
    salary?: number

    // Education & Certificates
    education?: string
    certificates?: UserCertificate[]

    // Additional Info
    bio?: string
    bioAr?: string
    specializations?: string[]

    // Timestamps
    createdAt: string
    updatedAt: string
}

export interface CreateUserRequest {
    code: string
    name: string
    nameAr?: string
    email: string
    role: UserRole
    status: UserStatus
    dateOfBirth?: string
    nationalId?: string
    address?: string
    city?: string
    phones: string[]
    emergencyContact?: {
        name: string
        phone: string
        relation: string
    }
    branchId?: string
    position?: string
    department?: string
    hireDate?: string
    salary?: number
    education?: string
    bio?: string
    bioAr?: string
    specializations?: string[]
}

export interface UpdateUserRequest extends Partial<CreateUserRequest> { }
