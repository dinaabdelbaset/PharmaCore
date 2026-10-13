"use client"

import { useState, useEffect } from "react"
import { Plus, Search, Loader2 } from "lucide-react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import { BranchTable } from "@/components/branches/branch-table"
import { BranchDialog } from "@/components/branches/branch-dialog"
import { BranchDetailsDialog } from "@/components/branches/branch-details-dialog"
import { Branch, BranchStatus } from "@/types/branch"
import apiClient from "@/lib/api-client"
import { toast } from "sonner"

export default function BranchesPage() {
    const [branches, setBranches] = useState<Branch[]>([])
    const [isLoading, setIsLoading] = useState(true)
    const [isSaving, setIsSaving] = useState(false)
    const [searchTerm, setSearchTerm] = useState("")
    const [statusFilter, setStatusFilter] = useState<string>("all")
    const [isBranchDialogOpen, setIsBranchDialogOpen] = useState(false)
    const [isBranchDetailsOpen, setIsBranchDetailsOpen] = useState(false)
    const [selectedBranch, setSelectedBranch] = useState<Branch | null>(null)

    const fetchBranches = async () => {
        setIsLoading(true)
        try {
            const response = await apiClient.get<Branch[]>("/branches")
            if (Array.isArray(response.data)) {
                setBranches(response.data)
            } else {
                setBranches([])
            }
        } catch (error: any) {
            console.error("Failed to fetch branches", error)
            const errorMsg = error.response?.data?.message || error.message || "Failed to load branches"
            toast.error(errorMsg)
        } finally {
            setIsLoading(false)
        }
    }

    useEffect(() => {
        fetchBranches()
    }, [])

    const filteredBranches = branches.filter((branch) => {
        const matchesSearch =
            branch.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
            branch.code.toLowerCase().includes(searchTerm.toLowerCase()) ||
            branch.city.toLowerCase().includes(searchTerm.toLowerCase()) ||
            (branch.nameAr && branch.nameAr.includes(searchTerm))
        const matchesStatus = statusFilter === "all" || branch.status === statusFilter
        return matchesSearch && matchesStatus
    })

    const handleAddBranch = () => {
        setSelectedBranch(null)
        setIsBranchDialogOpen(true)
    }

    const handleEditBranch = (branch: Branch) => {
        setSelectedBranch(branch)
        setIsBranchDialogOpen(true)
    }

    const handleViewDetails = (branch: Branch) => {
        setSelectedBranch(branch)
        setIsBranchDetailsOpen(true)
    }

    const handleSaveBranch = async (data: any) => {
        setIsSaving(true)
        try {
            if (selectedBranch) {
                // Update
                await apiClient.put(`/branches/${selectedBranch.id}`, data)
                toast.success("Branch updated successfully")
            } else {
                // Create
                await apiClient.post("/branches", data)
                toast.success("Branch created successfully")
            }
            setIsBranchDialogOpen(false)
            await fetchBranches()
        } catch (error: any) {
            console.error("Failed to save branch", error)
            const errorMsg = error.response?.data || error.message || "Failed to save branch"
            // If errorMsg is string
            if (typeof errorMsg === "string") {
                toast.error(errorMsg)
            } else if (errorMsg.message) {
                toast.error(errorMsg.message)
            } else {
                toast.error("Failed to save branch details.")
            }
        } finally {
            setIsSaving(false)
        }
    }

    return (
        <div className="space-y-6">
            <div className="flex items-center justify-between">
                <div>
                    <h1 className="text-3xl font-bold tracking-tight">Branch Management</h1>
                    <p className="text-muted-foreground">
                        Manage your organization branches and locations
                    </p>
                </div>
                <Button onClick={handleAddBranch} disabled={isLoading || isSaving}>
                    <Plus className="mr-2 h-4 w-4" />
                    Add Branch
                </Button>
            </div>

            <div className="flex items-center gap-4">
                <div className="relative flex-1">
                    <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                    <Input
                        placeholder="Search by code, name, or city..."
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        className="pl-10"
                        disabled={isLoading}
                    />
                </div>
                <Select value={statusFilter} onValueChange={setStatusFilter} disabled={isLoading}>
                    <SelectTrigger className="w-[200px]">
                        <SelectValue placeholder="Filter by status" />
                    </SelectTrigger>
                    <SelectContent>
                        <SelectItem value="all">All Status</SelectItem>
                        <SelectItem value={BranchStatus.ACTIVE}>Active</SelectItem>
                        <SelectItem value={BranchStatus.INACTIVE}>Inactive</SelectItem>
                        <SelectItem value={BranchStatus.PENDING}>Pending</SelectItem>
                    </SelectContent>
                </Select>
            </div>

            {isLoading ? (
                <div className="h-64 flex flex-col items-center justify-center gap-2 text-muted-foreground">
                    <Loader2 className="h-8 w-8 animate-spin text-primary" />
                    <span>Loading branches...</span>
                </div>
            ) : (
                <BranchTable
                    branches={filteredBranches}
                    onEdit={handleEditBranch}
                    onViewDetails={handleViewDetails}
                />
            )}

            <BranchDialog
                open={isBranchDialogOpen}
                onOpenChange={setIsBranchDialogOpen}
                onSave={handleSaveBranch}
                branch={selectedBranch}
            />

            <BranchDetailsDialog
                open={isBranchDetailsOpen}
                onOpenChange={setIsBranchDetailsOpen}
                branch={selectedBranch}
            />
        </div>
    )
}
