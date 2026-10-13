"use client"

import { useState } from "react"
import { Filter } from "lucide-react"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import { PasswordRequestTable } from "@/components/password-requests/password-request-table"
import { PasswordRequest, PasswordRequestStatus } from "@/types/password-request"
import { UserRole } from "@/types/user"
import { toast } from "sonner"

// Mock data
const mockRequests: PasswordRequest[] = [
    {
        id: "1",
        targetUserId: "4",
        targetUserName: "Nour Ibrahim",
        targetUserCode: "EMP004",
        requestedById: "2",
        requestedByName: "Sara Mohamed",
        requestedByCode: "EMP002",
        reason: "Employee forgot password and cannot access system",
        status: PasswordRequestStatus.PENDING,
        createdAt: new Date(Date.now() - 1000 * 60 * 60 * 2).toISOString(), // 2 hours ago
        updatedAt: new Date(Date.now() - 1000 * 60 * 60 * 2).toISOString(),
    },
    {
        id: "2",
        targetUserId: "5",
        targetUserName: "Omar Abdel",
        targetUserCode: "EMP005",
        requestedById: "3",
        requestedByName: "Khaled Ali",
        requestedByCode: "EMP003",
        reason: "Account compromised - immediate password reset required",
        status: PasswordRequestStatus.PENDING,
        createdAt: new Date(Date.now() - 1000 * 60 * 30).toISOString(), // 30 mins ago
        updatedAt: new Date(Date.now() - 1000 * 60 * 30).toISOString(),
    },
    {
        id: "3",
        targetUserId: "3",
        targetUserName: "Khaled Ali",
        targetUserCode: "EMP003",
        requestedById: "1",
        requestedByName: "Ahmed Hassan",
        requestedByCode: "EMP001",
        reason: "Regular security update as per policy",
        status: PasswordRequestStatus.APPROVED,
        createdAt: new Date(Date.now() - 1000 * 60 * 60 * 24).toISOString(), // 1 day ago
        updatedAt: new Date(Date.now() - 1000 * 60 * 60 * 23).toISOString(),
        reviewedById: "1",
        reviewedByName: "Ahmed Hassan",
        reviewedAt: new Date(Date.now() - 1000 * 60 * 60 * 23).toISOString(),
        newPassword: "********",
    },
]

export default function PasswordRequestsPage() {
    const [statusFilter, setStatusFilter] = useState<string>("all")
    const [requests, setRequests] = useState<PasswordRequest[]>(mockRequests)

    const filteredRequests = requests.filter((request) => {
        return statusFilter === "all" || request.status === statusFilter
    })

    const handleApprove = (request: PasswordRequest) => {
        // In real app, show dialog to enter new password
        setRequests(requests.map(r =>
            r.id === request.id
                ? {
                    ...r,
                    status: PasswordRequestStatus.APPROVED,
                    reviewedById: "1",
                    reviewedByName: "Current User",
                    reviewedAt: new Date().toISOString(),
                    updatedAt: new Date().toISOString(),
                }
                : r
        ))
        toast.success(`Password request approved for ${request.targetUserName}`)
    }

    const handleReject = (request: PasswordRequest) => {
        setRequests(requests.map(r =>
            r.id === request.id
                ? {
                    ...r,
                    status: PasswordRequestStatus.REJECTED,
                    reviewedById: "1",
                    reviewedByName: "Current User",
                    reviewedAt: new Date().toISOString(),
                    updatedAt: new Date().toISOString(),
                }
                : r
        ))
        toast.success(`Password request rejected for ${request.targetUserName}`)
    }

    const pendingCount = requests.filter(r => r.status === PasswordRequestStatus.PENDING).length

    return (
        <div className="space-y-6">
            <div className="flex items-center justify-between">
                <div>
                    <h1 className="text-3xl font-bold tracking-tight">Password Requests</h1>
                    <p className="text-muted-foreground">
                        Review and approve password change requests
                    </p>
                </div>
                {pendingCount > 0 && (
                    <div className="bg-amber-500/10 text-amber-600 px-4 py-2 rounded-lg font-semibold">
                        {pendingCount} Pending Request{pendingCount > 1 ? 's' : ''}
                    </div>
                )}
            </div>

            <div className="flex items-center gap-4">
                <div className="flex items-center gap-2">
                    <Filter className="h-4 w-4 text-muted-foreground" />
                    <Select value={statusFilter} onValueChange={setStatusFilter}>
                        <SelectTrigger className="w-[200px]">
                            <SelectValue placeholder="Filter by status" />
                        </SelectTrigger>
                        <SelectContent>
                            <SelectItem value="all">All Requests</SelectItem>
                            <SelectItem value={PasswordRequestStatus.PENDING}>
                                Pending
                            </SelectItem>
                            <SelectItem value={PasswordRequestStatus.APPROVED}>
                                Approved
                            </SelectItem>
                            <SelectItem value={PasswordRequestStatus.REJECTED}>
                                Rejected
                            </SelectItem>
                        </SelectContent>
                    </Select>
                </div>
            </div>

            <PasswordRequestTable
                requests={filteredRequests}
                onApprove={handleApprove}
                onReject={handleReject}
            />
        </div>
    )
}
