using RepairServiceProgram.Classes;
using System.Windows;
using System.Windows.Controls;


namespace RepairServiceProgram.Pages
{

    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new PageRegister());
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.UsernameData = Textbox_UsernameData.Text;
            MainWindow.PasswordData = PasswordBox_PasswordData.Password;

            PageManager.MainFrame.Navigate(new PageMain(Textbox_UsernameData.Text));

            PageManager.MainFrame.NavigationService.RemoveBackEntry();
        }
    }
}
