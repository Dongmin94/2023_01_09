using EmployeeData.DB;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace EmployeeData.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeService(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public async Task<IEnumerable<DB.Entites.Employee>> GetAllEmployeeQuery(int page, int pageSize)
        {
            int startIndex = (page - 1) * pageSize;
            var nextPage = _employeeContext.Employees
                .OrderBy(x => x._employeeNo)
                .Where(x => x._employeeNo > startIndex)
                .Take(pageSize)
                .ToList();

            return nextPage;
        }

        public async Task<DB.Entites.Employee> GetEmployeeQuery(string name)
        {
            return await _employeeContext.Employees.FirstOrDefaultAsync(x => x._name == name);
        }

        public async Task<int> InsertEmployeeCommand(DB.Entites.Employee employee)
        {
            _employeeContext.Employees.Add(employee);


			return await _employeeContext.SaveChangesAsync();
        }
    }
}
