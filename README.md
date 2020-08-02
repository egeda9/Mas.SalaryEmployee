# Mas Global - Salary Employee

This project contains the development of the .Net test for Mas Global, where the system developed allows calculating the annual salary of employees according to certain business rules. The project was developed in .Net Core version 3.1.

## Getting Started

After cloning the project on the local machine, the code can be run through the Visual Studio 2017, 2019 or Visual Studio Code IDEs or by the command line.

### Prerequisites

.Net Core SDK version 3.1 installed.

### Installing

- Clone the repo: https://github.com/egeda9/Mas.SalaryEmployee.git
- Build and restore the solution: 

Restore

```
dotnet restore
```

Build
```
dotnet build
```

- Run an instance of the project Mas.SalaryEmployee.Api
- Run an instance of the project Mas.SalaryEmployee.Web

## Running the tests

Tests can be run in two ways:

1. Using the command line, pointing to the folder with the project Mas.SalaryEmployee.Test:

Test
```
dotnet test
```

2. With the Visual Studio 2017 or 2019 test suite, running tests from the Mas.SalaryEmployee.Test project

### Break down into end to end tests

The unit tests created allow verifying the main logic of the functionality, focused on calculating the employee's annual salary based on the type of contract. Possible exceptions that may arise are also verified.

```
Get_Employees_Test
Get_Annual_Salary_Hourly_Based_Test
Get_Annual_Salary_Monthly_Based_Test
Get_Exception_Invalid_Contract_Type_Test
```

### And coding style tests

The Moq and FluentAssertion libraries are used to provide a more semantic structure to the test result. With Moq, fake instances of contracts used during runtime are created. 

## Code Structure

- **Mas.SalaryEmployee.Api**: Rest Api with all the interfaces to serve the front end project
- **Mas.SalaryEmployee.Web**: Front end project
- **Mas.SalaryEmployee.Model**: Entities, Enumerations and Data Transfer Objects
- **Mas.SalaryEmployee.Services**: All the business logic is centralized in this layer
- **Mas.SalaryEmployee.DataAccess**: Provide access to the data source
- **Mas.SalaryEmployee.Util**: Project that is used crosswise in the entire solution, with logic of common use
- **Mas.SalaryEmployee.Test**: Unit tests project

## Built With

* [.Net Core](https://dotnet.microsoft.com/download) - The framework used

## Authors

* **Juan Fernando Rojas** - *Initial work* - [egeda9](https://github.com/egeda9)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

