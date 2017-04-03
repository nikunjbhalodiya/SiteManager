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

namespace SiteManager
{
    /// <summary>
    /// Interaction logic for SiteDetail.xaml
    /// </summary>
    public partial class SiteDetail : Window
    {
        public SiteDetail(SiteModel siteModel)
        {
            InitializeComponent();
            if (siteModel == null)
                throw new ArgumentNullException(nameof(siteModel));
        }
    }
}
