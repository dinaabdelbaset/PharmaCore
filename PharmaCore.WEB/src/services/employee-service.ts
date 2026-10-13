import apiClient from '@/lib/api-client';

export interface EmployeeRole {
    id: string;
    name: string;
    description: string | null;
    parentRoleId: string | null;
    isSystemRole: boolean;
    sortOrder: number;
}

export interface FlatRole {
    id: string;
    name: string;
    description: string | null;
    display: string;
    sortOrder: number;
}

export interface Employee {
    id: string;
    employeeCode: string;
    fullName: string;
    jobTitle: string | null;
    grade: string | null;
    role: string;
    isActive: boolean;
    orgId: string;
    branchId: string | null;
    roleId: string | null;
}

export interface CreateEmployeeRequest {
    fullName: string;
    employeeCode?: string;
    pin: string;
    role: string;
    roleId?: string;
    jobTitle?: string;
    grade?: string;
    tenantId?: string;
    regionId?: string;
    branchId?: string;
}

export interface UpdateEmployeeRequest {
    fullName: string;
    role: string;
    roleId?: string;
    jobTitle?: string;
    grade?: string;
    isActive: boolean;
    branchId?: string;
    regionId?: string;
}

export interface SetPermissionsRequest {
    roleId?: string;
    permissions: string[];
}

export const employeeService = {
    // ── Employees API ──
    getEmployees: async (): Promise<Employee[]> => {
        const response = await apiClient.get<Employee[]>('/employees');
        return response.data;
    },

    createEmployee: async (data: CreateEmployeeRequest): Promise<Employee> => {
        const response = await apiClient.post<Employee>('/employees', data);
        return response.data;
    },

    updateEmployee: async (id: string, data: UpdateEmployeeRequest): Promise<{ message: string }> => {
        const response = await apiClient.put<{ message: string }>(`/employees/${id}`, data);
        return response.data;
    },

    deleteEmployee: async (id: string): Promise<{ message: string }> => {
        const response = await apiClient.delete<{ message: string }>(`/employees/${id}`);
        return response.data;
    },

    resetPin: async (id: string, newPin: string): Promise<{ message: string }> => {
        const response = await apiClient.put<{ message: string }>(`/employees/${id}/pin`, { newPin });
        return response.data;
    },

    // ── Roles API ──
    getRoles: async (): Promise<EmployeeRole[]> => {
        const response = await apiClient.get<EmployeeRole[]>('/employee-roles');
        return response.data;
    },

    getFlatRoles: async (): Promise<FlatRole[]> => {
        const response = await apiClient.get<FlatRole[]>('/employee-roles/flat');
        return response.data;
    },

    getRolePermissions: async (roleId: string): Promise<string[]> => {
        const response = await apiClient.get<string[]>(`/employee-roles/${roleId}/permissions`);
        return response.data;
    },

    createRole: async (data: { name: string; parentRoleId: string | null; permissions: string[] }): Promise<EmployeeRole> => {
        const response = await apiClient.post<EmployeeRole>('/employee-roles', data);
        return response.data;
    },

    updateRole: async (id: string, data: { name: string; parentRoleId: string | null; permissions: string[] }): Promise<void> => {
        await apiClient.put(`/employee-roles/${id}`, data);
    },

    deleteRole: async (id: string): Promise<void> => {
        await apiClient.delete(`/employee-roles/${id}`);
    },

    seedDefaultRoles: async (): Promise<{ seeded: boolean; message: string }> => {
        const response = await apiClient.post<{ seeded: boolean; message: string }>('/employee-roles/seed-defaults');
        return response.data;
    },

    // ── Permissions API ──
    getAvailablePermissions: async (): Promise<Record<string, string[]>> => {
        const response = await apiClient.get<Record<string, string[]>>('/employee-permissions/available');
        return response.data;
    },

    getEmployeePermissions: async (employeeId: string): Promise<string[]> => {
        const response = await apiClient.get<string[]>(`/employee-permissions/${employeeId}`);
        return response.data;
    },

    setEmployeePermissions: async (employeeId: string, data: SetPermissionsRequest): Promise<{ count: number; permissions: string[] }> => {
        const response = await apiClient.put<{ count: number; permissions: string[] }>(`/employee-permissions/${employeeId}`, data);
        return response.data;
    }
};
