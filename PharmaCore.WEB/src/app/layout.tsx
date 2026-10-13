import type { Metadata, Viewport } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";
import { Toaster } from "@/components/ui/sonner";
import { ThemeProvider } from "@/components/providers/theme-provider";
import { LanguageProvider } from "@/components/providers/language-provider";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: {
    default: "PharmaCore - Smart Pharmacy Management System",
    template: "%s | PharmaCore - Smart Pharmacy Management System",
  },
  description: "PharmaCore is a professional, smart pharmacy management system designed to optimize inventory, streamline sales, track prescriptions, and enhance operations.",
  keywords: [
    "PharmaCore",
    "Smart Pharmacy Management System",
    "Pharmacy Software",
    "Pharmacy Management",
    "Inventory Management",
    "Prescription Tracking",
    "Sales Tracking",
    "Healthcare SaaS",
    "Pharmacy POS"
  ],
  metadataBase: new URL(process.env.NEXT_PUBLIC_SITE_URL || "http://localhost:3000"),
  alternates: {
    canonical: "/",
  },
  openGraph: {
    title: "PharmaCore - Smart Pharmacy Management System",
    description: "Streamline pharmacy workflows, manage inventory in real-time, and track prescriptions efficiently.",
    url: process.env.NEXT_PUBLIC_SITE_URL || "http://localhost:3000",
    siteName: "PharmaCore",
    locale: "en_US",
    type: "website",
  },
  twitter: {
    card: "summary_large_image",
    title: "PharmaCore - Smart Pharmacy Management System",
    description: "Streamline pharmacy workflows, manage inventory in real-time, and track prescriptions efficiently.",
  },
  robots: {
    index: true,
    follow: true,
  },
};

export const viewport: Viewport = {
  width: "device-width",
  initialScale: 1,
  maximumScale: 5,
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en" suppressHydrationWarning>
      <body
        className={`${geistSans.variable} ${geistMono.variable} antialiased`}
      >
        <ThemeProvider
          attribute="class"
          defaultTheme="system"
          enableSystem
          disableTransitionOnChange
        >
          <LanguageProvider>
            {children}
            <Toaster />
          </LanguageProvider>
        </ThemeProvider>
      </body>
    </html>
  );
}
