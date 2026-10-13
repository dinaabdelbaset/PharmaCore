"use client"

import { useEffect, useState } from "react"
import { useForm } from "react-hook-form"
import { zodResolver } from "@hookform/resolvers/zod"
import * as z from "zod"
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
    DialogFooter,
} from "@/components/ui/dialog"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Textarea } from "@/components/ui/textarea"
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import { Label } from "@/components/ui/label"
import { Branch, BranchStatus, QualityCertificate } from "@/types/branch"
import { Plus, X, Trash2, Upload, Download, FileText } from "lucide-react"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import dynamic from "next/dynamic"

const LocationPicker = dynamic(() => import("@/components/ui/location-picker"), {
    ssr: false,
    loading: () => <div className="h-[300px] w-full bg-muted animate-pulse rounded-md" />,
})

const branchSchema = z.object({
    code: z.string().min(1, "Code is required"),
    name: z.string().min(1, "Name is required"),
    nameAr: z.string().optional(),
    address: z.string().min(1, "Address is required"),
    city: z.string().min(1, "City is required"),
    email: z.string().email("Invalid email").optional().or(z.literal("")),
    status: z.nativeEnum(BranchStatus),
    description: z.string().optional(),
    descriptionAr: z.string().optional(),
    openingHours: z.string().optional(),
    establishedDate: z.string().optional(),
    totalArea: z.string().optional(),
    latitude: z.string().optional(),
    longitude: z.string().optional(),
    managerName: z.string().optional(),
    managerPhone: z.string().optional(),
    managerEmail: z.string().email("Invalid email").optional().or(z.literal("")),
})

interface BranchDialogProps {
    open: boolean
    onOpenChange: (open: boolean) => void
    onSave: (data: any) => void
    branch: Branch | null
}

export function BranchDialog({
    open,
    onOpenChange,
    onSave,
    branch,
}: BranchDialogProps) {
    const [phones, setPhones] = useState<string[]>([""])
    const [certificates, setCertificates] = useState<QualityCertificate[]>([])

    const form = useForm<z.infer<typeof branchSchema>>({
        resolver: zodResolver(branchSchema),
        defaultValues: {
            code: "",
            name: "",
            nameAr: "",
            address: "",
            city: "",
            email: "",
            status: BranchStatus.ACTIVE,
            description: "",
            descriptionAr: "",
            openingHours: "",
            establishedDate: "",
            totalArea: "",
            latitude: "",
            longitude: "",
            managerName: "",
            managerPhone: "",
            managerEmail: "",
        },
    })

    useEffect(() => {
        if (branch) {
            form.reset({
                code: branch.code,
                name: branch.name,
                nameAr: branch.nameAr || "",
                address: branch.address,
                city: branch.city,
                email: branch.email || "",
                status: branch.status,
                description: branch.description || "",
                descriptionAr: branch.descriptionAr || "",
                openingHours: branch.openingHours || "",
                establishedDate: branch.establishedDate || "",
                totalArea: branch.totalArea?.toString() || "",
                latitude: branch.latitude?.toString() || "",
                longitude: branch.longitude?.toString() || "",
                managerName: branch.manager?.name || "",
                managerPhone: branch.manager?.phone || "",
                managerEmail: branch.manager?.email || "",
            })
            setPhones(branch.phones && branch.phones.length > 0 ? branch.phones : [""])
            setCertificates(branch.qualityCertificates || [])
        } else {
            form.reset({
                code: "",
                name: "",
                nameAr: "",
                address: "",
                city: "",
                email: "",
                status: BranchStatus.ACTIVE,
                description: "",
                descriptionAr: "",
                openingHours: "",
                establishedDate: "",
                totalArea: "",
                latitude: "",
                longitude: "",
                managerName: "",
                managerPhone: "",
                managerEmail: "",
            })
            setPhones([""])
            setCertificates([])
        }
    }, [branch, form])

    const addPhone = () => {
        setPhones([...phones, ""])
    }

    const removePhone = (index: number) => {
        if (phones.length > 1) {
            setPhones(phones.filter((_, i) => i !== index))
        }
    }

    const updatePhone = (index: number, value: string) => {
        const newPhones = [...phones]
        newPhones[index] = value
        setPhones(newPhones)
    }

    const addCertificate = () => {
        setCertificates([
            ...certificates,
            {
                id: String(Date.now()),
                name: "",
                issuedBy: "",
                issuedDate: "",
                expiryDate: "",
                certificateUrl: "",
            },
        ])
    }

    const removeCertificate = (index: number) => {
        setCertificates(certificates.filter((_, i) => i !== index))
    }

    const updateCertificate = (index: number, field: keyof QualityCertificate, value: string) => {
        const newCertificates = [...certificates]
        newCertificates[index] = { ...newCertificates[index], [field]: value }
        setCertificates(newCertificates)
    }

    const onSubmit = (data: z.infer<typeof branchSchema>) => {
        const payload: any = {
            ...data,
            phones: phones.filter((p) => p.trim() !== ""),
            qualityCertificates: certificates.filter((c) => c.name.trim() !== ""),
            totalArea: data.totalArea ? parseFloat(data.totalArea) : undefined,
            latitude: data.latitude ? parseFloat(data.latitude) : undefined,
            longitude: data.longitude ? parseFloat(data.longitude) : undefined,
        }

        if (data.managerName || data.managerPhone) {
            payload.manager = {
                id: branch?.manager?.id || String(Date.now()),
                name: data.managerName || "",
                phone: data.managerPhone || "",
                email: data.managerEmail || "",
            }
        }

        delete payload.managerName
        delete payload.managerPhone
        delete payload.managerEmail

        onSave(payload)
        form.reset()
        setPhones([""])
        setCertificates([])
    }

    return (
        <Dialog open={open} onOpenChange={onOpenChange}>
            <DialogContent className="max-w-4xl max-h-[90vh] overflow-y-auto">
                <DialogHeader>
                    <DialogTitle>{branch ? "Edit Branch" : "Add New Branch"}</DialogTitle>
                    <DialogDescription>
                        {branch
                            ? "Update branch information"
                            : "Fill in the details to create a new branch"}
                    </DialogDescription>
                </DialogHeader>

                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6">
                        <Tabs defaultValue="basic" className="w-full">
                            <TabsList className="grid w-full grid-cols-5">
                                <TabsTrigger value="basic">Basic Info</TabsTrigger>
                                <TabsTrigger value="contact">Contact</TabsTrigger>
                                <TabsTrigger value="location">Location</TabsTrigger>
                                <TabsTrigger value="additional">Additional</TabsTrigger>
                                <TabsTrigger value="certificates">Certificates</TabsTrigger>
                            </TabsList>

                            <TabsContent value="basic" className="space-y-4 mt-4">
                                <div className="grid grid-cols-2 gap-4">
                                    <FormField
                                        control={form.control}
                                        name="code"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel>Code *</FormLabel>
                                                <FormControl>
                                                    <Input placeholder="BR001" {...field} />
                                                </FormControl>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />

                                    <FormField
                                        control={form.control}
                                        name="status"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel>Status *</FormLabel>
                                                <Select
                                                    onValueChange={field.onChange}
                                                    value={field.value}
                                                >
                                                    <FormControl>
                                                        <SelectTrigger>
                                                            <SelectValue placeholder="Select status" />
                                                        </SelectTrigger>
                                                    </FormControl>
                                                    <SelectContent>
                                                        <SelectItem value={BranchStatus.ACTIVE}>Active</SelectItem>
                                                        <SelectItem value={BranchStatus.INACTIVE}>Inactive</SelectItem>
                                                        <SelectItem value={BranchStatus.PENDING}>Pending</SelectItem>
                                                    </SelectContent>
                                                </Select>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />
                                </div>

                                <FormField
                                    control={form.control}
                                    name="name"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>Name (English) *</FormLabel>
                                            <FormControl>
                                                <Input placeholder="Main Branch" {...field} />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />

                                <FormField
                                    control={form.control}
                                    name="nameAr"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>Name (Arabic)</FormLabel>
                                            <FormControl>
                                                <Input placeholder="الفرع الرئيسي" {...field} dir="rtl" />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />

                                <FormField
                                    control={form.control}
                                    name="description"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>Description</FormLabel>
                                            <FormControl>
                                                <Textarea placeholder="Branch description..." {...field} />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />

                                <FormField
                                    control={form.control}
                                    name="descriptionAr"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>Description (Arabic)</FormLabel>
                                            <FormControl>
                                                <Textarea placeholder="وصف الفرع..." {...field} dir="rtl" />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />
                            </TabsContent>

                            <TabsContent value="contact" className="space-y-4 mt-4">
                                <div>
                                    <Label>Phone Numbers *</Label>
                                    <div className="space-y-2 mt-2">
                                        {phones.map((phone, index) => (
                                            <div key={index} className="flex gap-2">
                                                <Input
                                                    placeholder="+20 123 456 7890"
                                                    value={phone}
                                                    onChange={(e) => updatePhone(index, e.target.value)}
                                                />
                                                {phones.length > 1 && (
                                                    <Button
                                                        type="button"
                                                        variant="outline"
                                                        size="icon"
                                                        onClick={() => removePhone(index)}
                                                    >
                                                        <X className="h-4 w-4" />
                                                    </Button>
                                                )}
                                            </div>
                                        ))}
                                        <Button
                                            type="button"
                                            variant="outline"
                                            size="sm"
                                            onClick={addPhone}
                                            className="w-full"
                                        >
                                            <Plus className="h-4 w-4 mr-2" />
                                            Add Phone Number
                                        </Button>
                                    </div>
                                </div>

                                <FormField
                                    control={form.control}
                                    name="email"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>Email</FormLabel>
                                            <FormControl>
                                                <Input
                                                    type="email"
                                                    placeholder="branch@pharmacore.com"
                                                    {...field}
                                                />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />

                                <div className="border-t pt-4 mt-4">
                                    <h4 className="font-semibold mb-3">Manager Information</h4>

                                    <FormField
                                        control={form.control}
                                        name="managerName"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel>Manager Name</FormLabel>
                                                <FormControl>
                                                    <Input placeholder="Ahmed Hassan" {...field} />
                                                </FormControl>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />

                                    <div className="grid grid-cols-2 gap-4 mt-4">
                                        <FormField
                                            control={form.control}
                                            name="managerPhone"
                                            render={({ field }) => (
                                                <FormItem>
                                                    <FormLabel>Manager Phone</FormLabel>
                                                    <FormControl>
                                                        <Input placeholder="+20 123 456 7890" {...field} />
                                                    </FormControl>
                                                    <FormMessage />
                                                </FormItem>
                                            )}
                                        />

                                        <FormField
                                            control={form.control}
                                            name="managerEmail"
                                            render={({ field }) => (
                                                <FormItem>
                                                    <FormLabel>Manager Email</FormLabel>
                                                    <FormControl>
                                                        <Input
                                                            type="email"
                                                            placeholder="manager@pharmacore.com"
                                                            {...field}
                                                        />
                                                    </FormControl>
                                                    <FormMessage />
                                                </FormItem>
                                            )}
                                        />
                                    </div>
                                </div>
                            </TabsContent>

                            <TabsContent value="location" className="space-y-4 mt-4">
                                <div className="mb-4">
                                    <Label className="mb-2 block">Location Map</Label>
                                    <LocationPicker
                                        value={{
                                            lat: parseFloat(form.watch("latitude") || "0"),
                                            lng: parseFloat(form.watch("longitude") || "0"),
                                        }}
                                        onChange={(val) => {
                                            form.setValue("latitude", val.lat.toString())
                                            form.setValue("longitude", val.lng.toString())
                                            if (val.address) form.setValue("address", val.address)
                                            if (val.city) form.setValue("city", val.city)
                                        }}
                                    />
                                </div>

                                <FormField
                                    control={form.control}
                                    name="city"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>City *</FormLabel>
                                            <FormControl>
                                                <Input placeholder="Cairo" {...field} />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />

                                <FormField
                                    control={form.control}
                                    name="address"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>Address *</FormLabel>
                                            <FormControl>
                                                <Textarea placeholder="123 Street Name..." {...field} />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />

                                <div className="grid grid-cols-2 gap-4">
                                    <FormField
                                        control={form.control}
                                        name="latitude"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel>Latitude</FormLabel>
                                                <FormControl>
                                                    <Input
                                                        type="number"
                                                        step="any"
                                                        placeholder="30.0444"
                                                        {...field}
                                                    />
                                                </FormControl>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />

                                    <FormField
                                        control={form.control}
                                        name="longitude"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel>Longitude</FormLabel>
                                                <FormControl>
                                                    <Input
                                                        type="number"
                                                        step="any"
                                                        placeholder="31.2357"
                                                        {...field}
                                                    />
                                                </FormControl>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />
                                </div>
                            </TabsContent>

                            <TabsContent value="additional" className="space-y-4 mt-4">
                                <FormField
                                    control={form.control}
                                    name="openingHours"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>Opening Hours</FormLabel>
                                            <FormControl>
                                                <Input placeholder="9 AM - 9 PM" {...field} />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />

                                <div className="grid grid-cols-2 gap-4">
                                    <FormField
                                        control={form.control}
                                        name="establishedDate"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel>Established Date</FormLabel>
                                                <FormControl>
                                                    <Input type="date" {...field} />
                                                </FormControl>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />

                                    <FormField
                                        control={form.control}
                                        name="totalArea"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel>Total Area (m²)</FormLabel>
                                                <FormControl>
                                                    <Input
                                                        type="number"
                                                        placeholder="250"
                                                        {...field}
                                                    />
                                                </FormControl>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />
                                </div>
                            </TabsContent>

                            <TabsContent value="certificates" className="space-y-4 mt-4">
                                <div className="space-y-4">
                                    {certificates.map((cert, index) => (
                                        <Card key={index}>
                                            <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                                                <CardTitle className="text-sm font-medium">
                                                    Certificate #{index + 1}
                                                </CardTitle>
                                                <Button
                                                    type="button"
                                                    variant="ghost"
                                                    size="sm"
                                                    onClick={() => removeCertificate(index)}
                                                >
                                                    <Trash2 className="h-4 w-4 text-red-500" />
                                                </Button>
                                            </CardHeader>
                                            <CardContent>
                                                <div className="space-y-4">
                                                    {/* PDF Preview & Upload Section */}
                                                    <div className="w-full border-2 border-dashed rounded-lg p-4 flex flex-col items-center justify-center bg-muted/50 min-h-[200px]">
                                                        {cert.certificateUrl ? (
                                                            <div className="w-full space-y-2">
                                                                <div className="flex items-center justify-between mb-2">
                                                                    <div className="flex items-center text-sm text-muted-foreground">
                                                                        <FileText className="h-4 w-4 mr-2" />
                                                                        Certificate Preview
                                                                    </div>
                                                                    <Button
                                                                        type="button"
                                                                        variant="outline"
                                                                        size="sm"
                                                                        asChild
                                                                    >
                                                                        <a href={cert.certificateUrl} download target="_blank" rel="noreferrer">
                                                                            <Download className="h-4 w-4 mr-2" />
                                                                            Download PDF
                                                                        </a>
                                                                    </Button>
                                                                </div>
                                                                <iframe
                                                                    src={cert.certificateUrl}
                                                                    className="w-full h-[300px] rounded-md border bg-white"
                                                                    title={`Certificate ${index + 1}`}
                                                                />
                                                            </div>
                                                        ) : (
                                                            <div className="text-center text-muted-foreground">
                                                                <FileText className="h-10 w-10 mx-auto mb-2 opacity-50" />
                                                                <p>No certificate file uploaded</p>
                                                            </div>
                                                        )}

                                                        <div className="mt-4 w-full max-w-xs">
                                                            <Label htmlFor={`cert-file-${index}`} className="cursor-pointer">
                                                                <div className="flex items-center justify-center w-full px-4 py-2 border border-transparent text-sm font-medium rounded-md text-primary-foreground bg-primary hover:bg-primary/90 shadow-sm">
                                                                    <Upload className="h-4 w-4 mr-2" />
                                                                    {cert.certificateUrl ? "Replace File" : "Upload PDF"}
                                                                </div>
                                                            </Label>
                                                            <Input
                                                                id={`cert-file-${index}`}
                                                                type="file"
                                                                accept="application/pdf"
                                                                className="hidden"
                                                                onChange={(e) => {
                                                                    const file = e.target.files?.[0]
                                                                    if (file) {
                                                                        const url = URL.createObjectURL(file)
                                                                        updateCertificate(index, "certificateUrl", url)
                                                                    }
                                                                }}
                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="grid grid-cols-2 gap-4">
                                                        <div className="space-y-2">
                                                            <Label>Name</Label>
                                                            <Input
                                                                value={cert.name}
                                                                onChange={(e) => updateCertificate(index, "name", e.target.value)}
                                                                placeholder="ISO 9001"
                                                            />
                                                        </div>
                                                        <div className="space-y-2">
                                                            <Label>Issued By</Label>
                                                            <Input
                                                                value={cert.issuedBy}
                                                                onChange={(e) => updateCertificate(index, "issuedBy", e.target.value)}
                                                                placeholder="Organization Name"
                                                            />
                                                        </div>
                                                        <div className="space-y-2">
                                                            <Label>Issued Date</Label>
                                                            <Input
                                                                type="date"
                                                                value={cert.issuedDate}
                                                                onChange={(e) => updateCertificate(index, "issuedDate", e.target.value)}
                                                            />
                                                        </div>
                                                        <div className="space-y-2">
                                                            <Label>Expiry Date</Label>
                                                            <Input
                                                                type="date"
                                                                value={cert.expiryDate || ""}
                                                                onChange={(e) => updateCertificate(index, "expiryDate", e.target.value)}
                                                            />
                                                        </div>
                                                    </div>
                                                </div>
                                            </CardContent>
                                        </Card>
                                    ))}
                                    <Button
                                        type="button"
                                        variant="outline"
                                        onClick={addCertificate}
                                        className="w-full"
                                    >
                                        <Plus className="h-4 w-4 mr-2" />
                                        Add Certificate
                                    </Button>
                                </div>
                            </TabsContent>
                        </Tabs>

                        <DialogFooter>
                            <Button
                                type="button"
                                variant="outline"
                                onClick={() => onOpenChange(false)}
                            >
                                Cancel
                            </Button>
                            <Button type="submit">{branch ? "Update" : "Create"} Branch</Button>
                        </DialogFooter>
                    </form>
                </Form>
            </DialogContent>
        </Dialog>
    )
}
