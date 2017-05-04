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
            _viewModel = new SupervisorDetailViewModel(SiteDetail.SiteId);
            DataContext = _viewModel;
            _viewModel.MessageBoxEvent += (msg) =>
            {
                var result = MessageBox.Show(msg, "Delete", MessageBoxButton.YesNo);
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
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                var supervisor = item.Content as Supervisor;
                DebitCreditInformation debitCredit = new DebitCreditInformation(new Entity { Identity = supervisor.DutyDescription, EntityId = supervisor.SupervisorId, EntityTypeId = 3, Name = supervisor.SupervisorName, Date = supervisor.CreatedDate, TotalAmount = supervisor.MonthlySalary, SiteId = SiteDetail.SiteId });
                debitCredit.ShowDialog();
            }
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
