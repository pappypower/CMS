import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider } from './contexts/AuthContext';
import ProtectedRoute from './components/Auth/ProtectedRoute';
import Login from './components/Auth/Login';
import Register from './components/Auth/Register';
import Layout from './components/Layout/Layout';
import Dashboard from './pages/Dashboard/Dashboard';
import DressList from './pages/Dresses/DressList';
import DressForm from './pages/Dresses/DressForm';
import CategoryList from './pages/Categories/CategoryList';
import CategoryForm from './pages/Categories/CategoryForm';
import OrderList from './pages/Orders/OrderList';
import OrderForm from './pages/Orders/OrderForm';
import './App.css';

function App() {
  return (
    <AuthProvider>
      <Routes>
        {/* Public Routes */}
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        
        {/* Protected Routes */}
        <Route path="/" element={
          <ProtectedRoute>
            <Layout />
          </ProtectedRoute>
        }>
          <Route index element={<Navigate to="/dashboard" replace />} />
          <Route path="dashboard" element={<Dashboard />} />
          
          {/* Dress Management */}
          <Route path="dresses" element={<DressList />} />
          <Route path="dresses/new" element={
            <ProtectedRoute requiredRoles={['Admin', 'Manager']}>
              <DressForm />
            </ProtectedRoute>
          } />
          <Route path="dresses/:id/edit" element={
            <ProtectedRoute requiredRoles={['Admin', 'Manager']}>
              <DressForm />
            </ProtectedRoute>
          } />
          
          {/* Category Management */}
          <Route path="categories" element={<CategoryList />} />
          <Route path="categories/new" element={
            <ProtectedRoute requiredRoles={['Admin', 'Manager']}>
              <CategoryForm />
            </ProtectedRoute>
          } />
          <Route path="categories/:id/edit" element={
            <ProtectedRoute requiredRoles={['Admin', 'Manager']}>
              <CategoryForm />
            </ProtectedRoute>
          } />
          
          {/* Order Management */}
          <Route path="orders" element={<OrderList />} />
          <Route path="orders/new" element={
            <ProtectedRoute requiredRoles={['Admin', 'Manager']}>
              <OrderForm />
            </ProtectedRoute>
          } />
          <Route path="orders/:id/edit" element={
            <ProtectedRoute requiredRoles={['Admin']}>
              <OrderForm />
            </ProtectedRoute>
          } />
        </Route>
        
        {/* Catch all route */}
        <Route path="*" element={<Navigate to="/dashboard" replace />} />
      </Routes>
    </AuthProvider>
  );
}

export default App; 