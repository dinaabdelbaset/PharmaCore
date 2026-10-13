"use client"

import { useState, useEffect, useCallback } from "react"

interface UnreadMessages {
    count: number
    conversations: string[]
}

export function useMessageNotifications() {
    const [unreadCount, setUnreadCount] = useState(0)

    // Play notification sound
    const playNotificationSound = useCallback(() => {
        // Create a simple notification beep using Web Audio API
        const audioContext = new (window.AudioContext || (window as any).webkitAudioContext)()

        const oscillator = audioContext.createOscillator()
        const gainNode = audioContext.createGain()

        oscillator.connect(gainNode)
        gainNode.connect(audioContext.destination)

        oscillator.frequency.value = 800 // Frequency in Hz
        oscillator.type = 'sine'

        gainNode.gain.setValueAtTime(0.3, audioContext.currentTime)
        gainNode.gain.exponentialRampToValueAtTime(0.01, audioContext.currentTime + 0.5)

        oscillator.start(audioContext.currentTime)
        oscillator.stop(audioContext.currentTime + 0.5)
    }, [])

    const checkNotifications = useCallback(() => {
        const isDemo = typeof window !== 'undefined' && localStorage.getItem("demoMode") === "true"
        if (!isDemo) {
            setUnreadCount(0)
            return false
        }
        setUnreadCount(4)
        return true
    }, [])

    // Simulate receiving new messages (in real app, this would be WebSocket or polling)
    useEffect(() => {
        const active = checkNotifications()
        if (!active) return

        // Simulate receiving a new message after 10 seconds
        const timer = setTimeout(() => {
            setUnreadCount(prev => prev + 1)
            playNotificationSound()

            // Show browser notification if permission granted
            if (Notification.permission === 'granted') {
                new Notification('New Message', {
                    body: 'You have a new message in PharmaCore',
                    icon: '/favicon.ico'
                })
            }
        }, 10000)

        return () => clearTimeout(timer)
    }, [playNotificationSound, checkNotifications])

    // Listen to changes in demo mode setting
    useEffect(() => {
        window.addEventListener("storage", checkNotifications)
        window.addEventListener("demoModeChanged", checkNotifications)
        return () => {
            window.removeEventListener("storage", checkNotifications)
            window.removeEventListener("demoModeChanged", checkNotifications)
        }
    }, [checkNotifications])

    // Request notification permission on mount
    useEffect(() => {
        if ('Notification' in window && Notification.permission === 'default') {
            Notification.requestPermission()
        }
    }, [])

    const markAsRead = useCallback((conversationId: string) => {
        setUnreadCount(prev => Math.max(0, prev - 1))
    }, [])

    const markAllAsRead = useCallback(() => {
        setUnreadCount(0)
    }, [])

    return {
        unreadCount,
        markAsRead,
        markAllAsRead,
        playNotificationSound
    }
}
