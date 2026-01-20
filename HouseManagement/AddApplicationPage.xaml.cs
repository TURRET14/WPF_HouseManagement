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
    /// Логика взаимодействия для AddApplicationPage.xaml
    /// </summary>
    public partial class AddApplicationPage : Page
    {
        private Applications selected;
        private bool isNew;
        public AddApplicationPage()
        {
            InitializeComponent();
            selected = new Applications();
            isNew = true;
            SetDataContext();
        }

        public AddApplicationPage(Applications selected)
        {
            InitializeComponent();
            this.selected = selected;
            isNew = false;
            SetDataContext();
        }

        public void SetDataContext()
        {
            DataContext = selected;
            ComboBox_Address.ItemsSource = Emelyanenko_HouseManagementEntities.GetInstance().AllAddresses.ToList();
            ComboBox_Employee.ItemsSource = Emelyanenko_HouseManagementEntities.GetInstance().Employees.ToList();
            ComboBox_Resident.ItemsSource = Emelyanenko_HouseManagementEntities.GetInstance().Residents.ToList();
            ComboBox_Status.ItemsSource = Emelyanenko_HouseManagementEntities.GetInstance().ApplicationStatuses.ToList();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (ComboBox_Address.SelectedItem == null)
            {
                errors.AppendLine("Адрес не заполнен!");
            }
            if (Int32.TryParse(TextBox_Apartment.Text, out int result) == false)
            {
                errors.AppendLine("Номер квартиры не указан!");
            }
            else if (Convert.ToInt32(TextBox_Apartment.Text) < 1)
            {
                errors.AppendLine("Номер квартиры не может быть отрицательным или нулем!");
            }
            if (ComboBox_Resident.SelectedItem == null)
            {
                errors.AppendLine("Жилец не указан!");
            }
            if (String.IsNullOrEmpty(TextBox_Phone.Text))
            {
                errors.AppendLine("Телефон не заполнен!");
            }
            if (String.IsNullOrEmpty(TextBox_Description.Text))
            {
                errors.AppendLine("Описание проблемы не заполнено!");
            }
            if (ComboBox_Employee.SelectedItem == null)
            {
                errors.AppendLine("Ответственный сотрудник не указан!");
            }
            if (ComboBox_Status.SelectedItem == null)
            {
                errors.AppendLine("Статус заявки не указан!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (isNew)
                {
                    Emelyanenko_HouseManagementEntities.GetInstance().Applications.Add(selected);
                }

                if (selected.ApplicationStatuses.Name == "Завершена")
                {
                    selected.DateOfCompletion = DateTime.Now;
                }
                else
                {
                    selected.DateOfCompletion = null;
                }

                Emelyanenko_HouseManagementEntities.GetInstance().SaveChanges();

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при работе с базой данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
