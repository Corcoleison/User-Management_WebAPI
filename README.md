# How to Run the application

Set UserManagement.Presentation.WebApi as startup project if it is not set up yet and run it through visual studio or dotnet command.

Swagger page will popup with the availables endpoints to be consumed.

The database is a SQLite one and It already contains several records.

# **Additional Info**

I will explain a bit the structure of the project. The idea was to provide a solution that follows a
layer architecture based on DDD and good quality principles.

I’ve followed a typical layer structure where I’ve separated everything related to the WebAPI
(controllers, Dto, etc) in the **Presentation** project, the business logic and some other related code
into the **Application** class library, the migrations, repositories and ef core code into the
**Infrastucture** (called Data in the solution) and finally the **Domain**, where the models rest and the
repository interfaces. That way, it is the domain the one that is driven the development.

<a href="https://imgbb.com/"><img src="https://i.ibb.co/TPG5WDc/estructura-prueba.png" alt="estructura-prueba" border="0" /></a>

I’ve used .Net 6 in all projects, test project and class libraries. I’ve used some packages like Automapper to map the DTOs and the Domain Models, Xunit and FluentAssertions for the unit
tests.

Regards,
Pablo
