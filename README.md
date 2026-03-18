☁️ EventEase – Cloud-Based Event Management System
📌 Project Overview

EventEase is a cloud-enabled event management and booking system developed as part of the Cloud Development A (CLDV7111) module.

The system addresses challenges faced by event management companies when handling multiple venues, scheduling events, and managing bookings manually. These challenges include:

Double bookings

Limited visibility of venue availability

Inefficient manual processes

This application provides a centralized, cloud-hosted solution to improve efficiency, accuracy, and usability.

🌐 Live Application

🔗 Access the deployed system here:
👉 https://st10389901-eventease-online-bzfwehbyd6ewcwhu.spaincentral-01.azurewebsites.net/ 

⚠️ Note:
This application is currently optimized for desktop use only.
For the best user experience, please access it using a laptop or desktop screen.
Mobile responsiveness will be improved in future development phases.

🎯 Part 1 Scope (Completed ✅)

This submission focuses on the foundation and initial deployment of the system:

✔ Database Design

Entity Relationship Diagram (ERD) created

Core tables implemented:

Venue

Event

Booking

User

✔ Application Development

ASP.NET Core MVC application created

CRUD operations implemented

Relationships between entities established

✔ Admin Platform

Dashboard displaying system statistics

Manage:

Venues

Events

Bookings

Clients

✔ Cloud Deployment

Successfully deployed to Microsoft Azure App Service

Connected to Azure SQL Database

🚀 Features

🔐 User Authentication (Login System)

📊 Dashboard with real-time statistics

📅 Booking management system

🏢 Venue management

🎊 Event scheduling

🔍 Search functionality for quick data access

🧱 System Architecture

The system follows the MVC (Model-View-Controller) architecture:

Models → Data & database structure

Views → User interface (Razor Views)

Controllers → Application logic

🗄️ Database Structure
Main Entities:

Venue

Event

Booking

User

Relationships:

One Venue → Many Events

One Event → Many Bookings

One User → Many Bookings

⚙️ Technologies Used

ASP.NET Core MVC (.NET 8)

Entity Framework Core

SQL Server / Azure SQL

Microsoft Azure App Service

Bootstrap 5

☁️ Cloud Implementation

The system is deployed using:

Azure Web App Service → Hosts the web application

Azure SQL Database → Stores all application data

This ensures scalability and accessibility from anywhere.

📦 How to Run the Project Locally

Clone the repository:

git clone https://github.com/your-username/eventease.git

Open in Visual Studio

Update appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "your-connection-string"
}

Run migrations:

Update-Database

Run the application
