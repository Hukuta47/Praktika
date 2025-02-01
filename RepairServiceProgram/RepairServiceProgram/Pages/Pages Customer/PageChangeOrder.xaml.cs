using RepairServiceProgram.Classes.Enums;
using RepairServiceProgram.Classes;
using RepairServiceProgram.DataDB;
using System.Windows;
using System.Windows.Controls;
using System.Linq;


namespace RepairServiceProgram.Pages.Pages_Customer
{
    public partial class PageChangeOrder : Page
    {
        Orders order;
        public PageChangeOrder(Orders order)
        {
            InitializeComponent();
            this.order = order;

            Textbox_TechTypeName.Text = order.Techs.TechTypeName;
            Textbox_Manufacturer.Text = order.Techs.Manufacturer;
            Textbox_Model.Text = order.Techs.Model;
            Textbox_DescriptionProblem.Text = order.Description;
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.GoBack();
            MainWindow.ChangeLabelWindow($"Добро пожаловать! {MainWindow.userData.FirstName} {MainWindow.userData.Patronymic}");
        }

        private void ChangeOrder_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ModelDB.Orders.FirstOrDefault(n => n.OrderID == order.OrderID).Description = Textbox_DescriptionProblem.Text;
            MainWindow.ModelDB.SaveChanges();



            PageManager.MainFrame.GoBack();
            MainWindow.ChangeLabelWindow($"Добро пожаловать! {MainWindow.userData.FirstName} {MainWindow.userData.Patronymic}");
            MainWindow.ShowNotification("Заявка изменена.", EnumNotification.Warning);
            PageMainCustomer.RefreshDataGrid();

        }
    }
}
