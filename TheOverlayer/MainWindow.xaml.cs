using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;
using Rectangle = System.Drawing.Rectangle;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
using System.Windows.Input;

namespace WPFTestAppTwo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitTimer(30);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void closeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
        private void maximizeWindow(object sender, ExecutedRoutedEventArgs e)
        {

            if (this.WindowState != WindowState.Maximized)
            {
                SystemCommands.MaximizeWindow(this);
            }
            else
            {
                SystemCommands.RestoreWindow(this);
            }
        }
        private void hideWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private Timer fpsTimer;
        public void InitTimer(int desiredFPS)
        {
            fpsTimer = new Timer();
            fpsTimer.Tick += new EventHandler(repeatedAction);
            fpsTimer.Interval = 1000 / desiredFPS; // in miliseconds
            fpsTimer.Start();
        }
        private void repeatedAction(object sender, EventArgs e)
        {
            screenshotImgOne.Source = screenshotSelection(1, 540, 960);
            screenshotImgTwo.Source = screenshotSelection(2, 540, 960);
        }
        public BitmapSource screenshotSelection(int screen, int Height, int Width)
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
