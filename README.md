# Employee_CQRS – .NET 8 Web API (CQRS + MediatR)

This project is a **production-ready Employee Management Web API** built using **.NET 8**, following **Clean Architecture** and **CQRS (Command Query Responsibility Segregation)** principles with **MediatR**.

The application demonstrates **best practices** used in real-world enterprise applications such as validation, logging, global exception handling, unit testing, and JWT authentication readiness.

---

## ✨ Key Features

- Clean Architecture (API, Application, Domain, Infrastructure)
- CQRS pattern using MediatR
- SQLite database for persistence
- FluentValidation for request validation
- Global exception handling middleware
- Standard API response structure
- Serilog logging (File + SQLite database)
- Unit testing using xUnit, Moq, EF Core InMemory
- Swagger API documentation
- JWT Authentication support (API protected)
- Interview-ready code structure and explanations

---

## 🧱 Solution Structure
```
Employee_CQRS
│
├── Employee_CQRS.API
│ ├── Controllers
│ ├── Middlewares
│ ├── Models
│ ├── Logs
│ ├── Program.cs
│ └── appsettings.json
│
├── Employee_CQRS.Application
│ ├── Common
│ │ ├── Behaviours
│ │ └── Interfaces
│ ├── Employees
│ │ ├── Commands
│ │ ├── Queries
│ │ ├── DTOs
│ │ └── Validators
│ └── AssemblyReference.cs
│
├── Employee_CQRS.Domain
│ └── Entities
│
├── Employee_CQRS.Infrastructure
│ └── Persistence
│
└── Employee_CQRS.Application.Tests
  └──  Employees
    ├── Commands
    └── Queries

```

## 🧠 Architecture Overview

### Clean Architecture Layers

- **API**  
  Entry point. Handles HTTP, middleware, Swagger.

- **Application**  
  Contains business logic, CQRS handlers, validators, interfaces.

- **Domain**  
  Contains core entities and business rules.

- **Infrastructure**  
  Database access (EF Core), persistence-related code.

---

## 🔀 CQRS Pattern

- **Commands** → Used for write operations (Create, Update, Delete)
- **Queries** → Used for read operations (Get, GetById)

Each command/query has:
- Request
- Handler
- Validator (if required)

MediatR dispatches requests to the appropriate handlers.

---

## ✅ Validation (FluentValidation)

- FluentValidation is integrated via MediatR pipeline behaviour
- Validation runs **before** handlers
- Invalid requests never reach database logic
- Validation errors are handled globally

---

## ⚠️ Global Exception Handling

- Centralised exception handling using custom middleware
- Converts exceptions into consistent API responses
- Handles:
  - ValidationException → 400
  - NotFoundException → 404
  - Unhandled exceptions → 500

---

## 📦 Standard API Response Structure

All APIs return a consistent response format:

```json
{
  "success": true,
  "statusCode": 200,
  "message": "Request successful",
  "data": {},
  "errors": null
}
```
This makes frontend integration simple and predictable.

---
## 📝 Logging (Serilog)

Logging is implemented using **Serilog**, following real-world production practices.

### Logging Targets

- **File Logging**
  - Rolling daily log files
  - Useful for operational monitoring and debugging

- **SQLite Database Logging**
  - Logs are stored in a separate SQLite database (`logs.db`)
  - Allows structured and searchable logs

### Why a Separate Database for Logs?

SQLite supports **only one writer at a time**.  
If application data and logs share the same database, write conflicts can occur.

To avoid database locking issues:

- **Application data** → `employee_cqrs.db`
- **Logs** → `logs.db`

This separation follows **real-world best practices** and ensures application stability.

---

## 🧪 Unit Testing

Unit tests are implemented to verify the **core business logic** of the application.

### Testing Tools Used

- **xUnit** – Test framework
- **Moq** – Mocking dependencies
- **EF Core InMemory Provider** – Simulates database behaviour
- **FluentAssertions** – Clean and readable assertions

### What Is Tested?

- **Command Handlers**
  - Create Employee
  - Update Employee
  - Delete Employee

- **Query Handlers**
  - Get All Employees
  - Get Employee By Id

### What Is NOT Tested?

- Controllers
- EF Core internal behaviour
- SQLite database interactions

This approach ensures **fast**, **isolated**, and **reliable** unit tests.

---

## 🚀 Running the Application

### Prerequisites

- .NET SDK 8.0 or later
- Visual Studio 2022 or VS Code
- SQLite (DB Browser is optional)

### Steps to Run

1. Clone the repository
2. Open the solution in Visual Studio
3. Restore NuGet packages
4. Run the application

Swagger UI will be available at:
```bash
http://localhost:1704/swagger/index.html
```
---

## 🧪 Running Unit Tests

From Visual Studio:
```bash
Test → Run All Tests
```

All unit tests should pass successfully.

---

## 🎯 Interview Highlights

This project demonstrates:

- Clean Architecture principles
- CQRS pattern using MediatR
- FluentValidation with MediatR pipeline behaviour
- Global exception handling
- Structured logging using Serilog
- SQLite limitations and real-world handling
- Proper unit testing strategy
- JWT authentication fundamentals

---

## 📌 Future Enhancements

- Role-based authorization
- Refresh token implementation
- Pagination and filtering
- Integration testing
- Docker support
- CI/CD pipeline setup

---

## 👨‍💻 Author

**Adarsh Kumar**  
***Built for learning, interviews, and real-world architectural understanding.***


