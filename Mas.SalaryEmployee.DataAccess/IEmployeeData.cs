using Mas.SalaryEmployee.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mas.SalaryEmployee.DataAccess
{
    public interface IEmployeeData
    {
        Task<IList<Employee>> GetAsync();
        Task<Employee> GetAsync(int id);
    }
}
