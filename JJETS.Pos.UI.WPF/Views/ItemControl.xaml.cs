using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using JJETS.Pos.Logic;
using JJETS.Pos.Models;
using JJETS.Pos.UI.WPF.Windows;
using MahApps.Metro.Controls.Dialogs;

namespace JJETS.Pos.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for ItemControl.xaml
    /// </summary>
    public partial class ItemControl : UserControl
    {
        public Models.Item ItemReference { get; set; }

        public ItemControl()
        {
            InitializeComponent();
        }

        public void AddItem(object sender, RoutedEventArgs e)
        {

        }
        
        private void UpdateItem(object sender, RoutedEventArgs e)
        {
            ItemReference?.Save(EntityState.Modified);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            ItemReference?.Save(EntityState.Deleted);
        }
    }
}
