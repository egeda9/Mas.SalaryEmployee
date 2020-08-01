using Mas.SalaryEmployee.Model.Entities;
using Newtonsoft.Json;

namespace Mas.SalaryEmployee.Model.Dto
{
    public class SalaryEmployee : Employee
    {
        [JsonProperty("annualSalary")]
        public double AnnualSalary { get; set; }
    }
}
