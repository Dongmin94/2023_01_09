using EmployeeData.Services;
using MediatR;
using System.Numerics;

namespace EmployeeData.Employee.Queries
{
    public class GetEmployeeQuery : IRequest<DB.Entites.Employee>
    {
        public string name { get; set; }

        public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, DB.Entites.Employee>
        {
            private readonly IEmployeeService _employeeService;

            public GetEmployeeQueryHandler(IEmployeeService employeeService)
            {
                _employeeService = employeeService;
            }

            public async Task<DB.Entites.Employee> Handle(GetEmployeeQuery query, CancellationToken cancellationToken)
            {
                return await _employeeService.GetEmployeeQuery(query.name);
            }
        }
    }
}
