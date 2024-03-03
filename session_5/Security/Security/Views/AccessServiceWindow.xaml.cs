using KeeperProCommonDivision.Database;
using Microsoft.EntityFrameworkCore;
using Security.Models;
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
using System.Windows.Shapes;

namespace Security.Views
{
    /// <summary>
    /// Логика взаимодействия для AccessServiceWindow.xaml
    /// </summary>
    public partial class AccessServiceWindow : Window
    {
        private readonly User _user;
        private readonly MyDbContext _myDbContext;

        public AccessServiceWindow(User user)
        {
            InitializeComponent();
            _user = user;
            _myDbContext = MyDbContext.GetContext();

            UserNameBlock.Text = user.FullName;

            TypesBox.ItemsSource = _myDbContext
                .UserTypes
                .ToList();

            UpdateList();
        }

        /// <summary>
        /// Обновление списков пользователей
        /// </summary>
        private void UpdateList()
        {
            UsersList.ItemsSource = _myDbContext
                .Users
                .Include(u => u.Type)
                .Include(u => u.Gender)
                .Where(u => u.IsAccepted == false)
                .ToList();

            UsersListMandates.ItemsSource = _myDbContext
                .Users
                .Where(u => u.IsAccepted)
                .Where(u => u.HasDataReadMandate == false)
                .Where(u => u.HasDataWriteMandate == false)
                .Where(u => u.HasFormateGraphMandate == false)
                .ToList();
        }

        /// <summary>
        /// Одобрение пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Approve(object sender, RoutedEventArgs e)
        {
            var users = (List<User>)UsersList.ItemsSource;

            var verifyUsers = users
                .Where(u => u.IsAccepted)
                .ToList();

            foreach (var user in verifyUsers)
            {
                if (user.Login == null ||  user.Password == null || user.SecretWord == null || user.Type == null)
                {
                    MessageBox.Show("Все поля пользователя должны быть заполнены: " + user.SecondName);
                    return;
                }

                if (user.Login == "" || user.Password == "" || user.SecretWord == "")
                {
                    MessageBox.Show("Все поля пользователя должны быть заполнены: " + user.SecondName);
                    return;
                }
            }

            var allUsers = _myDbContext
                .Users
                .ToList();

            allUsers.AddRange(verifyUsers);

            foreach (var user in allUsers)
            {
                if (allUsers.Count(u => u.Login == user.Login && user.IsAccepted && u.IsAccepted && user != u) >= 2)
                {
                    MessageBox.Show("Нельзя сохранить двух пользователей с одинаковым логином: " + user.Login);
                    return;
                }
            }

            _myDbContext.SaveChanges();
            MessageBox.Show("Данные сохранены");
            UpdateList();
        }

        
        private void Accept(object sender, RoutedEventArgs e)
        {
            _myDbContext.SaveChanges();
            MessageBox.Show("Данные сохранены");
            UpdateList();
        }
    }
}
