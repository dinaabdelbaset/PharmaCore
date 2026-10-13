"use client"

import React, { useState, useEffect } from "react"
import { Navbar } from "@/components/landing/navbar"
import Hero from "@/components/landing/hero"
import FeaturesSection from "@/components/landing/features"
import PricingSection from "@/components/landing/pricing"
import { FAQ } from "@/components/landing/faq"
import { Footer } from "@/components/landing/footer"

export default function Home() {
    const [sessionActive, setSessionActive] = useState<boolean>(false)
    const [mounted, setMounted] = useState<boolean>(false)

    useEffect(() => {
        // Safe check for localStorage on the client side
        const token = localStorage.getItem("token")
        setSessionActive(!!token)
        setMounted(true)
    }, [])

    if (!mounted) {
        // Render a basic structural placeholder to prevent SSR/hydration mismatch
        return (
            <div className="min-h-screen bg-background flex flex-col justify-between">
                <div className="h-16 w-full bg-background border-b border-border/40 animate-pulse" />
                <div className="flex-grow flex flex-col items-center justify-center p-8">
                    <div className="h-12 w-48 bg-muted rounded-xl animate-pulse mb-6" />
                    <div className="h-6 w-96 bg-muted rounded animate-pulse" />
                </div>
                <div className="h-32 w-full bg-muted animate-pulse" />
            </div>
        )
    }

    return (
        <div className="min-h-screen bg-background text-foreground flex flex-col">
            <Navbar sessionActive={sessionActive} />
            <main className="flex-grow animate-fade-in">
                <Hero sessionActive={sessionActive} />
                <FeaturesSection />
                <PricingSection />
                <FAQ />
            </main>
            <Footer />
        </div>
    )
}
