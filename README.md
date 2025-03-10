# LoadFit - Furniture Transportation System

## Overview
LoadFit is a specialized platform designed to facilitate the transportation of furniture and large items efficiently. The system connects users who need to move furniture with drivers who own suitable vehicles for transportation. The platform ensures smooth order placement, vehicle selection, pricing calculations, and secure payment processing.

## Features
### 1. **User Roles**
   - **Customers**: Can create transportation requests, select vehicles, view pricing, and make payments.
   - **Drivers**: Can manage assigned transportation orders and track their payments.
   
### 2. **Order Management**
   - Customers can create transportation orders based on item volume and weight.
   - The system recommends the best vehicle based on item specifications.
   - Orders are stored with details like selected vehicle, pricing, and payment status.

### 3. **Vehicle Management**
   - A variety of vehicles are available, each with defined capacity and pricing.
   - The system ensures the selected vehicle can handle the total weight and volume of the items.
   
### 4. **Dynamic Pricing System**
   - Pricing is based on the selected vehicle, item volume, and a predefined base price.
   - Ensures fair and accurate pricing for transportation requests.

### 5. **Secure Payment Integration**
   - Integrated with **Stripe** for payment processing.
   - Payment Intent is created during order placement and updated when necessary.
   - Customers receive a **Client Secret** for secure payment authentication.

### 6. **Basket System**
   - Customers can add multiple items to their transportation request before placing an order.
   - Items are stored in a temporary basket until checkout.

### 7. **Order Tracking & Status Updates**
   - Orders have different statuses: **Pending, Confirmed, In Transit, Completed**.
   - Customers can track their order progress.

## Admin Dashboard (ASP.NET MVC)

- **Order Management**: View, update, and manage all orders.

- **User Management**: Manage customers and drivers.

- **Vehicle Management**: Add, edit, or remove vehicle listings.

- **Payment Monitoring**: Track transactions and payment statuses.

- **Role-Based Access**: Secure authentication for admin functionalities

## Tech Stack & Architecture
### **Backend:**
- **.NET Core 8**: Backend framework for building APIs.
- **Entity Framework Core**: ORM for data access and management.
- **SQL Server**: Database for storing orders, users, and vehicles.
- **Stripe API**: Secure payment processing.

### **Architecture:**
- **Onion Architecture**: Ensures separation of concerns and modular development.
- **Repository Pattern**: Provides a clean abstraction over database operations.
- **Unit of Work Pattern**: Ensures atomic transactions and consistency.
- **Specification Pattern**: Used for complex query filtering and encapsulation.

### **Frontend (Handled by Flutter Dev Team):**
- **Flutter**: Cross-platform mobile application development.
- **Stripe SDK**: Handles secure payment processing on the mobile side.

## Installation & Setup
1. **Clone the repository:**
   ```sh
   git clone https://github.com/yourusername/loadfit.git
   cd loadfit
   ```
2. **Configure Environment Variables:**
   - Set up your `appsettings.json` file with the required **Stripe API keys**, database connection string, and other configurations.

3. **Run Database Migrations:**
   ```sh
   dotnet ef database update
   ```

4. **Start the application:**
   ```sh
   dotnet run
   ```

## Some API Endpoints
### **Basket API**
- `POST /api/basket` - Create a new basket
- `GET /api/basket/{basketId}` - Retrieve basket details
- `DELETE /api/basket/{basketId}` - Clear a basket

### **Order API**
- `POST /api/orders` - Create a new order
- `GET /api/orders/{orderId}` - Retrieve order details
- `GET /api/orders/user/{email}` - Get orders by user email

### **Payment API**
- `POST /api/payments` - Create or update a payment intent
- `GET /api/payments/{paymentIntentId}` - Retrieve payment details

## Contribution
1. **Fork the repository**.
2. **Create a new feature branch**.
3. **Commit your changes**.
4. **Submit a pull request**.


