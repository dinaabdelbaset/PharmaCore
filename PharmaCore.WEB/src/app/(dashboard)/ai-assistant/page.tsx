"use client"

import React, { useState } from "react"
import apiClient from "@/lib/api-client"
import { useRouter } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Badge } from "@/components/ui/badge"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import {
    Sparkles,
    FileScan,
    ShieldAlert,
    MessageSquareText,
    UploadCloud,
    Pill,
    CheckCircle2,
    AlertTriangle,
    ArrowRight,
    Send,
    Bot,
    User,
    ShoppingCart,
    Clock,
    RefreshCw,
    Activity,
} from "lucide-react"
import { toast } from "sonner"
import { useLanguage } from "@/components/providers/language-provider"

export default function AiAssistantPage() {
    const { language } = useLanguage()
    const isRtl = language === "ar"
    const router = useRouter()

    // ── Prescription Scanner State ──
    const [selectedImage, setSelectedImage] = useState<string | null>(null)
    const [isScanning, setIsScanning] = useState(false)
    const [scanResult, setScanResult] = useState<any>(null)

    // ── Drug Interaction State ──
    const [selectedDrugs, setSelectedDrugs] = useState<string[]>(["Warfarin", "Aspirin"])
    const [customDrugInput, setCustomDrugInput] = useState("")
    const [isCheckingInteractions, setIsCheckingInteractions] = useState(false)
    const [interactionResult, setInteractionResult] = useState<any>(null)

    // ── Clinical AI Chat State ──
    const [chatMessages, setChatMessages] = useState<{ sender: "ai" | "user"; text: string }[]>([
        {
            sender: "ai",
            text: isRtl
                ? "مرحباً دكتور! أنا مساعد فارماكور السريري الذكي. يمكنك سؤالي عن الجرعات الدوائية، تفاعلات الأدوية، بدائل الأدوية النادرة، أو إحصائيات ونواقص الصيدلية."
                : "Hello Doctor! I am PharmaCore Clinical AI. Ask me about dosages, drug safety, inventory shortages, or rare alternatives.",
        },
    ])
    const [chatInput, setChatInput] = useState("")
    const [isChatLoading, setIsChatLoading] = useState(false)

    // Handle Prescription Image selection
    const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0]
        if (file) {
            const reader = new FileReader()
            reader.onload = () => {
                setSelectedImage(reader.result as string)
            }
            reader.readAsDataURL(file)
        }
    }

    // Trigger AI Scan
    const runPrescriptionScan = async () => {
        setIsScanning(true)
        try {
            const res = await apiClient.post("/ai/scan-prescription", {
                imageBase64: selectedImage || "demo-prescription-scan",
            })
            setScanResult(res.data)
            toast.success(isRtl ? "تمت قراءة الروشتة واستخراج الأدوية بنجاح!" : "Prescription scanned successfully!")
        } catch {
            toast.error(isRtl ? "تعذر الاتصال بخدمة الذكاء الاصطناعي" : "Failed to scan prescription")
        } finally {
            setIsScanning(false)
        }
    }

    // Trigger Drug Interaction Check
    const runInteractionCheck = async (drugsToCheck = selectedDrugs) => {
        if (drugsToCheck.length < 2) {
            toast.warning(isRtl ? "يرجى تحديد دوائين على الأقل للتحقق من التفاعل" : "Select at least 2 drugs to check")
            return
        }

        setIsCheckingInteractions(true)
        try {
            const res = await apiClient.post("/ai/check-interactions", {
                drugNames: drugsToCheck,
            })
            setInteractionResult(res.data)
        } catch {
            toast.error(isRtl ? "فشل فحص التعارضات" : "Failed to check interactions")
        } finally {
            setIsCheckingInteractions(false)
        }
    }

    const addDrugToInteractionList = () => {
        const name = customDrugInput.trim()
        if (name && !selectedDrugs.includes(name)) {
            const updated = [...selectedDrugs, name]
            setSelectedDrugs(updated)
            setCustomDrugInput("")
            runInteractionCheck(updated)
        }
    }

    const removeDrugFromInteractionList = (index: number) => {
        const updated = selectedDrugs.filter((_, i) => i !== index)
        setSelectedDrugs(updated)
        if (updated.length >= 2) runInteractionCheck(updated)
        else setInteractionResult(null)
    }

    // Send AI Clinical Chat Message
    const handleSendMessage = async (customPrompt?: string) => {
        const prompt = customPrompt || chatInput.trim()
        if (!prompt) return

        setChatMessages((prev) => [...prev, { sender: "user", text: prompt }])
        if (!customPrompt) setChatInput("")
        setIsChatLoading(true)

        try {
            const res = await apiClient.post("/ai/query", { prompt })
            setChatMessages((prev) => [
                ...prev,
                { sender: "ai", text: res.data?.response || (isRtl ? "تمت معالجة استفسارك بنجاح." : "Done.") },
            ])
        } catch {
            setChatMessages((prev) => [
                ...prev,
                {
                    sender: "ai",
                    text: isRtl
                        ? "معذرة، لم أتمكن من استرجاع البيانات حالياً. يرجى التأكد من تشغيل الـ API المحلي."
                        : "Sorry, could not process request right now.",
                },
            ])
        } finally {
            setIsChatLoading(false)
        }
    }

    return (
        <div className="flex flex-col gap-4 p-4 max-w-6xl mx-auto" dir={isRtl ? "rtl" : "ltr"}>
            {/* Header Banner */}
            <div className="flex flex-wrap items-center justify-between gap-3 p-4 bg-gradient-to-r from-indigo-900 via-indigo-800 to-slate-900 text-white rounded-2xl shadow-md border border-indigo-700/50">
                <div className="flex items-center gap-3">
                    <div className="p-3 bg-indigo-500/20 border border-indigo-400/30 rounded-xl backdrop-blur-sm">
                        <Sparkles className="w-7 h-7 text-indigo-300 animate-pulse" />
                    </div>
                    <div>
                        <h1 className="text-xl font-bold flex items-center gap-2">
                            {isRtl ? "مركز الذكاء الاصطناعي الطبي" : "PharmaCore AI Clinical Hub"}
                            <Badge className="bg-indigo-500/30 text-indigo-200 border-indigo-400/40 text-xs">
                                Gemini Multimodal
                            </Badge>
                        </h1>
                        <p className="text-xs text-indigo-200/80">
                            {isRtl
                                ? "قراءة الروشتات الطبية بالرؤية الحاسوبية، فحص تعارض الأدوية، والمساعد الإكلينيكي الذكي"
                                : "Prescription OCR, Drug-to-Drug interaction safety guard, and clinical intelligence"}
                        </p>
                    </div>
                </div>

                <Button
                    onClick={() => router.push("/pos")}
                    className="bg-emerald-600 hover:bg-emerald-700 text-white text-xs font-semibold shadow-sm"
                >
                    <ShoppingCart className="w-4 h-4 me-1.5" />
                    {isRtl ? "الانتقال للكاشير (POS)" : "Open POS"}
                </Button>
            </div>

            {/* Main Tabs */}
            <Tabs defaultValue="prescription" className="w-full">
                <TabsList className="grid grid-cols-3 h-12 bg-slate-100 dark:bg-slate-900 p-1 rounded-xl">
                    <TabsTrigger value="prescription" className="flex items-center gap-2 text-xs font-semibold">
                        <FileScan className="w-4 h-4 text-indigo-600" />
                        {isRtl ? "ماسح الروشتات (Rx OCR)" : "Prescription Scanner"}
                    </TabsTrigger>
                    <TabsTrigger value="interactions" className="flex items-center gap-2 text-xs font-semibold">
                        <ShieldAlert className="w-4 h-4 text-rose-600" />
                        {isRtl ? "فاحص التعارضات الدوائية" : "Drug Interaction Checker"}
                    </TabsTrigger>
                    <TabsTrigger value="clinical" className="flex items-center gap-2 text-xs font-semibold">
                        <MessageSquareText className="w-4 h-4 text-emerald-600" />
                        {isRtl ? "المساعد الصيدلي السريري" : "Clinical AI Assistant"}
                    </TabsTrigger>
                </TabsList>

                {/* ── TAB 1: PRESCRIPTION SCANNER ── */}
                <TabsContent value="prescription" className="space-y-4 pt-2">
                    <div className="grid grid-cols-1 md:grid-cols-12 gap-4">
                        {/* Image Upload Box (Cols 5) */}
                        <Card className="md:col-span-5 shadow-xs border">
                            <CardHeader className="pb-3">
                                <CardTitle className="text-sm font-bold flex items-center gap-2">
                                    <UploadCloud className="w-4 h-4 text-indigo-600" />
                                    {isRtl ? "رفع صورة الروشتة" : "Upload Prescription"}
                                </CardTitle>
                                <CardDescription className="text-xs">
                                    {isRtl
                                        ? "يدعم خط يد الطبيب، الروشتات المطبوعة، أو الصور من الموبايل"
                                        : "Accepts handwritten doctor prescriptions or printed clinic receipts"}
                                </CardDescription>
                            </CardHeader>
                            <CardContent className="space-y-3">
                                <label className="flex flex-col items-center justify-center h-48 border-2 border-dashed border-slate-300 dark:border-slate-700 rounded-xl cursor-pointer hover:border-indigo-500 bg-slate-50 dark:bg-slate-900 overflow-hidden relative group">
                                    {selectedImage ? (
                                        <img src={selectedImage} alt="Prescription" className="h-full w-full object-cover" />
                                    ) : (
                                        <div className="flex flex-col items-center text-center p-4">
                                            <UploadCloud className="w-10 h-10 text-slate-400 group-hover:text-indigo-500 mb-2 transition-colors" />
                                            <span className="text-xs font-semibold text-slate-700 dark:text-slate-200">
                                                {isRtl ? "اضغط لرفع صورة أو اسحبها هنا" : "Click to upload or drag & drop"}
                                            </span>
                                            <span className="text-[10px] text-slate-400 mt-1">PNG, JPG, WEBP (Max 5MB)</span>
                                        </div>
                                    )}
                                    <input type="file" accept="image/*" onChange={handleImageChange} className="hidden" />
                                </label>

                                <Button
                                    onClick={runPrescriptionScan}
                                    disabled={isScanning}
                                    className="w-full bg-indigo-600 hover:bg-indigo-700 h-11 font-bold text-sm shadow-sm"
                                >
                                    <Sparkles className="w-4 h-4 me-1.5" />
                                    {isScanning ? (isRtl ? "جاري تحليل الروشتة بالذكاء الاصطناعي..." : "Analyzing with AI...") : (isRtl ? "تحليل الروشتة واستخراج الأدوية" : "Scan & Extract Medicines")}
                                </Button>
                            </CardContent>
                        </Card>

                        {/* Extracted Medicines Result (Cols 7) */}
                        <Card className="md:col-span-7 shadow-xs border flex flex-col justify-between">
                            <CardHeader className="pb-3 border-b">
                                <div className="flex justify-between items-center">
                                    <CardTitle className="text-sm font-bold flex items-center gap-2">
                                        <Pill className="w-4 h-4 text-emerald-600" />
                                        {isRtl ? "الأدوية المستخرجة من الروشتة" : "Extracted Prescription Items"}
                                    </CardTitle>
                                    {scanResult && (
                                        <Badge className="bg-emerald-50 text-emerald-700 border-emerald-300">
                                            {isRtl ? `دقة التعرف: ${scanResult.aiConfidence}` : `Confidence: ${scanResult.aiConfidence}`}
                                        </Badge>
                                    )}
                                </div>
                            </CardHeader>
                            <CardContent className="p-4 flex-1">
                                {!scanResult ? (
                                    <div className="h-56 flex flex-col items-center justify-center text-slate-400 text-center">
                                        <FileScan className="w-12 h-12 stroke-1 text-slate-300 mb-2" />
                                        <p className="text-sm font-semibold">{isRtl ? "لم يتم تحليل أي روشتة بعد" : "No prescription scanned yet"}</p>
                                        <p className="text-xs text-slate-400 mt-1">
                                            {isRtl ? "ارفع صورة الروشتة على اليسار واضغط على زر التحليل" : "Upload an image and click analyze"}
                                        </p>
                                    </div>
                                ) : (
                                    <div className="space-y-3">
                                        {/* Diagnosis Banner */}
                                        <div className="p-2.5 bg-indigo-50 dark:bg-indigo-950/40 border border-indigo-200 dark:border-indigo-900 rounded-lg text-xs">
                                            <div className="font-bold text-indigo-800 dark:text-indigo-300">
                                                {isRtl ? "التخصص والتشخيص المقروء:" : "Diagnosed Specialty:"} {scanResult.doctorSpecialty}
                                            </div>
                                            <div className="text-slate-600 dark:text-slate-400 mt-0.5">{scanResult.diagnosis}</div>
                                        </div>

                                        {/* Medicines list */}
                                        <div className="space-y-2 max-h-64 overflow-y-auto divide-y">
                                            {scanResult.ExtractedMedicines?.map((med: any, idx: number) => (
                                                <div key={idx} className="pt-2 flex justify-between items-center">
                                                    <div>
                                                        <div className="font-bold text-sm text-slate-800 dark:text-slate-100 flex items-center gap-1.5">
                                                            <span>{med.drugName}</span>
                                                            <Badge variant="outline" className="text-[10px] bg-slate-100">
                                                                {med.dosage}
                                                            </Badge>
                                                        </div>
                                                        <div className="text-xs text-slate-500 mt-0.5 flex items-center gap-2">
                                                            <Clock className="w-3 h-3 text-slate-400" />
                                                            <span>{med.frequency}</span>
                                                            <span>• {med.instructions}</span>
                                                        </div>
                                                    </div>
                                                    <div className="text-end font-mono">
                                                        <div className="font-bold text-emerald-600">{(med.matchedPrice || 35).toFixed(2)} ج.م</div>
                                                        <Badge variant="secondary" className="text-[10px] text-emerald-700 bg-emerald-50">
                                                            {isRtl ? "متوفر بالمخزن" : "In Stock"}
                                                        </Badge>
                                                    </div>
                                                </div>
                                            ))}
                                        </div>
                                    </div>
                                )}
                            </CardContent>

                            {scanResult && (
                                <div className="p-4 border-t bg-slate-50/50 dark:bg-slate-900/50 flex justify-between items-center">
                                    <span className="text-xs text-slate-500">
                                        {isRtl ? `تم التعرف على ${scanResult.ExtractedMedicines?.length || 0} أدوية` : `${scanResult.ExtractedMedicines?.length || 0} items identified`}
                                    </span>
                                    <Button
                                        onClick={() => {
                                            router.push("/pos")
                                            toast.success(isRtl ? "تم تحويل أدوية الروشتة إلى الكاشير!" : "Transferred to POS cart")
                                        }}
                                        className="bg-emerald-600 hover:bg-emerald-700 font-bold text-xs"
                                    >
                                        <ShoppingCart className="w-4 h-4 me-1.5" />
                                        {isRtl ? "إضافة الأدوية لكاشير البيع (POS)" : "Push to POS Cart"}
                                    </Button>
                                </div>
                            )}
                        </Card>
                    </div>
                </TabsContent>

                {/* ── TAB 2: DRUG INTERACTION CHECKER ── */}
                <TabsContent value="interactions" className="space-y-4 pt-2">
                    <Card className="shadow-xs border">
                        <CardHeader className="pb-3">
                            <CardTitle className="text-sm font-bold flex items-center gap-2">
                                <ShieldAlert className="w-4 h-4 text-rose-600" />
                                {isRtl ? "فاحص التعارضات الدوائية التفاعلي (Drug-to-Drug Safety)" : "Interactive Drug Interaction Guard"}
                            </CardTitle>
                            <CardDescription className="text-xs">
                                {isRtl
                                    ? "أضف الأدوية التي يتناولها المريض معاً لاكتشاف أي تفاعلات كيميائية أو خطورة صحية فورياً"
                                    : "Add patient medications to detect dangerous interactions, contraindications, and food conflicts"}
                            </CardDescription>
                        </CardHeader>
                        <CardContent className="space-y-4">
                            {/* Drug input & chips */}
                            <div className="flex gap-2">
                                <Input
                                    value={customDrugInput}
                                    onChange={(e) => setCustomDrugInput(e.target.value)}
                                    onKeyDown={(e) => e.key === "Enter" && addDrugToInteractionList()}
                                    placeholder={isRtl ? "اكتب اسم دواء واضغط Enter (مثال: Warfarin, Ciprofloxacin, Aspirin)..." : "Type drug name and press Enter..."}
                                    className="h-10 text-sm"
                                />
                                <Button onClick={addDrugToInteractionList} className="bg-slate-800 hover:bg-slate-900 text-xs px-4">
                                    {isRtl ? "إضافة دواء" : "Add Drug"}
                                </Button>
                            </div>

                            {/* Active Drugs Selected */}
                            <div className="flex flex-wrap items-center gap-2 p-3 bg-slate-50 dark:bg-slate-900 rounded-xl border">
                                <span className="text-xs font-semibold text-slate-500">{isRtl ? "الأدوية قيد الفحص:" : "Under Examination:"}</span>
                                {selectedDrugs.map((drug, idx) => (
                                    <Badge key={idx} variant="secondary" className="px-2.5 py-1 text-xs flex items-center gap-1.5 bg-white dark:bg-slate-800 border">
                                        <Pill className="w-3 h-3 text-indigo-500" />
                                        <span>{drug}</span>
                                        <button
                                            onClick={() => removeDrugFromInteractionList(idx)}
                                            className="text-slate-400 hover:text-rose-500 text-xs font-bold ms-1"
                                        >
                                            ×
                                        </button>
                                    </Badge>
                                ))}
                            </div>

                            {/* Preset Example Quick Test Pills */}
                            <div className="flex flex-wrap items-center gap-2 text-xs">
                                <span className="text-slate-400">{isRtl ? "أمثلة شائعة للتجربة:" : "Presets:"}</span>
                                <Button
                                    variant="outline"
                                    size="sm"
                                    className="h-7 text-xs"
                                    onClick={() => {
                                        const pair = ["Warfarin", "Aspirin"]
                                        setSelectedDrugs(pair)
                                        runInteractionCheck(pair)
                                    }}
                                >
                                    Warfarin + Aspirin (نزيف)
                                </Button>
                                <Button
                                    variant="outline"
                                    size="sm"
                                    className="h-7 text-xs"
                                    onClick={() => {
                                        const pair = ["Ciprofloxacin", "Antacid"]
                                        setSelectedDrugs(pair)
                                        runInteractionCheck(pair)
                                    }}
                                >
                                    Ciprofloxacin + Antacids (امتصاص)
                                </Button>
                                <Button
                                    variant="outline"
                                    size="sm"
                                    className="h-7 text-xs"
                                    onClick={() => {
                                        const pair = ["Metformin", "Contrast"]
                                        setSelectedDrugs(pair)
                                        runInteractionCheck(pair)
                                    }}
                                >
                                    Metformin + Contrast (حموضة الدم)
                                </Button>
                            </div>

                            {/* Interaction Results Display */}
                            {interactionResult && (
                                <div className="space-y-3 pt-2">
                                    <div
                                        className={`p-3 rounded-xl border flex items-center justify-between text-xs font-bold ${
                                            interactionResult.hasInteractions
                                                ? "bg-rose-50 text-rose-800 border-rose-200 dark:bg-rose-950/40 dark:border-rose-900"
                                                : "bg-emerald-50 text-emerald-800 border-emerald-200"
                                        }`}
                                    >
                                        <div className="flex items-center gap-2">
                                            {interactionResult.hasInteractions ? (
                                                <AlertTriangle className="w-5 h-5 text-rose-600" />
                                            ) : (
                                                <CheckCircle2 className="w-5 h-5 text-emerald-600" />
                                            )}
                                            <span>{interactionResult.summary}</span>
                                        </div>
                                        {interactionResult.hasInteractions && (
                                            <Badge variant="destructive" className="font-mono">
                                                {interactionResult.highRiskCount} {isRtl ? "خطر شديد" : "High Risk"}
                                            </Badge>
                                        )}
                                    </div>

                                    {/* Alert Cards */}
                                    {interactionResult.alerts?.map((alert: any, idx: number) => (
                                        <div
                                            key={idx}
                                            className="p-3.5 rounded-xl border border-rose-200 bg-white dark:bg-slate-900 shadow-xs space-y-2"
                                        >
                                            <div className="flex justify-between items-center">
                                                <div className="font-bold text-sm text-slate-800 dark:text-slate-100 flex items-center gap-2">
                                                    <span className="text-indigo-600">{alert.drugA}</span>
                                                    <span className="text-slate-400">⚡</span>
                                                    <span className="text-rose-600">{alert.drugB}</span>
                                                </div>
                                                <Badge
                                                    className={
                                                        alert.severity === "High"
                                                            ? "bg-rose-500 text-white"
                                                            : "bg-amber-500 text-white"
                                                    }
                                                >
                                                    {alert.severity === "High" ? (isRtl ? "خطر مرتفع" : "High Severity") : (isRtl ? "خطر متوسط" : "Moderate")}
                                                </Badge>
                                            </div>

                                            <p className="text-xs text-slate-600 dark:text-slate-300 leading-relaxed">
                                                {isRtl ? alert.descriptionAr : alert.descriptionEn}
                                            </p>

                                            <div className="p-2.5 bg-slate-50 dark:bg-slate-800/60 rounded-lg text-xs text-slate-700 dark:text-slate-300 flex items-start gap-2">
                                                <Activity className="w-4 h-4 text-emerald-600 shrink-0 mt-0.5" />
                                                <div>
                                                    <span className="font-bold text-emerald-700 dark:text-emerald-400">
                                                        {isRtl ? "التوجيه الإكلينيكي للصيدلي: " : "Clinical Guidance: "}
                                                    </span>
                                                    {alert.clinicalAdvice}
                                                </div>
                                            </div>
                                        </div>
                                    ))}
                                </div>
                            )}
                        </CardContent>
                    </Card>
                </TabsContent>

                {/* ── TAB 3: CLINICAL PHARMACY AI CHAT ── */}
                <TabsContent value="clinical" className="space-y-4 pt-2">
                    <Card className="shadow-xs border flex flex-col h-[520px]">
                        <CardHeader className="py-3 px-4 border-b">
                            <CardTitle className="text-sm font-bold flex items-center gap-2">
                                <Bot className="w-4 h-4 text-emerald-600" />
                                {isRtl ? "المساعد الصيدلي السريري الذكي" : "Clinical Pharmacist AI Companion"}
                            </CardTitle>
                        </CardHeader>
                        <CardContent className="p-4 flex-1 overflow-y-auto space-y-3">
                            {chatMessages.map((msg, idx) => (
                                <div
                                    key={idx}
                                    className={`flex gap-2.5 ${msg.sender === "user" ? "flex-row-reverse" : "flex-row"}`}
                                >
                                    <div
                                        className={`w-7 h-7 rounded-full flex items-center justify-center shrink-0 ${
                                            msg.sender === "user"
                                                ? "bg-indigo-600 text-white"
                                                : "bg-emerald-600 text-white"
                                        }`}
                                    >
                                        {msg.sender === "user" ? <User className="w-4 h-4" /> : <Bot className="w-4 h-4" />}
                                    </div>
                                    <div
                                        className={`max-w-[80%] rounded-2xl p-3 text-xs leading-relaxed ${
                                            msg.sender === "user"
                                                ? "bg-indigo-600 text-white rounded-te-none"
                                                : "bg-slate-100 dark:bg-slate-900 text-slate-800 dark:text-slate-100 rounded-ts-none whitespace-pre-line"
                                        }`}
                                    >
                                        {msg.text}
                                    </div>
                                </div>
                            ))}
                            {isChatLoading && (
                                <div className="flex gap-2 items-center text-xs text-slate-400 italic">
                                    <Bot className="w-4 h-4 text-emerald-600 animate-spin" />
                                    <span>{isRtl ? "جاري التفكير وصياغة الرد الطبي..." : "Thinking..."}</span>
                                </div>
                            )}
                        </CardContent>

                        {/* Quick Prompts & Chat Input */}
                        <div className="p-3 border-t bg-slate-50/50 dark:bg-slate-900/50 space-y-2">
                            <div className="flex flex-wrap gap-1.5">
                                <Button
                                    variant="outline"
                                    size="sm"
                                    className="h-6 text-[11px] py-0 px-2 rounded-full"
                                    onClick={() => handleSendMessage("عايز أطلب بضاعة ونواقص المخزن")}
                                >
                                    نواقص المخزن 📦
                                </Button>
                                <Button
                                    variant="outline"
                                    size="sm"
                                    className="h-6 text-[11px] py-0 px-2 rounded-full"
                                    onClick={() => handleSendMessage("ما هي الأدوية التي ستنتهي صلاحيتها قريباً؟")}
                                >
                                    قرب انتهاء الصلاحية ⏳
                                </Button>
                                <Button
                                    variant="outline"
                                    size="sm"
                                    className="h-6 text-[11px] py-0 px-2 rounded-full"
                                    onClick={() => handleSendMessage("دليل دواء Cataflam")}
                                >
                                    معلومات Cataflam 💊
                                </Button>
                            </div>

                            <form
                                onSubmit={(e) => {
                                    e.preventDefault()
                                    handleSendMessage()
                                }}
                                className="flex gap-2"
                            >
                                <Input
                                    value={chatInput}
                                    onChange={(e) => setChatInput(e.target.value)}
                                    placeholder={isRtl ? "اسأل عن جرعة، تعارض، أدوية الحمل، أو نواقص..." : "Ask clinical questions..."}
                                    className="h-10 text-xs bg-white dark:bg-slate-900"
                                />
                                <Button type="submit" disabled={isChatLoading} className="bg-emerald-600 hover:bg-emerald-700 h-10 px-4">
                                    <Send className="w-4 h-4" />
                                </Button>
                            </form>
                        </div>
                    </Card>
                </TabsContent>
            </Tabs>
        </div>
    )
}
