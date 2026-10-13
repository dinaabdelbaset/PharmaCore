"use client"

import React from "react"
import { useLanguage } from "@/components/providers/language-provider"
import { 
    Activity, 
    Github, 
    Twitter, 
    Linkedin, 
    Globe, 
    Mail, 
    Phone 
} from "lucide-react"

export function Footer() {
    const { t } = useLanguage()

    const productLinks = [
        { name: "footer.links.features", href: "#features" },
        { name: "footer.links.branches", href: "/branches" },
        { name: "footer.links.devices", href: "/devices" },
        { name: "footer.links.pricing", href: "#pricing" },
    ]

    const companyLinks = [
        { name: "footer.links.about", href: "#" },
        { name: "footer.links.careers", href: "#" },
        { name: "footer.links.contact", href: "#" },
        { name: "footer.links.blog", href: "#" },
    ]

    const legalLinks = [
        { name: "footer.links.privacy", href: "#" },
        { name: "footer.links.terms", href: "#" },
        { name: "footer.links.security", href: "#" },
        { name: "footer.links.cookies", href: "#" },
    ]

    const socialLinks = [
        { icon: Twitter, href: "https://twitter.com", label: "Twitter" },
        { icon: Linkedin, href: "https://linkedin.com", label: "LinkedIn" },
        { icon: Github, href: "https://github.com", label: "GitHub" },
    ]

    return (
        <footer className="w-full border-t border-border bg-background py-12 md:py-16 lg:py-20 font-sans">
            <div className="container mx-auto px-4">
                {/* Main Grid */}
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-5 gap-8 md:gap-12 mb-12">
                    {/* Brand Info */}
                    <div className="lg:col-span-2 space-y-4">
                        <div className="flex items-center gap-2">
                            <div className="flex h-10 w-10 items-center justify-center rounded-lg bg-primary text-primary-foreground shadow-md shadow-primary/20">
                                <Activity className="h-5 w-5" />
                            </div>
                            <span className="text-xl font-bold tracking-tight text-foreground font-sans">
                                PharmaCore
                            </span>
                        </div>
                        <p className="text-muted-foreground text-sm md:text-base leading-relaxed max-w-sm">
                            {t("footer.desc")}
                        </p>
                        <div className="flex items-center gap-3 pt-2">
                            {socialLinks.map((social, index) => {
                                const Icon = social.icon
                                return (
                                    <a
                                        key={index}
                                        href={social.href}
                                        target="_blank"
                                        rel="noopener noreferrer"
                                        className="h-9 w-9 flex items-center justify-center rounded-lg border border-border bg-card text-muted-foreground hover:text-primary hover:border-primary/30 transition-all duration-200"
                                        aria-label={social.label}
                                    >
                                        <Icon className="h-4 w-4" />
                                    </a>
                                )
                            })}
                        </div>
                    </div>

                    {/* Product Links */}
                    <div className="space-y-4">
                        <h3 className="text-sm font-semibold uppercase tracking-wider text-foreground">
                            {t("footer.product")}
                        </h3>
                        <ul className="space-y-2.5">
                            {productLinks.map((link) => (
                                <li key={link.name}>
                                    <a
                                        href={link.href}
                                        className="text-sm text-muted-foreground hover:text-primary hover:ps-1 transition-all duration-200"
                                    >
                                        {t(link.name)}
                                    </a>
                                </li>
                            ))}
                        </ul>
                    </div>

                    {/* Company Links */}
                    <div className="space-y-4">
                        <h3 className="text-sm font-semibold uppercase tracking-wider text-foreground">
                            {t("footer.company")}
                        </h3>
                        <ul className="space-y-2.5">
                            {companyLinks.map((link) => (
                                <li key={link.name}>
                                    <a
                                        href={link.href}
                                        className="text-sm text-muted-foreground hover:text-primary hover:ps-1 transition-all duration-200"
                                    >
                                        {t(link.name)}
                                    </a>
                                </li>
                            ))}
                        </ul>
                    </div>

                    {/* Legal Links */}
                    <div className="space-y-4">
                        <h3 className="text-sm font-semibold uppercase tracking-wider text-foreground">
                            {t("footer.legal")}
                        </h3>
                        <ul className="space-y-2.5">
                            {legalLinks.map((link) => (
                                <li key={link.name}>
                                    <a
                                        href={link.href}
                                        className="text-sm text-muted-foreground hover:text-primary hover:ps-1 transition-all duration-200"
                                    >
                                        {t(link.name)}
                                    </a>
                                </li>
                            ))}
                        </ul>
                    </div>
                </div>

                {/* Bottom Bar */}
                <div className="pt-8 border-t border-border flex flex-col md:flex-row items-center justify-between gap-4">
                    <p className="text-xs md:text-sm text-muted-foreground text-center md:text-start">
                        {t("footer.copyright")}
                    </p>
                    <div className="flex items-center gap-4 text-xs text-muted-foreground">
                        <div className="flex items-center gap-1.5">
                            <Globe className="h-3.5 w-3.5" />
                            <span>Arabic / English</span>
                        </div>
                    </div>
                </div>
            </div>
        </footer>
    )
}
