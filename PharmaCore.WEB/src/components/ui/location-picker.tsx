"use client"

import { useState, useEffect, useCallback } from "react"
import { MapContainer, TileLayer, Marker, useMapEvents } from "react-leaflet"
import "leaflet/dist/leaflet.css"
import "leaflet-defaulticon-compatibility"
import "leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css"
import { Loader2 } from "lucide-react"

interface LocationPickerProps {
    value: { lat: number; lng: number }
    onChange: (value: { lat: number; lng: number; address?: string; city?: string }) => void
}

function LocationMarker({
    position,
    setPosition,
    onChange,
}: {
    position: { lat: number; lng: number }
    setPosition: (pos: { lat: number; lng: number }) => void
    onChange: (value: { lat: number; lng: number; address?: string; city?: string }) => void
}) {
    const [loading, setLoading] = useState(false)

    const map = useMapEvents({
        click(e) {
            const newPos = { lat: e.latlng.lat, lng: e.latlng.lng }
            setPosition(newPos)
            fetchAddress(newPos)
        },
    })

    useEffect(() => {
        map.flyTo(position, map.getZoom())
    }, [position, map])

    const fetchAddress = async (pos: { lat: number; lng: number }) => {
        setLoading(true)
        try {
            const response = await fetch(
                `https://nominatim.openstreetmap.org/reverse?format=json&lat=${pos.lat}&lon=${pos.lng}&accept-language=en`
            )
            const data = await response.json()

            if (data) {
                const address = data.display_name
                const city = data.address.city || data.address.town || data.address.village || data.address.state || ""

                onChange({
                    lat: pos.lat,
                    lng: pos.lng,
                    address,
                    city,
                })
            } else {
                onChange({ lat: pos.lat, lng: pos.lng })
            }
        } catch (error) {
            console.error("Error fetching address:", error)
            onChange({ lat: pos.lat, lng: pos.lng })
        } finally {
            setLoading(false)
        }
    }

    return (
        <Marker position={position}>
        </Marker>
    )
}

export default function LocationPicker({ value, onChange }: LocationPickerProps) {
    // Default to Cairo if no value provided
    const defaultPosition = { lat: 30.0444, lng: 31.2357 }
    const position = value.lat && value.lng ? value : defaultPosition

    const [currentPosition, setCurrentPosition] = useState(position)

    useEffect(() => {
        if (value.lat && value.lng) {
            setCurrentPosition(value)
        }
    }, [value])

    return (
        <div className="h-[300px] w-full rounded-md overflow-hidden border relative z-0">
            <MapContainer
                center={currentPosition}
                zoom={13}
                scrollWheelZoom={false}
                className="h-full w-full"
            >
                <TileLayer
                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                <LocationMarker
                    position={currentPosition}
                    setPosition={setCurrentPosition}
                    onChange={onChange}
                />
            </MapContainer>
        </div>
    )
}
