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

        }



    }
}
