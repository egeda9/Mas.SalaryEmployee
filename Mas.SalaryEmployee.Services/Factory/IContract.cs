using System.Threading.Tasks;
using Mas.SalaryEmployee.Model.Entities;

namespace Mas.SalaryEmployee.Services.Factory
{
    public interface IContract
    {
        Task<Model.Dto.SalaryEmployee> GetAnnualSalary(Employee employee);
    }
}
