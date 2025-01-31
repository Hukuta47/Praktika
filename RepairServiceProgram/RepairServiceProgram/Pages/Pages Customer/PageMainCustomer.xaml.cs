﻿using System.Linq;
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
            MessageBox.Show(MainWindow.userData.UserID.ToString());
            DataGrid_CustomerOrders.ItemsSource = MainWindow.ModelDB.GetOrdersByCustomer(MainWindow.userData.UserID).ToList();
        }
    }
}
