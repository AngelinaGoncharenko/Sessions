using KeeperProApiWebService.Controllers.Json;
using KeeperProCommonDivision.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers.Json;

namespace WebApplication1.Controllers
{
    [Route("employees")]
    public class EmployeeController : Controller
    {
        private readonly MyDbContext _myDbContext;

        public EmployeeController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        /// <summary>
        /// Список сотрудников
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                var list = _myDbContext
                .Employees
                .Include(e => e.Department)
                .ToList();

                foreach (var employee in list)
                {
                    employee.Password = string.Empty;
                    employee.Bids = new List<Bid>();
                }

                return Ok(new EmployeesListJson { EmployeesList = list });
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageJson { Message = ex.Message });
            }
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                    throw new Exception();

                var existedEmployee =_myDbContext
                    .Employees
                    .Where(e => e.Code == employee.Code)
                    .Where(e => e.Division == "Охрана")
                    .FirstOrDefault(e => e.Password == employee.Password);

                if (existedEmployee == null)
                    throw new Exception("Авторизация провалена");

                return Ok(new AccessJson { Access = true });
            }
            catch
            {
                return BadRequest(new AccessJson { Access = false });
            }
        }
    }
}
