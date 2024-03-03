using KeeperProCommonDivision.Database;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Services
{
    public class UserService
    {
        private readonly MyDbContext _myDbContext;

        public UserService(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <exception cref="Exception"></exception>
        public void Register(string email, string password)
        {
            var existedUser = _myDbContext
                .Users
                .FirstOrDefault(u => u.Email == email);

            if (existedUser != null)
                throw new Exception("Пользователь уже существует");

            if (email.Length < 4 || email.Contains("@") == false || email.Contains(".") == false)
                throw new Exception("Почта введена неверно, формат: test@mail.ru");

            if (password.Length < 8 || password.ToLower() == password || password.ToUpper() == password)
                throw new Exception("Пароль введен неверно, минимальная длина 8 символов, обязателльно наличие цифры, строчной буквы и заглавной буквы, а так же спец. символа");

            //Шифрование пароля пользователя
            password = Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(password)));

            var user = new User { Email = email, Password = password };

            _myDbContext
                .Users
                .Add(user);

            _myDbContext.SaveChanges();
        }

        /// <summary>
        /// Метод для входа пользователя
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string Login(string email, string password)
        {
            password = Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(password)));


            //Поиск пользователей с зашифрованным паролем
            var user = _myDbContext
                .Users
                .Where(u => u.Email == email)
                .FirstOrDefault(u => u.Password == password);

            if (user == null)
                throw new Exception("Логин или пароль введены неверно");

            var token = Convert.ToBase64String(Encoding.ASCII.GetBytes(email));

            return token;
        }


        /// <summary>
        /// Метод для аунтефикации пользователя
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public User Auth(string token)
        {
            var email = "";

            //Попытка расшифровать токен из base64
            try
            {
                email = Encoding.ASCII.GetString(Convert.FromBase64String(token));
            }
            catch
            {
                throw new Exception("Пользователь не авторизован");
            }

            var user = _myDbContext
                .Users
                .FirstOrDefault(u => u.Email == email);

            if (user == null)
                throw new Exception("Пользователь не авторизован");

            return user;
        }
    }
}
