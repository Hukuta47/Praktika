using RepairServiceProgram.DataDB;
using RepairServiceProgram.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RepairServiceProgram.Pages.Pages_Customer
{
    public partial class PageMainCustomer : Page
    {
        public PageMainCustomer()
        {
            InitializeComponent();
            Label_HelloLabel.Content += $"{MainWindow.userData.FirstName} {MainWindow.userData.Patronymic}";
            DataGrid_CustomerOrders.ItemsSource = MainWindow.ModelDB.GetOrdersByCustomer(MainWindow.userData.UserID).ToList();
        }

        private void CreateOrder(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new PageCreateOrder());
        }

        private void RefreshDataGrid_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_CustomerOrders.Items.Refresh();
        }
    }
}
