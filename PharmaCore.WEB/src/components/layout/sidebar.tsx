"use client"

import Link from "next/link"
import { usePathname, useRouter } from "next/navigation"
import { cn } from "@/lib/utils"
import { Button } from "@/components/ui/button"
import { Badge } from "@/components/ui/badge"
import {
    Users,
    Building2,
    Smartphone,
    Key,
    LogOut,
    LayoutDashboard,
    FileText,
    ShieldCheck,
    MessageSquare,
    Landmark,
    Receipt,
    Wallet,
    Package,
    Truck,
    ShoppingBag,
    Barcode,
    Sparkles,
} from "lucide-react"
import { authService } from "@/services/auth-service"
import { useMessageNotifications } from "@/hooks/use-message-notifications"
import { useLanguage } from "@/components/providers/language-provider"

type SidebarProps = React.HTMLAttributes<HTMLDivElement>

export function Sidebar({ className }: SidebarProps) {
    const pathname = usePathname()
    const router = useRouter()
    const { unreadCount } = useMessageNotifications()
    const { language, t } = useLanguage()
    const isRtl = language === "ar"

    const handleLogout = () => {
        authService.logout()
        router.push("/login")
    }

    const navItems = [
        { href: "/pos", key: "nav.pos", icon: Barcode },
        { href: "/ai-assistant", key: "nav.ai_assistant", icon: Sparkles },
        { href: "/dashboard", key: "nav.dashboard", icon: LayoutDashboard },
        { href: "/catalog", key: "nav.catalog", icon: Package },
        { href: "/suppliers", key: "nav.suppliers", icon: Truck },
        { href: "/purchases", key: "nav.purchases", icon: ShoppingBag },
        { href: "/messages", key: "nav.messages", icon: MessageSquare, badge: unreadCount },
        { href: "/users", key: "nav.users", icon: Users },
        { href: "/password-requests", key: "nav.password_requests", icon: ShieldCheck },
        { href: "/logs", key: "nav.activity_logs", icon: FileText },
        { href: "/branches", key: "nav.branches", icon: Building2 },
        { href: "/devices", key: "nav.devices", icon: Smartphone },
        { href: "/activation-codes", key: "nav.activation_codes", icon: Key },
        { href: "/treasury", key: "nav.treasury", icon: Landmark },
        { href: "/expenses", key: "nav.expenses", icon: Receipt },
        { href: "/staff-finance", key: "nav.staff_finance", icon: Wallet },
    ]

    return (
        <div className={cn("pb-12", className)} dir={isRtl ? "rtl" : "ltr"}>
            <div className="space-y-4 py-4">
                <div className="px-3 py-2">
                    <h2 className={cn("mb-2 px-4 text-lg font-semibold tracking-tight", isRtl ? "text-right" : "text-left")}>
                        PharmaCore
                    </h2>
                    <div className="space-y-1">
                        {navItems.map((item) => (
                            <Button
                                key={item.href}
                                variant={pathname === item.href ? "secondary" : "ghost"}
                                className={cn("w-full justify-start", isRtl ? "text-right flex-row-reverse" : "text-left")}
                                asChild
                            >
                                <Link href={item.href}>
                                    <item.icon className={cn("h-4 w-4", isRtl ? "ms-2" : "me-2")} />
                                    <span>{t(item.key)}</span>
                                    {item.badge && item.badge > 0 && (
                                        <Badge className={isRtl ? "me-auto" : "ms-auto"} variant="destructive">
                                            {item.badge}
                                        </Badge>
                                    )}
                                </Link>
                            </Button>
                        ))}
                    </div>
                </div>
                <div className="px-3 py-2">
                    <div className="space-y-1">
                        <Button
                            variant="ghost"
                            className={cn("w-full justify-start text-destructive hover:text-destructive", isRtl ? "text-right flex-row-reverse" : "text-left")}
                            onClick={handleLogout}
                        >
                            <LogOut className={cn("h-4 w-4", isRtl ? "ms-2" : "me-2")} />
                            <span>{t("nav.logout")}</span>
                        </Button>
                    </div>
                </div>
            </div>
        </div>
    )
}
