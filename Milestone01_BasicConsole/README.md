# Car Rental System

A layered .NET application built as part of a multi-milestone backend development course.

---

## 🛠 Project Structure

* **Milestone01_BasicConsole:** Procedural C# console application with basic JSON file persistence.
* **Milestone02_OOP:** Refactored object-oriented console application using domain models, service layers, and JSON persistence.

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
  - **Service Layer:** `CarRentalService` handles core business logic, entity management, and calculations.
  - **Data Persistence:** Object serialization/deserialization with `System.Text.Json`.
  - **Separation of Concerns:** Clear boundary between user interaction (`Program.cs`), business logic (`Services/`), and domain structures (`Models/`).

---

## 🚀 How to Run

1. Open `CarRentalSystem.sln` in **JetBrains Rider** or **Visual Studio**.
2. Set `Milestone02_OOP` as the startup project.
3. Build and Run (`Ctrl + F5` or `Cmd + R`).