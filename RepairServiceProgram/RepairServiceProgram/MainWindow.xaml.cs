using System.Windows;
using RepairServiceProgram.Classes;
using RepairServiceProgram.DataDB;
using RepairServiceProgram.Pages;
using RepairServiceProgram.Classes.Objects;
using System.Windows.Controls;
using System.Threading.Tasks;
using RepairServiceProgram.Classes.Enums;
using System.Windows.Media;

namespace RepairServiceProgram
{
    public partial class MainWindow : Window
    {
        public static RepairServiceDBEntities ModelDB;
        public static UserData userData;
        static Label S_Label_PageName;
        static Label S_Label_Notification;
        
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
            S_Label_PageName = Label_PageName;
            S_Label_Notification = Label_Notification;
        }
        public static void ChangeLabelWindow(string Text)
        {
            S_Label_PageName.Content = Text;
        }
        public static void ChangeLabelVibility(Visibility visibility)
        {
            S_Label_PageName.Visibility = visibility;
        }

        public static async Task ShowNotification(string Text, EnumNotification enumNotification)
        {
            S_Label_Notification.Visibility = Visibility.Visible;
            switch (enumNotification)
            {
                case EnumNotification.Succses:
                    S_Label_Notification.Foreground = new SolidColorBrush(Color.FromRgb(14, 128, 18));
                    S_Label_Notification.Content = Text;
                    break;
                case EnumNotification.Warning:
                    S_Label_Notification.Foreground = new SolidColorBrush(Color.FromRgb(161, 103, 18));
                    S_Label_Notification.Content = Text;
                    break;
                case EnumNotification.Error:
                    S_Label_Notification.Foreground = new SolidColorBrush(Color.FromRgb(161, 25, 18));
                    S_Label_Notification.Content = Text;
                    break;
            }
            await Task.Delay(5000);
            S_Label_Notification.Visibility = Visibility.Hidden;
        }










    }
}
