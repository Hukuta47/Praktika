using System.Windows;
using RepairServiceProgram.Classes;
using RepairServiceProgram.Pages;

namespace RepairServiceProgram
{
    public partial class MainWindow : Window
    {
        public static string UsernameData;
        public static string PasswordData;
        public MainWindow()
        {
            InitializeComponent();
            InitializeProgramData();
            PageManager.MainFrame.Navigate(new PageLogin());
        }
        void InitializeProgramData()
        {
            PageManager.MainFrame = MainFrame;
        }
    }
}
