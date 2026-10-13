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
import { Edit, MapPin, Phone, FileText } from "lucide-react"
import { Branch, BranchStatus } from "@/types/branch"

interface BranchTableProps {
    branches: Branch[]
    onEdit: (branch: Branch) => void
    onViewDetails: (branch: Branch) => void
}

const statusColors: Record<BranchStatus, string> = {
    [BranchStatus.ACTIVE]: "bg-green-500/10 text-green-500 hover:bg-green-500/20",
    [BranchStatus.INACTIVE]: "bg-red-500/10 text-red-500 hover:bg-red-500/20",
    [BranchStatus.PENDING]: "bg-yellow-500/10 text-yellow-500 hover:bg-yellow-500/20",
}

export function BranchTable({ branches, onEdit, onViewDetails

}: BranchTableProps) {
    return (
        <div className="rounded-md border">
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead>Code</TableHead>
                        <TableHead>Name</TableHead>
                        <TableHead>Location</TableHead>
                        <TableHead>Contact</TableHead>
                        <TableHead>Manager</TableHead>
                        <TableHead>Employees</TableHead>
                        <TableHead>Status</TableHead>
                        <TableHead className="text-right">Actions</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {branches.length === 0 ? (
                        <TableRow>
                            <TableCell colSpan={8} className="h-24 text-center">
                                No branches found.
                            </TableCell>
                        </TableRow>
                    ) : (
                        branches.map((branch) => (
                            <TableRow key={branch.id}>
                                <TableCell className="font-mono font-semibold text-primary">
                                    {branch.code}
                                </TableCell>
                                <TableCell>
                                    <div>
                                        <div className="font-medium">{branch.name}</div>
                                        {branch.nameAr && (
                                            <div className="text-sm text-muted-foreground" dir="rtl">
                                                {branch.nameAr}
                                            </div>
                                        )}
                                    </div>
                                </TableCell>
                                <TableCell>
                                    <div className="flex items-start gap-1.5">
                                        <MapPin className="h-4 w-4 mt-0.5 text-muted-foreground" />
                                        <div>
                                            <div className="text-sm">{branch.city}</div>
                                            <div className="text-xs text-muted-foreground">{branch.address}</div>
                                        </div>
                                    </div>
                                </TableCell>
                                <TableCell>
                                    <div className="flex items-start gap-1.5">
                                        <Phone className="h-4 w-4 mt-0.5 text-muted-foreground" />
                                        <div>
                                            <div className="text-sm">{branch.phones[0]}</div>
                                            {branch.phones.length > 1 && (
                                                <div className="text-xs text-muted-foreground">
                                                    +{branch.phones.length - 1} more
                                                </div>
                                            )}
                                        </div>
                                    </div>
                                </TableCell>
                                <TableCell>
                                    {branch.manager?.name || <span className="text-muted-foreground">—</span>}
                                </TableCell>
                                <TableCell>
                                    <Badge variant="outline" className="font-mono">
                                        {branch.employeeCount || 0}
                                    </Badge>
                                </TableCell>
                                <TableCell>
                                    <Badge variant="outline" className={statusColors[branch.status]}>
                                        {branch.status}
                                    </Badge>
                                </TableCell>
                                <TableCell className="text-right">
                                    <div className="flex items-center justify-end gap-2">
                                        <Button
                                            variant="outline"
                                            size="sm"
                                            onClick={() => onViewDetails(branch)}
                                        >
                                            <FileText className="h-4 w-4 mr-1" />
                                            Details
                                        </Button>
                                        <Button variant="ghost" size="sm" onClick={() => onEdit(branch)}>
                                            <Edit className="h-4 w-4" />
                                        </Button>
                                    </div>
                                </TableCell>
                            </TableRow>
                        ))
                    )}
                </TableBody>
            </Table>
        </div>
    )
}
