# ðŸŽ“ Student Management System

A comprehensive student management system consisting of a backend REST API server, a client desktop application, and web/desktop CRUD operations.

## ðŸ“ Repository Structure

This repository is organized as a monorepo containing the following components:

- **[StudentAPI.Server](src/StudentAPI.Server)**: The backend REST API server built with ASP.NET Core. It provides the database connection and business logic endpoints for managing student records.
- **[StudentAPI.Client](src/StudentAPI.Client)**: A desktop client application that consumes the REST API to interact with the student data.
- **[Student.CRUD](src/Student.CRUD)**: A learning project demonstrating direct database CRUD operations using ASP.NET Core and Entity Framework.

## ðŸ› ï¸ Technologies Used

- **Framework**: .NET / ASP.NET Core
- **Database**: Entity Framework Core & SQL Server
- **Language**: C#
- **Patterns**: MVC, RESTful API, Repository Pattern

## ðŸš€ How to Run

### Backend API Server
1. Navigate to src/StudentAPI.Server.
2. Update the connection string in ppsettings.json to point to your SQL Server database.
3. Run dotnet ef database update to apply migrations.
4. Start the server using dotnet run.

### Desktop Client
1. Navigate to src/StudentAPI.Client.
2. Start the client using dotnet run (make sure the API server is running).

## ðŸ“„ License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.