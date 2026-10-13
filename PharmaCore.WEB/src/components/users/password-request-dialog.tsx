"use client"

import { useForm } from "react-hook-form"
import { zodResolver } from "@hookform/resolvers/zod"
import * as z from "zod"
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
} from "@/components/ui/dialog"
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form"
import { Textarea } from "@/components/ui/textarea"
import { Button } from "@/components/ui/button"
import { User } from "@/types/user"

const requestSchema = z.object({
    reason: z.string().min(10, "Reason must be at least 10 characters"),
})

type RequestFormData = z.infer<typeof requestSchema>

interface PasswordRequestDialogProps {
    open: boolean
    onOpenChange: (open: boolean) => void
    onSubmit: (reason: string) => void
    user: User | null
}

export function PasswordRequestDialog({
    open,
    onOpenChange,
    onSubmit,
    user,
}: PasswordRequestDialogProps) {
    const form = useForm<RequestFormData>({
        resolver: zodResolver(requestSchema),
        defaultValues: {
            reason: "",
        },
    })

    function handleSubmit(data: RequestFormData) {
        onSubmit(data.reason)
        form.reset()
        onOpenChange(false)
    }

    return (
        <Dialog open={open} onOpenChange={onOpenChange}>
            <DialogContent className="sm:max-w-[500px]">
                <DialogHeader>
                    <DialogTitle>Request Password Change</DialogTitle>
                    <DialogDescription>
                        Request a password change for <span className="font-semibold">{user?.name}</span> ({user?.code}).
                        This request will be sent to users with higher roles for approval.
                    </DialogDescription>
                </DialogHeader>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(handleSubmit)} className="space-y-4">
                        <FormField
                            control={form.control}
                            name="reason"
                            render={({ field }) => (
                                <FormItem>
                                    <FormLabel>Reason for Password Change</FormLabel>
                                    <FormControl>
                                        <Textarea
                                            placeholder="Please provide a detailed reason for this password change request..."
                                            rows={4}
                                            {...field}
                                        />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        <DialogFooter>
                            <Button type="button" variant="outline" onClick={() => onOpenChange(false)}>
                                Cancel
                            </Button>
                            <Button type="submit">Submit Request</Button>
                        </DialogFooter>
                    </form>
                </Form>
            </DialogContent>
        </Dialog>
    )
}
