# InvoiceApp

## Overview  

The **InvoiceApp** is a simple and practical web application designed to manage invoices and clients.  
It was built as part of a learning project to practice ASP.NET Core, Razor Pages, Entity Framework, and SQL Server integration.

---

## Folder Structure

- **Models** — Contains the C# classes for the application's data structures.
- **Pages** — Razor Pages used for handling the frontend logic and views.
- **Services** — Handles business logic like database operations and calculations.
- **Migrations** — Entity Framework Core migrations for database schema setup.
- **wwwroot** — Holds static files like CSS, JS, and images.

---

## Setup Instructions

### Prerequisites

- .NET SDK (7.0 or later recommended)
- SQL Server
- Visual Studio or Visual Studio Code

---

### Database Setup

1. Open **SQL Server Management Studio (SSMS)** or your preferred SQL tool.
2. Create a new database.
3. Update the connection string in `appsettings.json` to match your local database settings.
4. Run Migrations.

### Running the Application

1. Open **InvoiceApp.sln** in Visual Studio.
2. Press F5 to launch the application.

### Features

1. Create, view, edit, and delete invoices.
2. Manage client details.
3. Auto-calculate invoice totals.

### Usage

Once the application is running:

- Navigate to the homepage.
- Add new clients and invoices.
- View a detailed list of all created invoices.

