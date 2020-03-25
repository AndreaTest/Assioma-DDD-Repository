## Purpose:
* A very basic example of Clean Architecture / Domain Driven Design pattern.
* The Repository pattern has been implemented. 
* Unit of Work of work pattern has not been implemented (DbContext act as Unit of Work in this example).
* REST API to load and modify the data: the UI sample code under Controller\Api\ProductAPIController.cs

## Base technology:
* ASP.NET (or ASP.Net Core) App in C#

## Logging
* Logging abstractions for Microsoft.Extensions.Logging.
* It has been used without a provider implementation.
* It has been used only in the controller and Service layer only for demostartion.

## Database
* The projetc uses In-Memory version of EF Core defined in the test project (to make possible to test the application).
* To use MSSql Server LocalDB code first, in Startup.cs:
* * comment the two rows related to In-Memory DB.
* * uncomment the two rows under // Real database
* The database will be created the first time the application run (but only if in the machine is not istalled the localdb).

### Notes
* Controller must be agnostic about where to retrieve data, only logical commands here.
* Repository should hide the data implementation, no matter if they use a database, a web service or something else.
* Data Layer should not be known by any upper layer, for this reason following DDD rules the IRepository interface is defined in the Domain layer.
