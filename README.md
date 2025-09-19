# 📚 Library Management System  

A simple web application built with **ASP.NET MVC** to manage a library of books and members.  
The project demonstrates understanding of **MVC architecture, Entity Framework (Code First), AJAX calls, Partial Views, and Pagination**.  

---

## 🎯 Objective
This project was developed to practice:
- ASP.NET MVC (using .NET 6 / .NET 8)  
- Entity Framework Core (Code First with SQL Server)  
- AJAX calls for smooth UI updates  
- Partial Views integration  
- Pagination using AJAX  
- (Optional) ASP.NET Identity for authentication  

---

## 📦 Models

### Book
- Title  
- Author  
- PublishedDate  
- NumberOfCopies  

### Member
- Name  
- Phone  
- Email  

### Borrow
- BookId  
- MemberId  
- BorrowDate  
- ReturnDate  

---

## ⚙️ Features

- **CRUD for Books** (Add, Edit, Delete, View)  
- **CRUD for Members** (Add, Edit, Delete, View)  
- **Borrow Book** (assign a book to a member)  
- **Books List in Partial View** (AJAX-powered, no full page reload)  
- **Pagination** (10 items per page, AJAX-driven)  
- **Optional Authentication** with ASP.NET Identity (Login/Logout)  

---

## 🛠️ Tech Stack
- **Framework:** ASP.NET MVC (.NET 6 / .NET 8)  
- **Database:** SQL Server  
- **ORM:** Entity Framework Core (Code First)  
- **Frontend:** HTML, Bootstrap, AJAX, jQuery  
- **Authentication (Optional):** ASP.NET Identity  

---

## 🚀 Setup Instructions
1. Clone the repository:
   ```bash
   git clone https://github.com/EbrahemElzeer/Library-Management-System-with-Identity
