export interface WeddingDress {
  id: number;
  name: string;
  description: string;
  price: number;
  salePrice?: number;
  sku: string;
  stock: number;
  designer: string;
  style: string;
  silhouette: string;
  neckline: string;
  sleeveStyle: string;
  color: string;
  fabric: string;
  trainStyle: string;
  isAvailable: boolean;
  isFeatured: boolean;
  createdAt: string;
  updatedAt: string;
  categoryId: number;
  category: Category;
  images: DressImage[];
  sizes: DressSize[];
}

export interface Category {
  id: number;
  name: string;
  description: string;
  imageUrl: string;
  isActive: boolean;
  sortOrder: number;
  createdAt: string;
}

export interface DressImage {
  id: number;
  imageUrl: string;
  altText: string;
  isPrimary: boolean;
  sortOrder: number;
  createdAt: string;
  weddingDressId: number;
}

export interface DressSize {
  id: number;
  size: string;
  stock: number;
  isAvailable: boolean;
  weddingDressId: number;
}

export interface Order {
  id: number;
  orderNumber: string;
  customerName: string;
  customerEmail: string;
  customerPhone: string;
  shippingAddress: string;
  billingAddress: string;
  subTotal: number;
  tax: number;
  shippingCost: number;
  total: number;
  status: string;
  paymentStatus: string;
  notes: string;
  orderDate: string;
  shippedDate?: string;
  deliveredDate?: string;
  orderItems: OrderItem[];
}

export interface OrderItem {
  id: number;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
  size: string;
  specialInstructions: string;
  orderId: number;
  weddingDressId: number;
  weddingDress: WeddingDress;
}

// Authentication types
export interface LoginRequest {
  email: string;
  password: string;
  rememberMe?: boolean;
}

export interface RegisterRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  confirmPassword: string;
  phoneNumber?: string;
}

export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  fullName: string;
  phoneNumber: string;
  roles: string[];
}

export interface UserInfo {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  fullName: string;
  phoneNumber: string;
  roles: string[];
}

export interface AuthResponse {
  token: string;
  refreshToken: string;
  expiresAt: string;
  user: UserInfo;
}

export interface ChangePasswordRequest {
  currentPassword: string;
  newPassword: string;
  confirmNewPassword: string;
} 