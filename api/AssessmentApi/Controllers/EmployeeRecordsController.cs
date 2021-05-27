using AssessmentApi.Provider;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeRecordsController : Controller
    {
        private readonly IExcelReader _excelReader;

        public EmployeeRecordsController(IExcelReader excelReader)
        {
            _excelReader = excelReader;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _excelReader.ReadFromExcel();

            if (result?.Count > 0)
                return Ok(result);
            else
                return NoContent();
        }
    }
}
