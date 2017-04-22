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
        public static int SiteId { get; set; }
        public SiteDetail(int siteId)
        {
            InitializeComponent();
            SiteId = siteId;
        }
    }
}
