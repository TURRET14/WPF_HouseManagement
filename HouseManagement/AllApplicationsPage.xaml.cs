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
    /// Логика взаимодействия для ApplicationsPage.xaml
    /// </summary>
    public partial class ApplicationsPage : Page
    {
        public ApplicationsPage()
        {
            InitializeComponent();
            DataGrid_Main.ItemsSource = Emelyanenko_HouseManagementEntities.GetInstance().Заявки.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid_Main.ItemsSource = Emelyanenko_HouseManagementEntities.GetInstance().Заявки.ToList();
        }

        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddApplicationPage());
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddApplicationPage(DataGrid_Main.SelectedItem as Заявки));
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Main.SelectedItem != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить заявку?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Emelyanenko_HouseManagementEntities.GetInstance().Заявки.Remove(DataGrid_Main.SelectedItem as Заявки);
                        Emelyanenko_HouseManagementEntities.GetInstance().SaveChanges();
                        DataGrid_Main.ItemsSource = Emelyanenko_HouseManagementEntities.GetInstance().Заявки.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка при работе с базой данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Button_ApplicationsCompletetionHistory_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ApplicationsCompletionHistory());
        }
    }
}
