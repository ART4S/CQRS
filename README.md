## Technologies
* .NET Core 3.1
* ASP .NET Core 3.1
* Dapper
* Autofac
* AutoMapper
* FluentValidation
* Serilog
* Swashbuckle
* xUnit, Moq, FluentAssertions, Bogus

## Overview
### Domian
This project contains all business logic and data represented in entities, valueobjects, enums, exceptions, specific domian interfaces etc.

### Application
This project contains all application logic which is structly depends only from domian layer and nothing else. Core application logic and interfaces specified here. If you need to specify some services - simply put it there and implement it in layers below.

### Infrastructure
This project contains all implementation details of services specified in application project. All logic for accessing external resources such as file system, database, etc. also placed here.

### WebApi
This project intended to hold all UI logic and ties both application and infrastructure layer through dependency injection.
