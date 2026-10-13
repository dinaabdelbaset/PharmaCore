"use client"

import { useState, useEffect } from "react"
import { Search, Smartphone, Loader2, MoreVertical } from "lucide-react"
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
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { Device, DeviceStatus } from "@/types/device"
import { format, formatDistanceToNow } from "date-fns"
import apiClient from "@/lib/api-client"
import { toast } from "sonner"

const statusColors: Record<DeviceStatus, string> = {
    [DeviceStatus.ACTIVE]: "bg-green-500/10 text-green-500 hover:bg-green-500/20",
    [DeviceStatus.INACTIVE]: "bg-gray-500/10 text-gray-500 hover:bg-gray-500/20",
    [DeviceStatus.PENDING]: "bg-yellow-500/10 text-yellow-500 hover:bg-yellow-500/20",
    [DeviceStatus.BLOCKED]: "bg-red-500/10 text-red-500 hover:bg-red-500/20",
}

export default function DevicesPage() {
    const [devices, setDevices] = useState<Device[]>([])
    const [isLoading, setIsLoading] = useState(true)
    const [updatingDeviceId, setUpdatingDeviceId] = useState<string | null>(null)
    const [searchTerm, setSearchTerm] = useState("")
    const [statusFilter, setStatusFilter] = useState<string>("all")

    const fetchDevices = async () => {
        setIsLoading(true)
        try {
            const response = await apiClient.get<Device[]>("/devices")
            const data = response.data
            if (Array.isArray(data)) {
                setDevices(data)
            } else if (data && typeof data === "object" && Array.isArray((data as any).devices)) {
                setDevices((data as any).devices)
            } else if (data && typeof data === "object" && Array.isArray((data as any).data)) {
                setDevices((data as any).data)
            } else {
                setDevices([])
            }
        } catch (error: any) {
            console.error("Failed to fetch devices", error)
            const errorMsg = error.response?.data?.message || error.message || "Failed to load devices"
            toast.error(errorMsg)
        } finally {
            setIsLoading(false)
        }
    }

    useEffect(() => {
        fetchDevices()
    }, [])

    const handleToggleStatus = async (id: string) => {
        setUpdatingDeviceId(id)
        try {
            await apiClient.put(`/devices/${id}/toggle-status`)
            toast.success("Device status updated successfully")
            await fetchDevices()
        } catch (error: any) {
            console.error("Failed to toggle status", error)
            const errorMsg = error.response?.data?.message || error.message || "Failed to update device status"
            toast.error(errorMsg)
        } finally {
            setUpdatingDeviceId(null)
        }
    }

    const filteredDevices = devices.filter((device) => {
        const matchesSearch =
            device.deviceName.toLowerCase().includes(searchTerm.toLowerCase()) ||
            device.deviceId.toLowerCase().includes(searchTerm.toLowerCase()) ||
            (device.userName && device.userName.toLowerCase().includes(searchTerm.toLowerCase()))
        const matchesStatus = statusFilter === "all" || device.status === statusFilter
        return matchesSearch && matchesStatus
    })

    return (
        <div className="space-y-6">
            <div className="flex items-center justify-between">
                <div>
                    <h1 className="text-3xl font-bold tracking-tight">Device Control</h1>
                    <p className="text-muted-foreground">
                        Manage authorized devices and access control
                    </p>
                </div>
                <div className="flex items-center gap-2 text-sm text-muted-foreground">
                    <Smartphone className="h-4 w-4" />
                    <span>{devices.length} Devices</span>
                </div>
            </div>

            <div className="flex items-center gap-4">
                <div className="relative flex-1">
                    <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                    <Input
                        placeholder="Search by device name, ID, or user..."
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
                        <SelectItem value={DeviceStatus.ACTIVE}>Active</SelectItem>
                        <SelectItem value={DeviceStatus.INACTIVE}>Inactive</SelectItem>
                        <SelectItem value={DeviceStatus.PENDING}>Pending</SelectItem>
                        <SelectItem value={DeviceStatus.BLOCKED}>Blocked</SelectItem>
                    </SelectContent>
                </Select>
            </div>

            <div className="rounded-md border bg-card text-card-foreground">
                <Table>
                    <TableHeader>
                        <TableRow>
                            <TableHead>Device</TableHead>
                            <TableHead>User</TableHead>
                            <TableHead>Branch</TableHead>
                            <TableHead>Last Login</TableHead>
                            <TableHead>IP Address</TableHead>
                            <TableHead>Status</TableHead>
                            <TableHead className="text-right">Actions</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {isLoading ? (
                            <TableRow>
                                <TableCell colSpan={7} className="h-24 text-center">
                                    <div className="flex items-center justify-center gap-2 text-muted-foreground">
                                        <Loader2 className="h-4 w-4 animate-spin text-primary" />
                                        <span>Loading devices...</span>
                                    </div>
                                </TableCell>
                            </TableRow>
                        ) : filteredDevices.length === 0 ? (
                            <TableRow>
                                <TableCell colSpan={7} className="h-24 text-center">
                                    No devices found.
                                </TableCell>
                            </TableRow>
                        ) : (
                            filteredDevices.map((device) => (
                                <TableRow key={device.id}>
                                    <TableCell>
                                        <div>
                                            <div className="font-medium">{device.deviceName}</div>
                                            <div className="text-sm text-muted-foreground font-mono">
                                                {device.deviceId}
                                            </div>
                                            {device.deviceModel && (
                                                <div className="text-xs text-muted-foreground">
                                                    {device.deviceModel}
                                                </div>
                                            )}
                                        </div>
                                    </TableCell>
                                    <TableCell>{device.userName || "—"}</TableCell>
                                    <TableCell>{device.branchName || "—"}</TableCell>
                                    <TableCell className="text-sm text-muted-foreground">
                                        {device.lastLogin ? (
                                            <div className="flex flex-col">
                                                <span>
                                                    {format(new Date(device.lastLogin), "yyyy-MM-dd HH:mm")}
                                                </span>
                                                <span className="text-xs opacity-80">
                                                    {formatDistanceToNow(new Date(device.lastLogin), {
                                                        addSuffix: true,
                                                    })}
                                                </span>
                                            </div>
                                        ) : (
                                            "Never"
                                        )}
                                    </TableCell>
                                    <TableCell className="font-mono text-sm">
                                        {device.ipAddress || "—"}
                                    </TableCell>
                                    <TableCell>
                                        <Badge variant="outline" className={statusColors[device.status]}>
                                            {device.status}
                                        </Badge>
                                    </TableCell>
                                    <TableCell className="text-right">
                                        <DropdownMenu>
                                            <DropdownMenuTrigger asChild>
                                                <Button
                                                    variant="ghost"
                                                    size="sm"
                                                    disabled={updatingDeviceId === device.id}
                                                >
                                                    {updatingDeviceId === device.id ? (
                                                        <Loader2 className="h-4 w-4 animate-spin text-muted-foreground" />
                                                    ) : (
                                                        <MoreVertical className="h-4 w-4" />
                                                    )}
                                                </Button>
                                            </DropdownMenuTrigger>
                                            <DropdownMenuContent align="end">
                                                {device.status === DeviceStatus.ACTIVE ? (
                                                    <DropdownMenuItem
                                                        className="text-destructive focus:text-destructive cursor-pointer"
                                                        onClick={() => handleToggleStatus(device.id)}
                                                    >
                                                        Block/Deactivate
                                                    </DropdownMenuItem>
                                                ) : (
                                                    <DropdownMenuItem
                                                        className="text-green-600 dark:text-green-400 focus:text-green-600 focus:dark:text-green-400 cursor-pointer"
                                                        onClick={() => handleToggleStatus(device.id)}
                                                    >
                                                        Activate/Authorize
                                                    </DropdownMenuItem>
                                                )}
                                            </DropdownMenuContent>
                                        </DropdownMenu>
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
