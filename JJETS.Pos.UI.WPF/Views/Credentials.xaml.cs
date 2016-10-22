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
using MahApps.Metro.Controls.Dialogs;

namespace JJETS.Pos.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for Credentials.xaml
    /// </summary>
    public partial class Credentials : UserControl
    {
        public Credentials()
        {
            InitializeComponent();
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            var tile = (Tile) e.Source;

            var result =  MainWindow.Instance.ShowModalMessageExternal("Credentials", $"You Selected {tile.Title} as a User Role",
                MessageDialogStyle.AffirmativeAndNegative,
                new MetroDialogSettings
                {
                    AffirmativeButtonText = "Login",
                    NegativeButtonText = "Register",
                    AnimateShow = true
                }
            );

            var dialogSettings = new LoginDialogSettings
            {
                UsernameWatermark = "Email",
                PasswordWatermark = "Password",
                EnablePasswordPreview = true
            };

            if (result == MessageDialogResult.Affirmative)
            {
                var loginResult = MainWindow.Instance.ShowModalLoginExternal("Login", "Enter your username and password",
                    dialogSettings);

                var realResult = loginResult?.Username == "admin" && loginResult?.Password == "admin";

                MainWindow.Instance.ShowMessageAsync("Hello", realResult ? "Login Success" : "Login Failed");

                if (realResult)
                {        
                    var tempMain = new MainControl();
                    MainWindow.Instance.MainContentControl.Margin = new Thickness(100);
                    MainWindow.Instance.MainContentControl.Content = tempMain;
                }


            }
            else
            {
                var nameResult = MainWindow.Instance.ShowModalInputExternal($"Role : {tile.Title}","Set your name for your role");

                if (nameResult == "") return;

                var registerResult = MainWindow.Instance.ShowModalLoginExternal("Register", $"{nameResult} enter your credentials for security", dialogSettings);

                var realResult = registerResult?.Username != "admin";

                MainWindow.Instance.ShowMessageAsync("Hello", realResult ? "Register Success" : "Register Failed");
            }
        }
    }
}
