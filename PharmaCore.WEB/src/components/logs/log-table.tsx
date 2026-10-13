"use client"

import { useState } from "react"
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table"
import { Badge } from "@/components/ui/badge"
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
} from "@/components/ui/dialog"
import { AuditEventDto, FieldChangeDto } from "@/types/activity-log"
import { formatDistanceToNow, format } from "date-fns"
import apiClient from "@/lib/api-client"
import { Globe, Laptop, Terminal, Calendar, User, Eye } from "lucide-react"

interface LogTableProps {
    logs: AuditEventDto[]
    // Optional external control of the details modal
    onRowClick?: (log: AuditEventDto) => void
}

const getCategoryColor = (category: string) => {
    const colors: Record<string, string> = {
        USERS: "bg-blue-500/10 text-blue-500 border-blue-500/20",
        PASSWORDS: "bg-amber-500/10 text-amber-500 border-amber-500/20",
        ROLES: "bg-purple-500/10 text-purple-500 border-purple-500/20",
        BRANCHES: "bg-green-500/10 text-green-500 border-green-500/20",
        DEVICES: "bg-cyan-500/10 text-cyan-500 border-cyan-500/20",
        ACTIVATION_CODES: "bg-pink-500/10 text-pink-500 border-pink-500/20",
        EMPLOYEES: "bg-orange-500/10 text-orange-500 border-orange-500/20",
        SUPPLIERS: "bg-teal-500/10 text-teal-500 border-teal-500/20",
        SYSTEM: "bg-gray-500/10 text-gray-500 border-gray-500/20",
    }
    return colors[category.toUpperCase()] || "bg-slate-500/10 text-slate-500 border-slate-500/20"
}

export function LogTable({ logs, onRowClick }: LogTableProps) {
    const [selectedLog, setSelectedLog] = useState<AuditEventDto | null>(null)
    const [isDetailsOpen, setIsDetailsOpen] = useState(false)
    const [fieldChanges, setFieldChanges] = useState<FieldChangeDto[] | null>(null)
    const [isLoadingChanges, setIsLoadingChanges] = useState(false)
    const [errorChanges, setErrorChanges] = useState<string | null>(null)

    const handleRowClick = async (log: AuditEventDto) => {
        // Trigger optional callback first
        if (onRowClick) {
            onRowClick(log)
        }

        setSelectedLog(log)
        setIsDetailsOpen(true)
        setIsLoadingChanges(true)
        setErrorChanges(null)
        setFieldChanges(null)

        try {
            const response = await apiClient.get<FieldChangeDto[]>(`/audit-logs/${log.id}/changes`)
            setFieldChanges(response.data)
        } catch (err: any) {
            console.error("Error fetching audit log changes:", err)
            setErrorChanges("Failed to load details of modified fields.")
        } finally {
            setIsLoadingChanges(false)
        }
    }

    const formatDate = (dateStr: string) => {
        try {
            const d = new Date(dateStr)
            return format(d, "yyyy-MM-dd HH:mm:ss")
        } catch {
            return dateStr
        }
    }

    const formatDistance = (dateStr: string) => {
        try {
            const d = new Date(dateStr)
            return formatDistanceToNow(d, { addSuffix: true })
        } catch {
            return ""
        }
    }

    return (
        <div className="rounded-md border">
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead>Date/Time</TableHead>
                        <TableHead>Category</TableHead>
                        <TableHead>Event</TableHead>
                        <TableHead>Performed By</TableHead>
                        <TableHead>Screen</TableHead>
                        <TableHead>Description</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {logs.length === 0 ? (
                        <TableRow>
                            <TableCell colSpan={6} className="h-24 text-center">
                                No audit events found.
                            </TableCell>
                        </TableRow>
                    ) : (
                        logs.map((log) => (
                            <TableRow 
                                key={log.id} 
                                onClick={() => handleRowClick(log)}
                                className="cursor-pointer hover:bg-muted/50 transition-colors"
                            >
                                <TableCell className="text-sm text-muted-foreground whitespace-nowrap">
                                    <div>{formatDate(log.createdAt)}</div>
                                    <div className="text-xs opacity-75">{formatDistance(log.createdAt)}</div>
                                </TableCell>
                                <TableCell>
                                    <Badge variant="outline" className={getCategoryColor(log.category)}>
                                        {log.category}
                                    </Badge>
                                </TableCell>
                                <TableCell className="font-medium whitespace-nowrap">
                                    {log.eventType}
                                </TableCell>
                                <TableCell>
                                    <div>
                                        <div className="font-medium text-sm">{log.employeeName || "System"}</div>
                                        {log.employeeCode && (
                                            <div className="text-xs text-muted-foreground">{log.employeeCode}</div>
                                        )}
                                    </div>
                                </TableCell>
                                <TableCell>
                                    {log.screenName ? (
                                        <Badge variant="secondary" className="font-mono text-xs">
                                            {log.screenName}
                                        </Badge>
                                    ) : (
                                        <span className="text-sm text-muted-foreground">—</span>
                                    )}
                                </TableCell>
                                <TableCell className="max-w-md truncate text-sm">
                                    {log.description}
                                </TableCell>
                            </TableRow>
                        ))
                    )}
                </TableBody>
            </Table>

            {/* Event Details Dialog */}
            <Dialog open={isDetailsOpen} onOpenChange={setIsDetailsOpen}>
                <DialogContent className="max-w-3xl max-h-[90vh] overflow-y-auto">
                    <DialogHeader>
                        <div className="flex items-center gap-3">
                            <DialogTitle className="text-xl">Audit Event Details</DialogTitle>
                            {selectedLog && (
                                <Badge variant="outline" className={getCategoryColor(selectedLog.category)}>
                                    {selectedLog.category}
                                </Badge>
                            )}
                        </div>
                        <DialogDescription>
                            Detailed metadata and before-and-after value differences for this audit record.
                        </DialogDescription>
                    </DialogHeader>

                    {selectedLog && (
                        <div className="space-y-6 mt-4">
                            {/* Metadata Grid */}
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-4 bg-muted/30 p-4 rounded-lg border text-sm">
                                <div className="space-y-1">
                                    <span className="text-xs font-semibold text-muted-foreground uppercase tracking-wider block">Event Type</span>
                                    <span className="font-medium text-foreground flex items-center gap-1.5">
                                        <Terminal className="h-4 w-4 text-muted-foreground" />
                                        {selectedLog.eventType}
                                    </span>
                                </div>
                                <div className="space-y-1">
                                    <span className="text-xs font-semibold text-muted-foreground uppercase tracking-wider block">Date & Time</span>
                                    <span className="font-medium text-foreground flex items-center gap-1.5">
                                        <Calendar className="h-4 w-4 text-muted-foreground" />
                                        {formatDate(selectedLog.createdAt)} ({formatDistance(selectedLog.createdAt)})
                                    </span>
                                </div>
                                <div className="space-y-1">
                                    <span className="text-xs font-semibold text-muted-foreground uppercase tracking-wider block">Performed By</span>
                                    <span className="font-medium text-foreground flex items-center gap-1.5">
                                        <User className="h-4 w-4 text-muted-foreground" />
                                        {selectedLog.employeeName || "System"} {selectedLog.employeeCode ? `(${selectedLog.employeeCode})` : ""}
                                    </span>
                                </div>
                                <div className="space-y-1">
                                    <span className="text-xs font-semibold text-muted-foreground uppercase tracking-wider block">Screen</span>
                                    <span className="font-medium text-foreground flex items-center gap-1.5">
                                        <Eye className="h-4 w-4 text-muted-foreground" />
                                        {selectedLog.screenName || "—"}
                                    </span>
                                </div>
                                <div className="space-y-1">
                                    <span className="text-xs font-semibold text-muted-foreground uppercase tracking-wider block">IP Address</span>
                                    <span className="font-mono font-medium text-foreground flex items-center gap-1.5">
                                        <Globe className="h-4 w-4 text-muted-foreground" />
                                        {selectedLog.ipAddress || "—"}
                                    </span>
                                </div>
                                <div className="space-y-1">
                                    <span className="text-xs font-semibold text-muted-foreground uppercase tracking-wider block">Device Name</span>
                                    <span className="font-medium text-foreground flex items-center gap-1.5">
                                        <Laptop className="h-4 w-4 text-muted-foreground" />
                                        {selectedLog.deviceName || "—"}
                                    </span>
                                </div>
                                <div className="md:col-span-2 border-t pt-3 mt-1 space-y-1">
                                    <span className="text-xs font-semibold text-muted-foreground uppercase tracking-wider block">Description</span>
                                    <span className="text-foreground text-sm leading-relaxed">{selectedLog.description}</span>
                                </div>
                            </div>

                            {/* Modified Fields Comparison */}
                            <div className="space-y-3">
                                <h3 className="font-semibold text-base text-foreground">Field-Level Modifications</h3>

                                {isLoadingChanges ? (
                                    <div className="flex flex-col items-center justify-center p-12 border rounded-lg bg-muted/5 space-y-2">
                                        <div className="h-6 w-6 animate-spin rounded-full border-2 border-primary border-t-transparent" />
                                        <span className="text-sm text-muted-foreground">Fetching changes history...</span>
                                    </div>
                                ) : errorChanges ? (
                                    <div className="p-4 border border-destructive/20 rounded-lg bg-destructive/5 text-destructive text-sm text-center font-medium">
                                        {errorChanges}
                                    </div>
                                ) : !fieldChanges || fieldChanges.length === 0 ? (
                                    <div className="p-8 border border-dashed rounded-lg bg-muted/5 text-center text-sm text-muted-foreground">
                                        No specific value differences were recorded for this action.
                                    </div>
                                ) : (
                                    <div className="border rounded-md overflow-hidden bg-background">
                                        <Table>
                                            <TableHeader className="bg-muted/40">
                                                <TableRow>
                                                    <TableHead className="w-1/3">Field Name</TableHead>
                                                    <TableHead>Old Value</TableHead>
                                                    <TableHead>New Value</TableHead>
                                                </TableRow>
                                            </TableHeader>
                                            <TableBody>
                                                {fieldChanges.map((change, index) => (
                                                    <TableRow key={index} className="hover:bg-muted/10 transition-colors">
                                                        <TableCell className="font-semibold text-sm align-top py-3">
                                                            <div>{change.fieldLabel || change.fieldName}</div>
                                                            {change.fieldLabel && change.fieldLabel !== change.fieldName && (
                                                                <span className="block text-xs font-mono font-normal text-muted-foreground mt-0.5">
                                                                    ({change.fieldName})
                                                                </span>
                                                            )}
                                                        </TableCell>
                                                        <TableCell className="bg-red-500/5 text-red-700 dark:text-red-400 font-mono text-sm max-w-[220px] break-all align-top py-3">
                                                            {change.oldValue === null || change.oldValue === undefined ? (
                                                                <span className="italic text-muted-foreground/60">[null]</span>
                                                            ) : change.oldValue === "" ? (
                                                                <span className="italic text-muted-foreground/60">[empty]</span>
                                                            ) : (
                                                                change.oldValue
                                                            )}
                                                        </TableCell>
                                                        <TableCell className="bg-green-500/5 text-green-700 dark:text-green-400 font-mono text-sm max-w-[220px] break-all align-top py-3">
                                                            {change.newValue === null || change.newValue === undefined ? (
                                                                <span className="italic text-muted-foreground/60">[null]</span>
                                                            ) : change.newValue === "" ? (
                                                                <span className="italic text-muted-foreground/60">[empty]</span>
                                                            ) : (
                                                                change.newValue
                                                            )}
                                                        </TableCell>
                                                    </TableRow>
                                                ))}
                                            </TableBody>
                                        </Table>
                                    </div>
                                )}
                            </div>
                        </div>
                    )}
                </DialogContent>
            </Dialog>
        </div>
    )
}
