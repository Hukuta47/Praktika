using RepairServiceProgram.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RepairServiceProgram.Classes.Objects;
using RepairServiceProgram.Pages.Pages_Customer;


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
            HideErrorMessages();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            HideErrorMessages();

            string username = Textbox_UsernameData.Text;
            string password = PasswordBox_PasswordData.Password;

            if (string.IsNullOrEmpty(username))
            {
                ShowErrorMessage(Textblock_ErrLogin, "Поле пустое");
                return;
            }

            if (!IsUserExists(username))
            {
                ShowErrorMessage(Textblock_ErrLogin, "Такого пользователя нет");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowErrorMessage(Textblock_ErrPassword, "Поле пустое");
                return;
            }

            if (!IsPasswordCorrect(username, password))
            {
                ShowErrorMessage(Textblock_ErrPassword, "Пароль неверный");
                return;
            }

            // Вход удачный

            int LoginDataID = MainWindow.ModelDB.LoginData.FirstOrDefault(b => b.Username == username).LoginDataID;
            MainWindow.userData = new UserData
            {
                UserID = MainWindow.ModelDB.Users.FirstOrDefault(a => a.LoginDataID == LoginDataID).UserID,

                FirstName = MainWindow.ModelDB.Users.FirstOrDefault(a => a.LoginDataID == LoginDataID).FirstName,

                LastName = MainWindow.ModelDB.Users.FirstOrDefault(a => a.LoginDataID == LoginDataID).LastName,

                Patronymic = MainWindow.ModelDB.Users.FirstOrDefault(a => a.LoginDataID == LoginDataID).Patronymic,

                Phone = MainWindow.ModelDB.Users.FirstOrDefault(a => a.LoginDataID == LoginDataID).Phone,
            };

            PageManager.MainFrame.Navigate(new PageMainCustomer());

            
        }

        private void HideErrorMessages()
        {
            Textblock_ErrLogin.Visibility = Visibility.Hidden;
            Textblock_ErrPassword.Visibility = Visibility.Hidden;
        }

        private void ShowErrorMessage(TextBlock textBlock, string message)
        {
            textBlock.Text = message;
            textBlock.Visibility = Visibility.Visible;
        }

        private bool IsUserExists(string username)
        {
            return MainWindow.ModelDB.LoginData.Any(data => data.Username == username);
        }

        private bool IsPasswordCorrect(string username, string password)
        {
            return MainWindow.ModelDB.LoginData.Any(data => data.Username == username && data.Password == password);
        }
    }
}
