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
import { Edit, Key, Trash2, UserCheck, UserX } from "lucide-react"
import { Employee } from "@/services/employee-service"

interface UserTableProps {
    users: Employee[]
    onEdit: (user: Employee) => void
    onResetPin: (user: Employee) => void
    onDelete: (user: Employee) => void
}

export function UserTable({
    users,
    onEdit,
    onResetPin,
    onDelete,
}: UserTableProps) {
    return (
        <div className="rounded-md border" dir="rtl">
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead className="text-right">كود الموظف</TableHead>
                        <TableHead className="text-right">الاسم بالكامل</TableHead>
                        <TableHead className="text-right">المسمى الوظيفي</TableHead>
                        <TableHead className="text-right">الرتبة في النظام</TableHead>
                        <TableHead className="text-right">الحالة</TableHead>
                        <TableHead className="text-right">الرقم السري</TableHead>
                        <TableHead className="text-left">الإجراءات</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {users.length === 0 ? (
                        <TableRow>
                            <TableCell colSpan={7} className="h-24 text-center">
                                لا يوجد موظفين مسجلين حالياً.
                            </TableCell>
                        </TableRow>
                    ) : (
                        users.map((user) => (
                            <TableRow key={user.id}>
                                <TableCell className="font-mono font-semibold text-primary">
                                    {user.employeeCode}
                                </TableCell>
                                <TableCell className="font-medium">
                                    {user.fullName}
                                </TableCell>
                                <TableCell>
                                    {user.jobTitle || "—"}
                                    {user.grade && <span className="text-xs text-muted-foreground me-1">({user.grade})</span>}
                                </TableCell>
                                <TableCell>
                                    <Badge variant="outline" className="bg-primary/5 text-primary border-primary/20">
                                        {user.role}
                                    </Badge>
                                </TableCell>
                                <TableCell>
                                    {user.isActive ? (
                                        <Badge variant="outline" className="bg-green-500/10 text-green-500 hover:bg-green-500/20 border-green-500/20 gap-1">
                                            <UserCheck className="h-3 w-3" /> نشط
                                        </Badge>
                                    ) : (
                                        <Badge variant="outline" className="bg-red-500/10 text-red-500 hover:bg-red-500/20 border-red-500/20 gap-1">
                                            <UserX className="h-3 w-3" /> معطل
                                        </Badge>
                                    )}
                                </TableCell>
                                <TableCell>
                                    <Button
                                        variant="ghost"
                                        size="sm"
                                        onClick={() => onResetPin(user)}
                                        className="h-8 gap-1 text-xs"
                                        title="تغيير الرقم السري"
                                    >
                                        <Key className="h-3.5 w-3.5" /> إعادة تعيين
                                    </Button>
                                </TableCell>
                                <TableCell className="text-left">
                                    <div className="flex items-center justify-start gap-1">
                                        <Button
                                            variant="ghost"
                                            size="icon"
                                            onClick={() => onEdit(user)}
                                            className="h-8 w-8"
                                            title="تعديل"
                                        >
                                            <Edit className="h-4 w-4" />
                                        </Button>
                                        <Button
                                            variant="ghost"
                                            size="icon"
                                            onClick={() => onDelete(user)}
                                            className="h-8 w-8 text-destructive hover:bg-destructive/10"
                                            title="حذف"
                                        >
                                            <Trash2 className="h-4 w-4" />
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
