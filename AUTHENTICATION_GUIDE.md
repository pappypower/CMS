# Wedding Dress CMS - Authentication System Guide

## 🔐 Authentication Overview

The Wedding Dress CMS now includes a comprehensive authentication system with JWT tokens, role-based access control, and a beautiful login/register interface.

## 🏗️ Backend Authentication Features

### JWT Token Authentication
- **Secure JWT tokens** with configurable expiration
- **Token validation** middleware
- **Automatic token refresh** capability
- **Role-based claims** in tokens

### User Management
- **ASP.NET Core Identity** integration
- **Custom User model** with additional properties
- **Role-based authorization** (Admin, Manager, User)
- **Password validation** and security

### API Security
- **Protected endpoints** with `[Authorize]` attributes
- **Role-based access control** for sensitive operations
- **Swagger integration** with JWT authentication support

## 🎨 Frontend Authentication Features

### Beautiful UI Components
- **Modern login form** with elegant styling
- **Registration form** with validation
- **Protected routes** with role checking
- **User profile menu** in navigation
- **Responsive design** for all devices

### State Management
- **React Context** for global auth state
- **Local storage** for token persistence
- **Automatic logout** on token expiration
- **Loading states** and error handling

## 🚀 Getting Started

### Default Admin Account
- **Email**: `admin@weddingdresscms.com`
- **Password**: `Admin123!`
- **Role**: Admin (full access)

### User Roles & Permissions

#### 🔴 Admin
- Full access to all features
- Can create, edit, and delete everything
- User management capabilities

#### 🟡 Manager  
- Can create and edit dresses
- Can manage categories
- Can update orders
- Cannot delete orders

#### 🟢 User
- Can view dresses and categories
- Can create new orders
- Can view their own orders
- Read-only access to most features

## 🛡️ Security Features

### Password Requirements
- Minimum 6 characters
- Must contain uppercase letter
- Must contain lowercase letter  
- Must contain digit
- Special characters optional

### JWT Configuration
- **Issuer**: WeddingDressCMS
- **Audience**: WeddingDressCMS-Users
- **Expiration**: 24 hours
- **Secure key**: 256-bit encryption

### API Protection
- **HTTPS enforcement** in production
- **CORS policy** for React frontend
- **Request validation** and sanitization
- **Error handling** without information leakage

## 📱 Frontend Routes

### Public Routes
- `/login` - User login
- `/register` - User registration

### Protected Routes
- `/` - Dashboard (All users)
- `/dresses` - View dresses (All users)
- `/dresses/new` - Add dress (Admin, Manager only)
- `/dresses/edit/:id` - Edit dress (Admin, Manager only)
- `/categories` - View categories (All users)
- `/categories/new` - Add category (Admin, Manager only)
- `/categories/edit/:id` - Edit category (Admin, Manager only)
- `/orders` - View orders (All users)
- `/orders/new` - Create order (All users)
- `/orders/edit/:id` - Edit order (Admin, Manager only)

## 🔧 API Endpoints

### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration  
- `GET /api/auth/me` - Get current user info
- `POST /api/auth/change-password` - Change password
- `POST /api/auth/logout` - User logout
- `GET /api/auth/check` - Verify token

### Protected Endpoints
All existing CRUD endpoints now require authentication:
- Viewing data requires any valid user
- Creating/editing requires Admin or Manager role
- Deleting requires Admin role (orders)

## 🎯 Usage Examples

### Login Process
1. User enters email and password
2. Frontend calls `/api/auth/login`
3. Backend validates credentials
4. JWT token returned with user info
5. Token stored in localStorage
6. User redirected to dashboard

### Role Checking
```typescript
// Example: Check if user can edit dresses
const canEditDresses = user?.roles.includes('Admin') || user?.roles.includes('Manager');
```

### Protected API Calls
```typescript
// Token automatically added to requests
const response = await dressesApi.create(newDress);
```

## 🔒 Swagger Integration

The API documentation now includes:
- **Bearer token authentication**
- **Interactive testing** with JWT
- **Role-based endpoint documentation**
- **Security scheme visualization**

### Using Swagger
1. Navigate to `https://localhost:7000/swagger`
2. Click "Authorize" button
3. Enter: `Bearer <your-jwt-token>`
4. Test protected endpoints

## 🚨 Error Handling

### Common Scenarios
- **401 Unauthorized**: Invalid or expired token
- **403 Forbidden**: Insufficient permissions
- **400 Bad Request**: Invalid login credentials
- **500 Server Error**: System error

### Automatic Handling
- **Token expiration**: Auto-logout and redirect to login
- **Network errors**: User-friendly error messages
- **Validation errors**: Field-specific error display

## 📈 Future Enhancements

Potential improvements:
- **Email verification** for new accounts
- **Password reset** functionality
- **Two-factor authentication** (2FA)
- **Session management** and device tracking
- **Activity logging** and audit trails
- **Social login** integration (Google, Facebook)

## 🎉 Success!

Your Wedding Dress CMS now has enterprise-grade authentication with:
- ✅ Secure JWT implementation
- ✅ Beautiful login/register UI
- ✅ Role-based access control
- ✅ Protected routes and API endpoints
- ✅ Comprehensive error handling
- ✅ Mobile-responsive design

The system is ready for production with proper security measures in place! 