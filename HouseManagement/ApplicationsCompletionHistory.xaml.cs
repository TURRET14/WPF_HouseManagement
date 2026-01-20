using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HouseManagement
{
    /// <summary>
    /// Логика взаимодействия для ApplicationsCompletionHistory.xaml
    /// </summary>
    public partial class ApplicationsCompletionHistory : Page
    {
        Employees selectAllEmployeesObject = new Employees() { FIO = "Все Сотрудники" };
        AllAddresses selectAllAddressesObject = new AllAddresses() { Name = "Все адреса" };
        public ApplicationsCompletionHistory()
        {
            InitializeComponent();
            DataGrid_Main.ItemsSource = Emelyanenko_HouseManagementEntities.GetInstance().Applications.Where((application) => application.ApplicationStatuses.Name == "Завершена").ToList();


            List<Employees> employeesList = Emelyanenko_HouseManagementEntities.GetInstance().Employees.ToList();
            employeesList.Add(selectAllEmployeesObject);
            ComboBox_Employee.ItemsSource = employeesList;

            List<AllAddresses> addressesList = Emelyanenko_HouseManagementEntities.GetInstance().AllAddresses.ToList();
            addressesList.Add(selectAllAddressesObject);
            ComboBox_Address.ItemsSource = addressesList;
        }

        private void ComboBox_Employee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void TextBox_Address_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            List<Applications> list = Emelyanenko_HouseManagementEntities.GetInstance().Applications.Where((application) => application.ApplicationStatuses.Name == "Завершена").ToList();
            if (ComboBox_Employee.SelectedItem != null)
            {
                if (ComboBox_Employee.SelectedItem != selectAllEmployeesObject)
                {
                    list = list.Where((application) => application.Employees == ComboBox_Employee.SelectedItem).ToList();
                }
            }

            if (ComboBox_Address.SelectedItem != null)
            {
                if (ComboBox_Address.SelectedItem != selectAllAddressesObject)
                {
                    list = list.Where((application) => application.AllAddresses == ComboBox_Address.SelectedItem).ToList();
                }
            }

            DataGrid_Main.ItemsSource = list;
        }
    }
}
