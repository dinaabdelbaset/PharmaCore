"use client"

import { useState, useEffect } from "react"
import { Filter, Loader2 } from "lucide-react"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import { LogTable } from "@/components/logs/log-table"
import { ActivityCategory, AuditEventDto } from "@/types/activity-log"
import apiClient from "@/lib/api-client"

// Mock data conforming to AuditEventDto as a fallback
const mockEvents: AuditEventDto[] = [
    {
        id: "1",
        orgId: "org-1",
        branchId: "branch-1",
        employeeId: "emp-1",
        employeeCode: "EMP001",
        employeeName: "Ahmed Hassan",
        eventType: "USER_CREATED",
        category: "USERS",
        screenName: "User Management",
        targetTable: "Users",
        targetId: "6",
        description: "Created new employee account for Fatima Ali (EMP006)",
        severity: 1,
        ipAddress: "192.168.1.50",
        deviceName: "Ahmed-Laptop",
        createdAt: new Date(Date.now() - 1000 * 60 * 30).toISOString(),
    },
    {
        id: "2",
        orgId: "org-1",
        branchId: "branch-1",
        employeeId: "emp-2",
        employeeCode: "EMP002",
        employeeName: "Sara Mohamed",
        eventType: "PASSWORD_CHANGE_REQUESTED",
        category: "PASSWORDS",
        screenName: "Password Reset",
        targetTable: "Users",
        targetId: "4",
        description: "Requested password change: Employee forgot password",
        severity: 2,
        ipAddress: "192.168.1.51",
        deviceName: "Sara-PC",
        createdAt: new Date(Date.now() - 1000 * 60 * 60 * 2).toISOString(),
    },
    {
        id: "3",
        orgId: "org-1",
        branchId: "branch-1",
        employeeId: "emp-1",
        employeeCode: "EMP001",
        employeeName: "Ahmed Hassan",
        eventType: "PASSWORD_COPIED",
        category: "PASSWORDS",
        screenName: "Credentials Safe",
        targetTable: "Users",
        targetId: "3",
        description: "Copied password information for user Khaled Ali",
        severity: 3,
        ipAddress: "192.168.1.50",
        deviceName: "Ahmed-Laptop",
        createdAt: new Date(Date.now() - 1000 * 60 * 60 * 5).toISOString(),
    },
    {
        id: "4",
        orgId: "org-1",
        branchId: "branch-1",
        employeeId: "emp-1",
        employeeCode: "EMP001",
        employeeName: "Ahmed Hassan",
        eventType: "ROLE_CHANGED",
        category: "ROLES",
        screenName: "Role Settings",
        targetTable: "UserRoles",
        targetId: "2",
        description: "Changed role from EMPLOYEE to HR",
        severity: 2,
        ipAddress: "192.168.1.50",
        deviceName: "Ahmed-Laptop",
        createdAt: new Date(Date.now() - 1000 * 60 * 60 * 24).toISOString(),
    },
    {
        id: "5",
        orgId: "org-1",
        branchId: "branch-1",
        employeeId: "emp-3",
        employeeCode: "EMP003",
        employeeName: "Khaled Ali",
        eventType: "LOGIN",
        category: "SYSTEM",
        screenName: "Authentication",
        targetTable: "Sessions",
        targetId: "session-1",
        description: "User logged in successfully",
        severity: 0,
        ipAddress: "192.168.1.55",
        deviceName: "Khaled-Tablet",
        createdAt: new Date(Date.now() - 1000 * 60 * 60 * 24 * 2).toISOString(),
    },
]

export default function LogsPage() {
    const [categoryFilter, setCategoryFilter] = useState<string>("all")
    const [logs, setLogs] = useState<AuditEventDto[]>(mockEvents)
    const [isLoading, setIsLoading] = useState(false)

    useEffect(() => {
        const fetchLogs = async () => {
            setIsLoading(true)
            try {
                const response = await apiClient.get<{ items: AuditEventDto[] }>("/audit-logs?size=100")
                if (response.data && response.data.items) {
                    setLogs(response.data.items)
                }
            } catch (error) {
                console.error("Error fetching audit logs from API, using fallback data:", error)
            } finally {
                setIsLoading(false)
            }
        }
        fetchLogs()
    }, [])

    const filteredLogs = logs.filter((log) => {
        if (!log || !log.category) return false
        return categoryFilter === "all" || log.category.toUpperCase() === categoryFilter.toUpperCase()
    })

    const logCounts = logs.reduce((acc, log) => {
        if (log && log.category) {
            const cat = log.category.toUpperCase()
            acc[cat] = (acc[cat] || 0) + 1
        }
        return acc
    }, {} as Record<string, number>)

    return (
        <div className="space-y-6">
            <div className="flex items-center justify-between">
                <div>
                    <h1 className="text-3xl font-bold tracking-tight">Activity & Audit Logs</h1>
                    <p className="text-muted-foreground">
                        Track database modifications and system security events
                    </p>
                </div>
            </div>

            <div className="flex items-center justify-between gap-4">
                <div className="flex items-center gap-2">
                    <Filter className="h-4 w-4 text-muted-foreground" />
                    <Select value={categoryFilter} onValueChange={setCategoryFilter}>
                        <SelectTrigger className="w-[250px]">
                            <SelectValue placeholder="Filter by category" />
                        </SelectTrigger>
                        <SelectContent>
                            <SelectItem value="all">
                                All Categories ({logs.length})
                            </SelectItem>
                            <SelectItem value={ActivityCategory.USERS}>
                                Users {logCounts[ActivityCategory.USERS] ? `(${logCounts[ActivityCategory.USERS]})` : ''}
                            </SelectItem>
                            <SelectItem value={ActivityCategory.PASSWORDS}>
                                Passwords {logCounts[ActivityCategory.PASSWORDS] ? `(${logCounts[ActivityCategory.PASSWORDS]})` : ''}
                            </SelectItem>
                            <SelectItem value={ActivityCategory.ROLES}>
                                Roles {logCounts[ActivityCategory.ROLES] ? `(${logCounts[ActivityCategory.ROLES]})` : ''}
                            </SelectItem>
                            <SelectItem value={ActivityCategory.BRANCHES}>
                                Branches {logCounts[ActivityCategory.BRANCHES] ? `(${logCounts[ActivityCategory.BRANCHES]})` : ''}
                            </SelectItem>
                            <SelectItem value={ActivityCategory.DEVICES}>
                                Devices {logCounts[ActivityCategory.DEVICES] ? `(${logCounts[ActivityCategory.DEVICES]})` : ''}
                            </SelectItem>
                            <SelectItem value={ActivityCategory.ACTIVATION_CODES}>
                                Activation Codes {logCounts[ActivityCategory.ACTIVATION_CODES] ? `(${logCounts[ActivityCategory.ACTIVATION_CODES]})` : ''}
                            </SelectItem>
                            <SelectItem value={ActivityCategory.EMPLOYEES}>
                                Employees {logCounts[ActivityCategory.EMPLOYEES] ? `(${logCounts[ActivityCategory.EMPLOYEES]})` : ''}
                            </SelectItem>
                            <SelectItem value={ActivityCategory.SUPPLIERS}>
                                Suppliers {logCounts[ActivityCategory.SUPPLIERS] ? `(${logCounts[ActivityCategory.SUPPLIERS]})` : ''}
                            </SelectItem>
                            <SelectItem value={ActivityCategory.SYSTEM}>
                                System {logCounts[ActivityCategory.SYSTEM] ? `(${logCounts[ActivityCategory.SYSTEM]})` : ''}
                            </SelectItem>
                        </SelectContent>
                    </Select>
                </div>

                {isLoading && (
                    <div className="flex items-center gap-2 text-sm text-muted-foreground">
                        <Loader2 className="h-4 w-4 animate-spin" />
                        Updating logs...
                    </div>
                )}
            </div>

            <LogTable logs={filteredLogs} />
        </div>
    )
}
