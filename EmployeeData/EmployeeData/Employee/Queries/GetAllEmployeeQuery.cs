using EmployeeData.Services;
using MediatR;
using System.Collections;
using System.Numerics;

namespace EmployeeData.Employee.Queries
{
    public class GetAllEmployeeQuery : IRequest<IEnumerable<DB.Entites.Employee>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }

        public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, IEnumerable<DB.Entites.Employee>>
        {
            private readonly IEmployeeService _employeeService;

            public GetAllEmployeeQueryHandler(IEmployeeService employeeService)
            {
                _employeeService = employeeService;
            }

            public async Task<IEnumerable<DB.Entites.Employee>> Handle(GetAllEmployeeQuery query, CancellationToken cancellationToken)
            {
                return await _employeeService.GetAllEmployeeQuery(query.page, query.pageSize);
            }
        }
    }
}
