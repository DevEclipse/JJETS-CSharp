using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using MahApps.Metro.Controls.Dialogs;

namespace JJETS.Pos.UI.WPF.Windows
{
    using Views;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool _shutdown;
 
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private async void RightWindowClick(object sender, RoutedEventArgs e)
        {
            var temp = (Button)e.Source;
            
            switch(temp.Content.ToString()) {
                case "My Account":
                    Profile.IsOpen = true;
                    break;
                case "Log Out":

                    var mySettings = new MetroDialogSettings()
                    {
                        AffirmativeButtonText = "Log Out",
                        NegativeButtonText = "Stay",
                        AnimateShow = true,
                    };

                    var result = await this.ShowMessageAsync("Log Out", "Are you sure you that you want to log out?",MessageDialogStyle.AffirmativeAndNegative, mySettings);

                    if (result == MessageDialogResult.Affirmative)
                    {
                        MainContentControl.Content = new Credentials { Margin = new Thickness(300) };
                    }
                    break;
            }
        }

        private async void ClosingVerify(object sender, CancelEventArgs e)
        {
            if (e.Cancel) return;
            e.Cancel = !_shutdown;
            if (_shutdown) return;
                
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Quit",
                NegativeButtonText = "Cancel",
                AnimateShow = true,
                AnimateHide = false
            };

            var result = await this.ShowMessageAsync("Quit application?",
                "Sure you want to quit application?",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            _shutdown = result == MessageDialogResult.Affirmative;

            if (_shutdown)
                Application.Current.Shutdown();
            
        }

    }
}
