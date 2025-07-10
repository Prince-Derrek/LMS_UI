# 📚 Library Management System – UI (MVC)

This is the **Frontend UI** for the Library Management System API, built using **ASP.NET Core MVC**. It allows Admins and Users to interact with the system securely through a clean interface, enabling actions such as **authentication, managing books, borrowing/returning books, and managing users** (for Admins only).

> ⚠️ This is a **standalone UI project** that communicates with a separately hosted **ASP.NET Core Web API**. It does not expose or hardcode API endpoints in the browser.

---

## 🔧 Technologies Used

- **ASP.NET Core MVC (.NET 8)**
- **Razor Views**
- **C#**
- **Tailwind CSS (custom styling)**
- **HttpClient Factory** (to consume the API)
- **Session-Based Storage** (JWT tokens and user role)
- **Role-Based UI Rendering**
- **Authentication via JWT**

---

## 📂 Project Structure

LibraryManagementSystem_UI/
│
├── Controllers/
│ ├── AuthController.cs # Handles login/register logic
│ ├── BookController.cs # Handles Book CRUD + Borrow/Return
│ └── UserController.cs # Handles User CRUD (Admin only)
│
├── Views/
│ ├── Book/ # Book views (Index, Create, Edit, Delete, Details, Borrow, Return)
│ ├── User/ # User views (Index, Create, Edit, Delete, Details)
│ ├── Auth/ # Login and Register forms
│ ├── AdminDashboard/ # Admin homepage
│ └── UserDashboard/ # User homepage
│
├── Models/
│ └── ViewModels/ # BookViewModel, UserViewModel
│
├── wwwroot/
│ └── css/js # Tailwind & static assets
│
├── appsettings.json # Stores API base URL
└── Program.cs / Startup.cs # Middleware, session, and authentication setup
---

## ✅ Features

### 🔐 Authentication
- Login and Register via JWT-based authentication
- Secure token stored in session for API communication

### 👥 Role-Based Access
- Admins: Full access (Add/Edit/Delete Books and Users)
- Users: View, Borrow, and Return Books

### 📚 Book Management
- Admins can perform full CRUD on books
- Users can borrow and return books

### 👤 User Management (Admins Only)
- Admins can view all users, add, edit or delete users

---

## 🚀 Getting Started

### 1. Clone the UI Repository

git clone https://github.com/your-username/LibraryManagementSystem_UI.git
cd LibraryManagementSystem_UI

### 2. Prerequisites
.NET 8 SDK

Node.js (for Tailwind CSS, if styling from scratch)

API Project must be running with the correct JWT setup

### 3. Configure API Base URL
Open appsettings.json and update:
"ApiSettings": {
  "BaseUrl": "https://localhost:your-api-port"
}
### 4. Run the Project
dotnet run
Navigate to: http://localhost:your-ui-port

### 🛡 Login Credentials
Use existing credentials from your API database. To test:

Register a new user

Use login to receive and store JWT for session

### ✍️ Notes
JWT tokens are stored in session and passed via Authorization headers

Views are rendered conditionally based on the logged-in user's role

All protected actions are secured from the UI by checking the role and enforcing in the API

### 📌 Future Improvements
Add toast notifications for feedback

Improve responsive design with Tailwind

Add unit/integration testing
