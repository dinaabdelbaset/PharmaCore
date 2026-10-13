export interface Expense {
    id: string;
    branchId?: string;
    branchName?: string;
    amount: number;
    entryType: string;
    description?: string;
    employeeName: string;
    isBank: boolean;
    date: string;
}

export interface CreateExpenseRequest {
    branchId?: string;
    amount: number;
    category: string;
    description?: string;
    isBank: boolean;
    date: string;
}
