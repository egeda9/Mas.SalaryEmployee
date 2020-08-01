﻿using System.Threading.Tasks;
using Mas.SalaryEmployee.Model.Entities;

namespace Mas.SalaryEmployee.Services.Factory.Implementation
{
    public class MonthlyBasedSalary : IContract
    {
        public async Task<Model.Dto.SalaryEmployee> GetAnnualSalary(Employee employee)
        {
            return new Model.Dto.SalaryEmployee
            {
                ContractTypeName = employee.ContractTypeName,
                HourlySalary = employee.HourlySalary,
                MonthlySalary = employee.MonthlySalary,
                AnnualSalary = employee.MonthlySalary * 12,
                Id = employee.Id,
                Name = employee.Name,
                RoleDescription = employee.RoleDescription,
                RoleId = employee.RoleId,
                RoleName = employee.RoleName
            };
        }
    }
}
