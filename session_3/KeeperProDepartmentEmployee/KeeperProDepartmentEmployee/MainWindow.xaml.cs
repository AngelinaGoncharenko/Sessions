using KeeperProCommonDivision.Database;
using KeeperProSecurityTerminal.Views;
using Microsoft.EntityFrameworkCore;
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

namespace KeeperProDepartmentEmployee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Вход по логину и паролю для сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login(object sender, RoutedEventArgs e)
        {
            var context = MyDbContext.GetContext();
            var employee = context
                .Employees
                .Where(e => e.Code == CodeBox.Text)
                .Include(b => b.Department)
                .FirstOrDefault(e => e.Password == PasswordBox.Text);

            if (employee == null)
            {
                MessageBox.Show("Сотрудник не найден");
                return;
            }

            if (employee.Department == null)
            {
                MessageBox.Show("Роль сотрудника не позволяет воспользоваться терминалом");
                return;
            }

            var bidsListWindow = new BidsListWindow();
            bidsListWindow.Show();
            Close();
        }
    }
}
