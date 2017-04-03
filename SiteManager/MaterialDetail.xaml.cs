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
    /// Interaction logic for MaterialDetail.xaml
    /// </summary>
    public partial class MaterialDetail : Page
    {
        private List<MaterialType> _materialTypes;
        public MaterialDetail() 
        {
            InitializeComponent();
            _materialTypes = new List<MaterialType>
            {
                new MaterialType { Id = 0, Name= "Select Any one" },
                new MaterialType { Id = 1, Name= "Sand" },
                new MaterialType { Id = 2, Name= "Bricks" },
                new MaterialType { Id = 3, Name= "Cement" },
                new MaterialType { Id = 4, Name= "Metal" },
                new MaterialType { Id = 5, Name= "Marbles" },
                new MaterialType { Id = 6, Name= "Playwood" },
            };
            cmbMaterialType.ItemsSource = _materialTypes;
        }
    }

    public class MaterialType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
