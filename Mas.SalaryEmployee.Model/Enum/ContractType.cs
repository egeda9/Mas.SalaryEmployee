using System.ComponentModel;

namespace Mas.SalaryEmployee.Model.Enum
{
    public enum ContractType
    {
        [Description("HourlySalaryEmployee")]
        Hourly = 0,

        [Description("MonthlySalaryEmployee")]
        Monthly = 1
    }
}
