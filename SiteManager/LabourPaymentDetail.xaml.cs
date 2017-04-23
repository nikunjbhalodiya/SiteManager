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
    /// Interaction logic for LabourPaymentDetail.xaml
    /// </summary>
    public partial class LabourPaymentDetail : Page
    {
        private readonly LabourViewModel _viewModel;
        public LabourPaymentDetail()
        {
            InitializeComponent();
            _viewModel = new LabourViewModel(SiteDetail.SiteId);
            DataContext = _viewModel;
            _viewModel.MessageBoxEvent += () =>
            {
                var result = MessageBox.Show("Would you like to delete item.", "Delete", MessageBoxButton.YesNo);
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

        private void txtWorkName_GotFocus(object sender, RoutedEventArgs e)
        {
            lblWorkNameErrMsg.Visibility = Visibility.Hidden;
        }

        private void txtWorkName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWorkName.Text))
            {
                lblWorkNameErrMsg.Visibility = Visibility.Visible;
            }
        }

        private void txtContractorName_GotFocus(object sender, RoutedEventArgs e)
        {
            lblContractorErrMsg.Visibility = Visibility.Hidden;
        }

        private void txtContractorName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContractorName.Text))
            {
                lblContractorErrMsg.Visibility = Visibility.Visible;
            }
        }

        //private void btnContractor_Click(object sender, RoutedEventArgs e)
        //{
        //    ContractorGrid.IsEnabled = true;
        //}

        //private void btnAddWorkType_Click(object sender, RoutedEventArgs e)
        //{
        //    WorkTypeGrid.IsEnabled = true;
        //}

        //private void btnAddContractor_Click(object sender, RoutedEventArgs e)
        //{
        //    ContractorGrid.IsEnabled = false;
        //}

        //private void addWorkType_Click(object sender, RoutedEventArgs e)
        //{
        //    WorkTypeGrid.IsEnabled = false;
        //}

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void cmbContractor_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            comboBox.SelectedIndex = 0;
        }

        private void cmbWrokType_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            comboBox.SelectedIndex = 0;
        }
    }
}
