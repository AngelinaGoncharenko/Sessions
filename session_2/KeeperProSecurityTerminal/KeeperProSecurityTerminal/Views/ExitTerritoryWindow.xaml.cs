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
        public Bid Bid { get; set; }

        public ExitTerritoryWindow(Bid bid)
        {
            InitializeComponent();
            Bid = bid;
            DataContext = Bid;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            if (Bid.ExitSecurityTime != null)
            {
                MessageBox.Show("Время убытия по заявке уже было указано");
                MessageBox.Show("Время убытия по заявке уже было указано");
                return;
            }

            Bid.ExitSecurityTime = DateTime.Now.TimeOfDay;
            MessageBox.Show("Время убытия указано успешно");
        }
    }
}
