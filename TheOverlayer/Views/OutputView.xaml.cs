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
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;
using Rectangle = System.Drawing.Rectangle;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace TheOverlayer.Views
{
    /// <summary>
    /// Interaction logic for OutputView.xaml
    /// </summary>
    public partial class OutputView : System.Windows.Controls.UserControl
    {
        public OutputView()
        {
            InitializeComponent();
            Enable_Top_Layer(true);
            Enable_Bottom_Layer(true);
            Init_Timer(30);
        }

        private static Timer fpsTimer;
        private static bool hasRun;
        private static bool topLayerEnabled;
        private static bool bottomLayerEnabled;

        public static void Enable_Top_Layer(bool setting)
        {
            topLayerEnabled = setting;
        }
        public static void Enable_Bottom_Layer(bool setting)
        {
            bottomLayerEnabled = setting;
        }
        public void Init_Timer(int desiredFPS)
        {
            hasRun = true;
            fpsTimer = new Timer();
            fpsTimer.Tick += new EventHandler(Repeated_Action);
            fpsTimer.Interval = 1000 / desiredFPS; // in miliseconds
            estFPS.Text = "Estimated FPS: " + desiredFPS;
            fpsTimer.Start();
        }
        public static bool Timer_Has_Started()
        {
            return hasRun;
        }
        private void Slow_Timer_Speed(object sender, RoutedEventArgs e)
        {
            if (!Timer_Has_Started())
            {
                return;
            }
            End_Timer();
            fpsTimer.Interval = fpsTimer.Interval + (fpsTimer.Interval / 7); // in miliseconds
            estFPS.Text = "Estimated FPS: " + (1000/ fpsTimer.Interval);
            hasRun = true;
            fpsTimer.Start();
        }

        private void Acccelerate_Timer_Speed(object sender, RoutedEventArgs e)
        {
            if (!Timer_Has_Started())
            {
                return;
            }
            End_Timer();
            fpsTimer.Interval = fpsTimer.Interval - (fpsTimer.Interval / 9); // in miliseconds
            estFPS.Text = "Estimated FPS: " + (1000 / fpsTimer.Interval);
            hasRun = true;
            fpsTimer.Start();
        }
        public static void End_Timer()
        {
            hasRun = false;
            fpsTimer.Stop();
        }
        private void Repeated_Action(object sender, EventArgs e)
        {
            if (topLayerEnabled)
            {
                screenshotImgOne.Source = Screenshot_Selection(1, 540, 960);
            }
            if (bottomLayerEnabled)
            {
                screenshotImgTwo.Source = Screenshot_Selection(2, 540, 960);
            }
        }
        public BitmapSource Screenshot_Selection(int screen, int Height, int Width)
        {
            byte[] Buffer = new byte[Width * Height * 4];
            Point screenshotCoord;

            if (screen == 2)
            {
                screenshotCoord = new Point(1920, 0);
            }
            else
            {
                screenshotCoord = new Point(0, 0);
            }

            using (var BMP = new Bitmap(Width, Height, PixelFormat.Format32bppArgb))
            using (var g = Graphics.FromImage(BMP))
            {
                //Gets pixels from the screen
                g.CopyFromScreen(screenshotCoord, Point.Empty, new Size(Width, Height), CopyPixelOperation.SourceCopy);
                g.Flush();

                var BMPBits = BMP.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                Marshal.Copy(BMPBits.Scan0, Buffer, 0, Buffer.Length);
                BMP.UnlockBits(BMPBits);
            }

            var format = PixelFormats.Bgra32;
            var stride = (Width * format.BitsPerPixel + 7) / 8;

            return BitmapSource.Create(Width, Height, 96, 96, format, null, Buffer, stride);
        }
    }
}
