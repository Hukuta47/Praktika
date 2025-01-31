using System.Windows;
using RepairServiceProgram.Classes;
using RepairServiceProgram.DataDB;
using RepairServiceProgram.Pages;
using RepairServiceProgram.Classes.Objects;

namespace RepairServiceProgram
{
    public partial class MainWindow : Window
    {
        public static RepairServiceDBEntities ModelDB;
        public static UserData userData;
        
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
