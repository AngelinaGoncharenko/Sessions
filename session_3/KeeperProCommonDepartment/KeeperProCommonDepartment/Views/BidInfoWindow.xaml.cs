using KeeperProCommonDivision.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
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

namespace KeeperProCommonDepartment.Views
{
    /// <summary>
    /// Логика взаимодействия для BidInfoWindow.xaml
    /// </summary>
    public partial class BidInfoWindow : Window
    {
        private readonly BidStatus _zeroStatus;
        private readonly MyDbContext _mDbContext;

        public Bid Bid { get; set; }

        public BidInfoWindow(Bid bid)
        {
            InitializeComponent();
            _mDbContext = MyDbContext.GetContext();
            Bid = bid;
            DataContext = Bid;

            var statuses = _mDbContext
               .BidStatuses
               .ToList();

            _zeroStatus = new BidStatus { Name = "" };
            statuses.Add(_zeroStatus);

            StatusesBox.ItemsSource = statuses;
            StatusesBox.SelectedItem = Bid.Status;

            foreach (var visitor in Bid.Visitors)
            {
                if (_mDbContext.BannedUsers.Any(b => b.User == visitor.User))
                {
                    MessageBox.Show("Заявка в черном списке");
                    Bid.Status = _mDbContext.BidStatuses.First(s => s.Name == "Отклонена");
                    return;
                }
            }

            MessageBox.Show("Заявка не в черном списке");
        }

        /// <summary>
        /// Сохранение инфомации по заявке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save(object sender, RoutedEventArgs e)
        {
            if (Bid.Status.Name != "Проверка")
            {
                MessageBox.Show("Невозможно изменить статус заяввки, которая уже имеет статус отличный от проверки");
                return;
            }

            var date = DateBox.SelectedDate;
            if (date != null)
            {
                Bid.VisitDate = date;
            }

            var time = TimeBox.Text;
            if (time != null)
            {
                Bid.VisitTime = TimeSpan.Parse(time);
            }

            var status = (BidStatus)StatusesBox.SelectedItem;
            if (status != null && status != _zeroStatus)
            {
                Bid.Status = status;
            }

            _mDbContext.SaveChanges();
            MessageBox.Show("Данные по заявке сохранены");
            Close();
        }
    }
}
