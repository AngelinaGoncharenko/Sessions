using KeeperProCommonDivision.Database;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.Json;

namespace WebApplication1.Controllers
{
    [Route("departments")]
    public class DepartmentController : Controller
    {
        private readonly MyDbContext _myDbContext;

        public DepartmentController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        /// <summary>
        /// Список подразделений
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                var list = _myDbContext
                .Departments
                .ToList();

                return Ok(new DepartmentsListJson { DepartmentsList = list });
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageJson { Message = ex.Message });
            }
        }
    }
}
