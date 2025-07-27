# Wedding Dress CMS

A comprehensive Content Management System for wedding dress stores, built with .NET Core API backend and React frontend with JWT authentication.

## Features

- **User Authentication**: Secure login/register system with JWT tokens
- **Role-Based Access Control**: Admin, Manager, and User roles with different permissions
- **Wedding Dress Management**: Add, edit, and manage wedding dress inventory
- **Category Organization**: Organize dresses by style categories (A-Line, Mermaid, Ball Gown, etc.)
- **Order Management**: Track customer orders and order status
- **Inventory Tracking**: Monitor stock levels and availability
- **Beautiful UI**: Modern, responsive design with wedding-appropriate aesthetics
- **Search & Filter**: Advanced search and filtering capabilities
- **Featured Dresses**: Highlight special or popular dresses
- **Protected Routes**: Role-based page access control

## Technology Stack

### Backend (.NET Core API)
- .NET 8.0
- Entity Framework Core
- SQL Server LocalDB
- AutoMapper
- Swagger/OpenAPI with JWT authentication
- ASP.NET Core Identity
- JWT Bearer Authentication

### Frontend (React)
- React 18 with TypeScript
- Material-UI (MUI) for components
- React Router for navigation
- Axios for API communication
- React Hook Form for form handling
- React Toastify for notifications
- React Context API for state management
- Protected Routes with role checking

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (version 16 or later)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)

### Backend Setup

1. **Navigate to the backend directory:**
   ```bash
   cd backend
   ```

2. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

3. **Update the database with migrations:**
   ```bash
   dotnet ef database update
   ```

4. **Run the API:**
   ```bash
   dotnet run
   ```

   The API will be available at:
   - **HTTPS**: `https://localhost:7000`
   - **HTTP**: `http://localhost:5000`

5. **Access Swagger UI:**
   Open `https://localhost:7000/swagger` in your browser to explore the API endpoints with JWT authentication support.

### Frontend Setup

1. **Navigate to the frontend directory:**
   ```bash
   cd frontend
   ```

2. **Install npm dependencies:**
   ```bash
   npm install
   ```

3. **Start the development server:**
   ```bash
   npm start
   ```

   The React app will be available at `https://localhost:3001`

## Authentication

### Default Admin Account
The system automatically creates a default admin user:
- **Email**: `admin@weddingdresscms.com`
- **Password**: `Admin123!`
- **Role**: Admin

### User Roles
- **Admin**: Full access to all features
- **Manager**: Can manage dresses and categories, limited order access
- **User**: Read-only access to most content

### JWT Configuration
JWT tokens are configured with:
- **Issuer**: `WeddingDressCMS`
- **Audience**: `WeddingDressCMS-Users`
- **Duration**: 24 hours
- **Secret Key**: Configured in `appsettings.json`

## Database Seeding

The database will be automatically seeded with:
- **User Roles**: Admin, Manager, User
- **Default Admin User**: admin@weddingdresscms.com
- **Categories**: 5 wedding dress categories (A-Line, Mermaid, Ball Gown, Sheath, Bohemian)
- **Sample Dresses**: 2 sample wedding dresses with detailed information

## API Endpoints

### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration
- `GET /api/auth/me` - Get current user info
- `POST /api/auth/change-password` - Change user password
- `POST /api/auth/logout` - User logout
- `GET /api/auth/check` - Check authentication status

### Wedding Dresses
- `GET /api/dresses` - Get all dresses (with optional search and category filter)
- `GET /api/dresses/{id}` - Get dress by ID
- `GET /api/dresses/featured` - Get featured dresses
- `POST /api/dresses` - Create new dress **[Admin/Manager only]**
- `PUT /api/dresses/{id}` - Update dress **[Admin/Manager only]**
- `DELETE /api/dresses/{id}` - Delete dress **[Admin only]**

### Categories
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get category by ID
- `POST /api/categories` - Create new category **[Admin/Manager only]**
- `PUT /api/categories/{id}` - Update category **[Admin/Manager only]**
- `DELETE /api/categories/{id}` - Delete category **[Admin only]**

### Orders
- `GET /api/orders` - Get all orders (with optional status filter)
- `GET /api/orders/{id}` - Get order by ID
- `GET /api/orders/by-number/{orderNumber}` - Get order by order number
- `POST /api/orders` - Create new order **[Admin/Manager only]**
- `PUT /api/orders/{id}` - Update order **[Admin/Manager only]**
- `DELETE /api/orders/{id}` - Delete order **[Admin only]**

## Proxy Configuration

The frontend uses React's built-in proxy to avoid CORS issues:
- **Frontend**: `https://localhost:3001`
- **API Calls**: Made to `/api/*` (same origin)
- **Proxy Target**: Automatically forwards to `https://localhost:7000/api/*`

This eliminates CORS configuration complexity during development.

## Project Structure

```
CMS/
├── backend/
│   ├── Controllers/              # API controllers
│   │   ├── AuthController.cs     # Authentication endpoints
│   │   ├── DressesController.cs  # Dress management
│   │   ├── CategoriesController.cs
│   │   └── OrdersController.cs
│   ├── Data/                     # Entity Framework DbContext
│   │   └── WeddingDressContext.cs
│   ├── Models/                   # Data models
│   │   ├── Auth/                 # Authentication DTOs
│   │   ├── User.cs               # Identity user model
│   │   ├── WeddingDress.cs
│   │   ├── Category.cs
│   │   └── Order.cs
│   ├── Services/                 # Business logic services
│   │   ├── IAuthService.cs       # Authentication service
│   │   ├── AuthService.cs
│   │   ├── IJwtService.cs        # JWT token service
│   │   ├── JwtService.cs
│   │   └── ...
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── appsettings.json          # Configuration with JWT settings
│   ├── Program.cs                # Application entry point
│   └── WeddingDressCMS.API.csproj
├── frontend/
│   └── src/
│       ├── components/           # Reusable React components
│       │   ├── Auth/             # Authentication components
│       │   │   ├── Login.tsx
│       │   │   ├── Register.tsx
│       │   │   └── ProtectedRoute.tsx
│       │   └── Layout/
│       ├── contexts/             # React Context providers
│       │   └── AuthContext.tsx   # Authentication state management
│       ├── pages/                # Page components
│       ├── services/             # API service layer
│       │   └── api.ts            # Axios configuration with JWT
│       ├── types/                # TypeScript type definitions
│       │   └── index.ts          # Auth and data types
│       ├── App.tsx               # Main app with protected routing
│       └── theme.ts              # Material-UI theme configuration
├── WeddingDressCMS.sln          # Visual Studio solution file
└── README.md
```

## Development

### Adding New Features

1. **Backend**: Add new controllers, services, and models in their respective folders
2. **Frontend**: Create new components in the appropriate page or component directories
3. **Database**: Use Entity Framework migrations for schema changes:
   ```bash
   dotnet ef migrations add MigrationName
   dotnet ef database update
   ```

### Authentication Flow

1. User logs in via `/api/auth/login`
2. Server returns JWT token in response
3. Token stored in React Context and localStorage
4. Token sent in Authorization header for protected endpoints
5. Server validates token and user roles for each request

### Protected Routes

Routes are protected based on authentication and roles:
- **Public**: Login, Register pages
- **Authenticated**: Dashboard, view pages
- **Admin/Manager**: Create/Edit forms
- **Admin Only**: Delete operations

### Styling

The application uses Material-UI with a custom theme featuring elegant wedding-appropriate colors:
- Primary: Elegant gold (#d4a574)
- Secondary: Soft rose (#f8d7da)
- Beautiful gradient backgrounds and glass-morphism effects

## Troubleshooting

### Common Issues

1. **Module not found errors**: Run `npm install` in the frontend directory
2. **CORS errors**: Ensure proxy is configured correctly in `package.json`
3. **Authentication failures**: Check JWT configuration in `appsettings.json`
4. **Database errors**: Run `dotnet ef database update` to apply migrations

### Port Configuration

- **Frontend**: Configured to run on port 3001 (set in `package.json`)
- **Backend**: Runs on ports 7000 (HTTPS) and 5000 (HTTP)
- **Proxy**: Frontend automatically proxies `/api/*` calls to backend

## Deployment

### Backend Deployment
1. Update connection string in `appsettings.json` for production database
2. Configure JWT settings for production
3. Publish the application: `dotnet publish -c Release`
4. Deploy to your hosting provider (Azure, AWS, etc.)

### Frontend Deployment
1. Update API base URL for production if not using proxy
2. Build for production: `npm run build`
3. Deploy the `build` folder to your web hosting service

## Security Notes

- JWT tokens expire after 24 hours
- Passwords are hashed using ASP.NET Core Identity
- Role-based authorization protects sensitive endpoints
- HTTPS enforced in production

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## License

This project is licensed under the MIT License.

## Support

For support and questions, please open an issue in the repository. 