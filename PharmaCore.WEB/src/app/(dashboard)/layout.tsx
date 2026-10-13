import { Header } from "@/components/layout/header"
import { Sidebar } from "@/components/layout/sidebar"

export default function DashboardLayout({
    children,
}: {
    children: React.ReactNode
}) {
    return (
        <div className="flex min-h-screen flex-col">
            <Header />
            <div className="container mx-auto flex-1 items-start md:grid md:grid-cols-[220px_minmax(0,1fr)] md:gap-6 lg:grid-cols-[240px_minmax(0,1fr)] lg:gap-10">
                <aside className="fixed top-14 z-30 -ms-2 hidden h-[calc(100vh-3.5rem)] w-full shrink-0 md:sticky md:block">
                    <ScrollArea className="h-full py-6 pe-6 lg:py-8">
                        <Sidebar />
                    </ScrollArea>
                </aside>
                <main className="flex w-full flex-col overflow-hidden py-6 lg:py-8">
                    {children}
                </main>
            </div>
        </div>
    )
}

import { ScrollArea } from "@/components/ui/scroll-area"
