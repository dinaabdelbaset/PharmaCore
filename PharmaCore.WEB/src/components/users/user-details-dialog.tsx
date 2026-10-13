"use client"

import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
} from "@/components/ui/dialog"
import { Badge } from "@/components/ui/badge"
import {
    Phone,
    Mail,
    Calendar,
    Award,
    Building,
    MapPin,
    User as UserIcon,
    Briefcase,
    GraduationCap,
    Heart,
} from "lucide-react"
import { User, UserRole, UserStatus } from "@/types/user"
import { formatDistanceToNow } from "date-fns"

interface UserDetailsDialogProps {
    open: boolean
    onOpenChange: (open: boolean) => void
    user: User | null
}

const roleColors: Record<UserRole, string> = {
    [UserRole.ORG_OWNER]: "bg-purple-500/10 text-purple-500",
    [UserRole.ORG_ADMIN]: "bg-blue-500/10 text-blue-500",
    [UserRole.HR]: "bg-green-500/10 text-green-500",
    [UserRole.MANAGER]: "bg-orange-500/10 text-orange-500",
    [UserRole.SUPERVISOR]: "bg-yellow-500/10 text-yellow-500",
    [UserRole.EMPLOYEE]: "bg-gray-500/10 text-gray-500",
}

const statusColors: Record<UserStatus, string> = {
    [UserStatus.ACTIVE]: "bg-green-500/10 text-green-500",
    [UserStatus.INACTIVE]: "bg-red-500/10 text-red-500",
    [UserStatus.PENDING]: "bg-yellow-500/10 text-yellow-500",
}

export function UserDetailsDialog({
    open,
    onOpenChange,
    user,
}: UserDetailsDialogProps) {
    if (!user) return null

    return (
        <Dialog open={open} onOpenChange={onOpenChange}>
            <DialogContent className="max-w-4xl max-h-[90vh] overflow-y-auto">
                <DialogHeader>
                    <div className="flex items-start justify-between">
                        <div>
                            <DialogTitle className="text-2xl flex items-center gap-3">
                                {user.name}
                                <Badge variant="outline" className={roleColors[user.role]}>
                                    {user.role.replace("_", " ")}
                                </Badge>
                                <Badge variant="outline" className={statusColors[user.status]}>
                                    {user.status}
                                </Badge>
                            </DialogTitle>
                            {user.nameAr && (
                                <p className="text-lg text-muted-foreground mt-1" dir="rtl">
                                    {user.nameAr}
                                </p>
                            )}
                            <DialogDescription className="mt-2">
                                Employee Code: <span className="font-mono font-semibold">{user.code}</span>
                            </DialogDescription>
                        </div>
                    </div>
                </DialogHeader>

                <div className="grid gap-6 mt-6">
                    {/* Personal Information */}
                    <div className="space-y-3">
                        <h3 className="font-semibold text-lg flex items-center gap-2">
                            <UserIcon className="h-5 w-5 text-primary" />
                            Personal Information
                        </h3>
                        <div className="bg-muted/50 rounded-lg p-4 space-y-3">
                            <div className="grid grid-cols-2 gap-4">
                                <div>
                                    <p className="text-sm font-medium">Email</p>
                                    <div className="flex items-center gap-2">
                                        <Mail className="h-3.5 w-3.5 text-muted-foreground" />
                                        <p className="text-sm text-muted-foreground">{user.email}</p>
                                    </div>
                                </div>
                                {user.dateOfBirth && (
                                    <div>
                                        <p className="text-sm font-medium">Date of Birth</p>
                                        <div className="flex items-center gap-2">
                                            <Calendar className="h-3.5 w-3.5 text-muted-foreground" />
                                            <p className="text-sm text-muted-foreground">
                                                {new Date(user.dateOfBirth).toLocaleDateString()}
                                            </p>
                                        </div>
                                    </div>
                                )}
                            </div>
                            {user.nationalId && (
                                <div>
                                    <p className="text-sm font-medium">National ID</p>
                                    <p className="text-sm text-muted-foreground font-mono">{user.nationalId}</p>
                                </div>
                            )}
                            {user.address && (
                                <div>
                                    <p className="text-sm font-medium">Address</p>
                                    <div className="flex items-center gap-2">
                                        <MapPin className="h-3.5 w-3.5 text-muted-foreground" />
                                        <p className="text-sm text-muted-foreground">{user.address}</p>
                                        {user.city && <span className="text-sm text-muted-foreground">• {user.city}</span>}
                                    </div>
                                </div>
                            )}
                            {user.bio && (
                                <div>
                                    <p className="text-sm font-medium mb-1">Bio</p>
                                    <p className="text-sm text-muted-foreground">{user.bio}</p>
                                </div>
                            )}
                            {user.bioAr && (
                                <div>
                                    <p className="text-sm font-medium mb-1">Arabic Bio</p>
                                    <p className="text-sm text-muted-foreground" dir="rtl">{user.bioAr}</p>
                                </div>
                            )}
                        </div>
                    </div>

                    {/* Contact Information */}
                    <div className="space-y-3">
                        <h3 className="font-semibold text-lg flex items-center gap-2">
                            <Phone className="h-5 w-5 text-primary" />
                            Contact Information
                        </h3>
                        <div className="bg-muted/50 rounded-lg p-4 space-y-3">
                            <div>
                                <p className="text-sm font-medium mb-1">Phone Numbers</p>
                                <div className="space-y-1">
                                    {user.phones.map((phone, idx) => (
                                        <div key={idx} className="flex items-center gap-2">
                                            <Phone className="h-3.5 w-3.5 text-muted-foreground" />
                                            <p className="text-sm text-muted-foreground">{phone}</p>
                                        </div>
                                    ))}
                                </div>
                            </div>
                            {user.emergencyContact && (
                                <div className="border-t pt-3">
                                    <p className="text-sm font-medium mb-2 flex items-center gap-1.5">
                                        <Heart className="h-4 w-4 text-red-500" />
                                        Emergency Contact
                                    </p>
                                    <div className="space-y-1">
                                        <p className="text-sm"><span className="font-medium">Name:</span> {user.emergencyContact.name}</p>
                                        <p className="text-sm"><span className="font-medium">Phone:</span> {user.emergencyContact.phone}</p>
                                        <p className="text-sm"><span className="font-medium">Relation:</span> {user.emergencyContact.relation}</p>
                                    </div>
                                </div>
                            )}
                        </div>
                    </div>

                    {/* Employment Details */}
                    <div className="space-y-3">
                        <h3 className="font-semibold text-lg flex items-center gap-2">
                            <Briefcase className="h-5 w-5 text-primary" />
                            Employment Details
                        </h3>
                        <div className="bg-muted/50 rounded-lg p-4 space-y-3">
                            <div className="grid grid-cols-2 gap-4">
                                {user.position && (
                                    <div>
                                        <p className="text-sm font-medium">Position</p>
                                        <p className="text-sm text-muted-foreground">{user.position}</p>
                                    </div>
                                )}
                                {user.department && (
                                    <div>
                                        <p className="text-sm font-medium">Department</p>
                                        <p className="text-sm text-muted-foreground">{user.department}</p>
                                    </div>
                                )}
                            </div>
                            {user.branchName && (
                                <div>
                                    <p className="text-sm font-medium">Branch</p>
                                    <div className="flex items-center gap-2">
                                        <Building className="h-3.5 w-3.5 text-muted-foreground" />
                                        <p className="text-sm text-muted-foreground">{user.branchName}</p>
                                    </div>
                                </div>
                            )}
                            {user.hireDate && (
                                <div>
                                    <p className="text-sm font-medium">Hire Date</p>
                                    <div className="flex items-center gap-2">
                                        <Calendar className="h-3.5 w-3.5 text-muted-foreground" />
                                        <p className="text-sm text-muted-foreground">
                                            {new Date(user.hireDate).toLocaleDateString()} (
                                            {formatDistanceToNow(new Date(user.hireDate), { addSuffix: true })})
                                        </p>
                                    </div>
                                </div>
                            )}
                            {user.specializations && user.specializations.length > 0 && (
                                <div>
                                    <p className="text-sm font-medium mb-2">Specializations</p>
                                    <div className="flex flex-wrap gap-2">
                                        {user.specializations.map((spec, idx) => (
                                            <Badge key={idx} variant="secondary">
                                                {spec}
                                            </Badge>
                                        ))}
                                    </div>
                                </div>
                            )}
                        </div>
                    </div>

                    {/* Education & Certificates */}
                    <div className="space-y-3">
                        <h3 className="font-semibold text-lg flex items-center gap-2">
                            <GraduationCap className="h-5 w-5 text-primary" />
                            Education & Certificates
                        </h3>
                        <div className="bg-muted/50 rounded-lg p-4 space-y-3">
                            {user.education && (
                                <div>
                                    <p className="text-sm font-medium">Education</p>
                                    <p className="text-sm text-muted-foreground">{user.education}</p>
                                </div>
                            )}
                            {user.certificates && user.certificates.length > 0 && (
                                <div>
                                    <p className="text-sm font-medium mb-3">Professional Certificates</p>
                                    <div className="grid gap-3">
                                        {user.certificates.map((cert) => (
                                            <div
                                                key={cert.id}
                                                className="border-2 border-primary/20 rounded-lg p-3 bg-gradient-to-br from-primary/5 to-transparent"
                                            >
                                                <div className="flex items-start gap-2">
                                                    <Award className="h-4 w-4 text-primary mt-0.5" />
                                                    <div className="flex-1">
                                                        <h4 className="font-semibold text-sm">{cert.name}</h4>
                                                        <p className="text-xs text-muted-foreground mt-1">
                                                            Issued by: {cert.issuedBy}
                                                        </p>
                                                        <div className="flex gap-3 text-xs text-muted-foreground mt-1">
                                                            <span>Issued: {new Date(cert.issuedDate).toLocaleDateString()}</span>
                                                            {cert.expiryDate && (
                                                                <span>Expires: {new Date(cert.expiryDate).toLocaleDateString()}</span>
                                                            )}
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        ))}
                                    </div>
                                </div>
                            )}
                        </div>
                    </div>

                    {/* Timestamps */}
                    <div className="text-xs text-muted-foreground border-t pt-4">
                        <p>Account Created: {formatDistanceToNow(new Date(user.createdAt), { addSuffix: true })}</p>
                        <p>Last Updated: {formatDistanceToNow(new Date(user.updatedAt), { addSuffix: true })}</p>
                    </div>
                </div>
            </DialogContent>
        </Dialog>
    )
}
