# Product RESTful API Solution

This is a technical assessment implementation for a Product Management API using .NET 8, following Clean Architecture principles.

## Features
- **CRUD Operations**: Complete CRUD for Products.
- **Authentication**: JWT-based authentication with Role-based access.
- **Architecture**: Domain-Driven Design (DDD) with Repository and Unit of Work patterns.
- **Database**: SQL Server via Entity Framework Core.
- **Docker**: Containerized application with Docker Compose.
- **Documentation**: Swagger/OpenAPI integration.
- **Testing**: Unit tests with xUnit and Moq.

## Getting Started

### Prerequisites
- Docker & Docker Compose
- .NET 8 SDK (optional, for local development)

### Running with Docker
1. Navigate to the root folder.
2. Run the following command:
   ```bash
   docker-compose up --build
   ```
3. The API will be available at `http://localhost:5000`.
4. Swagger UI can be accessed at `http://localhost:5000/swagger`.

### Local Development
1. Update `appsettings.json` with your SQL Server connection string.
2. Run migrations (if any) or ensure the database is created.
3. Run the project:
   ```bash
   dotnet run --project src/API/API.csproj
   ```

## API Usage

### 1. Authentication
To access the API, you first need to get a token:
- **POST** `/api/auth/login`
- **Body**: `{"Username": "admin", "Password": "password"}`
- **Response**: Returns a JWT token.

Use this token in the `Authorization` header as `Bearer <token>`.

### 2. Products
- **GET** `/api/products`: Get all products.
- **GET** `/api/products/{id}`: Get product by ID.
- **POST** `/api/products`: Create a new product.
- **PUT** `/api/products/{id}`: Update a product.
- **DELETE** `/api/products/{id}`: Delete a product.

## Implementation Details
- **Middlewares**: Custom Exception Middleware handles global errors and returns consistent JSON responses.
- **Data Integrity**: Uses Entity Framework's `SaveChangesAsync` override to automatically handle `CreatedOn` and `ModifiedOn` timestamps.
- **Validation**: FluentValidation is integrated (check `Application/Validators`).

## Security
- JWT tokens are used for stateless authentication.
- Password hashing should be implemented in a real-world scenario (omitted for this assessment's simplicity).
- CORS policy is configured in `Program.cs`.
