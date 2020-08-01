using System.Collections.Generic;

namespace Mas.SalaryEmployee.Web.Models
{
    public class SalaryEmployeeViewModel
    {
        public List<Model.Dto.SalaryEmployee> Employees { get; set; }
        public string SearchString { get; set; }
    }
}
