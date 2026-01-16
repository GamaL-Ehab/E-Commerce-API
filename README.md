# 🛍️ E-Commerce API (ASP.NET Core Web API)

A professional **E-Commerce Backend API** built using **ASP.NET Core Web API**, designed to support online shopping platforms with secure authentication, high performance, and scalable architecture.

This project demonstrates real-world backend development using **Onion Architecture**, **Specification Design Pattern**, **Entity Framework Core**, **Redis Caching**, and **RESTful API best practices**.

---

## 📋 Table of Contents
1. Overview  
2. Key Features  
3. Architecture  
4. Design Patterns  
5. Caching Strategy  
6. Technology Stack  
7. API Endpoints  
8. Data Models  
9. Authentication & Authorization  
10. Deployment  
11. Getting Started  
12. Contributing 
13. Author

---

## 🚀 Overview

The **E-Commerce API** is a scalable backend solution for managing an online store.  
It exposes RESTful endpoints to handle products, categories, users, orders, and authentication.

Special focus was placed on:
- Clean, maintainable architecture  
- High performance using caching  
- Separation of concerns  
- Enterprise-level backend practices  

---

## ✨ Key Features

- RESTful API design  
- **Onion Architecture** for clean separation of concerns  
- **Specification Design Pattern** for flexible and reusable queries  
- **Redis caching** for most frequent and expensive requests  
- JWT Authentication & Role-Based Authorization  
- Product, Category, and Order Management  
- Entity Framework Core (Code First + Migrations)  
- Swagger API Documentation  
- Centralized error handling  

---

## 🏗️ Architecture (Onion Architecture)

The project follows **Onion Architecture**, ensuring that business logic is independent from frameworks and external services.

Core (Domain)
│
├── Entities
├── Interfaces
│

Application
│
├── Services
├── Specifications
│

Infrastructure
│
├── EF Core (DbContext, Repositories)
├── Redis Cache
│

API
│
└── Controllers


### Architecture Benefits
- High maintainability  
- Testable business logic  
- Loose coupling  
- Clear dependency flow (outer layers depend on inner layers only)

---

## 🧩 Design Patterns Used

- **Specification Pattern**  
  - Encapsulates query logic  
  - Enables reusable, readable, and flexible filtering  
  - Used with Entity Framework Core  

- **Repository Pattern**  
  - Abstracts data access logic  

- **Dependency Injection**  
  - Improves testability and modularity  

---

## ⚡ Caching Strategy (Redis)

To improve performance and reduce database load:

- **Redis** is used to cache:
  - Most frequently requested products  
  - Categories list  
  - Read-heavy endpoints  

- Cached data is returned directly from Redis when available  
- Cache invalidation is handled on data updates  

This significantly improves response time and scalability.

---

## 🛠️ Technology Stack

| Category | Technology |
|--------|-----------|
| Framework | ASP.NET Core Web API |
| Language | C# |
| Architecture | Onion Architecture |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Caching | Redis |
| Authentication | JWT (JSON Web Tokens) |
| Documentation | Swagger |
| Tools | Visual Studio, Postman, Git |

---

## 📡 API Endpoints (Examples)

| Method | Endpoint | Description |
|------|---------|-------------|
| POST | `/api/auth/register` | Register new user |
| POST | `/api/auth/login` | Login and get JWT token |
| GET | `/api/products` | Get all products |
| GET | `/api/products/{id}` | Get product by ID |
| POST | `/api/products` | Create product (Admin) |
| PUT | `/api/products/{id}` | Update product (Admin) |
| DELETE | `/api/products/{id}` | Delete product (Admin) |
| POST | `/api/orders` | Create new order |
| GET | `/api/orders/user/{id}` | Get user orders |

---

## 🧱 Data Models

| Entity | Description |
|------|-------------|
| User | Stores user credentials and roles |
| Role | Authorization roles |
| Product | Store products |
| Category | Product categorization |
| Order | Customer orders |
| OrderItem | Products inside orders |
| Cart | Temporary shopping cart |

---

## 🔐 Authentication & Authorization

- Uses **JWT (JSON Web Tokens)** for stateless authentication  
- Secure endpoints require a valid token  
- Token must be sent in request headers:

Authorization: Bearer {token}


- Role-based authorization (Admin / User)

---

## 🌐 Deployment

The project is deployed on **MonsterASP**.

🔗 **Live API Host:**  
http://storebygamal.runasp.net/swagger/index.html


⚠️ **Important:** Use **HTTP (not HTTPS)**  
MonsterASP does not provide SSL by default.

---

## ⚙️ Getting Started

### 📌 Prerequisites

- .NET 7 or later  
- SQL Server  
- Redis Server  
- Visual Studio / VS Code  
- Postman or Swagger  

---
## 🤝 Contributing
#### Contributions are welcome!

Fork the repository

Create a feature branch

Commit your changes

Open a Pull Request

📜 License
This project is licensed under the MIT License.

## 👨‍💻 Author
#### GamaL Ehab
Backend / Full Stack Developer

GitHub: https://github.com/GamaL-Ehab

LinkedIn: www.linkedin.com/in/gamal-ehab

Email: gamalehab.dev@gmail.com

#### ⭐ If you like this project, please give it a star!

