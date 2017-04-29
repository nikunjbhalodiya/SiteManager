using SiteManager.Core;
using SiteManager.Core.Model;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SiteInformationViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            
            _viewModel = new SiteInformationViewModel();
            DataContext = _viewModel;
            _viewModel.MessageBoxEvent += (msg) =>
            {
                var result = MessageBox.Show(msg, "Delete Site",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    return true;
                }
                else {
                    return false;
                }
            };
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                
                this.Hide();
                var site = item.Content as SiteModel;
                SiteDetail siteDetail = new SiteDetail(site.SiteId);
                siteDetail.ShowDialog();
                this.Show();
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtAddress_GotFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.AddressErrorVisibility = Visibility.Hidden;
        }

        private void txtSiteName_GotFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.SiteErrorVisibility = Visibility.Hidden;
        }
    }
}
