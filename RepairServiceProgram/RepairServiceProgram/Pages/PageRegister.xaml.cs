using RepairServiceProgram.Classes;
using RepairServiceProgram.DataDB;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            if (string.IsNullOrEmpty(Textbox_FullNameData.Text))
            {
                Textblock_ErrFullNameData.Visibility = Visibility.Visible;
                Textblock_ErrFullNameData.Text = "Поле пустрое";
            }

            string Fullname = Textbox_FullNameData.Text;
            string FirstName = Fullname.Split(' ')[0];
            string LastName = Fullname.Split(' ')[1];
            string Patronymic = Fullname.Split(' ')[2];
            string Username = Textbox_UsernameData.Text;
            string NumberPhone = Textbox_NumberPhoneData.Text;

            string FirstPassword = Textbox_PasswordData.Text;
            string SecondPassword = Textbox_RePasswordData.Text;











            /*
            LoginData loginData = new LoginData()
            {
                Username = Username,
                Password = SecondPassword
            };


            Users users = new Users()
            {
                
            };
            */

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

        private bool IsUserExists(string username)
        {
            return MainWindow.ModelDB.LoginData.Any(data => data.Username == username);
        }
        bool CheckIsNullOrEmptyFields()
        {
            List<TextBox> textBoxs = new List<TextBox>()
            {
                Textbox_FullNameData,
                Textbox_UsernameData,
                Textbox_NumberPhoneData,
                Textbox_PasswordData,
                Textbox_RePasswordData
            };
            List<TextBlock> textBlocks = new List<TextBlock>()
            {
                Textblock_ErrFullNameData,
                Textblock_ErrUsernameData,
                Textblock_ErrNumberPhoneData,
                Textblock_ErrPasswordData,
                Textblock_ErrRePasswordData
            };
            for (int i = 0; i < textBoxs.Count; i++)
            {
                if (string.IsNullOrEmpty(textBoxs[i].Text))
                {
                    textBlocks[i].Text = "Поле пустрое";
                    textBlocks[i].Visibility = Visibility.Visible;
                }
                else
                {
                    textBlocks[i].Text = "";
                    textBlocks[i].Visibility = Visibility.Hidden;
                }
            }
        }
        private bool IsPasswordCorrect(string username, string password)
        {
            return MainWindow.ModelDB.LoginData.Any(data => data.Username == username && data.Password == password);
        }



    }
}
