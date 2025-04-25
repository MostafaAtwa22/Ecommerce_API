# E-commerce API

A robust and scalable RESTful API for an e-commerce platform built with ASP.NET Core. This API provides comprehensive functionality for managing products, orders, user authentication, and shopping cart operations.

## 🚀 Features

- **Product Management**
  - CRUD operations for products
  - Product categorization (Brands & Types)
  - Product image upload and management
  - Product filtering and sorting

- **User Management**
  - User authentication and authorization
  - JWT token-based security
  - Role-based access control

- **Shopping Experience**
  - Shopping cart functionality
  - Order processing
  - Payment integration
  - Order history

- **Additional Features**
  - Response caching
  - Error handling
  - API documentation
  - Data validation

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core
- **Database**: Entity Framework Core
- **Authentication**: JWT Token Authentication
- **Documentation**: Swagger/OpenAPI
- **Caching**: SQL Server
- **Payment Processing**: Stripe Integration

## 📋 Prerequisites

- .NET 7.0 SDK or later
- SQL Server
- Visual Studio 2022 or any preferred IDE

## ⚙️ Configuration

1. Clone the repository:
```bash
git clone [repository-url]
```

2. Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your_SQL_Server_Connection_String"
  }
}
```

3. Apply database migrations:
```bash
dotnet ef database update
```

## 🚀 Running the Application

1. Navigate to the project directory:
```bash
cd Ecommerce.API
```

2. Run the application:
```bash
dotnet run
```

The API will be available at `https://localhost:5001` (HTTPS) or `http://localhost:5000` (HTTP)

## 📚 API Documentation

Once the application is running, you can access the Swagger documentation at:
```
https://localhost:5001/swagger
```

### Key Endpoints

#### Products
- `GET /api/Products/GetAll` - Get all products
- `GET /api/Products/GetById/{id}` - Get product by ID
- `POST /api/Products/Create` - Create new product
- `PUT /api/Products/Update/{id}` - Update product
- `DELETE /api/Products/Delete/{id}` - Delete product

#### Product Brands
- `GET /api/ProductBrands/GetAll` - Get all brands
- `GET /api/ProductBrands/GetById/{id}` - Get brand by ID
- `POST /api/ProductBrands/Create` - Create new brand
- `PUT /api/ProductBrands/Update/{id}` - Update brand
- `DELETE /api/ProductBrands/Delete/{id}` - Delete brand

#### Product Types
- `GET /api/ProductTypes/GetAll` - Get all types
- `GET /api/ProductTypes/GetById/{id}` - Get type by ID
- `POST /api/ProductTypes/Create` - Create new type
- `PUT /api/ProductTypes/Update/{id}` - Update type
- `DELETE /api/ProductTypes/Delete/{id}` - Delete type

## 🔒 Authentication

The API uses JWT Bearer token authentication. To access protected endpoints:

1. Register a new user or login with existing credentials
2. Use the received token in the Authorization header:
```
Authorization: Bearer {your-token}
```

## 🏗️ Project Structure

```
Ecommerce.API/
├── Controllers/         # API Controllers
├── DTOs/               # Data Transfer Objects
├── Extensions/         # Extension Methods
├── Helpers/           # Helper Classes
└── Middleware/        # Custom Middleware

Ecommerce.Core/
├── Models/            # Domain Models
├── Interfaces/        # Interfaces
└── Specifications/    # Specification Pattern

Ecommerce.Infrastructure/
├── Data/              # Database Context
├── Services/          # Implementation of Core Interfaces
└── Repository/        # Data Access Layer
```

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📧 Contact

Mostafa Atwa - [atwamostafa5@gmail.com.com]

Project Link: [https://github.com/yourusername/Ecommerce_API] 
