## Summary:
You are expected to develop an ASP.NET Core API as described below. We expect you to complete it within 3 h. of work, but you are free to spend more time if needed. You can commit right to main branch or create a working one.

## Problem description:
The API must manage users, and their payment methods, with the features below:

- All aggregate roots must have basic CRUD operations.
- All users with payment methods must have a default one.
- An endpoint with pagination and search features that returns a list of users with their default payment method is required.
- A user may not have duplicate payment methods, nor more than 5. You decide what makes a payment method a duplicate.

## Specs:
- All endpoints must be designed to be consumed.
- Focus on providing a high quality code, making use of good coding practices and design principles.
- CQRS is optional, but it would be appreciated if used.

## Mandatory:
- .NET 6/7
- Domain-Driven Design
- Dependency Injection
- Use of EF Core combined with SQLite/SQL Server
- Unit tests for, at least, a couple of use cases
- At least one Code First Migration
- Use of Swagger

## Deliverables:
A solution with the implementation described above. If the project is in a runnable state and other dependencies are required, please include a short description of how to run it in a file named how-to-run.md.
