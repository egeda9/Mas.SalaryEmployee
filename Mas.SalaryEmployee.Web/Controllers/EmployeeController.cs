using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Mas.SalaryEmployee.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mas.SalaryEmployee.Web.Models;
using Microsoft.Extensions.Options;

namespace Mas.SalaryEmployee.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IHttpClientService _httpClientService;
        private readonly IOptions<Settings> _settings;

        private const string Endpoint = "api/Employee";

        public EmployeeController(ILogger<EmployeeController> logger, IHttpClientService httpClientService, IOptions<Settings> settings)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._httpClientService = httpClientService ?? throw new ArgumentNullException(nameof(httpClientService));
            this._settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var result = new List<Model.Dto.SalaryEmployee>();

            try
            {
                if (string.IsNullOrEmpty(searchString))
                    result = await this._httpClientService.GetAsync<List<Model.Dto.SalaryEmployee>>($"{this._settings.Value.ApiUri}{Endpoint}");

                else
                {
                    var isNumeric = int.TryParse(searchString, out var id);

                    if (isNumeric)
                        result.Add(await this._httpClientService.GetAsync<Model.Dto.SalaryEmployee>($"{this._settings.Value.ApiUri}{Endpoint}/{id}"));
                }
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
            }

            return View(new SalaryEmployeeViewModel {Employees = result });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
