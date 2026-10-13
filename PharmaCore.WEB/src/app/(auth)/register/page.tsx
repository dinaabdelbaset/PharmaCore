"use client"

import { useState } from "react"
import { useForm } from "react-hook-form"
import { zodResolver } from "@hookform/resolvers/zod"
import { useRouter } from "next/navigation"
import { Loader2, Building2, User, Mail, Lock, ArrowRight, ArrowLeft } from "lucide-react"
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
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form"
import { authService, registerSchema, type RegisterRequest } from "@/services/auth-service"
import { useLanguage } from "@/components/providers/language-provider"

export default function RegisterPage() {
    const router = useRouter()
    const { language, t } = useLanguage()
    const [isLoading, setIsLoading] = useState(false)
    const [error, setError] = useState<string | null>(null)

    const dir = language === "ar" ? "rtl" : "ltr"

    const form = useForm<RegisterRequest>({
        resolver: zodResolver(registerSchema),
        defaultValues: {
            organizationName: "",
            adminName: "",
            email: "",
            password: "",
        },
    })

    async function onSubmit(data: RegisterRequest) {
        setIsLoading(true)
        setError(null)
        try {
            const response = await authService.register(data)
            localStorage.setItem("token", response.token)
            localStorage.setItem("user", JSON.stringify(response.user))
            localStorage.removeItem("setup_completed")
            router.push("/register/setup")
        } catch (err) {
            setError("auth.register.error")
            console.error(err)
        } finally {
            setIsLoading(false)
        }
    }

    const orgPlaceholder = language === "ar" ? "مثال: صيدلية الشفاء" : "e.g., PharmaCare Pharmacy"
    const namePlaceholder = language === "ar" ? "الاسم الكامل للمدير" : "e.g., John Doe"

    return (
        <div dir={dir} className="w-full relative group/card">
            {/* Background premium glow effect */}
            <div className="absolute -inset-0.5 bg-gradient-to-r from-emerald-500 via-teal-500 to-cyan-500 rounded-2xl blur opacity-20 group-hover/card:opacity-30 transition duration-1000 group-hover/card:duration-200" />
            
            <Card className="relative w-full border border-neutral-200/60 dark:border-neutral-800/60 bg-white/90 dark:bg-neutral-900/90 backdrop-blur-xl shadow-2xl rounded-2xl overflow-hidden">
                {/* Visual top accent line */}
                <div className="absolute top-0 left-0 right-0 h-1.5 bg-gradient-to-r from-emerald-500 via-teal-500 to-cyan-500" />
                
                <CardHeader className="space-y-2 pt-8 pb-6">
                    <CardTitle className="text-2xl md:text-3xl font-extrabold text-center tracking-tight bg-gradient-to-r from-emerald-600 via-teal-600 to-cyan-600 dark:from-emerald-400 dark:via-teal-400 dark:to-cyan-400 bg-clip-text text-transparent">
                        {t("auth.register.title")}
                    </CardTitle>
                    <CardDescription className="text-center text-sm md:text-base text-muted-foreground px-4">
                        {t("auth.register.subtitle")}
                    </CardDescription>
                </CardHeader>
                
                <CardContent className="px-6 pb-6">
                    <Form {...form}>
                        <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-5">
                            {/* Organization Name Field */}
                            <FormField
                                control={form.control}
                                name="organizationName"
                                render={({ field }) => (
                                    <FormItem className="space-y-1.5">
                                        <FormLabel className="text-sm font-semibold text-neutral-700 dark:text-neutral-300">
                                            {t("auth.register.org_name")}
                                        </FormLabel>
                                        <FormControl>
                                            <div className="relative group/field flex items-center">
                                                <Building2 className="absolute left-3.5 rtl:left-auto rtl:right-3.5 h-5 w-5 text-muted-foreground group-focus-within/field:text-teal-500 transition-colors duration-200" />
                                                <Input 
                                                    placeholder={orgPlaceholder} 
                                                    className="pl-11 pr-4 rtl:pl-4 rtl:pr-11 h-11 transition-all duration-200 border-neutral-200 dark:border-neutral-800 bg-neutral-50/50 dark:bg-neutral-950/50 focus-visible:ring-1 focus-visible:ring-teal-500 focus-visible:border-teal-500"
                                                    {...field} 
                                                />
                                            </div>
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />

                            {/* Admin Full Name Field */}
                            <FormField
                                control={form.control}
                                name="adminName"
                                render={({ field }) => (
                                    <FormItem className="space-y-1.5">
                                        <FormLabel className="text-sm font-semibold text-neutral-700 dark:text-neutral-300">
                                            {t("auth.register.admin_name")}
                                        </FormLabel>
                                        <FormControl>
                                            <div className="relative group/field flex items-center">
                                                <User className="absolute left-3.5 rtl:left-auto rtl:right-3.5 h-5 w-5 text-muted-foreground group-focus-within/field:text-teal-500 transition-colors duration-200" />
                                                <Input 
                                                    placeholder={namePlaceholder} 
                                                    className="pl-11 pr-4 rtl:pl-4 rtl:pr-11 h-11 transition-all duration-200 border-neutral-200 dark:border-neutral-800 bg-neutral-50/50 dark:bg-neutral-950/50 focus-visible:ring-1 focus-visible:ring-teal-500 focus-visible:border-teal-500"
                                                    {...field} 
                                                />
                                            </div>
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />

                            {/* Admin Email Field */}
                            <FormField
                                control={form.control}
                                name="email"
                                render={({ field }) => (
                                    <FormItem className="space-y-1.5">
                                        <FormLabel className="text-sm font-semibold text-neutral-700 dark:text-neutral-300">
                                            {t("auth.register.email")}
                                        </FormLabel>
                                        <FormControl>
                                            <div className="relative group/field flex items-center">
                                                <Mail className="absolute left-3.5 rtl:left-auto rtl:right-3.5 h-5 w-5 text-muted-foreground group-focus-within/field:text-teal-500 transition-colors duration-200" />
                                                <Input 
                                                    type="email"
                                                    placeholder="admin@example.com" 
                                                    className="pl-11 pr-4 rtl:pl-4 rtl:pr-11 h-11 transition-all duration-200 border-neutral-200 dark:border-neutral-800 bg-neutral-50/50 dark:bg-neutral-950/50 focus-visible:ring-1 focus-visible:ring-teal-500 focus-visible:border-teal-500"
                                                    {...field} 
                                                />
                                            </div>
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />

                            {/* Password Field */}
                            <FormField
                                control={form.control}
                                name="password"
                                render={({ field }) => (
                                    <FormItem className="space-y-1.5">
                                        <FormLabel className="text-sm font-semibold text-neutral-700 dark:text-neutral-300">
                                            {t("auth.register.password")}
                                        </FormLabel>
                                        <FormControl>
                                            <div className="relative group/field flex items-center">
                                                <Lock className="absolute left-3.5 rtl:left-auto rtl:right-3.5 h-5 w-5 text-muted-foreground group-focus-within/field:text-teal-500 transition-colors duration-200" />
                                                <Input 
                                                    type="password"
                                                    placeholder="••••••" 
                                                    className="pl-11 pr-4 rtl:pl-4 rtl:pr-11 h-11 transition-all duration-200 border-neutral-200 dark:border-neutral-800 bg-neutral-50/50 dark:bg-neutral-950/50 focus-visible:ring-1 focus-visible:ring-teal-500 focus-visible:border-teal-500"
                                                    {...field} 
                                                />
                                            </div>
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />

                            {/* Server/Submition Error Display */}
                            {error && (
                                <div className="p-3 rounded-lg bg-destructive/10 border border-destructive/20 text-sm text-destructive text-center font-medium animate-pulse">
                                    {t(error)}
                                </div>
                            )}

                            {/* Submit Button */}
                            <Button 
                                className="w-full h-11 bg-gradient-to-r from-emerald-600 via-teal-600 to-cyan-600 hover:from-emerald-500 hover:via-teal-500 hover:to-cyan-500 text-white font-semibold shadow-md hover:shadow-lg transition-all duration-300 group" 
                                type="submit" 
                                disabled={isLoading}
                            >
                                {isLoading ? (
                                    <Loader2 className="h-5 w-5 animate-spin" />
                                ) : (
                                    <span className="inline-flex items-center justify-center gap-1.5">
                                        {t("auth.register.btn")}
                                        {language === "ar" ? (
                                            <ArrowLeft className="h-4 w-4 transition-transform duration-200 group-hover:-translate-x-1" />
                                        ) : (
                                            <ArrowRight className="h-4 w-4 transition-transform duration-200 group-hover:translate-x-1" />
                                        )}
                                    </span>
                                )}
                            </Button>
                        </form>
                    </Form>
                </CardContent>

                {/* Footer redirection link */}
                <CardFooter className="flex flex-col space-y-3 items-center border-t border-neutral-100 dark:border-neutral-800/80 bg-neutral-50/50 dark:bg-neutral-900/50 py-5">
                    <div className="text-sm text-muted-foreground text-center">
                        {t("auth.register.has_account")}{" "}
                        <Link href="/login" className="text-teal-600 dark:text-teal-400 hover:text-teal-500 hover:underline font-semibold transition-colors duration-200">
                            {t("auth.register.sign_in")}
                        </Link>
                    </div>
                </CardFooter>
            </Card>
        </div>
    )
}
