using System.Threading.Tasks;

namespace Mas.SalaryEmployee.Util
{
    public interface IHttpClientService
    {
        Task<TOut> GetAsync<TOut>(string uri);
    }
}
