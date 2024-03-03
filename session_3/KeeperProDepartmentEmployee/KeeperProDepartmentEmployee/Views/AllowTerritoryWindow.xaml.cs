using Accessibility;
using KeeperProCommonDivision.Database;
using KeeperProDepartmentEmployee.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace KeeperProSecurityTerminal.Views
{
    /// <summary>
    /// Логика взаимодействия для AllowTerritoryWindow.xaml
    /// </summary>
    public partial class AllowTerritoryWindow : Window
    {
        public Bid Bid { get; set; }

        private MyDbContext _myDbContext;

        public AllowTerritoryWindow(Bid bid)
        {
            InitializeComponent();
            Bid = bid;
            DataContext = Bid;
            _myDbContext = MyDbContext.GetContext();
        }

        private void FixEntry(object sender, RoutedEventArgs e)
        {
            if (Bid.EntrySecurityTime == null)
            {
                MessageBox.Show("Доступ на территорию по этой заявке еще не был разрешен");
                return;
            }

            if (Bid.EntryDepartmentTime != null)
            {
                MessageBox.Show("Вход на подразделение по заявке уже был зафиксирован");
                return;
            }

            Bid.EntryDepartmentTime = DateTime.Now.TimeOfDay;
            Bid.ExitDepartmentTime = null;
            MessageBox.Show("Вход зафиксирован");
            _myDbContext.SaveChanges();
        }

        private void FixExit(object sender, RoutedEventArgs e)
        {
            if (Bid.EntryDepartmentTime == null)
            {
                MessageBox.Show("У этой заявки еще не было фиксации входа в подразделение");
                return;
            }

            if (Bid.ExitDepartmentTime != null)
            {
                MessageBox.Show("Выход из подразделения по заявке уже был зафиксирован");
                return;
            }

            Bid.ExitDepartmentTime = DateTime.Now.TimeOfDay;
            Bid.ExitSecurityTime = null;
            MessageBox.Show("Выход зафиксирован");
            _myDbContext.SaveChanges();
        }

        private void AddInBlackList(object sender, RoutedEventArgs e)
        {
            var visitor = (Visitor)VisitorsList.SelectedItem;

            var hasBan = _myDbContext.BannedUsers.Any(u => u.User == visitor.User);

            if (hasBan)
            {
                MessageBox.Show("Пользователь уже находится в черном списке");
                return;
            }

            var blackListWindow = new AddBlackListWindow();
            blackListWindow.Owner = this;
            if (blackListWindow.ShowDialog() == true)
            {
                var note = blackListWindow.MyNote;
                if (note == null || note == "")
                {
                    MessageBox.Show("Причина не может быть пустой");
                    return;
                }

                _myDbContext.BannedUsers.Add(new BannedUser { User = visitor.User, Note = note });
                MessageBox.Show("Пользователь добавлен в черный список");
                _myDbContext.SaveChanges();
            }
        }
    }
}
