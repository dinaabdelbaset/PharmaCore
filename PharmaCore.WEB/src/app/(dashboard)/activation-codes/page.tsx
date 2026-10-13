"use client"

import { useState } from "react"
import { Search, Copy, Check } from "lucide-react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Badge } from "@/components/ui/badge"
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import { ActivationCode, ActivationCodeStatus } from "@/types/activation-code"
import { formatDistanceToNow } from "date-fns"
import { toast } from "sonner"

const mockCodes: ActivationCode[] = [
    {
        id: "1",
        code: "AC-" + Math.random().toString(36).substring(2, 10).toUpperCase(),
        userId: "4",
        userName: "Nour Ibrahim",
        deviceId: "DEV-M3N4O5",
        deviceName: "Nour's iPhone",
        expiresAt: new Date(Date.now() + 1000 * 60 * 60 * 24 * 7).toISOString(),
        usedAt: new Date(Date.now() - 1000 * 60 * 60).toISOString(),
        status: ActivationCodeStatus.USED,
        createdById: "1",
        createdByName: "Ahmed Hassan",
        createdAt: new Date(Date.now() - 1000 * 60 * 60 * 2).toISOString(),
        updatedAt: new Date(Date.now() - 1000 * 60 * 60).toISOString(),
    },
    {
        id: "2",
        code: "AC-" + Math.random().toString(36).substring(2, 10).toUpperCase(),
        expiresAt: new Date(Date.now() + 1000 * 60 * 60 * 24 * 30).toISOString(),
        status: ActivationCodeStatus.ACTIVE,
        createdById: "2",
        createdByName: "Sara Mohamed",
        createdAt: new Date(Date.now() - 1000 * 60 * 30).toISOString(),
        updatedAt: new Date(Date.now() - 1000 * 60 * 30).toISOString(),
    },
]

const statusColors: Record<ActivationCodeStatus, string> = {
    [ActivationCodeStatus.ACTIVE]: "bg-green-500/10 text-green-500",
    [ActivationCodeStatus.USED]: "bg-blue-500/10 text-blue-500",
    [ActivationCodeStatus.EXPIRED]: "bg-gray-500/10 text-gray-500",
    [ActivationCodeStatus.REVOKED]: "bg-red-500/10 text-red-500",
}

export default function ActivationCodesPage() {
    const [codes] = useState<ActivationCode[]>(mockCodes)
    const [searchTerm, setSearchTerm] = useState("")
    const [statusFilter, setStatusFilter] = useState<string>("all")
    const [copiedId, setCopiedId] = useState<string | null>(null)

    const filteredCodes = codes.filter((code) => {
        const matchesSearch =
            code.code.toLowerCase().includes(searchTerm.toLowerCase()) ||
            (code.userName && code.userName.toLowerCase().includes(searchTerm.toLowerCase()))
        const matchesStatus = statusFilter === "all" || code.status === statusFilter
        return matchesSearch && matchesStatus
    })

    const handleCopyCode = (code: string, id: string) => {
        navigator.clipboard.writeText(code)
        setCopiedId(id)
        toast.success("Activation code copied to clipboard")
        setTimeout(() => setCopiedId(null), 2000)
    }

    return (
        <div className="space-y-6">
            <div className="flex items-center justify-between">
                <div>
                    <h1 className="text-3xl font-bold tracking-tight">Activation Codes</h1>
                    <p className="text-muted-foreground">
                        Generate and manage device activation codes
                    </p>
                </div>
                <Button>
                    <Copy className="mr-2 h-4 w-4" />
                    Generate Code
                </Button>
            </div>

            <div className="flex items-center gap-4">
                <div className="relative flex-1">
                    <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                    <Input
                        placeholder="Search by code or user..."
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        className="pl-10"
                    />
                </div>
                <Select value={statusFilter} onValueChange={setStatusFilter}>
                    <SelectTrigger className="w-[200px]">
                        <SelectValue placeholder="Filter by status" />
                    </SelectTrigger>
                    <SelectContent>
                        <SelectItem value="all">All Status</SelectItem>
                        <SelectItem value={ActivationCodeStatus.ACTIVE}>Active</SelectItem>
                        <SelectItem value={ActivationCodeStatus.USED}>Used</SelectItem>
                        <SelectItem value={ActivationCodeStatus.EXPIRED}>Expired</SelectItem>
                        <SelectItem value={ActivationCodeStatus.REVOKED}>Revoked</SelectItem>
                    </SelectContent>
                </Select>
            </div>

            <div className="rounded-md border">
                <Table>
                    <TableHeader>
                        <TableRow>
                            <TableHead>Code</TableHead>
                            <TableHead>User</TableHead>
                            <TableHead>Device</TableHead>
                            <TableHead>Created By</TableHead>
                            <TableHead>Expires</TableHead>
                            <TableHead>Status</TableHead>
                            <TableHead className="text-right">Actions</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {filteredCodes.length === 0 ? (
                            <TableRow>
                                <TableCell colSpan={7} className="h-24 text-center">
                                    No activation codes found.
                                </TableCell>
                            </TableRow>
                        ) : (
                            filteredCodes.map((code) => (
                                <TableRow key={code.id}>
                                    <TableCell className="font-mono font-semibold text-primary">
                                        {code.code}
                                    </TableCell>
                                    <TableCell>{code.userName || "—"}</TableCell>
                                    <TableCell>
                                        {code.deviceName ? (
                                            <div>
                                                <div className="text-sm">{code.deviceName}</div>
                                                <div className="text-xs text-muted-foreground font-mono">
                                                    {code.deviceId}
                                                </div>
                                            </div>
                                        ) : (
                                            "—"
                                        )}
                                    </TableCell>
                                    <TableCell className="text-sm">{code.createdByName}</TableCell>
                                    <TableCell className="text-sm text-muted-foreground">
                                        {formatDistanceToNow(new Date(code.expiresAt), {
                                            addSuffix: true,
                                        })}
                                    </TableCell>
                                    <TableCell>
                                        <Badge variant="outline" className={statusColors[code.status]}>
                                            {code.status}
                                        </Badge>
                                    </TableCell>
                                    <TableCell className="text-right">
                                        <Button
                                            variant="ghost"
                                            size="sm"
                                            onClick={() => handleCopyCode(code.code, code.id)}
                                        >
                                            {copiedId === code.id ? (
                                                <Check className="h-4 w-4 text-green-500" />
                                            ) : (
                                                <Copy className="h-4 w-4" />
                                            )}
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            ))
                        )}
                    </TableBody>
                </Table>
            </div>
        </div>
    )
}
