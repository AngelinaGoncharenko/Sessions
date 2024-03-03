using KeeperProCommonDivision.Database;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.Json;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Обработчик регистрации пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            try
            {
                if (user == null)
                    throw new Exception("Json body заполнено некорректно");

                if (user.Password == null)
                    throw new Exception("Пароль введен неверно");

                _userService.Register(user.Email, user.Password);
                return Ok(new MessageJson { Message = "Пользователь успешно зарегестрирован" });
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageJson { Message = ex.Message });
            }
        }

        /// <summary>
        /// Обработчик авторизации пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            try
            {
                if (user == null)
                    throw new Exception("Json body заполнено некорректно");

                if (user.Password == null)
                    throw new Exception("Пароль введен неверно");

                var token = _userService.Login(user.Email, user.Password);
                return Ok(new TokenJson { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageJson { Message = ex.Message });
            }
        }
    }
}
