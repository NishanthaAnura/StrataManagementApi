# Strata Management API

The **Strata Management API** is a RESTful web API built using **ASP.NET Core** and **Entity Framework**. It provides authentication, authorization, and management functionalities for a strata management system. The API supports two user roles: **Admin (Strata Manager)** and **Owner/Tenant**, with role-based access control (RBAC) implemented using JWT tokens.

---

## Features

### Authentication & Authorization
- **JWT-based Authentication**: Uses ASP.NET Core Identity for user, role, and claim management.
- **Two User Roles**:
  - **Admin (Strata Manager)**: Can manage buildings, owners, and maintenance requests.
  - **Owner/Tenant**: Can view and create maintenance requests for their assigned building.
- **JWT Token Generation**: Tokens are generated using a secure 256-bit key.

### Entity Models & API Endpoints
- **Buildings**: Name, Address, Number of Units.
- **Owners**: Name, Contact, Assigned Building.
- **Tenants**: Name, Contact, Assigned Unit & Building.
- **Maintenance Requests**: Title, Description, Status (Pending/In Progress/Completed), Created By, Assigned Building.

### Database & Best Practices
- **Entity Framework**: Used for database operations and migrations.
- **Repository & Service Layers**: Implemented for separation of concerns and maintainability.
- **RESTful API**: Follows RESTful best practices.
- **Swagger**: Integrated for API documentation and testing.

---


## Setup Instructions

### Prerequisites
- **.NET SDK**: Ensure you have the .NET SDK installed (version 6.0 or higher).
- **Database**: SQL Server or any compatible database supported by Entity Framework.

### Steps to Run the Project

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/NishanthaAnura/StrataManagementApi
   ```

2. Navigate to the Project Directory
   ```bash
    cd StrataManagement.Api/src/WebApiApi
    ```
3. Generate JWT Key
Use OpenSSL to generate a 256-bit (32-byte) key
```bash
openssl rand -base64 32
```
Example Output:
```bash

T2m7T/oOM77rYfpSYcum3l0V5a2tkNUD5+wpBtXQFXg=
```

4. Add the generated key to appsettings.json
```json
"Jwt": {
  "Key": "T2m7T/oOM77rYfpSYcum3l0V5a2tkNUD5+wpBtXQFXg=",
  "Issuer": "StrataManagementApi",
  "Audience": "StrataManagementApiUsers"
}
```

5. Configure Database
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=your-server;Database=StrataManagementDb;User Id=your-user;Password=your-password;"
}
```

6. Apply Database Migrations
```bash
dotnet ef database update
```
7. Run the API:
```bash
dotnet run
```
8. Access Swagger Documentation
http://localhost:5173/swagger 
