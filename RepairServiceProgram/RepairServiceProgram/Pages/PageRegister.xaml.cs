using RepairServiceProgram.Classes;
using System.Windows;
using System.Windows.Controls;

namespace RepairServiceProgram.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageRegister.xaml
    /// </summary>
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
    }
}
