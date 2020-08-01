using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mas.SalaryEmployee.Services
{
    public interface IEmployeeService
    {
        Task<IList<Model.Dto.SalaryEmployee>> GetAsync();
        Task<Model.Dto.SalaryEmployee> GetAsync(int id);
    }
}
