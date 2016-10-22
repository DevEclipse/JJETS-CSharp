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
    /// Interaction logic for Items.xaml
    /// </summary>
    public partial class Items : UserControl
    {
        public Items()
        {
            InitializeComponent();
        }

        private void ItemTileClick(object sender, RoutedEventArgs e)
        {
            var tempItem = (JJETS.Pos.Models.Item) (((Tile) sender).DataContext);

            var itemControl = (ItemControl) MainWindow.Instance.ItemControl.Content;

            itemControl.StackItemPanel.DataContext = tempItem;

            MainWindow.Instance.ItemControl.IsOpen = true;
        }
    }
}
