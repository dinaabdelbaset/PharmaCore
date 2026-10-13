"use client"

import { Moon, Sun, Languages, Check } from "lucide-react"
import { useTheme } from "next-themes"
import { useLanguage } from "@/components/providers/language-provider"
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { Button } from "@/components/ui/button"
import { Settings } from "lucide-react"

export function SettingsDropdown() {
    const { theme, setTheme } = useTheme()
    const { language, setLanguage, t } = useLanguage()

    return (
        <DropdownMenu>
            <DropdownMenuTrigger asChild>
                <Button variant="ghost" size="icon">
                    <Settings className="h-5 w-5" />
                </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end" className="w-56">
                <DropdownMenuLabel>Settings</DropdownMenuLabel>
                <DropdownMenuSeparator />

                {/* Theme Section */}
                <DropdownMenuLabel className="text-xs text-muted-foreground">Theme</DropdownMenuLabel>
                <DropdownMenuItem onClick={() => setTheme("light")}>
                    <Sun className="mr-2 h-4 w-4" />
                    Light
                    {theme === "light" && <Check className="ml-auto h-4 w-4" />}
                </DropdownMenuItem>
                <DropdownMenuItem onClick={() => setTheme("dark")}>
                    <Moon className="mr-2 h-4 w-4" />
                    Dark
                    {theme === "dark" && <Check className="ml-auto h-4 w-4" />}
                </DropdownMenuItem>

                <DropdownMenuSeparator />

                {/* Language Section */}
                <DropdownMenuLabel className="text-xs text-muted-foreground">Language</DropdownMenuLabel>
                <DropdownMenuItem onClick={() => setLanguage("en")}>
                    <Languages className="mr-2 h-4 w-4" />
                    English
                    {language === "en" && <Check className="ml-auto h-4 w-4" />}
                </DropdownMenuItem>
                <DropdownMenuItem onClick={() => setLanguage("ar")}>
                    <Languages className="mr-2 h-4 w-4" />
                    العربية
                    {language === "ar" && <Check className="ml-auto h-4 w-4" />}
                </DropdownMenuItem>
            </DropdownMenuContent>
        </DropdownMenu>
    )
}
