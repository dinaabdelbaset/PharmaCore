"use client"

import { useState } from "react"
import { useForm } from "react-hook-form"
import { zodResolver } from "@hookform/resolvers/zod"
import { useRouter } from "next/navigation"
import { Loader2 } from "lucide-react"
import Link from "next/link"

import { Button } from "@/components/ui/button"
import {
    Card,
    CardContent,
    CardDescription,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form"
import { authService, loginSchema, type LoginRequest } from "@/services/auth-service"
import { useLanguage } from "@/components/providers/language-provider"

export default function LoginPage() {
    const router = useRouter()
    const { t } = useLanguage()
    const [isLoading, setIsLoading] = useState(false)
    const [error, setError] = useState<string | null>(null)

    const form = useForm<LoginRequest>({
        resolver: zodResolver(loginSchema),
        defaultValues: {
            email: "",
            password: "",
        },
    })

    async function onSubmit(data: LoginRequest) {
        setIsLoading(true)
        setError(null)
        try {
            const response = await authService.login(data)
            if (response.setupCompleted) {
                localStorage.setItem("setup_completed", "true")
            } else {
                localStorage.removeItem("setup_completed")
            }
            localStorage.setItem("token", response.token)
            localStorage.setItem("user", JSON.stringify(response.user))
            router.push("/dashboard")
        } catch (err) {
            setError("auth.login.error")
            console.error(err)
        } finally {
            setIsLoading(false)
        }
    }

    return (
        <Card className="w-full border-none shadow-lg">
            <CardHeader className="space-y-1">
                <CardTitle className="text-2xl font-bold text-center">{t("auth.login.title")}</CardTitle>
                <CardDescription className="text-center">
                    {t("auth.login.subtitle")}
                </CardDescription>
            </CardHeader>
            <CardContent>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
                        <FormField
                            control={form.control}
                            name="email"
                            render={({ field }) => (
                                <FormItem>
                                    <FormLabel>{t("auth.login.email")}</FormLabel>
                                    <FormControl>
                                        <Input placeholder="admin@example.com" {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        <FormField
                            control={form.control}
                            name="password"
                            render={({ field }) => (
                                <FormItem>
                                    <FormLabel>{t("auth.login.password")}</FormLabel>
                                    <FormControl>
                                        <Input type="password" placeholder="••••••" {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        {error && (
                            <div className="text-sm text-destructive text-center font-medium">
                                {t(error)}
                            </div>
                        )}
                        <Button className="w-full" type="submit" disabled={isLoading}>
                            {isLoading && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
                            {t("auth.login.btn")}
                        </Button>
                    </form>
                </Form>
            </CardContent>
            <CardFooter className="flex flex-col space-y-3 items-center">
                <div className="text-sm text-muted-foreground">
                    {t("auth.login.no_account")}{" "}
                    <Link href="/register" className="text-primary hover:underline font-medium">
                        {t("auth.login.sign_up")}
                    </Link>
                </div>
                <p className="text-xs text-muted-foreground">
                    {t("auth.login.security")}
                </p>
            </CardFooter>
        </Card>
    )
}
