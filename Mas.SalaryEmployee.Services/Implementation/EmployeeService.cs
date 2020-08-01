using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mas.SalaryEmployee.DataAccess;
using Mas.SalaryEmployee.Model.Enum;
using Mas.SalaryEmployee.Services.Extension;
using Mas.SalaryEmployee.Services.Factory;

namespace Mas.SalaryEmployee.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeData _employeeData;
        private readonly IContractFactory _contractFactory;

        public EmployeeService(IEmployeeData employeeData, IContractFactory contractFactory)
        {
            this._employeeData = employeeData ?? throw new ArgumentNullException(nameof(employeeData));
            this._contractFactory = contractFactory ?? throw new ArgumentNullException(nameof(contractFactory));
        }

        /// <summary>
        /// Get list of employees
        /// </summary>
        /// <returns>List of SalaryEmployee</returns>
        public async Task<IList<Model.Dto.SalaryEmployee>> GetAsync()
        {
            var employees = await this._employeeData.GetAsync();
            var salaryEmployees = new List<Model.Dto.SalaryEmployee>();

            foreach (var employee in employees)
            {
                var contractType = employee.ContractTypeName.GetFromDescription<ContractType>();
                var contractTypeInstance = this._contractFactory.Create(contractType);

                salaryEmployees.Add(await contractTypeInstance.GetAnnualSalary(employee));
            }

            return salaryEmployees;
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>A SalaryEmployee</returns>
        public async Task<Model.Dto.SalaryEmployee> GetAsync(int id)
        {
            Model.Dto.SalaryEmployee result = null;
            var employee = await this._employeeData.GetAsync(id);

            if (employee != null)
            {
                var contractType = employee.ContractTypeName.GetFromDescription<ContractType>();
                var contractTypeInstance = this._contractFactory.Create(contractType);
                result = await contractTypeInstance.GetAnnualSalary(employee);
            }

            return result;
        }
    }
}
