using EmployeeData.Services;
using MediatR;
using System.Numerics;

namespace EmployeeData.Employee.Commands
{
    public class InsertEmployeeCommand : IRequest<int>
    {
        public int      _employeeNo { get; set; }
        public string   _name       { get; set; }
        public string   _email      { get; set; }
        public string   _tel        { get; set; }
        public DateTime _joined     { get; set; }

        public class InsertEmployeeCommandHandler : IRequestHandler<InsertEmployeeCommand, int>
        {
            private readonly IEmployeeService _employeeService;

            public InsertEmployeeCommandHandler(IEmployeeService employeeService)
            {
                _employeeService = employeeService;
            }

            public async Task<int> Handle(InsertEmployeeCommand command, CancellationToken cancellationToken)
            {
                var employee = new DB.Entites.Employee()
                {
                    _name       = command._name,
                    _email      = command._email,
                    _tel        = command._tel,
                    _joined     = command._joined
                };

                return await _employeeService.InsertEmployeeCommand(employee);
            }
        }
    }
}
