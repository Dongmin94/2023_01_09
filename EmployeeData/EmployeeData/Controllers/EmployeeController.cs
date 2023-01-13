using EmployeeData.Employee.Commands;
using EmployeeData.Employee.Queries;
using EmployeeData.Log;
using EmployeeData.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeData.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly string jsonType = "application/json";
        private readonly string csvType  = "text/csv";

        private readonly IMediator _mediator;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IMediator mediator, ILogger<EmployeeController> logger)
        {
            _mediator = mediator;
            _logger   = logger;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<JsonResult> Get(int page, int pageSize)
        {
			return Json(await _mediator.Send(new GetAllEmployeeQuery() { page = page, pageSize = pageSize}));
		}

        [HttpGet]
        [Route("api/[controller]/{name}")]
        public async Task<JsonResult> Get(string name)
        {
            return Json(await _mediator.Send(new GetEmployeeQuery() { name = name }));
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            string[] allowFileType = { jsonType, csvType };

            try
            {
                if (file.Length > 0 && allowFileType.Contains(file.ContentType.ToLower()))
                {
                    if (file.ContentType.ToLower().Equals(jsonType, StringComparison.OrdinalIgnoreCase))
                    {
                        using (var reader = new StreamReader(file.OpenReadStream()))
                        {
                            string json = reader.ReadToEnd();
                            List<EmployeeDataJsonModel> data = JsonConvert.DeserializeObject<List<EmployeeDataJsonModel>>(json);

                            foreach (var item in data)
                            {
                                await _mediator.Send(new InsertEmployeeCommand() {  _name   = item.name,
                                                                                    _email  = item.email,
                                                                                    _tel    = item.tel,
                                                                                    _joined = Convert.ToDateTime(item.joined)
                                });
                            }
                        }
                    }
                    else if(file.ContentType.ToLower().Equals(csvType, StringComparison.OrdinalIgnoreCase))
                    {
                        using (var reader = new StreamReader(file.OpenReadStream()))
                        {
                            while (reader.Peek() >= 0)
                            {
                                string[] values = (await reader.ReadLineAsync()).Split(',');
                                await _mediator.Send(new InsertEmployeeCommand() {  _name   = values[0]
                                                                                   ,_email  = values[1]
                                                                                   ,_tel    = values[2]
                                                                                   ,_joined = Convert.ToDateTime(values[3])
                                });
                            }
                        }
                    }
                    else
                    {
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteLog(_logger, LogEventsKey.ErrFileUplaod, $"File Upload Fail : {ex.ToString()}");
            }

            return Ok();
        }
    }
}
