# Talabat E-Commerce Platform

A comprehensive e-commerce solution built with ASP.NET Core 6.0, implementing clean architecture principles and industry best practices.

## Project Architecture

This solution implements a clean, layered architecture:

- **Talabat.Core**: Domain entities, interfaces, and business logic
- **Talabat.Repository**: Data access implementations using Entity Framework Core
- **Talabat.Service**: Service layer implementations and business logic
- **Talabat.APIs**: RESTful API endpoints for client applications
- **AdminDashboard**: MVC admin portal for managing products, orders, and users

## Key Features

- 🛍️ **Product Catalog**: Complete product management with brands and categories
- 🛒 **Shopping Basket**: Redis-backed basket implementation for persistence
- 💳 **Payments**: Stripe integration for secure payment processing
- 📦 **Order Management**: Complete ordering process from checkout to delivery
- 👤 **Identity**: ASP.NET Core Identity with JWT authentication
- 📊 **Admin Dashboard**: Comprehensive management portal

## Technologies Used

- **Backend**: ASP.NET Core 6.0
- **ORM**: Entity Framework Core 6.0
- **Database**: SQL Server
- **Cache**: Redis for basket storage
- **Authentication**: JWT with ASP.NET Core Identity
- **Payment Processing**: Stripe API
- **Frontend (Admin)**: ASP.NET MVC, Bootstrap

## Getting Started

### Prerequisites
- .NET 6.0 SDK
- SQL Server (or SQL Server Express)
- Redis Server
- Visual Studio 2019/2022 or VS Code

### Database Setup

1. Update connection strings in `appsettings.json` for both projects:
