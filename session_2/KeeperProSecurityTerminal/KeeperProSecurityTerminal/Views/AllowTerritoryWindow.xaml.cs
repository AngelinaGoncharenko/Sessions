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
    /// Логика взаимодействия для AllowTerritoryWindow.xaml
    /// </summary>
    public partial class AllowTerritoryWindow : Window
    {
        public Bid Bid { get; set; } 

        public AllowTerritoryWindow(Bid bid)
        {
            InitializeComponent();
            Bid = bid;
            DataContext = Bid;
        }

        private void Allow(object sender, RoutedEventArgs e)
        {
            if (Bid.EntrySecurityTime != null)
            {
                MessageBox.Show("Доступ на территорию по этой заявке уже был разрешен");
                return;
            }

            Bid.EntrySecurityTime = DateTime.Now.TimeOfDay;
            Bid.ExitSecurityTime = null;
            MessageBox.Show("Доступ разрешен");
            SystemSounds.Beep.Play();
        }
    }
}
