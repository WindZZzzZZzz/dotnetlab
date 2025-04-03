# dotnetlab

## Overview
`dotnetlab` is a .NET-based web API project designed to manage user data. It includes features such as user authentication, CRUD operations for user management, and role-based access control. The project is built using ASP.NET Core and follows best practices for modern web development.

## Features
- User authentication using cookie-based authentication.
- Role-based access control with policies (e.g., Admin-only access).
- RESTful APIs for managing users:
  - Get user by ID.
  - Get all users.
  - Create a new user.
  - Update an existing user.
  - Delete a user.
- Middleware for custom login handling.
- Dependency injection for service management.

## Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- Visual Studio Code or any C#-compatible IDE
- Postman or an HTTP client for testing APIs

## Getting Started

### 1. Clone the Repository
```bash
git clone <repository-url>
cd dotnetlab
```

### 2. Build and Run the Application
```bash
dotnet build
dotnet run
```

The application will start on `http://localhost:5000` (or another port specified in the console output).

### 3. Test the APIs
Use the provided `user-manage-api.http` file to test the APIs directly in Visual Studio Code or use tools like Postman.

### API Endpoints
| HTTP Method | Endpoint                     | Description                  |
|-------------|------------------------------|------------------------------|
| `GET`       | `/api/User/{id}`             | Get user by ID               |
| `GET`       | `/api/User/all`              | Get all users                |
| `POST`      | `/api/User/create`           | Create a new user            |
| `PUT`       | `/api/User/update/{id}`      | Update an existing user      |
| `DELETE`    | `/api/User/delete/{id}`      | Delete a user                |

### Authentication
The application uses cookie-based authentication. To access protected endpoints:
1. Log in using the `/login` endpoint.
2. Use the authentication cookie in subsequent requests.

### Project Structure
- **Controllers**: Contains API controllers (e.g., `UserController`).
- **Services**: Contains business logic (e.g., `UserService`).
- **Models**: Defines data models (e.g., `User`).
- **Program.cs**: Configures the application and middleware.
