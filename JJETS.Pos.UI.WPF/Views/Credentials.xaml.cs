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
using JJETS.Pos.Logic;
using JJETS.Pos.Models;
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

        private async void Tile_Click(object sender, RoutedEventArgs e)
        {
            var tile = (Tile) e.Source;

            var metroDialogSettings = new MetroDialogSettings
            {
                AffirmativeButtonText = "Login",
                FirstAuxiliaryButtonText = "Register",
                AnimateShow = true
            };
            var result = await App.Window.ShowMessageAsync("Credentials", $"You Selected {tile.Title} as a User Role ",
                MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, metroDialogSettings);

            var dialogSettings = new LoginDialogSettings
            {
                UsernameWatermark = "Email",
                PasswordWatermark = "Password",
                EnablePasswordPreview = true,
                FirstAuxiliaryButtonText = "Reset Password",
               
            };

            switch (result)
            {
                case MessageDialogResult.Affirmative:
                    var loginResult = await App.Window.ShowLoginAsync("Login", "Enter your username and password",
                        dialogSettings);

                    if (loginResult == null) return;

                    var loginResultString = POS.LoginCheck(loginResult.Username, loginResult.Password,tile.Title);

                    if(loginResultString == "Login Successful" || loginResultString == "Welcome Admin") { 

                        App.Window.MainContentControl.Content = new MainControl { Margin = new Thickness(100)};
                    }
                    await  App.Window.ShowMessageAsync("Login", loginResultString);
                    break;

                case MessageDialogResult.FirstAuxiliary:
                    var nameResult = App.Window.ShowModalInputExternal($"Role : {tile.Title}","Set your name for your role");

                    if (nameResult == null)
                    {
                        await  App.Window.ShowMessageAsync("Register", "You must enter a name");
                    }
                    else
                    {
                        dialogSettings.AffirmativeButtonText = "Register";

                        var registerResult = await App.Window.ShowLoginAsync("Register",
                            $"{tile.Title}: {nameResult} enter your credentials for security", dialogSettings);

                        await   App.Window.ShowMessageAsync("Register",
                            POS.RegisterCheck(nameResult, registerResult?.Username, registerResult?.Password, tile.Title));

                    }
                    break;
            }
        }
    }
}
