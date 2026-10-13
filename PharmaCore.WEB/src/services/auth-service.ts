import apiClient from '@/lib/api-client';
import { z } from 'zod';

export const loginSchema = z.object({
    email: z.string().email({ message: "Invalid email address" }),
    password: z.string().min(6, { message: "Password must be at least 6 characters" }),
});

export type LoginRequest = z.infer<typeof loginSchema>;

export const registerSchema = z.object({
  organizationName: z.string().min(2, { message: "Organization name must be at least 2 characters" }),
  adminName: z.string().min(2, { message: "Name must be at least 2 characters" }),
  email: z.string().email({ message: "Invalid email address" }),
  password: z.string().min(6, { message: "Password must be at least 6 characters" }),
});
export type RegisterRequest = z.infer<typeof registerSchema>;

export interface LoginResponse {
    token: string;
    user: {
        id: string;
        email: string;
        name: string;
        role: string;
    };
    setupCompleted?: boolean;
}

export const authService = {
    login: async (data: LoginRequest): Promise<LoginResponse> => {
        const response = await apiClient.post<LoginResponse>('/auth/login', data);
        return response.data;
    },

    register: async (data: RegisterRequest): Promise<LoginResponse> => {
        const response = await apiClient.post<LoginResponse>('/auth/register', {
            organizationName: data.organizationName,
            adminName: data.adminName,
            adminEmail: data.email,
            password: data.password
        });
        return response.data;
    },

    logout: () => {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
    },

    getToken: () => {
        if (typeof window !== 'undefined') {
            return localStorage.getItem('token');
        }
        return null;
    },

    getUser: () => {
        if (typeof window !== 'undefined') {
            const userStr = localStorage.getItem('user');
            return userStr ? JSON.parse(userStr) : null;
        }
        return null;
    }
};
