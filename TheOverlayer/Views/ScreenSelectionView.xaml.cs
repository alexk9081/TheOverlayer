using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheOverlayer.Views
{
    /// <summary>
    /// Interaction logic for ScreenSelectionView.xaml
    /// </summary>
    public partial class ScreenSelectionView : System.Windows.Controls.UserControl
    {
        public ScreenSelectionView()
        {
            InitializeComponent();

            foreach (var screen in Screen.AllScreens)
            {
                // For each screen, add the screen properties to a list box.
                Console.WriteLine("Device Name: " + screen.DeviceName);
                Console.WriteLine("Bounds: " + screen.Bounds.ToString());
                Console.WriteLine("Type: " + screen.GetType().ToString());
                Console.WriteLine("Working Area: " + screen.WorkingArea.ToString());
                Console.WriteLine("Primary Screen: " + screen.Primary.ToString());
            }
        }
    }
}
