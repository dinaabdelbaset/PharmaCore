import { UserRole } from "@/types/user"

// Role hierarchy - from highest to lowest
export const ROLE_HIERARCHY: Record<UserRole, number> = {
    [UserRole.ORG_OWNER]: 6,
    [UserRole.ORG_ADMIN]: 5,
    [UserRole.HR]: 4,
    [UserRole.MANAGER]: 3,
    [UserRole.SUPERVISOR]: 2,
    [UserRole.EMPLOYEE]: 1,
}

/**
 * Check if userRole can manage targetRole
 * A role can manage any role with a lower hierarchy level
 */
export function canManageRole(userRole: UserRole, targetRole: UserRole): boolean {
    return ROLE_HIERARCHY[userRole] > ROLE_HIERARCHY[targetRole]
}

/**
 * Check if userRole can edit a user with targetRole
 * Same as canManageRole - cannot edit users at same or higher level
 */
export function canEditUser(userRole: UserRole, targetUserRole: UserRole): boolean {
    return ROLE_HIERARCHY[userRole] > ROLE_HIERARCHY[targetUserRole]
}

/**
 * Get list of roles that a user can assign to others
 * Returns all roles with lower hierarchy level
 */
export function getAssignableRoles(userRole: UserRole): UserRole[] {
    const userLevel = ROLE_HIERARCHY[userRole]

    return (Object.keys(ROLE_HIERARCHY) as UserRole[]).filter(
        (role) => ROLE_HIERARCHY[role] < userLevel
    )
}

/**
 * Get the next higher role(s) in hierarchy for approval workflows
 */
export function getHigherRoles(userRole: UserRole): UserRole[] {
    const userLevel = ROLE_HIERARCHY[userRole]

    return (Object.keys(ROLE_HIERARCHY) as UserRole[]).filter(
        (role) => ROLE_HIERARCHY[role] > userLevel
    )
}

/**
 * Get all roles at or below the given role (for logs visibility)
 */
export function getRolesAtOrBelow(userRole: UserRole): UserRole[] {
    const userLevel = ROLE_HIERARCHY[userRole]

    return (Object.keys(ROLE_HIERARCHY) as UserRole[]).filter(
        (role) => ROLE_HIERARCHY[role] <= userLevel
    )
}

/**
 * Check if a role can view logs for a specific user
 * Can view logs for users with lower roles only
 */
export function canViewUserLogs(viewerRole: UserRole, targetUserRole: UserRole): boolean {
    return ROLE_HIERARCHY[viewerRole] > ROLE_HIERARCHY[targetUserRole]
}
