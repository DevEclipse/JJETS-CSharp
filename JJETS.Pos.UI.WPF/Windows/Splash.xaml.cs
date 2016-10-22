using System;
using System.Collections.Generic;
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
                MouseDown += (_a, es) =>
                {
                    DragMove();
                };
                await Task.Run(async () =>
                {
                    var i = 0;
                    while (i <= 100)
                    {
                       await Task.Delay(10);
                       Dispatcher.Invoke(() => { LoadLabel.Text = $"Loading ... {i++}%"; });
                    }
                });
                LoadLabel.Text = "Completed";
                await Task.Delay(1000);
                MainWindow.Instance.Show();
                Close();
            };
        }

    }
}
