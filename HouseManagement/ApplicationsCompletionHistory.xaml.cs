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
        Сотрудники selectAllEmployeesObject = new Сотрудники() { ФИО = "Все сотрудники" };
        СписокЖилогоФонда selectAllAddressesObject = new СписокЖилогоФонда() { Адрес = "Все адреса" };
        public ApplicationsCompletionHistory()
        {
            InitializeComponent();
            DataGrid_Main.ItemsSource = Emelyanenko_HouseManagementEntities.GetInstance().Заявки.Where((application) => application.СтатусыЗаявок.Название == "Завершена").ToList();


            List<Сотрудники> employeesList = Emelyanenko_HouseManagementEntities.GetInstance().Сотрудники.ToList();
            employeesList.Add(selectAllEmployeesObject);
            ComboBox_Employee.ItemsSource = employeesList;

            List<СписокЖилогоФонда> addressesList = Emelyanenko_HouseManagementEntities.GetInstance().СписокЖилогоФонда.ToList();
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
            List<Заявки> list = Emelyanenko_HouseManagementEntities.GetInstance().Заявки.Where((application) => application.СтатусыЗаявок.Название == "Завершена").ToList();
            if (ComboBox_Employee.SelectedItem != null)
            {
                if (ComboBox_Employee.SelectedItem != selectAllEmployeesObject)
                {
                    list = list.Where((application) => application.Сотрудники == ComboBox_Employee.SelectedItem).ToList();
                }
            }

            if (ComboBox_Address.SelectedItem != null)
            {
                if (ComboBox_Address.SelectedItem != selectAllAddressesObject)
                {
                    list = list.Where((application) => application.СписокЖилогоФонда == ComboBox_Address.SelectedItem).ToList();
                }
            }

            DataGrid_Main.ItemsSource = list;
        }
    }
}
