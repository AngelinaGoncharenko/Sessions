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
    }
}
