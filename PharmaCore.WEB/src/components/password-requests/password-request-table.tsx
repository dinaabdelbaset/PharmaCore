"use client"

import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table"
import { Badge } from "@/components/ui/badge"
import { Button } from "@/components/ui/button"
import { Check, X } from "lucide-react"
import { PasswordRequest, PasswordRequestStatus } from "@/types/password-request"
import { formatDistanceToNow } from "date-fns"

interface PasswordRequestTableProps {
    requests: PasswordRequest[]
    onApprove: (request: PasswordRequest) => void
    onReject: (request: PasswordRequest) => void
}

const statusColors: Record<PasswordRequestStatus, string> = {
    [PasswordRequestStatus.PENDING]: "bg-yellow-500/10 text-yellow-500",
    [PasswordRequestStatus.APPROVED]: "bg-green-500/10 text-green-500",
    [PasswordRequestStatus.REJECTED]: "bg-red-500/10 text-red-500",
}

export function PasswordRequestTable({ requests, onApprove, onReject }: PasswordRequestTableProps) {
    return (
        <div className="rounded-md border">
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead>Time</TableHead>
                        <TableHead>Target User</TableHead>
                        <TableHead>Requested By</TableHead>
                        <TableHead>Reason</TableHead>
                        <TableHead>Status</TableHead>
                        <TableHead className="text-right">Actions</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {requests.length === 0 ? (
                        <TableRow>
                            <TableCell colSpan={6} className="h-24 text-center">
                                No password requests found.
                            </TableCell>
                        </TableRow>
                    ) : (
                        requests.map((request) => (
                            <TableRow key={request.id}>
                                <TableCell className="text-sm text-muted-foreground">
                                    {formatDistanceToNow(new Date(request.createdAt), { addSuffix: true })}
                                </TableCell>
                                <TableCell>
                                    <div>
                                        <div className="font-medium">{request.targetUserName}</div>
                                        <div className="text-sm text-muted-foreground">{request.targetUserCode}</div>
                                    </div>
                                </TableCell>
                                <TableCell>
                                    <div>
                                        <div className="font-medium">{request.requestedByName}</div>
                                        <div className="text-sm text-muted-foreground">{request.requestedByCode}</div>
                                    </div>
                                </TableCell>
                                <TableCell className="max-w-md">
                                    <p className="truncate">{request.reason}</p>
                                </TableCell>
                                <TableCell>
                                    <Badge variant="outline" className={statusColors[request.status]}>
                                        {request.status}
                                    </Badge>
                                </TableCell>
                                <TableCell className="text-right">
                                    {request.status === PasswordRequestStatus.PENDING ? (
                                        <div className="flex items-center justify-end gap-2">
                                            <Button
                                                variant="ghost"
                                                size="sm"
                                                onClick={() => onApprove(request)}
                                                className="text-green-600 hover:text-green-700 hover:bg-green-50"
                                            >
                                                <Check className="h-4 w-4 mr-1" />
                                                Approve
                                            </Button>
                                            <Button
                                                variant="ghost"
                                                size="sm"
                                                onClick={() => onReject(request)}
                                                className="text-red-600 hover:text-red-700 hover:bg-red-50"
                                            >
                                                <X className="h-4 w-4 mr-1" />
                                                Reject
                                            </Button>
                                        </div>
                                    ) : (
                                        <div className="text-sm text-muted-foreground">
                                            {request.reviewedByName && (
                                                <div>
                                                    By {request.reviewedByName}
                                                </div>
                                            )}
                                        </div>
                                    )}
                                </TableCell>
                            </TableRow>
                        ))
                    )}
                </TableBody>
            </Table>
        </div>
    )
}
