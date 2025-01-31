using RepairServiceProgram.Classes;
using RepairServiceProgram.DataDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RepairServiceProgram.Pages.Pages_Customer
{
    public partial class PageCreateOrder : Page
    {
        public PageCreateOrder()
        {
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.GoBack();
        }

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            string DescriptionProblem = Textbox_DescriptionProblem.Text;
            string TechTypeName = Textbox_TechTypeName.Text;
            string Manufacturer = Textbox_Manufacturer.Text;
            string Model = Textbox_Model.Text;

            Techs techs = new Techs()
            {
                TechTypeName = TechTypeName,
                Manufacturer = Manufacturer,
                Model = Model
            };
            MainWindow.ModelDB.Techs.Add(techs);
            MainWindow.ModelDB.SaveChanges();

            int newTechID = MainWindow.ModelDB.Techs.FirstOrDefault(t => t.TechTypeName == TechTypeName && t.Manufacturer == Manufacturer && t.Model == Model).TechID;

            MainWindow.ModelDB.CreateRepairRequest(MainWindow.userData.UserID, newTechID, DescriptionProblem);




        }
    }
}
