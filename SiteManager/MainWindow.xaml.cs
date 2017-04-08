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
        List<SiteModel> _siteModel;
        SiteInformationViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _siteModel = new List<SiteModel>
            {
                new SiteModel { SiteName = "Paradigm", Address = "Manek chowk, paldi" },
                new SiteModel { SiteName = "ABC", Address = "APs" },

            };
            _viewModel = new SiteInformationViewModel(_siteModel);
            DataContext = _viewModel;
            _viewModel.MessageBoxEvent += () =>
            {
                var result = MessageBox.Show("Would you like to delete the site.", "Delete Site",MessageBoxButton.YesNo);
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
                SiteDetail siteDetail = new SiteDetail(item.Content as SiteModel);
                siteDetail.ShowDialog();
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
