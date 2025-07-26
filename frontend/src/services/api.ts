import axios from 'axios';
import { WeddingDress, Category, Order, LoginRequest, RegisterRequest, AuthResponse, User, ChangePasswordRequest } from '../types';

const API_BASE_URL = process.env.REACT_APP_API_URL || '/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true,
});

// Single request interceptor for auth and logging
api.interceptors.request.use(
  (config) => {
    // Add auth token
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    
    // Logging
    console.log(`API Request: ${config.method?.toUpperCase()} ${config.baseURL}${config.url}`);
    console.log('Request headers:', config.headers);
    console.log('withCredentials:', config.withCredentials);
    
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor for error handling
api.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    console.error('🚨 API Error Details:');
    console.error('   Message:', error.message);
    console.error('   Code:', error.code);
    console.error('   Config:', error.config);
    console.error('   Request:', error.request);
    console.error('   Response:', error.response);
    
    if (error.message?.includes('CORS')) {
      console.error('🚨 CORS ERROR DETECTED!');
      console.error('   This means the browser blocked the request due to CORS policy');
      console.error('   Check if backend is running and CORS is configured properly');
    }
    
    // Handle authentication errors
    if (error.response?.status === 401) {
      // Remove token and redirect to login
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      window.location.href = '/login';
    }
    
    return Promise.reject(error);
  }
);

// Wedding Dresses API
export const dressesApi = {
  getAll: (search?: string, categoryId?: number) => {
    const params = new URLSearchParams();
    if (search) params.append('search', search);
    if (categoryId) params.append('categoryId', categoryId.toString());
    return api.get<WeddingDress[]>(`/dresses?${params.toString()}`);
  },
  getById: (id: number) => api.get<WeddingDress>(`/dresses/${id}`),
  getFeatured: () => api.get<WeddingDress[]>('/dresses/featured'),
  create: (dress: Partial<WeddingDress>) => api.post<WeddingDress>('/dresses', dress),
  update: (id: number, dress: Partial<WeddingDress>) => api.put<WeddingDress>(`/dresses/${id}`, dress),
  delete: (id: number) => api.delete(`/dresses/${id}`),
};

// Categories API
export const categoriesApi = {
  getAll: () => api.get<Category[]>('/categories'),
  getById: (id: number) => api.get<Category>(`/categories/${id}`),
  create: (category: Partial<Category>) => api.post<Category>('/categories', category),
  update: (id: number, category: Partial<Category>) => api.put<Category>(`/categories/${id}`, category),
  delete: (id: number) => api.delete(`/categories/${id}`),
};

// Orders API
export const ordersApi = {
  getAll: (status?: string) => {
    const params = status ? `?status=${status}` : '';
    return api.get<Order[]>(`/orders${params}`);
  },
  getById: (id: number) => api.get<Order>(`/orders/${id}`),
  getByOrderNumber: (orderNumber: string) => api.get<Order>(`/orders/by-number/${orderNumber}`),
  create: (order: Partial<Order>) => api.post<Order>('/orders', order),
  update: (id: number, order: Partial<Order>) => api.put<Order>(`/orders/${id}`, order),
  delete: (id: number) => api.delete(`/orders/${id}`),
};

// Authentication API
export const authApi = {
  login: (request: LoginRequest) => api.post<AuthResponse>('/auth/login', request),
  register: (request: RegisterRequest) => api.post<AuthResponse>('/auth/register', request),
  getUserInfo: () => api.get<User>('/auth/me'),
  changePassword: (request: ChangePasswordRequest) => api.post('/auth/change-password', request),
  logout: () => api.post('/auth/logout'),
  checkAuth: () => api.get('/auth/check'),
};

export default api; 