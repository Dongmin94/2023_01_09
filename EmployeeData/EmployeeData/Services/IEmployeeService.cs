using System.Numerics;

namespace EmployeeData.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<DB.Entites.Employee>> GetAllEmployeeQuery(int page, int pageSize);
        Task<DB.Entites.Employee> GetEmployeeQuery(string name);
        Task<int> InsertEmployeeCommand(DB.Entites.Employee employee);
    }
}
