using RepairServiceProgram.DataDB;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Windows;

namespace RepairServiceProgram.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для LogInDialog.xaml
    /// </summary>
    public partial class LogInDialog : Window
    {
        public LogInDialog()
        {
            InitializeComponent();

            MessageBox.Show(new RepairServiceDBEntities().GenerateRepairReport(1).ToList()[0].MasterEmployeeID.ToString());
        }
    }
}
