# Car Rental System

A layered .NET application built as part of a multi-milestone backend development course, transitioning from basic console workflows to professional ORM-managed relational databases.

---

## 🛠 Project Structure

* **Milestone01_BasicConsole:** Procedural C# console application with basic JSON file persistence.
* **Milestone02_OOP:** Refactored object-oriented console application using domain models, service layers, and JSON persistence.
* **Milestone03_EFCore:** Modernized application utilizing Entity Framework Core (EF Core) and SQLite for robust relational database storage.

---

## 📌 Milestones Overview

### Milestone 01: Basic Console Application
- **Focus:** Basic C# syntax, CRUD concepts, data validation, and basic file storage.
- **Key Features:**
  - Console menu for managing rental records.
  - Basic input validation.
  - JSON file persistence (`data.json`).

### Milestone 02: Object-Oriented Programming (OOP) & Architecture
- **Focus:** Refactoring procedural code into clean OOP principles and separating concerns into dedicated layers.
- **Key Features:**
  - **Domain Models:** `Car`, `Customer`, and `Rental` classes with encapsulated properties and constructors.
  - **Service Layer:** `CarRentalService` handles core business logic and calculations.
  - **Data Persistence:** Object serialization/deserialization with `System.Text.Json`.

### Milestone 03: Entity Framework Core & Relational Database
- **Focus:** Transitioning from file storage to an Object-Relational Mapper (ORM) using EF Core and SQLite.
- **Key Features:**
  - **Database Context (`AppDbContext`):** Maps domain models directly to relational database tables using `DbSet<T>`.
  - **LINQ Queries:** Replaced raw file reading/writing with robust, type-safe database queries.
  - **Automatic Database Initialization:** Automatically provisions the local `carrental.db` database and seeds default data on startup.

---

## 🚀 How to Run

1. Open `CarRentalSystem.sln` in **JetBrains Rider** or **Visual Studio**.
2. Set `Milestone03_EFCore` as the startup project.
3. Build and Run (`Ctrl + F5` or `Cmd + R`).