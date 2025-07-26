# Wedding Dress CMS

A comprehensive Content Management System for wedding dress stores, built with .NET Core API backend and React frontend.

## Features

- **Wedding Dress Management**: Add, edit, and manage wedding dress inventory
- **Category Organization**: Organize dresses by style categories (A-Line, Mermaid, Ball Gown, etc.)
- **Order Management**: Track customer orders and order status
- **Inventory Tracking**: Monitor stock levels and availability
- **Beautiful UI**: Modern, responsive design with wedding-appropriate aesthetics
- **Search & Filter**: Advanced search and filtering capabilities
- **Featured Dresses**: Highlight special or popular dresses

## Technology Stack

### Backend (.NET Core API)
- .NET 8.0
- Entity Framework Core
- SQL Server LocalDB
- AutoMapper
- Swagger/OpenAPI

### Frontend (React)
- React 18 with TypeScript
- Material-UI (MUI) for components
- React Router for navigation
- Axios for API communication
- React Hook Form for form handling
- React Toastify for notifications

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (version 16 or later)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)

### Backend Setup

1. **Navigate to the backend directory:**
   ```bash
   cd backend/WeddingDressCMS.API
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

   The API will be available at `https://localhost:7000` and `http://localhost:5000`

5. **Access Swagger UI:**
   Open `https://localhost:7000/swagger` in your browser to explore the API endpoints.

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

   The React app will be available at `http://localhost:3000`

## Database Seeding

The database will be automatically seeded with sample data including:
- 5 wedding dress categories (A-Line, Mermaid, Ball Gown, Sheath, Bohemian)
- 2 sample wedding dresses with detailed information

## API Endpoints

### Wedding Dresses
- `GET /api/dresses` - Get all dresses (with optional search and category filter)
- `GET /api/dresses/{id}` - Get dress by ID
- `GET /api/dresses/featured` - Get featured dresses
- `POST /api/dresses` - Create new dress
- `PUT /api/dresses/{id}` - Update dress
- `DELETE /api/dresses/{id}` - Delete dress

### Categories
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get category by ID
- `POST /api/categories` - Create new category
- `PUT /api/categories/{id}` - Update category
- `DELETE /api/categories/{id}` - Delete category

### Orders
- `GET /api/orders` - Get all orders (with optional status filter)
- `GET /api/orders/{id}` - Get order by ID
- `GET /api/orders/by-number/{orderNumber}` - Get order by order number
- `POST /api/orders` - Create new order
- `PUT /api/orders/{id}` - Update order
- `DELETE /api/orders/{id}` - Delete order

## Project Structure

```
CMS/
├── backend/
│   └── WeddingDressCMS.API/
│       ├── Controllers/          # API controllers
│       ├── Data/                 # Entity Framework DbContext
│       ├── Models/               # Data models
│       ├── Services/             # Business logic services
│       └── Program.cs            # Application entry point
├── frontend/
│   └── src/
│       ├── components/           # Reusable React components
│       ├── pages/                # Page components
│       ├── services/             # API service layer
│       ├── types/                # TypeScript type definitions
│       └── theme.ts              # Material-UI theme configuration
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

### Styling

The application uses Material-UI with a custom theme featuring elegant wedding-appropriate colors:
- Primary: Elegant gold (#d4a574)
- Secondary: Soft rose (#f8d7da)
- Beautiful gradient backgrounds and glass-morphism effects

## Deployment

### Backend Deployment
1. Update connection string in `appsettings.json` for production database
2. Publish the application: `dotnet publish -c Release`
3. Deploy to your hosting provider (Azure, AWS, etc.)

### Frontend Deployment
1. Build for production: `npm run build`
2. Deploy the `build` folder to your web hosting service

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## License

This project is licensed under the MIT License.

## Support

For support and questions, please open an issue in the repository. 