"use client"

import { useState, useEffect } from "react"
import { Clock, Key, ArrowRight, UserCheck, LogOut, CheckCircle2, AlertCircle } from "lucide-react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { toast } from "sonner"
import apiClient from "@/lib/api-client"
import Link from "next/link"

export default function AttendancePage() {
    const [currentTime, setCurrentTime] = useState<Date | null>(null)
    const [employeeCode, setEmployeeCode] = useState("")
    const [pin, setPin] = useState("")
    const [isLoading, setIsLoading] = useState(false)
    const [step, setStep] = useState<"code" | "pin">("code")
    
    // Status states after verification
    const [verifiedEmployee, setVerifiedEmployee] = useState<any>(null)
    const [statusMessage, setStatusMessage] = useState<string | null>(null)
    const [statusType, setStatusType] = useState<"in" | "out" | null>(null)

    // Tick the clock
    useEffect(() => {
        setCurrentTime(new Date())
        const timer = setInterval(() => setCurrentTime(new Date()), 1000)
        return () => clearInterval(timer)
    }, [])

    const handleVerifyCode = async (e: React.FormEvent) => {
        e.preventDefault()
        if (!employeeCode.trim()) {
            toast.error("برجاء إدخال كود الموظف")
            return
        }
        setStep("pin")
    }

    const handlePinKeyPress = (digit: string) => {
        if (pin.length < 4) {
            setPin(prev => prev + digit)
        }
    }

    const handleClearPin = () => {
        setPin("")
    }

    const handleBackspacePin = () => {
        setPin(prev => prev.slice(0, -1))
    }

    const handleClockAction = async (action: "in" | "out") => {
        if (!verifiedEmployee) return
        setIsLoading(true)
        try {
            if (action === "in") {
                // Clock-in: Create Session
                await apiClient.post("/audit-logs/sessions", {
                    employeeId: verifiedEmployee.id,
                    employeeCode: verifiedEmployee.employeeCode,
                    employeeName: verifiedEmployee.fullName,
                    branchId: verifiedEmployee.branchId || null,
                    deviceName: "حضور وانصراف PIN Pad",
                    ipAddress: "127.0.0.1"
                }, {
                    headers: {
                        // Pass organization context header
                        "X-Org-Id": verifiedEmployee.orgId
                    }
                })
                setStatusMessage(`تم تسجيل حضورك بنجاح! طاب يومك، ${verifiedEmployee.fullName}.`)
                setStatusType("in")
            } else {
                // Clock-out: Close Session
                if (!verifiedEmployee.activeSessionId) {
                    toast.error("لا يوجد شيفت مفتوح حالياً لتسجيل الانصراف")
                    return
                }
                await apiClient.put(`/audit-logs/sessions/${verifiedEmployee.activeSessionId}/close`, {}, {
                    headers: {
                        "X-Org-Id": verifiedEmployee.orgId
                    }
                })
                setStatusMessage(`تم تسجيل انصرافك بنجاح! نراك قريباً، ${verifiedEmployee.fullName}.`)
                setStatusType("out")
            }
            
            // Auto reset after 4 seconds
            setTimeout(() => {
                resetAll()
            }, 4000)
        } catch (err: any) {
            toast.error("فشل تسجيل العملية في قاعدة البيانات")
        } finally {
            setIsLoading(false)
        }
    }

    // Auto submit pin when it reaches 4 digits
    useEffect(() => {
        if (pin.length === 4) {
            submitVerification()
        }
    }, [pin])

    const submitVerification = async () => {
        setIsLoading(true)
        try {
            const response = await apiClient.post("/employees/verify-pin", {
                employeeCode: employeeCode.trim(),
                pin: pin
            })
            setVerifiedEmployee(response.data)
        } catch (err: any) {
            toast.error(err.response?.data?.error || "الرقم السري أو كود الموظف غير صحيح")
            setPin("")
        } finally {
            setIsLoading(false)
        }
    }

    const resetAll = () => {
        setEmployeeCode("")
        setPin("")
        setStep("code")
        setVerifiedEmployee(null)
        setStatusMessage(null)
        setStatusType(null)
    }

    return (
        <div className="min-h-screen bg-slate-950 flex flex-col justify-between p-6 text-slate-100" dir="rtl">
            <header className="flex justify-between items-center max-w-4xl mx-auto w-full border-b border-slate-800 pb-4">
                <div className="flex items-center gap-2">
                    <Clock className="h-6 w-6 text-sky-500" />
                    <span className="font-bold text-xl tracking-wide bg-gradient-to-r from-sky-400 to-indigo-400 bg-clip-text text-transparent">
                        PharmaCore HR Attendance
                    </span>
                </div>
                <Link href="/login">
                    <Button variant="ghost" className="text-slate-400 hover:text-white">
                        العودة للوحة الدخول
                    </Button>
                </Link>
            </header>

            <main className="flex-1 flex items-center justify-center py-12">
                <div className="w-full max-w-md">
                    {/* Time display Card */}
                    <div className="text-center mb-6">
                        <h2 className="text-4xl font-mono font-bold tracking-wider text-sky-400">
                            {currentTime ? currentTime.toLocaleTimeString("ar-EG") : "00:00:00"}
                        </h2>
                        <p className="text-slate-400 text-sm mt-1">
                            {currentTime ? currentTime.toLocaleDateString("ar-EG", { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' }) : ""}
                        </p>
                    </div>

                    <Card className="border-slate-800 bg-slate-900/90 shadow-2xl">
                        <CardHeader className="text-center border-b border-slate-800/60 pb-6">
                            <CardTitle className="text-xl font-bold text-white">
                                {statusMessage ? "تمت العملية" : verifiedEmployee ? "تأكيد الهوية" : step === "code" ? "تسجيل الحضور والانصراف" : "أدخل الرمز السري"}
                            </CardTitle>
                            <CardDescription className="text-slate-400">
                                {statusMessage ? "تأكيد حفظ البيانات" : verifiedEmployee ? "اختر العملية المطلوبة للشيفت" : step === "code" ? "برجاء إدخال كود الموظف الخاص بك للبدء" : `الموظف: ${employeeCode}`}
                            </CardDescription>
                        </CardHeader>

                        <CardContent className="pt-6">
                            {/* Success State */}
                            {statusMessage && (
                                <div className="text-center py-6 space-y-4">
                                    <div className="inline-flex p-3 rounded-full bg-emerald-500/10 text-emerald-400 border border-emerald-500/20">
                                        <CheckCircle2 className="h-12 w-12" />
                                    </div>
                                    <p className="text-lg font-semibold text-emerald-400 leading-relaxed px-4">
                                        {statusMessage}
                                    </p>
                                    <Button onClick={resetAll} variant="outline" className="border-slate-800 text-slate-300">
                                        العودة للرئيسية
                                    </Button>
                                </div>
                            )}

                            {/* Verified Employee State */}
                            {!statusMessage && verifiedEmployee && (
                                <div className="space-y-6">
                                    <div className="bg-slate-950 p-4 rounded-lg border border-slate-800 text-center space-y-2">
                                        <h3 className="text-lg font-bold text-sky-400">{verifiedEmployee.fullName}</h3>
                                        <p className="text-xs text-slate-400 font-mono">الكود: {verifiedEmployee.employeeCode} | الرتبة: {verifiedEmployee.role}</p>
                                        <div className="inline-block mt-2">
                                            {verifiedEmployee.hasActiveSession ? (
                                                <span className="text-[10px] font-semibold text-emerald-400 bg-emerald-500/10 px-2.5 py-1 rounded-full border border-emerald-500/20">
                                                    حالة الحضور: نشط حالياً
                                                </span>
                                            ) : (
                                                <span className="text-[10px] font-semibold text-rose-400 bg-rose-500/10 px-2.5 py-1 rounded-full border border-rose-500/20">
                                                    حالة الحضور: غير مسجل حضور
                                                </span>
                                            )}
                                        </div>
                                    </div>

                                    <div className="flex flex-col gap-3">
                                        <Button
                                            onClick={() => handleClockAction("in")}
                                            disabled={isLoading || verifiedEmployee.hasActiveSession}
                                            className="h-14 bg-gradient-to-r from-emerald-600 to-teal-600 hover:from-emerald-500 hover:to-teal-500 font-bold text-white text-lg rounded-xl gap-2 shadow-lg shadow-emerald-950/20"
                                        >
                                            <UserCheck className="h-5 w-5" />
                                            تسجيل حضور (Clock In)
                                        </Button>

                                        <Button
                                            onClick={() => handleClockAction("out")}
                                            disabled={isLoading || !verifiedEmployee.hasActiveSession}
                                            variant="destructive"
                                            className="h-14 bg-gradient-to-r from-rose-600 to-red-600 hover:from-rose-500 hover:to-red-500 font-bold text-white text-lg rounded-xl gap-2 shadow-lg shadow-rose-950/20"
                                        >
                                            <LogOut className="h-5 w-5" />
                                            تسجيل انصراف (Clock Out)
                                        </Button>

                                        <Button onClick={resetAll} variant="ghost" className="text-slate-400 hover:text-white mt-2">
                                            إلغاء وتغيير الكود
                                        </Button>
                                    </div>
                                </div>
                            )}

                            {/* Step 1: Input Code */}
                            {!statusMessage && !verifiedEmployee && step === "code" && (
                                <form onSubmit={handleVerifyCode} className="space-y-4">
                                    <div className="space-y-2">
                                        <Input
                                            type="text"
                                            placeholder="أدخل كود الموظف (مثال: E1001)"
                                            value={employeeCode}
                                            onChange={(e) => setEmployeeCode(e.target.value)}
                                            className="h-12 bg-slate-950 border-slate-800 text-center text-lg font-mono tracking-wider focus-visible:ring-sky-500"
                                            autoFocus
                                        />
                                    </div>
                                    <Button type="submit" className="w-full h-12 bg-sky-600 hover:bg-sky-500 text-white font-bold rounded-xl gap-2">
                                        المتابعة لإدخال الرمز
                                        <ArrowRight className="h-4 w-4 rotate-180" />
                                    </Button>
                                </form>
                            )}

                            {/* Step 2: PIN Keyboard Pad */}
                            {!statusMessage && !verifiedEmployee && step === "pin" && (
                                <div className="space-y-6">
                                    {/* Masked PIN Display */}
                                    <div className="flex justify-center gap-3">
                                        {[0, 1, 2, 3].map((index) => (
                                            <div
                                                key={index}
                                                className={`w-10 h-10 rounded-full border-2 flex items-center justify-center font-bold text-xl ${
                                                    pin.length > index
                                                        ? "border-sky-500 bg-sky-500/20 text-sky-400"
                                                        : "border-slate-800 bg-slate-950 text-slate-600"
                                                }`}
                                            >
                                                {pin.length > index ? "•" : ""}
                                            </div>
                                        ))}
                                    </div>

                                    {/* PIN Grid Keyboard */}
                                    <div className="grid grid-cols-3 gap-3 max-w-[280px] mx-auto font-mono">
                                        {["1", "2", "3", "4", "5", "6", "7", "8", "9"].map((digit) => (
                                            <Button
                                                key={digit}
                                                onClick={() => handlePinKeyPress(digit)}
                                                variant="outline"
                                                disabled={isLoading}
                                                className="h-14 border-slate-800 bg-slate-950 hover:bg-slate-800 text-lg font-bold text-white rounded-xl"
                                            >
                                                {digit}
                                            </Button>
                                        ))}
                                        <Button
                                            onClick={handleClearPin}
                                            variant="ghost"
                                            disabled={isLoading}
                                            className="h-14 text-rose-500 hover:text-rose-400 font-bold"
                                        >
                                            إلغاء
                                        </Button>
                                        <Button
                                            onClick={() => handlePinKeyPress("0")}
                                            variant="outline"
                                            disabled={isLoading}
                                            className="h-14 border-slate-800 bg-slate-950 hover:bg-slate-800 text-lg font-bold text-white rounded-xl"
                                        >
                                            0
                                        </Button>
                                        <Button
                                            onClick={handleBackspacePin}
                                            variant="ghost"
                                            disabled={isLoading}
                                            className="h-14 text-amber-500 hover:text-amber-400 font-bold"
                                        >
                                            مسح
                                        </Button>
                                    </div>

                                    <Button onClick={() => setStep("code")} variant="ghost" className="w-full text-slate-400 hover:text-white text-xs">
                                        رجوع لتغيير الكود
                                    </Button>
                                </div>
                            )}
                        </CardContent>
                    </Card>
                </div>
            </main>

            <footer className="text-center text-xs text-slate-600 max-w-4xl mx-auto w-full border-t border-slate-900 pt-4">
                PharmaCore v1.4 • نظام الصيدليات الذكي المتكامل
            </footer>
        </div>
    )
}
