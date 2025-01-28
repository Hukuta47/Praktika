using System.Windows;
using RepairServiceProgram.Dialogs;

namespace RepairServiceProgram
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (new LogInDialog().ShowDialog() == true)
            {

            }
        }
    }
}
