# Sportradar Coding Academy — Coding Exercise (BE)

## Overview
This project is a sports event calendar application developed as part of the Sportradar Coding Academy backend exercise. It allows users to browse, add, and manage sports events. The solution is built with a modern .NET stack, featuring a robust RESTful API and a dynamic Blazor-based frontend.

## Features
- **Event Management**: Full CRUD functionality for sports events, including date, time, sport, and teams.
- **Categorization**: Events are categorized by sport (e.g., Football, Ice Hockey).
- **Venue and Team Tracking**: Support for venues (places) and team information.
- **RESTful API**: A clean backend API built with ASP.NET Core.
- **Modern Frontend**: A responsive UI built with Blazor Server and styled with Tailwind CSS.
- **API Documentation**: Interactive API documentation powered by OpenAPI and Scalar.
- **Automated Testing**: Unit tests for core service logic.

## Technology Stack
- **Backend**: .NET 9, ASP.NET Core Web API
- **Frontend**: Blazor Server, Tailwind CSS
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **Testing**: xUnit

## Project Structure
- `SportCalendar`: The backend API containing models, data context, migrations, and business services.
- `SportCalendar.Web`: The Blazor frontend application.
- `SportCalendar.Tests`: Unit tests for the backend services.

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Node.js & npm](https://nodejs.org/) (for Tailwind CSS processing)

### Database Setup
1. Ensure PostgreSQL is running.
2. Update the connection string in `SportCalendar/appsettings.json` 

3. Apply database migrations:
   ```bash
   dotnet ef database update --project SportCalendar
   ```

### Running the Application
To run both the API and the Web frontend, you can use the following commands in separate terminals:

**1. Start the Backend API:**
```bash
cd SportCalendar
dotnet run
```
The API will be available at `https://localhost:7000` (or as configured in `launchSettings.json`). You can access the API documentation at `/scalar/v1`.

**2. Start the Frontend:**
```bash
cd SportCalendar.Web
npm install
dotnet run
```
The application will be available at `https://localhost:7241`.

### Running Tests
To execute the unit tests:
```bash
dotnet test
```
