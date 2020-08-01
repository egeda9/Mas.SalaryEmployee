using System;
using Mas.SalaryEmployee.Model.Enum;

namespace Mas.SalaryEmployee.Services.Factory.Implementation
{
    public class ContractFactory : IContractFactory
    {
        private readonly HourlyBasedSalary _hourlyBasedSalary;
        private readonly MonthlyBasedSalary _monthlyBasedSalary;

        public ContractFactory(HourlyBasedSalary hourlyBasedSalary, MonthlyBasedSalary monthlyBasedSalary)
        {
            _hourlyBasedSalary = hourlyBasedSalary;
            _monthlyBasedSalary = monthlyBasedSalary;
        }

        public IContract Create(ContractType contractType)
        {
            return contractType switch
            {
                ContractType.Hourly => this._hourlyBasedSalary,
                ContractType.Monthly => this._monthlyBasedSalary,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
