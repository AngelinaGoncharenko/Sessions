using KeeperProCommonDivision.Database;
using Security.Models;
using Security.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Security
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MyDbContext _myDbContext;

        public MainWindow()
        {
            InitializeComponent();

            _myDbContext = MyDbContext.GetContext();

            Types.ItemsSource = _myDbContext
                .UserTypes
                .ToList();
        }

        /// <summary>
        /// Взод пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login(object sender, RoutedEventArgs e)
        {
            var login = Logins.Text;
            var password = Passwords.Text;
            var secretWord = SecretWords.Text;
            var type = (UserType)Types.SelectedItem;

            if (login == null || password == null || secretWord == null || type == null)
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }

            if (login == "" || password == "" || secretWord == "" || type.Name == "")
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }

            var user = _myDbContext
                .Users
                .Where(u => u.Login == login)
                .Where(u => u.Password == password)
                .Where(u => u.SecretWord == secretWord)
                .FirstOrDefault(u => u.Type == type);

            if (user == null || user.Type == null)
            {
                MessageBox.Show("Пользователь с такими данными не обнаружен");
                return;
            }

            if (user.Type.Name == "Администратор доступа")
            {
                MessageBox.Show("Успешный вход в систему!");
                var approveControlWindow = new ApproveControlWindow(user);
                approveControlWindow.Show();
                Close();
            }
            else if (user.Type.Name == "Служба ИБ")
            {
                MessageBox.Show("Успешный вход в систему!");
                var accessServiceWindow = new AccessServiceWindow(user);
                accessServiceWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Окно для этого поьзователя не реализовано");
            }
        }

        private void FakeMessage(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данная функция еще не реализована");
        }
    }
}
