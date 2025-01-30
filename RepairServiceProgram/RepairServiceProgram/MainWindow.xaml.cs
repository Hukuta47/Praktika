using System.Windows;
using RepairServiceProgram.Classes;
using RepairServiceProgram.DataDB;
using RepairServiceProgram.Pages;

namespace RepairServiceProgram
{
    public partial class MainWindow : Window
    {
        public static RepairServiceDBEntities ModelDB;

        public static string UsernameData;
        public static string PasswordData;
        public MainWindow()
        {
            InitializeComponent();
            InitializeProgramData();
            ModelDB = new RepairServiceDBEntities();
            PageManager.MainFrame.Navigate(new PageLogin());
        }
        void InitializeProgramData()
        {
            PageManager.MainFrame = MainFrame;
        }
    }
}
