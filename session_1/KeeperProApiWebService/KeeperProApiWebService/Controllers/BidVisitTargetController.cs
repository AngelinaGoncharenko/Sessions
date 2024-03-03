using KeeperProCommonDivision.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers.Json;

namespace WebApplication1.Controllers
{
    [Route("targets")]
    public class BidVisitTargetController : Controller
    {
        private readonly MyDbContext _myDbContext;

        public BidVisitTargetController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }


        /// <summary>
        /// Список целей посещения
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                var list = _myDbContext
                .BidVisitTargets
                .ToList();

                return Ok(new VisitTargetsListJson { VisitTargetsList = list });
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageJson { Message = ex.Message });
            }
        }
    }
}
