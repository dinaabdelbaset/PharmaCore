import { UserRole } from "./user"

export enum ActivityCategory {
    USERS = "USERS",
    PASSWORDS = "PASSWORDS",
    ROLES = "ROLES",
    BRANCHES = "BRANCHES",
    DEVICES = "DEVICES",
    ACTIVATION_CODES = "ACTIVATION_CODES",
    EMPLOYEES = "EMPLOYEES",
    SUPPLIERS = "SUPPLIERS",
    SYSTEM = "SYSTEM",
}

export enum ActivityAction {
    // User actions
    USER_CREATED = "USER_CREATED",
    USER_UPDATED = "USER_UPDATED",
    USER_DELETED = "USER_DELETED",
    USER_STATUS_CHANGED = "USER_STATUS_CHANGED",

    // Password actions
    PASSWORD_CHANGE_REQUESTED = "PASSWORD_CHANGE_REQUESTED",
    PASSWORD_CHANGE_APPROVED = "PASSWORD_CHANGE_APPROVED",
    PASSWORD_CHANGE_REJECTED = "PASSWORD_CHANGE_REJECTED",
    PASSWORD_COPIED = "PASSWORD_COPIED",

    // Role actions
    ROLE_ASSIGNED = "ROLE_ASSIGNED",
    ROLE_CHANGED = "ROLE_CHANGED",

    // Branch actions
    BRANCH_CREATED = "BRANCH_CREATED",
    BRANCH_UPDATED = "BRANCH_UPDATED",
    BRANCH_DELETED = "BRANCH_DELETED",

    // Device actions
    DEVICE_REGISTERED = "DEVICE_REGISTERED",
    DEVICE_ACTIVATED = "DEVICE_ACTIVATED",
    DEVICE_DEACTIVATED = "DEVICE_DEACTIVATED",
    DEVICE_DELETED = "DEVICE_DELETED",

    // Activation code actions
    ACTIVATION_CODE_GENERATED = "ACTIVATION_CODE_GENERATED",
    ACTIVATION_CODE_USED = "ACTIVATION_CODE_USED",
    ACTIVATION_CODE_EXPIRED = "ACTIVATION_CODE_EXPIRED",

    // Employee actions
    EMPLOYEE_ADDED = "EMPLOYEE_ADDED",
    EMPLOYEE_UPDATED = "EMPLOYEE_UPDATED",
    EMPLOYEE_REMOVED = "EMPLOYEE_REMOVED",

    // Supplier actions
    SUPPLIER_CREATED = "SUPPLIER_CREATED",
    SUPPLIER_UPDATED = "SUPPLIER_UPDATED",
    SUPPLIER_DELETED = "SUPPLIER_DELETED",

    // System actions
    LOGIN = "LOGIN",
    LOGOUT = "LOGOUT",
    FAILED_LOGIN = "FAILED_LOGIN",
}

export interface ActivityLog {
    id: string
    category: ActivityCategory
    action: ActivityAction
    performedById: string
    performedByName: string
    performedByCode: string
    performedByRole: UserRole
    targetUserId?: string
    targetUserName?: string
    targetUserCode?: string
    targetUserRole?: UserRole
    description: string
    metadata?: Record<string, any> // Additional details
    ipAddress?: string
    userAgent?: string
    createdAt: string
}

export interface CreateActivityLogInput {
    category: ActivityCategory
    action: ActivityAction
    performedById: string
    performedByName: string
    performedByCode: string
    performedByRole: UserRole
    targetUserId?: string
    targetUserName?: string
    targetUserCode?: string
    targetUserRole?: UserRole
    description: string
    metadata?: Record<string, any>
}
export interface FieldChangeDto {
    fieldName: string
    fieldLabel: string | null
    oldValue: string | null
    newValue: string | null
}

export interface AuditEventDto {
    id: string
    orgId: string
    branchId: string | null
    employeeId: string | null
    employeeCode: string | null
    employeeName: string | null
    eventType: string
    category: string
    screenName: string | null
    targetTable: string | null
    targetId: string | null
    description: string
    severity: number
    ipAddress: string | null
    deviceName: string | null
    createdAt: string
    dataChanges?: FieldChangeDto[] | null
}
