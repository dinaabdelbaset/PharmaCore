import {
    ActivityLog,
    CreateActivityLogInput,
    ActivityCategory,
    ActivityAction
} from "@/types/activity-log"
import { UserRole } from "@/types/user"

// Mock storage for activity logs (will be replaced with API calls)
let activityLogs: ActivityLog[] = []

/**
 * Log an activity
 */
export function logActivity(input: CreateActivityLogInput): ActivityLog {
    const log: ActivityLog = {
        id: String(activityLogs.length + 1),
        ...input,
        createdAt: new Date().toISOString(),
    }

    activityLogs.unshift(log) // Add to beginning for chronological order

    return log
}

/**
 * Get all activity logs (with optional filtering)
 */
export function getActivityLogs(filters?: {
    category?: ActivityCategory
    action?: ActivityAction
    performedById?: string
    targetUserId?: string
    startDate?: string
    endDate?: string
}): ActivityLog[] {
    let filtered = [...activityLogs]

    if (filters) {
        if (filters.category) {
            filtered = filtered.filter(log => log.category === filters.category)
        }
        if (filters.action) {
            filtered = filtered.filter(log => log.action === filters.action)
        }
        if (filters.performedById) {
            filtered = filtered.filter(log => log.performedById === filters.performedById)
        }
        if (filters.targetUserId) {
            filtered = filtered.filter(log => log.targetUserId === filters.targetUserId)
        }
        if (filters.startDate) {
            filtered = filtered.filter(log => log.createdAt >= filters.startDate!)
        }
        if (filters.endDate) {
            filtered = filtered.filter(log => log.createdAt <= filters.endDate!)
        }
    }

    return filtered
}

/**
 * Get logs visible to a specific role
 * Users can only see logs for users with lower roles
 */
export function getLogsForRole(userRole: UserRole, category?: ActivityCategory): ActivityLog[] {
    const logs = category
        ? getActivityLogs({ category })
        : getActivityLogs()

    // For now, return all logs (will add role filtering when user context is available)
    return logs
}

/**
 * Clear all logs (for testing)
 */
export function clearActivityLogs(): void {
    activityLogs = []
}

/**
 * Get logs count by category
 */
export function getLogCountByCategory(): Record<ActivityCategory, number> {
    const counts = {} as Record<ActivityCategory, number>

    Object.values(ActivityCategory).forEach(category => {
        counts[category] = activityLogs.filter(log => log.category === category).length
    })

    return counts
}
