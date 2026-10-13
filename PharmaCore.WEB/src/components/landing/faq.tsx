"use client"

import React, { useState } from "react"
import { useLanguage } from "@/components/providers/language-provider"
import { cn } from "@/lib/utils"
import { 
    ChevronDown, 
    WifiOff, 
    Cpu, 
    ShieldCheck, 
    CreditCard,
    HelpCircle
} from "lucide-react"

export function FAQ() {
    const { t } = useLanguage()
    const [openIndex, setOpenIndex] = useState<number | null>(null)

    const faqItems = [
        {
            key: "offline",
            icon: WifiOff,
            question: "faq.offline.question",
            answer: "faq.offline.answer",
        },
        {
            key: "hardware",
            icon: Cpu,
            question: "faq.hardware.question",
            answer: "faq.hardware.answer",
        },
        {
            key: "security",
            icon: ShieldCheck,
            question: "faq.security.question",
            answer: "faq.security.answer",
        },
        {
            key: "plans",
            icon: CreditCard,
            question: "faq.plans.question",
            answer: "faq.plans.answer",
        },
    ]

    const toggleIndex = (index: number) => {
        setOpenIndex(openIndex === index ? null : index)
    }

    return (
        <section id="faq" className="w-full py-16 md:py-24 bg-muted/30">
            <div className="container mx-auto px-4 max-w-4xl">
                <div className="text-center mb-12 md:mb-16">
                    <div className="inline-flex items-center gap-2 px-3 py-1 rounded-full bg-primary/10 text-primary text-sm font-semibold mb-4">
                        <HelpCircle className="w-4 h-4" />
                        <span>FAQ</span>
                    </div>
                    <h2 className="text-3xl md:text-4xl font-bold tracking-tight text-foreground font-sans mb-4">
                        {t("faq.title")}
                    </h2>
                    <p className="text-muted-foreground text-lg max-w-2xl mx-auto font-sans">
                        {t("faq.subtitle")}
                    </p>
                </div>

                <div className="space-y-4">
                    {faqItems.map((item, idx) => {
                        const Icon = item.icon
                        const isOpen = openIndex === idx

                        return (
                            <div 
                                key={item.key}
                                className={cn(
                                    "border border-border rounded-xl overflow-hidden transition-all duration-300",
                                    isOpen 
                                        ? "bg-card shadow-md border-primary/20 scale-[1.01]" 
                                        : "bg-card/50 hover:bg-card hover:border-border/80"
                                )}
                            >
                                <button
                                    onClick={() => toggleIndex(idx)}
                                    className="w-full flex items-center justify-between p-5 md:p-6 text-start font-sans focus:outline-none focus-visible:ring-2 focus-visible:ring-primary/50"
                                    aria-expanded={isOpen}
                                >
                                    <div className="flex items-center gap-4">
                                        <div className={cn(
                                            "p-2.5 rounded-lg transition-colors duration-200",
                                            isOpen 
                                                ? "bg-primary text-primary-foreground" 
                                                : "bg-muted text-muted-foreground"
                                        )}>
                                            <Icon className="w-5 h-5" />
                                        </div>
                                        <span className="font-semibold text-foreground text-base md:text-lg">
                                            {t(item.question)}
                                        </span>
                                    </div>
                                    <ChevronDown className={cn(
                                        "w-5 h-5 text-muted-foreground transition-transform duration-300 ease-out ms-4 flex-shrink-0",
                                        isOpen && "transform rotate-180 text-primary"
                                    )} />
                                </button>
                                
                                <div 
                                    className={cn(
                                        "grid transition-all duration-300 ease-in-out font-sans",
                                        isOpen ? "grid-rows-[1fr] opacity-100" : "grid-rows-[0fr] opacity-0"
                                    )}
                                >
                                    <div className="overflow-hidden">
                                        <div className="px-5 pb-5 md:px-6 md:pb-6 pt-0 text-muted-foreground text-sm md:text-base leading-relaxed ps-[3.75rem] pe-6">
                                            {t(item.answer)}
                                        </div>
                                    </div>
                                </div>
                            </div>
                        )
                    })}
                </div>
            </div>
        </section>
    )
}
