using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using JJETS.Pos.Models;

namespace JJETS.Pos.UI.WPF.Windows
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash
    {
        public Splash()
        {
            InitializeComponent();
            Loaded +=  async (_, e) =>
            {
                App.Context = new Context("name=Offline");
                try
                {
                    LoadLabel.Text = "Connecting... to Database";
                    await App.Context.Database.Connection.OpenAsync();
                    await Task.Delay(2500);
                }
                catch
                {
                    await Task.Delay(2500);
                    LoadLabel.Text = "Connection Failed Exiting...";
                    await Task.Delay(2500);
                    Application.Current.Shutdown();
                }

                await Task.Run(async () =>
                {
                    if (App.Context.Database.Connection.State != ConnectionState.Open || !App.Context.Database.Exists())
                        return;
                    await Dispatcher.InvokeAsync(() => { LoadLabel.Text = "Loading... Database"; });
                    await App.Context.Users.LoadAsync();
                    await App.Context.Managers.LoadAsync();
                    await App.Context.Employees.LoadAsync();
                    await App.Context.Customers.LoadAsync();
                    await App.Context.Items.LoadAsync();
                    await App.Context.Categories.LoadAsync();
                    await App.Context.Suppliers.LoadAsync();
                    await App.Context.Stocks.LoadAsync();
                    await App.Context.Locations.LoadAsync();
                    await App.Context.Stores.LoadAsync();
                    await App.Context.TransactionItems.LoadAsync();
                    await App.Context.Transactions.LoadAsync();
                    await App.Context.Notifications.LoadAsync(); ;
                });

                await Task.Delay(100);
                LoadLabel.Text = "Initializing... UI";
                await App.Context.Admins.LoadAsync();
                await Task.Delay(100);
                LoadLabel.Text = "UI Initialized";

                await Task.Delay(100);
                LoadLabel.Text = "Initializing... JJETS";
                await Task.Delay(100);
                LoadLabel.Text = "JJETS Initialized";

                await Task.Delay(500);
                LoadLabel.Text = "Starting...";
                await Task.Delay(500);
                LoadLabel.Text = "Completed";
                await Task.Delay(1000);
                App.Window = new MainWindow();
                App.Window.Show();
                Close();
            };
        }

    }
}
