using SiteManager.Core.Model;
using System;
using System.Windows;

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
