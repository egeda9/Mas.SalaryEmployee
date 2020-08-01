using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Mas.SalaryEmployee.DataAccess;
using Mas.SalaryEmployee.Model.Entities;
using Mas.SalaryEmployee.Model.Enum;
using Mas.SalaryEmployee.Services.Factory;
using Mas.SalaryEmployee.Services.Factory.Implementation;
using Mas.SalaryEmployee.Services.Implementation;
using Moq;
using Xunit;

namespace Mas.SalaryEmployee.Test
{
    public class EmployeeServiceTest
    {
        private readonly Mock<IEmployeeData> _employeeDataMock;
        private readonly Mock<IContractFactory> _contractFactoryMock;

        private IList<Employee> _employees;
        private Employee _employee3;

        public EmployeeServiceTest()
        {
            this._contractFactoryMock = new Mock<IContractFactory>();
            this._employeeDataMock = new Mock<IEmployeeData>();

            Initialize();
        }

        [Fact]
        public async Task Get_Employees_Test()
        {
            // Given
            this._employeeDataMock
                .Setup(x => x.GetAsync())
                .ReturnsAsync(this._employees);

            this._contractFactoryMock
                .Setup(x => x.Create(ContractType.Hourly))
                .Returns(new HourlyBasedSalary());

            this._contractFactoryMock
                .Setup(x => x.Create(ContractType.Monthly))
                .Returns(new MonthlyBasedSalary());

            // When
            var employeeService = new EmployeeService(this._employeeDataMock.Object, this._contractFactoryMock.Object);
            var result = await employeeService.GetAsync();

            // Then
            result[0].AnnualSalary.Should().NotBe(0);
            result[1].AnnualSalary.Should().NotBe(0);
            this._employeeDataMock.Verify(x => x.GetAsync(), Times.Once);
            this._contractFactoryMock.Verify(x => x.Create(It.IsAny<ContractType>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Get_Annual_Salary_Hourly_Based_Test()
        {
            // Given
            this._employeeDataMock
                .Setup(x => x.GetAsync(1))
                .ReturnsAsync(this._employees[0]);

            this._contractFactoryMock
                .Setup(x => x.Create(ContractType.Hourly))
                .Returns(new HourlyBasedSalary());

            // When
            var employeeService = new EmployeeService(this._employeeDataMock.Object, this._contractFactoryMock.Object);
            var result = await employeeService.GetAsync(1);

            // Then
            result.AnnualSalary.Should().Be(120 * this._employees[0].HourlySalary * 12);
            this._employeeDataMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
            this._contractFactoryMock.Verify(x => x.Create(It.IsAny<ContractType>()), Times.Once);
        }

        [Fact]
        public async Task Get_Annual_Salary_Monthly_Based_Test()
        {
            // Given
            this._employeeDataMock
                .Setup(x => x.GetAsync(2))
                .ReturnsAsync(this._employees[1]);

            this._contractFactoryMock
                .Setup(x => x.Create(ContractType.Monthly))
                .Returns(new MonthlyBasedSalary());

            // When
            var employeeService = new EmployeeService(this._employeeDataMock.Object, this._contractFactoryMock.Object);
            var result = await employeeService.GetAsync(2);

            // Then
            result.AnnualSalary.Should().Be(this._employees[1].MonthlySalary * 12);
            this._employeeDataMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
            this._contractFactoryMock.Verify(x => x.Create(It.IsAny<ContractType>()), Times.Once);
        }

        [Fact]
        public void Get_Exception_Invalid_Contract_Type_Test()
        {
            // Given
            this._employeeDataMock
                .Setup(x => x.GetAsync(3))
                .ReturnsAsync(this._employee3);

            this._contractFactoryMock
                .Setup(x => x.Create(It.IsAny<ContractType>()))
                .Returns(It.IsAny<IContract>());

            // When
            var employeeService = new EmployeeService(this._employeeDataMock.Object, this._contractFactoryMock.Object);
            Func<Task> act = async () => { await employeeService.GetAsync(3); };

            // Then
            act.Should().Throw<ArgumentException>();
        }

        private void Initialize()
        {
            this._employees = new List<Employee>();

            var employee1 = new Employee
            {
                ContractTypeName = "HourlySalaryEmployee",
                HourlySalary = 60000,
                MonthlySalary = 80000,
                Id = 1,
                Name = "Juan",
                RoleDescription = null,
                RoleId = 1,
                RoleName = "Administrator"
            };

            var employee2 = new Employee
            {
                ContractTypeName = "MonthlySalaryEmployee",
                HourlySalary = 60000,
                MonthlySalary = 80000,
                Id = 2,
                Name = "Sebastian",
                RoleDescription = null,
                RoleId = 2,
                RoleName = "Contractor"
            };

            this._employees.Add(employee1);
            this._employees.Add(employee2);

            _employee3 = new Employee
            {
                ContractTypeName = "DailySalaryEmployee",
                HourlySalary = 60000,
                MonthlySalary = 80000,
                Id = 3,
                Name = "Sebastian",
                RoleDescription = null,
                RoleId = 3,
                RoleName = "Contractor"
            };
        }
    }
}
