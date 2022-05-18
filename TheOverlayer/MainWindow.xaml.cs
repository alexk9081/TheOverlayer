using System.Windows;
using System.Windows.Input;
using TheOverlayer.ViewModels;

namespace WPFTestAppTwo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new FirstSelectionViewModel();
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Can_Execute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Window_Close(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
        private void Window_Maximize(object sender, ExecutedRoutedEventArgs e)
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
        private void Window_Hide(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void FirstSelection_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new FirstSelectionViewModel();
            if (TheOverlayer.Views.OutputView.Timer_Has_Started())
            {
                TheOverlayer.Views.OutputView.End_Timer();
            }
        }
        private void SecondSelection_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new SecondSelectionViewModel();
            if (TheOverlayer.Views.OutputView.Timer_Has_Started())
            {
                TheOverlayer.Views.OutputView.End_Timer();
            }
        }
        private void ScreenSelection_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ScreenSelectionViewModel();
        }

        private void Output_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new OutputViewModel();
        }

    }
}
