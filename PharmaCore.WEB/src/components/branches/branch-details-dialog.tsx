"use client"

import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
} from "@/components/ui/dialog"
import { Badge } from "@/components/ui/badge"
import { Button } from "@/components/ui/button"
import {
    MapPin,
    Phone,
    Mail,
    Calendar,
    Award,
    Building,
    User,
    Clock,
    X,
} from "lucide-react"
import { Branch, BranchStatus } from "@/types/branch"
import { formatDistanceToNow } from "date-fns"

interface BranchDetailsDialogProps {
    open: boolean
    onOpenChange: (open: boolean) => void
    branch: Branch | null
}

const statusColors: Record<BranchStatus, string> = {
    [BranchStatus.ACTIVE]: "bg-green-500/10 text-green-500",
    [BranchStatus.INACTIVE]: "bg-red-500/10 text-red-500",
    [BranchStatus.PENDING]: "bg-yellow-500/10 text-yellow-500",
}

export function BranchDetailsDialog({
    open,
    onOpenChange,
    branch,
}: BranchDetailsDialogProps) {
    if (!branch) return null

    return (
        <Dialog open={open} onOpenChange={onOpenChange}>
            <DialogContent className="max-w-4xl max-h-[90vh] overflow-y-auto">
                <DialogHeader>
                    <div className="flex items-start justify-between">
                        <div>
                            <DialogTitle className="text-2xl">
                                {branch.name}
                                <Badge variant="outline" className={`ml-3 ${statusColors[branch.status]}`}>
                                    {branch.status}
                                </Badge>
                            </DialogTitle>
                            {branch.nameAr && (
                                <p className="text-lg text-muted-foreground mt-1" dir="rtl">
                                    {branch.nameAr}
                                </p>
                            )}
                            <DialogDescription className="mt-2">
                                Branch Code: <span className="font-mono font-semibold">{branch.code}</span>
                            </DialogDescription>
                        </div>
                    </div>
                </DialogHeader>

                <div className="grid gap-6 mt-6">
                    {/* Location Info */}
                    <div className="space-y-3">
                        <h3 className="font-semibold text-lg flex items-center gap-2">
                            <MapPin className="h-5 w-5 text-primary" />
                            Location
                        </h3>
                        <div className="bg-muted/50 rounded-lg p-4 space-y-2">
                            <div>
                                <p className="text-sm font-medium">Address</p>
                                <p className="text-sm text-muted-foreground">{branch.address}</p>
                            </div>
                            <div>
                                <p className="text-sm font-medium">City</p>
                                <p className="text-sm text-muted-foreground">{branch.city}</p>
                            </div>
                            {(branch.latitude && branch.longitude) && (
                                <div className="mt-4">
                                    <p className="text-sm font-medium mb-2">Map Location</p>
                                    <div className="w-full h-48 bg-muted rounded-md flex items-center justify-center">
                                        <div className="text-center text-muted-foreground">
                                            <MapPin className="h-8 w-8 mx-auto mb-2" />
                                            <p className="text-sm">
                                                Coordinates: {branch.latitude}, {branch.longitude}
                                            </p>
                                            <p className="text-xs mt-1">
                                                Map integration coming soon
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            )}
                        </div>
                    </div>

                    {/* Contact Info */}
                    <div className="space-y-3">
                        <h3 className="font-semibold text-lg flex items-center gap-2">
                            <Phone className="h-5 w-5 text-primary" />
                            Contact Information
                        </h3>
                        <div className="bg-muted/50 rounded-lg p-4 space-y-3">
                            <div>
                                <p className="text-sm font-medium mb-1">Phone Numbers</p>
                                <div className="space-y-1">
                                    {branch.phones.map((phone, idx) => (
                                        <div key={idx} className="flex items-center gap-2">
                                            <Phone className="h-3.5 w-3.5 text-muted-foreground" />
                                            <p className="text-sm text-muted-foreground">{phone}</p>
                                        </div>
                                    ))}
                                </div>
                            </div>
                            {branch.email && (
                                <div>
                                    <p className="text-sm font-medium mb-1">Email</p>
                                    <div className="flex items-center gap-2">
                                        <Mail className="h-3.5 w-3.5 text-muted-foreground" />
                                        <p className="text-sm text-muted-foreground">{branch.email}</p>
                                    </div>
                                </div>
                            )}
                        </div>
                    </div>

                    {/* Manager Info */}
                    {branch.manager && (
                        <div className="space-y-3">
                            <h3 className="font-semibold text-lg flex items-center gap-2">
                                <User className="h-5 w-5 text-primary" />
                                Branch Manager
                            </h3>
                            <div className="bg-muted/50 rounded-lg p-4 space-y-2">
                                <div>
                                    <p className="text-sm font-medium">Name</p>
                                    <p className="text-sm text-muted-foreground">{branch.manager.name}</p>
                                </div>
                                <div>
                                    <p className="text-sm font-medium">Phone</p>
                                    <p className="text-sm text-muted-foreground">{branch.manager.phone}</p>
                                </div>
                                {branch.manager.email && (
                                    <div>
                                        <p className="text-sm font-medium">Email</p>
                                        <p className="text-sm text-muted-foreground">{branch.manager.email}</p>
                                    </div>
                                )}
                            </div>
                        </div>
                    )}

                    {/* Quality Certificates */}
                    {branch.qualityCertificates && branch.qualityCertificates.length > 0 && (
                        <div className="space-y-3">
                            <h3 className="font-semibold text-lg flex items-center gap-2">
                                <Award className="h-5 w-5 text-primary" />
                                Quality Certificates
                            </h3>
                            <div className="grid gap-3">
                                {branch.qualityCertificates.map((cert) => (
                                    <div
                                        key={cert.id}
                                        className="border-2 border-primary/20 rounded-lg p-4 bg-gradient-to-br from-primary/5 to-transparent"
                                    >
                                        <div className="flex items-start justify-between">
                                            <div className="space-y-2 flex-1">
                                                <div className="flex items-center gap-2">
                                                    <Award className="h-5 w-5 text-primary" />
                                                    <h4 className="font-semibold">{cert.name}</h4>
                                                </div>
                                                <p className="text-sm text-muted-foreground">
                                                    Issued by: {cert.issuedBy}
                                                </p>
                                                <div className="flex gap-4 text-xs text-muted-foreground">
                                                    <span>Issued: {new Date(cert.issuedDate).toLocaleDateString()}</span>
                                                    {cert.expiryDate && (
                                                        <span>Expires: {new Date(cert.expiryDate).toLocaleDateString()}</span>
                                                    )}
                                                </div>
                                            </div>
                                            {cert.certificateUrl && (
                                                <Button variant="outline" size="sm">
                                                    View
                                                </Button>
                                            )}
                                        </div>
                                    </div>
                                ))}
                            </div>
                        </div>
                    )}

                    {/* Additional Info */}
                    <div className="space-y-3">
                        <h3 className="font-semibold text-lg flex items-center gap-2">
                            <Building className="h-5 w-5 text-primary" />
                            Additional Information
                        </h3>
                        <div className="bg-mut/50 rounded-lg p-4 space-y-3">
                            {branch.description && (
                                <div>
                                    <p className="text-sm font-medium mb-1">Description</p>
                                    <p className="text-sm text-muted-foreground">{branch.description}</p>
                                </div>
                            )}
                            {branch.descriptionAr && (
                                <div>
                                    <p className="text-sm font-medium mb-1">Arabic Description</p>
                                    <p className="text-sm text-muted-foreground" dir="rtl">
                                        {branch.descriptionAr}
                                    </p>
                                </div>
                            )}
                            {branch.openingHours && (
                                <div>
                                    <p className="text-sm font-medium mb-1">Opening Hours</p>
                                    <div className="flex items-center gap-2">
                                        <Clock className="h-3.5 w-3.5 text-muted-foreground" />
                                        <p className="text-sm text-muted-foreground">{branch.openingHours}</p>
                                    </div>
                                </div>
                            )}
                            {branch.establishedDate && (
                                <div>
                                    <p className="text-sm font-medium mb-1">Established</p>
                                    <div className="flex items-center gap-2">
                                        <Calendar className="h-3.5 w-3.5 text-muted-foreground" />
                                        <p className="text-sm text-muted-foreground">
                                            {new Date(branch.establishedDate).toLocaleDateString()} (
                                            {formatDistanceToNow(new Date(branch.establishedDate), { addSuffix: true })})
                                        </p>
                                    </div>
                                </div>
                            )}
                            {branch.totalArea && (
                                <div>
                                    <p className="text-sm font-medium mb-1">Total Area</p>
                                    <p className="text-sm text-muted-foreground">{branch.totalArea} m²</p>
                                </div>
                            )}
                            <div>
                                <p className="text-sm font-medium mb-1">Employees</p>
                                <p className="text-sm text-muted-foreground">
                                    {branch.employeeCount || 0} employees
                                </p>
                            </div>
                        </div>
                    </div>

                    {/* Timestamps */}
                    <div className="text-xs text-muted-foreground border-t pt-4">
                        <p>Created: {formatDistanceToNow(new Date(branch.createdAt), { addSuffix: true })}</p>
                        <p>Last Updated: {formatDistanceToNow(new Date(branch.updatedAt), { addSuffix: true })}</p>
                    </div>
                </div>
            </DialogContent>
        </Dialog>
    )
}
