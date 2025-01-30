using RepairServiceProgram.Classes;
using System.Collections.Generic;
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


            //string Fullname = Textbox_FullNameData.Text;
            //string FirstName = Fullname.Split(' ')[0];
            //string LastName = Fullname.Split(' ')[1];
            //string Patronymic = Fullname.Split(' ')[2];
            //string Username = Textbox_UsernameData.Text;
            //string NumberPhone = Textbox_NumberPhoneData.Text;

            //string FirstPassword = Textbox_PasswordData.Text;
            //string SecondPassword = Textbox_RePasswordData.Text;


            List<TextBox> textBoxs = new List<TextBox>()
            {
                Textbox_FullNameData,
                Textbox_NumberPhoneData,
                Textbox_UsernameData,
                Textbox_PasswordData,
                Textbox_RePasswordData
            };
            List<TextBlock> textBlocks = new List<TextBlock>()
            {
                Textblock_ErrFullNameData,
                Textblock_ErrNumberPhoneData,
                Textblock_ErrUsernameData,
                Textblock_ErrPasswordData,
                Textblock_ErrRePasswordData
            };
            for (int i = 0; i < textBoxs.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(textBoxs[i].Text))
                {
                    textBlocks[i].Visibility = Visibility.Visible;
                    textBlocks[i].Text = "Бля ты еблан?";
                }
                else
                {
                    textBlocks[i].Visibility = Visibility.Hidden;
                    textBlocks[i].Text = "";

                    string Fullname = Textbox_FullNameData.Text;
                    string FirstName = Fullname.Split(' ')[0];
                    string LastName = Fullname.Split(' ')[1];
                    string Patronymic = Fullname.Split(' ')[2];
                    string Username = Textbox_UsernameData.Text;
                    string NumberPhone = Textbox_NumberPhoneData.Text;

                    string FirstPassword = Textbox_PasswordData.Text;
                    string SecondPassword = Textbox_RePasswordData.Text;




                }
                
            }









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



    }
}
