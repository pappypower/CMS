import { createTheme } from '@mui/material/styles';

export const theme = createTheme({
  palette: {
    primary: {
      main: '#d4a574', // Elegant gold
      light: '#e8c4a0',
      dark: '#b8956a',
      contrastText: '#ffffff',
    },
    secondary: {
      main: '#f8d7da', // Soft rose
      light: '#fbe9eb',
      dark: '#e6b3b8',
      contrastText: '#2c2c2c',
    },
    background: {
      default: '#fafafa',
      paper: '#ffffff',
    },
    text: {
      primary: '#2c2c2c',
      secondary: '#666666',
    },
  },
  typography: {
    fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
    h1: {
      fontWeight: 300,
      fontSize: '2.5rem',
      color: '#2c2c2c',
    },
    h2: {
      fontWeight: 400,
      fontSize: '2rem',
      color: '#2c2c2c',
    },
    h3: {
      fontWeight: 500,
      fontSize: '1.75rem',
      color: '#2c2c2c',
    },
    h4: {
      fontWeight: 500,
      fontSize: '1.5rem',
      color: '#2c2c2c',
    },
    h5: {
      fontWeight: 500,
      fontSize: '1.25rem',
      color: '#2c2c2c',
    },
    h6: {
      fontWeight: 600,
      fontSize: '1rem',
      color: '#2c2c2c',
    },
    body1: {
      fontSize: '1rem',
      lineHeight: 1.6,
    },
    body2: {
      fontSize: '0.875rem',
      lineHeight: 1.5,
    },
  },
  shape: {
    borderRadius: 12,
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          textTransform: 'none',
          borderRadius: 12,
          fontWeight: 500,
          padding: '10px 24px',
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          borderRadius: 16,
          boxShadow: '0 4px 20px rgba(0, 0, 0, 0.1)',
          transition: 'transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out',
          '&:hover': {
            transform: 'translateY(-2px)',
            boxShadow: '0 8px 30px rgba(0, 0, 0, 0.15)',
          },
        },
      },
    },
    MuiAppBar: {
      styleOverrides: {
        root: {
          background: 'linear-gradient(135deg, #d4a574 0%, #b8956a 100%)',
          boxShadow: '0 2px 20px rgba(0, 0, 0, 0.1)',
        },
      },
    },
    MuiTextField: {
      styleOverrides: {
        root: {
          '& .MuiOutlinedInput-root': {
            borderRadius: 12,
          },
        },
      },
    },
  },
}); 