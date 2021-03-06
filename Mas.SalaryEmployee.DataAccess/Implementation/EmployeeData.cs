﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mas.SalaryEmployee.Model.Dto;
using Mas.SalaryEmployee.Model.Entities;
using Mas.SalaryEmployee.Util;
using Microsoft.Extensions.Options;

namespace Mas.SalaryEmployee.DataAccess.Implementation
{
    public class EmployeeData : IEmployeeData
    {
        private const string Endpoint = "/api/Employees";

        private readonly IHttpClientService _httpClientService;
        private readonly IOptions<Settings> _settings;

        public EmployeeData(IHttpClientService httpClientService, IOptions<Settings> settings)
        {
            this._httpClientService = httpClientService ?? throw new ArgumentNullException(nameof(httpClientService));
            this._settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Get list of employees
        /// </summary>
        /// <returns>List of employees</returns>
        public async Task<IList<Employee>> GetAsync()
        {
            return await GetDataSourceAsync();
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee</returns>
        public async Task<Employee> GetAsync(int id)
        {
            var employees = await GetDataSourceAsync();
            return employees.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Get list of employees from http data repository
        /// </summary>
        /// <returns>List of employees</returns>
        private async Task<IList<Employee>> GetDataSourceAsync()
        {
            return await this._httpClientService.GetAsync<IList<Employee>>($"{this._settings.Value.DataSourceUri}{Endpoint}");
        }
    }
}
