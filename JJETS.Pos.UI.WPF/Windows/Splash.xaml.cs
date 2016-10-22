using System;
using System.Collections.Generic;
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
                LoadLabel.Text = "Connecting... Database";
                await Task.Delay(100);
                LoadingBar.Value = 3;
                await Task.Delay(100);
                LoadLabel.Text = "Database Connected";
                LoadingBar.Value = 6;

                await Task.Delay(100);
                LoadLabel.Text = "Connecting... Users";
                await App.Context.Users.LoadAsync();
                LoadingBar.Value = 9;
                await Task.Delay(100);
                LoadLabel.Text = "Users Connected";
                LoadingBar.Value = 12;

                await Task.Delay(100);
                LoadLabel.Text = "Connecting... Managers";
                await App.Context.Managers.LoadAsync();
                LoadingBar.Value = 18;
                await Task.Delay(100);
                LoadLabel.Text = "Managers Connected";
                LoadingBar.Value = 21;

                await Task.Delay(100);
                LoadLabel.Text = "Connecting... Employees";
                await App.Context.Employees.LoadAsync();
                LoadingBar.Value = 24;
                await Task.Delay(100);
                LoadLabel.Text = "Employees Connected";
                LoadingBar.Value = 27;

                await Task.Delay(100);
                LoadLabel.Text = "Connecting... Customers";
                await App.Context.Customers.LoadAsync();
                LoadingBar.Value = 30;
                await Task.Delay(100);
                LoadLabel.Text = "Customers Connected";
                LoadingBar.Value = 33;

                await Task.Delay(100);
                LoadLabel.Text = "Connecting... Items";
                await App.Context.Items.LoadAsync();
                LoadingBar.Value = 36;
                await Task.Delay(100);
                LoadLabel.Text = "Items Connected";
                LoadingBar.Value = 39;

                await Task.Delay(100);
                LoadLabel.Text = "Connecting... Categories";
                await App.Context.Categories.LoadAsync();
                LoadingBar.Value = 42;
                await Task.Delay(100);
                LoadLabel.Text = "Categories Connected";
                LoadingBar.Value = 45;

                await Task.Delay(100);
                LoadLabel.Text = "Connecting... Suppliers";
                await App.Context.Suppliers.LoadAsync();
                LoadingBar.Value = 48;
                await Task.Delay(100);
                LoadLabel.Text = "Suppliers Connected";
                LoadingBar.Value = 51;

                await Task.Delay(100);
                LoadLabel.Text = "Connecting... Stocks";
                await App.Context.Stocks.LoadAsync();
                LoadingBar.Value = 54;
                await Task.Delay(100);
                LoadLabel.Text = "Stocks Connected";
                LoadingBar.Value = 57;

                await Task.Delay(100);
                LoadLabel.Text = "Connecting... Locations";
                await App.Context.Locations.LoadAsync();
                LoadingBar.Value = 60;
                await Task.Delay(100);
                LoadLabel.Text = "Locations Connected";
                LoadingBar.Value = 63;

                await Task.Delay(100);
                LoadLabel.Text = "Connecting... Stores";
                await App.Context.Stores.LoadAsync();
                LoadingBar.Value = 66;
                await Task.Delay(100);
                LoadLabel.Text = "Stores Connected";
                LoadingBar.Value = 69;

                await Task.Delay(100);
                LoadLabel.Text = "Connecting... TransactionItems";
                await App.Context.TransactionItems.LoadAsync();
                LoadingBar.Value = 72;
                await Task.Delay(100);
                LoadLabel.Text = "TransactionItems Connected";
                LoadingBar.Value = 75;

                await Task.Delay(100);
                LoadLabel.Text = "Connecting... Transactions";
                await App.Context.Transactions.LoadAsync();
                LoadingBar.Value = 78;
                await Task.Delay(100);
                LoadLabel.Text = "Transactions Connected";
                LoadingBar.Value = 81;

                await Task.Delay(100);
                LoadLabel.Text = "Initializing... UI";
                await App.Context.Admins.LoadAsync();
                LoadingBar.Value = 83;
                await Task.Delay(100);
                LoadLabel.Text = "UI Initialized";
                LoadingBar.Value = 86;

                await Task.Delay(100);
                LoadLabel.Text = "Initializing... JJETS";
                LoadingBar.Value = 89;
                await Task.Delay(100);
                LoadLabel.Text = "JJETS Initialized";

                await Task.Delay(500);
                LoadLabel.Text = "Starting...";
                LoadingBar.Value = 100;
                await Task.Delay(500);
                LoadLabel.Text = "Completed";
                await Task.Delay(1000);
                MainWindow.Instance.Show();
                Close();
            };
        }

    }
}
