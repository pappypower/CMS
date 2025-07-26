import React, { useEffect, useState } from 'react';
import {
  Box,
  Card,
  CardContent,
  Typography,
  Grid,
  IconButton,
  Button,
  CircularProgress,
  Alert,
} from '@mui/material';
import {
  LocalMall as DressIcon,
  Category as CategoryIcon,
  ShoppingCart as OrderIcon,
  TrendingUp as TrendingIcon,
  Add as AddIcon,
} from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import { WeddingDress, Category, Order } from '../../types';
import { dressesApi, categoriesApi, ordersApi } from '../../services/api';
import { toast } from 'react-toastify';

const Dashboard: React.FC = () => {
  const [loading, setLoading] = useState(true);
  const [featuredDresses, setFeaturedDresses] = useState<WeddingDress[]>([]);
  const [stats, setStats] = useState({
    totalDresses: 0,
    totalCategories: 0,
    totalOrders: 0,
    pendingOrders: 0,
  });
  const navigate = useNavigate();

  useEffect(() => {
    fetchDashboardData();
  }, []);

  const fetchDashboardData = async () => {
    try {
      setLoading(true);
      const [dressesRes, categoriesRes, ordersRes, featuredRes] = await Promise.all([
        dressesApi.getAll(),
        categoriesApi.getAll(),
        ordersApi.getAll(),
        dressesApi.getFeatured(),
      ]);

      const pendingOrders = ordersRes.data.filter(order => order.status === 'Pending').length;

      setStats({
        totalDresses: dressesRes.data.length,
        totalCategories: categoriesRes.data.length,
        totalOrders: ordersRes.data.length,
        pendingOrders,
      });

      setFeaturedDresses(featuredRes.data);
    } catch (error) {
      console.error('Error fetching dashboard data:', error);
      toast.error('Failed to load dashboard data');
    } finally {
      setLoading(false);
    }
  };

  const StatCard: React.FC<{
    title: string;
    value: number;
    icon: React.ReactNode;
    color: string;
    onClick?: () => void;
  }> = ({ title, value, icon, color, onClick }) => (
    <Card
      sx={{
        height: '100%',
        cursor: onClick ? 'pointer' : 'default',
        transition: 'all 0.3s ease',
        '&:hover': onClick ? { transform: 'translateY(-4px)' } : {},
      }}
      onClick={onClick}
    >
      <CardContent>
        <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between' }}>
          <Box>
            <Typography variant="h4" sx={{ fontWeight: 'bold', color }}>
              {value}
            </Typography>
            <Typography variant="body2" color="text.secondary">
              {title}
            </Typography>
          </Box>
          <Box sx={{ color, fontSize: '3rem' }}>
            {icon}
          </Box>
        </Box>
      </CardContent>
    </Card>
  );

  if (loading) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '50vh' }}>
        <CircularProgress size={60} />
      </Box>
    );
  }

  return (
    <Box className="fade-in">
      <Typography variant="h3" gutterBottom sx={{ fontWeight: 'light', color: 'text.primary' }}>
        Dashboard
      </Typography>
      
      <Grid container spacing={3} sx={{ mb: 4 }}>
        <Grid item xs={12} sm={6} md={3}>
          <StatCard
            title="Total Dresses"
            value={stats.totalDresses}
            icon={<DressIcon />}
            color="#d4a574"
            onClick={() => navigate('/dresses')}
          />
        </Grid>
        <Grid item xs={12} sm={6} md={3}>
          <StatCard
            title="Categories"
            value={stats.totalCategories}
            icon={<CategoryIcon />}
            color="#f8d7da"
            onClick={() => navigate('/categories')}
          />
        </Grid>
        <Grid item xs={12} sm={6} md={3}>
          <StatCard
            title="Total Orders"
            value={stats.totalOrders}
            icon={<OrderIcon />}
            color="#a8e6cf"
            onClick={() => navigate('/orders')}
          />
        </Grid>
        <Grid item xs={12} sm={6} md={3}>
          <StatCard
            title="Pending Orders"
            value={stats.pendingOrders}
            icon={<TrendingIcon />}
            color="#ffb3ba"
            onClick={() => navigate('/orders?status=Pending')}
          />
        </Grid>
      </Grid>

      <Card sx={{ mb: 4 }}>
        <CardContent>
          <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
            <Typography variant="h5" sx={{ fontWeight: 'medium' }}>
              Featured Wedding Dresses
            </Typography>
            <Button
              variant="contained"
              startIcon={<AddIcon />}
              onClick={() => navigate('/dresses/new')}
              sx={{ borderRadius: 2 }}
            >
              Add New Dress
            </Button>
          </Box>
          
          {featuredDresses.length === 0 ? (
            <Alert severity="info">
              No featured dresses found. Add some dresses and mark them as featured to see them here.
            </Alert>
          ) : (
            <Grid container spacing={3}>
              {featuredDresses.slice(0, 6).map((dress) => (
                <Grid item xs={12} sm={6} md={4} key={dress.id}>
                  <Card
                    sx={{
                      height: '100%',
                      cursor: 'pointer',
                      transition: 'all 0.3s ease',
                      '&:hover': { transform: 'translateY(-4px)' },
                    }}
                    onClick={() => navigate(`/dresses/edit/${dress.id}`)}
                  >
                    <CardContent>
                      <Typography variant="h6" gutterBottom noWrap>
                        {dress.name}
                      </Typography>
                      <Typography variant="body2" color="text.secondary" sx={{ mb: 1 }}>
                        {dress.designer} • {dress.style}
                      </Typography>
                      <Typography variant="h6" color="primary.main" sx={{ fontWeight: 'bold' }}>
                        ${dress.salePrice || dress.price}
                        {dress.salePrice && (
                          <Typography
                            component="span"
                            variant="body2"
                            sx={{ textDecoration: 'line-through', ml: 1, color: 'text.secondary' }}
                          >
                            ${dress.price}
                          </Typography>
                        )}
                      </Typography>
                      <Typography variant="body2" color="text.secondary">
                        Stock: {dress.stock} • {dress.category.name}
                      </Typography>
                    </CardContent>
                  </Card>
                </Grid>
              ))}
            </Grid>
          )}
        </CardContent>
      </Card>
    </Box>
  );
};

export default Dashboard; 