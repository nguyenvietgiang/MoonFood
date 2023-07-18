# MoonFood

MoonFood is a Food Ordering Web Application built using ASP.NET Core 7.0 and Entity Framework, with SQL Server and PostgreSQL as the database. It provides a convenient platform for users to browse menus, place orders, and make payments online.

## System Requirements
- .NET 7.0 SDK
- SQL Server & PostgreSQL
- Any compatible code editor (Visual Studio Code, Visual Studio, etc.)

## Installation and Setup
1. Clone the repository or download it as a ZIP file.
2. Open the project in your preferred code editor.
3. Configure the SQL Server connection:
   - Open the appsettings.json file.
   - Update the ConnectionString property with your SQL Server connection string.
4. Run the application using the `dotnet run` command or by pressing F5 in Visual Studio.

## Features
### Menu Browsing
- Users can browse the available menus to view different food options.
- Each menu item displays details such as name, description, and price.

### Order Placement
- Users can add items to their cart and place orders.
- The application supports customization options for each food item, such as toppings or sides.
- Users can specify delivery or pickup preferences.

### Online Payments
- The application integrates with a secure payment gateway to process online payments.
- Users can safely make payments using various payment methods.

### Order Tracking
- Users can track the status of their orders in real-time.
- The application provides updates on order preparation, delivery, and estimated delivery times.

### User Authentication and Authorization
- Users can create accounts, log in, and manage their profiles.
- The application utilizes JWT authentication for secure user login and registration.
- Role-based authorization controls access to certain functionalities based on user roles.

### Reviews and Ratings
- Users can leave reviews and ratings for the food items they have ordered.
- The application displays average ratings and reviews for each menu item.

## API Documentation
The API endpoints and their descriptions are documented using Swagger. Once the application is running, you can access the Swagger UI by navigating to `/swagger` on your local machine.

## Conclusion
MoonFood provides a seamless food ordering experience with features such as menu browsing, order placement, online payments, order tracking, user authentication, and reviews. It aims to simplify the process of ordering food and enhance customer satisfaction.
