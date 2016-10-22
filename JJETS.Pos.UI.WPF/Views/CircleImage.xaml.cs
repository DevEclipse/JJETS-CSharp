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

namespace JJETS.Pos.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for CircleImage.xaml
    /// </summary>
    public partial class CircleImage : UserControl
    {

        public ImageSource CircleSourceImage
        {
            get { return (ImageSource)GetValue(CircleSourceImageProperty); }
            set { SetValue(CircleSourceImageProperty, value); }
        }

        public static readonly DependencyProperty CircleSourceImageProperty =
            DependencyProperty.Register("CircleSourceImage", typeof(ImageSource), typeof(CircleImage));


        public CircleImage()
        {
            InitializeComponent();
        }
    }
}
