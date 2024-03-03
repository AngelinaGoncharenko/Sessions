using KeeperProApiWebService.Controllers.Json;
using KeeperProCommonDivision.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers.Json;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// Обработчик методов связангных с заявками
    /// </summary>
    [Route("bids")]
    public class BidController : Controller
    {
        private readonly MyDbContext _myDbContext;
        private readonly UserService _userService;

        public BidController(MyDbContext myDbContext, UserService userService)
        {
            _myDbContext = myDbContext;
            _userService = userService;
        }


        /// <summary>
        /// История заявок по пользователю
        /// </summary>
        /// <param name="authorizationJson"></param>
        /// <returns></returns>
        [Route("history")]
        [HttpGet]
        public IActionResult History([FromHeader] AuthorizationJson authorizationJson)
        {
            try
            {
                if (authorizationJson == null)
                    throw new Exception("Headers authorization заполнен некорректно");

                var user = _userService.Auth(authorizationJson.Authorization);

                var bids = _myDbContext
                    .Bids
                    .Where(b => b.Visitors
                        .Any(v => v.User == user))
                    .Include(b => b.Employee)
                        .ThenInclude(e => e.Department)
                    .Include(b => b.Status)
                    .Include(b => b.VisitTarget)
                    .Include(b => b.Type)
                    .ToList();
                    
                return Ok(new BidsJson { Bids = bids});
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageJson { Message = ex.Message });
            }
        }

        /// <summary>
        /// Создание заяки, тип который зависит от количества посетителей
        /// </summary>
        /// <param name="authorizationJson"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromHeader] AuthorizationJson authorizationJson, [FromBody] Bid bid)
        {
            try
            {
                if (authorizationJson == null)
                    throw new Exception("Headers authorization заполнен некорректно");

                if (bid == null)
                    throw new Exception("Json body заполнен некорректно");

                _userService.Auth(authorizationJson.Authorization);

                var employee = _myDbContext
                    .Employees
                    .FirstOrDefault(e => e.Code == bid.Employee.Code);

                if (employee == null)
                    throw new Exception("Код сотрудника заполнен неверно");

                bid.Employee = employee;

                if (bid.Visitors.Count <= 0)
                    throw new Exception("В заявке должен быть минимум 1 посетитель");

                BidType? type = null;

                if (bid.Visitors.Count == 1)
                {
                    type = _myDbContext
                        .BidTypes
                        .FirstOrDefault(s => s.Name == "Личная");
                }
                else if (bid.Visitors.Count >= 5)
                {
                    type = _myDbContext
                        .BidTypes
                        .FirstOrDefault(s => s.Name == "Групповая");
                }
                else
                {
                    throw new Exception("Неверное количество пользователей в заявке (личная - 1, групповая - минимум 5)");
                }

                if (type == null)
                    throw new Exception("Ошибка на стороне БД");

                bid.Type = type;

                var visitTarget = _myDbContext
                    .BidVisitTargets
                    .FirstOrDefault(t => t.Name == bid.VisitTarget.Name);

                if (visitTarget == null)
                    throw new Exception("Цель заявки указана неверно");

                bid.VisitTarget = visitTarget;

                var status = _myDbContext
                    .BidStatuses
                    .FirstOrDefault(s => s.Name == "Проверка");

                if (status == null)
                    throw new Exception("Ошибка на стороне БД");

                bid.Status = status;

                bid.VisitDate = null;
                bid.VisitTime = null;
                bid.StatusNote = null;

                if (bid.StartDate == DateTime.MinValue || (bid.StartDate > DateTime.Now && bid.StartDate < DateTime.Now + TimeSpan.FromDays(15)) == false)
                    throw new Exception("Дата начала действия заявки: минимум следующий день от подачи заявки, максимум 15 дней от даты подачи заявки");

                if (bid.EndDate == DateTime.MinValue || (bid.EndDate > bid.StartDate && bid.EndDate < bid.StartDate + TimeSpan.FromDays(15)) == false)
                    throw new Exception("Дата окончания действия заявки: минимум следующий день от даты начала действия заявки, максимум 15 дней от даты начала действия заявки");

                if (bid.Documents.Count <= 0)
                    throw new Exception("Должен быть как минимум 1 прикрепленный документ");

                foreach (var document in bid.Documents)
                {
                    document.Bid = bid;
                }

                foreach (var visitor in bid.Visitors)
                {
                    visitor.Bid = bid;

                    if (visitor.SecondName.Length < 1)
                        throw new Exception("Фамилия посетителя указано неверно");

                    if (visitor.FirstName.Length < 1)
                        throw new Exception("Имя посетителя указано неверно");

                    if (visitor.LastName != null && visitor.LastName.Length < 1)
                        throw new Exception("Отчетсов посетителя указано неверно");

                    if (visitor.PhoneNumber != null && visitor.PhoneNumber.Length != 18)
                        throw new Exception("Телефон посетителя указано неверно");

                    var user = _myDbContext
                        .Users
                        .FirstOrDefault(u => u.Email == visitor.User.Email);

                    if (user == null)
                    {
                        if (visitor.User.Email.Length < 4 || visitor.User.Email.Contains("@") == false || visitor.User.Email.Contains(".") == false)
                            throw new Exception("Почта введена неверно, формат: test@mail.ru");

                        user = new User { Email = visitor.User.Email };
                        _myDbContext.Users.Add(user);
                    }

                    if (visitor.BirthDay >= DateTime.Now + TimeSpan.FromDays(15))
                        throw new Exception("Дата рождения пользователя сликом маленькая");

                    visitor.User = user;

                    if (visitor.PassportNumber.Length != 6)
                        throw new Exception("Номер паспорта посетителя указан неверно");
                    if (visitor.PassportSeries.Length != 4)
                        throw new Exception("Серия паспорта посетителя указана неверно");
                }

                _myDbContext
                    .Bids
                    .Add(bid);

                _myDbContext.SaveChanges();

                return Ok(new MessageJson { Message = "Заявка создана успешно" });
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageJson { Message = ex.Message });
            }
        }

        [Route("all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var bids = new List<EmployeeBidJson>();
                foreach (var bid in _myDbContext.Bids
                    .Include(b=>b.Employee)
                    .ThenInclude(e => e.Department)
                    .Include(b => b.Type)
                    .ToList())
                {
                    if (bid.Employee.Department == null)
                        bid.Employee.Department = new Department { Name = "Не указан" };

                    if (bid.VisitTime == null)
                        bid.VisitTime = TimeSpan.MinValue;

                    bids.Add(new EmployeeBidJson { Department = bid.Employee.Department.Name, Type = bid.Type.Name, VisitTime = bid.VisitTime.Value});
                }

                return Ok(bids);
            }
            catch
            {
                return BadRequest(new EmployeeBidsJson { EmployeeBids = new List<EmployeeBidJson>() });
            }
        }
    }
}
