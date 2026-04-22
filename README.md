GoodBurger API

A RESTful API for managing burger orders, built with .NET 8, following Clean Architecture principles and best practices.
________________________________________
Features
• Create, update, delete, and retrieve orders
• Business rules applied in domain layer (pricing & discounts)
• Global exception handling via middleware
• SQLite database with Entity Framework Core
• Clean and maintainable architecture
• Menu endpoint to retrieve available items
________________________________________
Architecture
The project follows a layered architecture:
GoodBurger
├── GoodBurger.API # Entry point (Controllers, Middleware)
├── GoodBurger.Application # Services, DTOs
├── GoodBurger.Domain # Entities, Enums, Business Rules
├── GoodBurger.Infrastructure # Database (EF Core, DbContext)
└── GoodBurger.Tests # Unit tests (optional)
________________________________________
Technologies
• .NET 8
• ASP.NET Core Web API
• Entity Framework Core
• SQLite
• Swagger (OpenAPI)
________________________________________
Getting Started
1.	Clone the repository
git clone https://github.com/your-username/goodburger-api.git
cd goodburger-api
________________________________________
2.	Run migrations
dotnet ef database update --project GoodBurger.Infrastructure --startup-project GoodBurger.API
________________________________________
3.	Run the application
dotnet run --project GoodBurger.API
________________________________________
4.	Access Swagger
https://localhost:xxxx/swagger
________________________________________
API Endpoints
Method | Endpoint | Description
POST | /api/orders | Create a new order
GET | /api/orders | Get all orders
GET | /api/orders/{id} | Get order by ID
PUT | /api/orders/{id} | Update an order
DELETE | /api/orders/{id} | Delete an order
GET | /api/orders/menu | Get available menu items
________________________________________
Menu Endpoint Example
GET /api/orders/menu
Response:
{
"sandwiches": [
{ "name": "X Burger", "price": 5.00 },
{ "name": "X Egg", "price": 4.50 },
{ "name": "X Bacon", "price": 7.00 }
],
"extras": [
{ "name": "Fries", "price": 2.00 },
{ "name": "Drink", "price": 2.50 }
]
}
________________________________________
Business Rules
• Each order must include a sandwich
• Optional items: fries and drink
Discounts:
• Sandwich + Drink → 15%
• Sandwich + Fries → 10%
• Sandwich + Drink + Fries → 20%
________________________________________
Error Handling
The API uses a global exception middleware:
Exception Type | HTTP Status
ArgumentException | 400
KeyNotFoundException | 404
Other exceptions | 500
________________________________________
Testing
Unit tests can be added in the GoodBurger.Tests project using xUnit.
________________________________________
Possible Improvements

Although the project meets all the requirements, there are some enhancements that could be implemented to improve maintainability, scalability, and adherence to best practices:

Introduce Service Interfaces
- Create interfaces such as IOrderService to decouple implementation from usage and improve testability.


DTO Validation with Data Annotations

- Add attributes like [Required] and validation rules to request DTOs to provide better client feedback and automatic model validation.

Refactor Menu into Application Layer
- Move menu logic to a dedicated MenuService to maintain consistency with the application architecture.

Expand Test Coverage
- Add more unit tests for:
- Update scenarios
- Invalid inputs
- Edge cases in business rules

Logging
- Add structured logging (e.g., using ILogger) for better observability and debugging.
________________________________________
License
This project is for technical evaluation purposes.
________________________________________
Author
Developed by Carlos Henrique Moreira Vidigal

