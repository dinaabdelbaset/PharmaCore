"use client"

import React, { useState, useEffect } from "react"
import { useTheme } from "next-themes"
import { useLanguage } from "@/components/providers/language-provider"
import { Button } from "@/components/ui/button"
import {
    Sheet,
    SheetContent,
    SheetTrigger,
    SheetClose,
    SheetTitle,
} from "@/components/ui/sheet"
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { Menu, Sun, Moon, Languages, ChevronDown, Check, Pill } from "lucide-react"

// Navigation items configuration
const navItems = [
    { href: "#features", labelKey: "nav.features" },
    { href: "#pricing", labelKey: "nav.pricing" },
    { href: "#about", labelKey: "nav.about" },
    { href: "#contact", labelKey: "nav.contact" },
]

// Fallback/landing translations dictionary matching the format of useLanguage provider
const landingTranslations = {
    en: {
        "nav.features": "Features",
        "nav.pricing": "Pricing",
        "nav.about": "About Us",
        "nav.contact": "Contact",
        "nav.signIn": "Sign In",
        "nav.getStarted": "Get Started",
        "nav.dashboard": "Go to Dashboard",
        "nav.logo": "PharmaCore",
        "nav.menu": "Menu",
    },
    ar: {
        "nav.features": "المميزات",
        "nav.pricing": "الأسعار",
        "nav.about": "من نحن",
        "nav.contact": "اتصل بنا",
        "nav.signIn": "تسجيل الدخول",
        "nav.getStarted": "ابدأ الآن",
        "nav.dashboard": "لوحة التحكم",
        "nav.logo": "فارماكور",
        "nav.menu": "القائمة",
    },
}

export interface NavbarProps {
    sessionActive?: boolean
}

export function Navbar({ sessionActive }: NavbarProps = {}) {
    const { language, setLanguage, t } = useLanguage()
    const { theme, setTheme } = useTheme()
    
    const [mounted, setMounted] = useState(false)
    const [hasToken, setHasToken] = useState(false)
    const [scrolled, setScrolled] = useState(false)

    useEffect(() => {
        setMounted(true)
        // Check for token on client mount
        if (sessionActive !== undefined) {
            setHasToken(sessionActive)
        } else {
            const token = localStorage.getItem("token")
            setHasToken(!!token)
        }

        // Scroll listener for sticky header transition
        const handleScroll = () => {
            setScrolled(window.scrollY > 20)
        }
        window.addEventListener("scroll", handleScroll)
        return () => window.removeEventListener("scroll", handleScroll)
    }, [sessionActive])

    // Helper to translate key with fallback to landingTranslations
    const getTranslation = (key: string): string => {
        const globalValue = t(key)
        if (globalValue !== key) return globalValue

        const langDict = landingTranslations[language] || landingTranslations.en
        return langDict[key as keyof typeof landingTranslations.en] || key
    }

    if (!mounted) {
        // Return a structural placeholder of the same height to prevent layout shifts during hydration
        return (
            <header className="sticky top-0 z-50 w-full border-b border-transparent bg-transparent py-4 transition-all duration-300">
                <div className="container mx-auto px-4 flex h-14 items-center justify-between">
                    <div className="flex items-center gap-2">
                        <div className="h-9 w-9 rounded-xl bg-muted animate-pulse" />
                        <div className="h-6 w-24 bg-muted animate-pulse rounded" />
                    </div>
                    <div className="flex gap-4 items-center">
                        <div className="h-9 w-20 bg-muted animate-pulse rounded-full" />
                        <div className="h-9 w-9 bg-muted animate-pulse rounded-full" />
                    </div>
                </div>
            </header>
        )
    }

    const isRtl = language === "ar"

    return (
        <header
            className={`sticky top-0 z-50 w-full transition-all duration-300 ${
                scrolled
                    ? "backdrop-blur-md bg-background/80 border-b border-border/40 shadow-sm py-2"
                    : "bg-transparent py-4"
            }`}
        >
            <div className="container mx-auto px-4 flex h-14 items-center justify-between">
                
                {/* Logo Section */}
                <a href="/" className="flex items-center gap-2 group transition-all">
                    <div className="flex h-9 w-9 items-center justify-center rounded-xl bg-gradient-to-tr from-primary to-primary/70 text-primary-foreground shadow-md transition-transform duration-300 group-hover:scale-105">
                        <Pill className={`h-5 w-5 rotate-45 transition-transform duration-500 group-hover:rotate-90`} />
                    </div>
                    <span className="font-bold text-xl tracking-tight bg-gradient-to-r from-foreground to-foreground/80 bg-clip-text">
                        {getTranslation("nav.logo")}
                    </span>
                </a>

                {/* Desktop Menu Center Links */}
                <nav className="hidden md:flex items-center gap-8">
                    {navItems.map((item) => (
                        <a
                            key={item.href}
                            href={item.href}
                            className="text-sm font-medium text-muted-foreground hover:text-foreground transition-colors relative py-1 after:absolute after:bottom-0 after:left-0 after:h-[2px] after:w-0 after:bg-primary after:transition-all hover:after:w-full"
                        >
                            {getTranslation(item.labelKey)}
                        </a>
                    ))}
                </nav>

                {/* Right Settings & Auth Actions */}
                <div className="flex items-center gap-2 md:gap-3">
                    
                    {/* Language Switcher */}
                    <DropdownMenu>
                        <DropdownMenuTrigger asChild>
                            <Button
                                variant="ghost"
                                size="sm"
                                className="rounded-full gap-1.5 hover:bg-accent/50 text-muted-foreground hover:text-foreground font-sans"
                            >
                                <Languages className="h-4 w-4" />
                                <span className="text-xs font-semibold uppercase">{language}</span>
                                <ChevronDown className="h-3 w-3 opacity-50" />
                            </Button>
                        </DropdownMenuTrigger>
                        <DropdownMenuContent align="end" className="w-32 backdrop-blur-md bg-background/95">
                            <DropdownMenuItem
                                onClick={() => setLanguage("en")}
                                className="gap-2 cursor-pointer flex justify-between items-center"
                            >
                                <span className="text-sm font-sans">English</span>
                                {language === "en" && <Check className="h-4 w-4 text-primary" />}
                            </DropdownMenuItem>
                            <DropdownMenuItem
                                onClick={() => setLanguage("ar")}
                                className="gap-2 cursor-pointer flex justify-between items-center"
                            >
                                <span className="text-sm font-sans">العربية</span>
                                {language === "ar" && <Check className="h-4 w-4 text-primary" />}
                            </DropdownMenuItem>
                        </DropdownMenuContent>
                    </DropdownMenu>

                    {/* Dark/Light Mode Theme Toggle */}
                    <Button
                        variant="ghost"
                        size="icon"
                        onClick={() => setTheme(theme === "dark" ? "light" : "dark")}
                        aria-label="Toggle theme"
                        className="rounded-full hover:bg-accent/50"
                    >
                        <Sun className="h-[1.2rem] w-[1.2rem] rotate-0 scale-100 transition-all dark:-rotate-90 dark:scale-0" />
                        <Moon className="absolute h-[1.2rem] w-[1.2rem] rotate-90 scale-0 transition-all dark:rotate-0 dark:scale-100" />
                    </Button>

                    {/* Desktop Auth Actions */}
                    <div className="hidden md:flex items-center gap-2">
                        {hasToken ? (
                            <Button
                                asChild
                                className="rounded-full px-5 bg-primary text-primary-foreground hover:bg-primary/90 transition-all shadow-sm font-medium"
                            >
                                <a href="/dashboard">{getTranslation("nav.dashboard")}</a>
                            </Button>
                        ) : (
                            <>
                                <Button
                                    asChild
                                    variant="ghost"
                                    className="rounded-full text-muted-foreground hover:text-foreground hover:bg-accent/50 font-medium"
                                >
                                    <a href="/login">{getTranslation("nav.signIn")}</a>
                                </Button>
                                <Button
                                    asChild
                                    className="rounded-full px-5 bg-primary text-primary-foreground hover:bg-primary/90 transition-all shadow-sm font-medium"
                                >
                                    <a href="/register">{getTranslation("nav.getStarted")}</a>
                                </Button>
                            </>
                        )}
                    </div>

                    {/* Responsive Mobile burger menu sheet drawer */}
                    <Sheet>
                        <SheetTrigger asChild>
                            <Button
                                variant="ghost"
                                size="icon"
                                className="md:hidden rounded-full hover:bg-accent/50"
                            >
                                <Menu className="h-6 w-6" />
                                <span className="sr-only">Toggle Menu</span>
                            </Button>
                        </SheetTrigger>
                        <SheetContent
                            side={isRtl ? "right" : "left"}
                            className="w-[280px] sm:w-[350px] flex flex-col h-full bg-background/95 backdrop-blur-md border-r border-border/40 p-6"
                        >
                            <SheetTitle className="sr-only">{getTranslation("nav.menu")}</SheetTitle>
                            
                            {/* Logo in Drawer */}
                            <div className="flex items-center gap-2 py-4 border-b border-border/40">
                                <div className="flex h-9 w-9 items-center justify-center rounded-xl bg-gradient-to-tr from-primary to-primary/70 text-primary-foreground shadow-sm">
                                    <Pill className="h-5 w-5 rotate-45" />
                                </div>
                                <span className="font-bold text-xl tracking-tight">
                                    {getTranslation("nav.logo")}
                                </span>
                            </div>

                            {/* Mobile Navigation Links */}
                            <nav className="flex flex-col gap-3 mt-6">
                                {navItems.map((item) => (
                                    <SheetClose asChild key={item.href}>
                                        <a
                                            href={item.href}
                                            className="text-base font-medium text-muted-foreground hover:text-foreground transition-colors py-2 px-3 rounded-lg hover:bg-accent/40 flex items-center justify-between"
                                        >
                                            <span>{getTranslation(item.labelKey)}</span>
                                        </a>
                                    </SheetClose>
                                ))}
                            </nav>

                            {/* Mobile Auth Actions at bottom */}
                            <div className="mt-auto flex flex-col gap-2 pt-6 border-t border-border/40">
                                {hasToken ? (
                                    <SheetClose asChild>
                                        <Button
                                            asChild
                                            className="w-full rounded-xl py-5 font-semibold text-sm shadow-sm"
                                        >
                                            <a href="/dashboard">{getTranslation("nav.dashboard")}</a>
                                        </Button>
                                    </SheetClose>
                                ) : (
                                    <>
                                        <SheetClose asChild>
                                            <Button
                                                asChild
                                                variant="outline"
                                                className="w-full rounded-xl py-5 font-semibold text-sm"
                                            >
                                                <a href="/login">{getTranslation("nav.signIn")}</a>
                                            </Button>
                                        </SheetClose>
                                        <SheetClose asChild>
                                            <Button
                                                asChild
                                                className="w-full rounded-xl py-5 font-semibold text-sm shadow-sm"
                                            >
                                                <a href="/register">{getTranslation("nav.getStarted")}</a>
                                            </Button>
                                        </SheetClose>
                                    </>
                                )}
                            </div>
                        </SheetContent>
                    </Sheet>

                </div>
            </div>
        </header>
    )
}
