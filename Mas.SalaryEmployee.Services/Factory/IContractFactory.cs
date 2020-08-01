using Mas.SalaryEmployee.Model.Enum;

namespace Mas.SalaryEmployee.Services.Factory
{
    public interface IContractFactory
    {
        IContract Create(ContractType contractType);
    }
}
