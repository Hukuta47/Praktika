using RepairServiceProgram.Classes;
using RepairServiceProgram.DataDB;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RepairServiceProgram.Pages
{
    public partial class PageRegister : Page
    {
        public PageRegister()
        {
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.GoBack();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            HideErrorMessages();

            string firstName = Textbox_FullNameData.Text.Split(' ')[0];
            string lastName = Textbox_FullNameData.Text.Split(' ')[1];
            string patronymic = Textbox_FullNameData.Text.Split(' ')[2];
            string username = Textbox_UsernameData.Text;
            string phoneNumber = Textbox_NumberPhoneData.Text;
            string password = Textbox_PasswordData.Text;
            string rePassword = Textbox_RePasswordData.Text;

            if (string.IsNullOrEmpty(Textbox_FullNameData.Text))
            {
                ShowErrorMessage(Textblock_ErrFullNameData, "Поле пустое");
                return;
            }

            if (string.IsNullOrEmpty(username))
            {
                ShowErrorMessage(Textblock_ErrUsernameData, "Поле пустое");
                return;
            }

            if (IsUsernameExists(username))
            {
                ShowErrorMessage(Textblock_ErrUsernameData, "Такое имя пользователя уже существует");
                return;
            }

            if (string.IsNullOrEmpty(phoneNumber))
            {
                ShowErrorMessage(Textblock_ErrNumberPhoneData, "Поле пустое");
                return;
            }

            if (IsPhoneNumberExists(phoneNumber))
            {
                ShowErrorMessage(Textblock_ErrNumberPhoneData, "Такой номер телефона уже существует");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowErrorMessage(Textblock_ErrPasswordData, "Поле пустое");
                return;
            }

            if (password != rePassword)
            {
                ShowErrorMessage(Textblock_ErrRePasswordData, "Пароли не совпадают");
                return;
            }

           
            LoginData loginData = new LoginData()
            {
                Username = username,
                Password = rePassword,
            };

            MainWindow.ModelDB.LoginData.Add(loginData);
            MainWindow.ModelDB.SaveChanges();

            Users user = new Users()
            {
                FirstName = firstName,
                LastName = lastName,
                Patronymic = patronymic,
                Phone = phoneNumber,
                LoginDataID = MainWindow.ModelDB.LoginData.FirstOrDefault(n => n.Username == username).LoginDataID,
                UserTypeID = 4
            };

            MainWindow.ModelDB.Users.Add(user);
            MainWindow.ModelDB.SaveChanges();


            Textblock_SuccsesRegister.Visibility = Visibility.Visible;
        }


        private void HideErrorMessages()
        {
            Textblock_ErrFullNameData.Visibility = Visibility.Hidden;
            Textblock_ErrUsernameData.Visibility = Visibility.Hidden;
            Textblock_ErrNumberPhoneData.Visibility = Visibility.Hidden;
            Textblock_ErrPasswordData.Visibility = Visibility.Hidden;
            Textblock_ErrRePasswordData.Visibility = Visibility.Hidden;
        }

        private void ShowErrorMessage(TextBlock textBlock, string message)
        {
            textBlock.Text = message;
            textBlock.Visibility = Visibility.Visible;
        }

        private bool IsUsernameExists(string username)
        {
            return MainWindow.ModelDB.LoginData.Any(data => data.Username == username);
        }

        private bool IsPhoneNumberExists(string phoneNumber)
        {
            return MainWindow.ModelDB.Users.Any(user => user.Phone == phoneNumber);
        }



    }
}
