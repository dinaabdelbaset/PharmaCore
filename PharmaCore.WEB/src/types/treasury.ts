export interface TreasuryEntry {
    id: string
    branchId?: string
    branchName?: string
    amount: number
    entryType: string
    description?: string
    employeeName: string
    isBank: boolean
    date: string
}

export interface TreasuryBalances {
    totalCash: number
    totalBank: number
    netFlowChart: {
        date: string
        netFlow: number
        cashFlow: number
        bankFlow: number
    }[]
}

export interface CreateTreasuryEntryRequest {
    amount: number
    isBank: boolean
    entryType: string
    description?: string
    branchId?: string
    date?: string
}
