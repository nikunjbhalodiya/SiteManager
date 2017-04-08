using SiteManager.Core;
using SiteManager.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace SiteManager
{
    /// <summary>
    /// Interaction logic for SupervisorDetail.xaml
    /// </summary>
    public partial class SupervisorDetail : Page
    {
        private SupervisorDetailViewModel _viewModel;
        public SupervisorDetail()
        {
            InitializeComponent();
            _viewModel = new SupervisorDetailViewModel();
            DataContext = _viewModel;
            _viewModel.MessageBoxEvent += () =>
            {
                var result = MessageBox.Show("Would you like to delete the Supervisor ?", "Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var supervisor = sender as ListViewItem;
            //_viewModel.SupervisorToAdd = supervisor.Content  as Supervisor;
        }

        private void txtSupervisorName_GotFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.NameVisibility = Visibility.Hidden;
        }

        private void txtSalary_GotFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.SalaryVisibility = Visibility.Hidden;
        }

        private void txtSalary_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
    }
}
