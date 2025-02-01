using RepairServiceProgram.DataDB;
using RepairServiceProgram.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RepairServiceProgram.Pages.Pages_Customer
{
    public partial class PageMainCustomer : Page
    {
        static DataGrid S_DataGrid_CustomerOrders;
        public PageMainCustomer()
        {
            InitializeComponent();
            S_DataGrid_CustomerOrders = DataGrid_CustomerOrders;
            MainWindow.ChangeLabelVibility(Visibility.Visible);
            MainWindow.ChangeLabelWindow($"Добро пожаловать! {MainWindow.userData.FirstName} {MainWindow.userData.Patronymic}");
            DataGrid_CustomerOrders.ItemsSource = MainWindow.ModelDB.GetOrdersByCustomer(MainWindow.userData.UserID).ToList();
        }

        private void CreateOrder(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new PageCreateOrder());
            MainWindow.ChangeLabelWindow("Создание заявки");
        }

        private void RefreshDataGrid_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_CustomerOrders.ItemsSource = null;
            DataGrid_CustomerOrders.ItemsSource = MainWindow.ModelDB.GetOrdersByCustomer(MainWindow.userData.UserID).ToList();
        }
        public static void RefreshDataGrid()
        {
            S_DataGrid_CustomerOrders.ItemsSource = null;
            S_DataGrid_CustomerOrders.ItemsSource = MainWindow.ModelDB.GetOrdersByCustomer(MainWindow.userData.UserID).ToList();
        }
        private void ChangeOrder_Click(object sender, RoutedEventArgs e)
        {
            int idOrder = ((GetOrdersByCustomer_Result)DataGrid_CustomerOrders.SelectedItem).OrderID;


            PageManager.MainFrame.Navigate(new PageChangeOrder(MainWindow.ModelDB.Orders.FirstOrDefault(n => n.OrderID == idOrder)));
            MainWindow.ChangeLabelWindow("Изменение заявки");
        }

        private void DataGrid_CustomerOrders_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (DataGrid_CustomerOrders.SelectedItem == null)
            {
                Button_ChangeOrder.IsEnabled = false;
            }
            else
            {
                Button_ChangeOrder.IsEnabled = true;
            }
        }
    }
}
