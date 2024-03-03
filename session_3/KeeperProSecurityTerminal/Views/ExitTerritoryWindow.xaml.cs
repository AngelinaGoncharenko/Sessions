using KeeperProCommonDivision.Database;
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
    /// Логика взаимодействия для ExitTerritoryWindow.xaml
    /// </summary>
    public partial class ExitTerritoryWindow : Window
    {
        private readonly MyDbContext _myDbContext;

        public Bid Bid { get; set; }

        public ExitTerritoryWindow(Bid bid)
        {
            InitializeComponent();
            Bid = bid;
            DataContext = Bid;
            _myDbContext = MyDbContext.GetContext();
        }

        /// <summary>
        /// Указать время убытия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit(object sender, RoutedEventArgs e)
        {
            if (Bid.ExitDepartmentTime == null)
            {
                MessageBox.Show("Выход из подразделения по заявке еще не был указан");
                return;
            }

            if (Bid.ExitSecurityTime != null)
            {
                MessageBox.Show("Время убытия по заявке уже было указано");
                return;
            }

            Bid.ExitSecurityTime = DateTime.Now.TimeOfDay;
            MessageBox.Show("Время убытия указано успешно");
            _myDbContext.SaveChanges();
        }
    }
}
