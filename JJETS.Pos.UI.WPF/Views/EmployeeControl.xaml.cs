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
using JJETS.Pos.UI.WPF.Windows;
using MahApps.Metro.Controls;

namespace JJETS.Pos.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for ManagerControl.xaml
    /// </summary>
    public partial class EmployeeControl : UserControl
    {
        public EmployeeControl()
        {
            InitializeComponent();
        }

        private void Tile_Click(object sender, RoutedEventArgs e)   
        {
            var tile = (Tile)e.Source;

            switch (tile.Title)
            {
                case "Store Register (POS)":
                    App.Window.PointOfSale.IsOpen = true;
                    break;
            }
        }

    }
}
