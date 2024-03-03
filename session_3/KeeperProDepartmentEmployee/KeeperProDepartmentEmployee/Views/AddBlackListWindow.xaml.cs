using KeeperProCommonDivision.Database;
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

namespace KeeperProDepartmentEmployee.Views
{
    /// <summary>
    /// Логика взаимодействия для AddBlackListWindow.xaml
    /// </summary>
    public partial class AddBlackListWindow : Window
    {
        public string? MyNote => Note.Text;

        public AddBlackListWindow()
        {
            InitializeComponent();
        }

        private void AddBlackList(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
