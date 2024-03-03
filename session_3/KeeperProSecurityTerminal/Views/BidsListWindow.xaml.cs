using KeeperProCommonDivision.Database;
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
using System.Windows.Shapes;

namespace KeeperProSecurityTerminal.Views
{
    /// <summary>
    /// Логика взаимодействия для BidsListWindow.xaml
    /// </summary>
    public partial class BidsListWindow : Window
    {
        private readonly MyDbContext _mDbContext;
        private readonly BidType _zeroType;
        private readonly Department _zeroDepartment;

        public BidsListWindow()
        {
            InitializeComponent();
            _mDbContext = MyDbContext.GetContext();

            var types = _mDbContext
                .BidTypes
                .ToList();

            _zeroType = new BidType { Name = "" };
            types.Add(_zeroType);

            TypesBox.ItemsSource = types;

            var departments = _mDbContext
                .Departments
                .ToList();

            _zeroDepartment = new Department { Name = "" };
            departments.Add(_zeroDepartment);

            DepartmentsBox.ItemsSource = departments;

            UpdateList();
        }

        /// <summary>
        /// Обновить список заявок
        /// </summary>
        public void UpdateList()
        {
            var bids = _mDbContext
                .Bids
                .Include(b => b.Employee)
                    .ThenInclude(e => e.Department)
                .Include(b => b.Status)
                .Include(b => b.VisitTarget)
                .Include(b => b.Type)
                .Include(b => b.Visitors)
                .Where(b => b.Status.Name != "Проверка")
                .ToList();

            var startDateFilter = DateStartFilter.SelectedDate;
            var endDateFilter = DateEndFilter.SelectedDate;

            if (startDateFilter != null)
            {
                bids = bids
                    .Where(b => b.VisitDate >= startDateFilter)
                    .ToList();
            }

            if (endDateFilter != null)
            {
                bids = bids
                    .Where(b => b.VisitDate <= endDateFilter)
                    .ToList();
            }

            var type = TypesBox.SelectedItem;
            if (type != null && type != _zeroType)
            {
                bids = bids
                    .Where(b => b.Type == type)
                    .ToList();
            }

            var department = DepartmentsBox.SelectedItem;
            if (department != null && department != _zeroDepartment)
            {
                bids = bids
                    .Where(b => b.Employee.Department == department)
                    .ToList();
            }

            var secondName = SecondNameBox.Text;
            var firstName = FirstNameBox.Text;
            var lastName = LastNameBox.Text;
            var numberPassport = NumberPassportBox.Text;

            if (secondName != null)
            {
                bids = bids
                    .Where(b => b.Visitors.Any(v => v.SecondName.ToLower().Contains(secondName.ToLower())))
                    .ToList();
            }

            if (firstName != null)
            {
                bids = bids
                    .Where(b => b.Visitors.Any(v => v.FirstName.ToLower().Contains(firstName.ToLower())))
                    .ToList();
            }

            if (lastName != null)
            {
                bids = bids
                    .Where(b => b.Visitors.Any(v => v.LastName == null ? false : v.LastName.ToLower().Contains(lastName.ToLower())))
                    .ToList();
            }

            if (numberPassport != null)
            {
                bids = bids
                    .Where(b => b.Visitors.Any(v => v.PassportNumber.Contains(numberPassport)))
                    .ToList();
            }

            BidsList.ItemsSource = bids;

            if (bids.Count <= 0)
                MessageBox.Show("Под указанные параметры не попадает ни одна заявка");
        }

        private void UpdateList(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void UpdateList(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void AllowTerritory(object sender, RoutedEventArgs e)
        {
            var bid = (Bid)BidsList.SelectedItem;
            if (bid == null)
            {
                MessageBox.Show("Заявка не выбрана");
                return;
            }

            var allowWindow = new AllowTerritoryWindow(bid);
            allowWindow.Owner = this;
            if (allowWindow.ShowDialog() == true)
            {
            }
            _mDbContext.SaveChanges();
        }

        private void ExitTerritory(object sender, RoutedEventArgs e)
        {
            var bid = (Bid)BidsList.SelectedItem;
            if (bid == null)
            {
                MessageBox.Show("Заявка не выбрана");
                return;
            }

            var exitTerritoryWindow = new ExitTerritoryWindow(bid);
            exitTerritoryWindow.Owner = this;
            if (exitTerritoryWindow.ShowDialog() == true)
            {
            }
            _mDbContext.SaveChanges();
        }
    }
}
