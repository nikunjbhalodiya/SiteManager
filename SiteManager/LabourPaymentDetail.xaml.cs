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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SiteManager
{
    /// <summary>
    /// Interaction logic for LabourPaymentDetail.xaml
    /// </summary>
    public partial class LabourPaymentDetail : Page
    {
        public LabourPaymentDetail()
        {
            InitializeComponent();
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
            lblContractorErrMsg.Visibility = Visibility.Visible;
        }

        private void txtContractorName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContractorName.Text))
            {
                lblContractorErrMsg.Visibility = Visibility.Visible;
            }
        }

        private void btnContractor_Click(object sender, RoutedEventArgs e)
        {
            ContractorGrid.IsEnabled = true;
        }

        private void btnAddWorkType_Click(object sender, RoutedEventArgs e)
        {
            WorkTypeGrid.IsEnabled = true;
        }

        private void btnAddContractor_Click(object sender, RoutedEventArgs e)
        {
            ContractorGrid.IsEnabled = false;
        }

        private void addWorkType_Click(object sender, RoutedEventArgs e)
        {
            WorkTypeGrid.IsEnabled = false;
            
        }
    }
}
