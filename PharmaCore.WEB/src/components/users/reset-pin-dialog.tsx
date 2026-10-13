"use client"

import { useState } from "react"
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
    DialogFooter,
} from "@/components/ui/dialog"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Employee, employeeService } from "@/services/employee-service"
import { toast } from "sonner"

interface ResetPinDialogProps {
    open: boolean
    onOpenChange: (open: boolean) => void
    employee: Employee | null
}

export function ResetPinDialog({
    open,
    onOpenChange,
    employee,
}: ResetPinDialogProps) {
    const [pin, setPin] = useState("")
    const [isLoading, setIsLoading] = useState(false)

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault()
        if (!employee) return

        if (!/^\d+$/.test(pin)) {
            toast.error("يجب أن يتكون الرقم السري من أرقام فقط")
            return
        }

        if (pin.length < 4) {
            toast.error("يجب أن يكون الرقم السري 4 أرقام على الأقل")
            return
        }

        setIsLoading(true)
        try {
            await employeeService.resetPin(employee.id, pin)
            toast.success("تم تغيير الرقم السري بنجاح")
            setPin("")
            onOpenChange(false)
        } catch (err: any) {
            toast.error(err.response?.data?.error || "فشل تغيير الرقم السري")
        } finally {
            setIsLoading(false)
        }
    }

    return (
        <Dialog open={open} onOpenChange={onOpenChange}>
            <DialogContent className="sm:max-w-[425px]">
                <form onSubmit={handleSubmit}>
                    <DialogHeader>
                        <DialogTitle>إعادة تعيين الرقم السري</DialogTitle>
                        <DialogDescription>
                            تغيير رمز الدخول الخاص بالموظف: {employee?.fullName}
                        </DialogDescription>
                    </DialogHeader>

                    <div className="grid gap-4 py-4">
                        <div className="flex flex-col gap-2">
                            <Label htmlFor="pin">الرقم السري الجديد (أرقام فقط)</Label>
                            <Input
                                id="pin"
                                type="password"
                                inputMode="numeric"
                                pattern="[0-9]*"
                                maxLength={8}
                                placeholder="أدخل الرقم السري الجديد"
                                value={pin}
                                onChange={(e) => setPin(e.target.value.replace(/\D/g, ""))}
                                required
                            />
                        </div>
                    </div>

                    <DialogFooter className="gap-2 sm:gap-0">
                        <Button
                            type="button"
                            variant="outline"
                            onClick={() => onOpenChange(false)}
                            disabled={isLoading}
                        >
                            إلغاء
                        </Button>
                        <Button type="submit" disabled={isLoading}>
                            {isLoading ? "جاري الحفظ..." : "حفظ التغييرات"}
                        </Button>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>
    )
}
