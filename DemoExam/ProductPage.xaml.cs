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

namespace DemoExam
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public Type FilterAllObject { get; set; } = new Type { Name = "Все" };
        public List<Type> FilterDataList { get; set; }
        public List<Product> MainDataList { get; set; }

        public ProductPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFilterData();
            LoadMainData();

            if (UserData.CurrentUser != null && UserData.CurrentUser.Role != null)
            {
                switch (UserData.CurrentUser.Role.Name)
                {
                    case "Пользователь":
                        break;
                    case "Менеджер":
                        break;
                    case "Администратор":
                        break;
                }
            }
            else
            {
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
        }

        private void LoadMainData()
        {
            MainDataList = DBEntities.GetInstance().Product.ToList();
            
            DataGrid_Main.ItemsSource = null;
            DataGrid_Main.ItemsSource = MainDataList;
        }

        private void LoadFilterData()
        {
            FilterDataList = DBEntities.GetInstance().Type.ToList();
            FilterDataList.Insert(0, FilterAllObject);
            
            ComboBox_Filter.ItemsSource = null;
            ComboBox_Filter.ItemsSource = FilterDataList;
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            // NavigationService.Navigate();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            Product selected = DataGrid_Main.SelectedItem as Product;
            if (!(selected is null))
            {
                if (MessageHelper.ShowConfirmationMessage("Вы уверены, что хотите удалить товар?"))
                {
                    DBEntities.GetInstance().Product.Remove(selected);
                    DBEntities.GetInstance().SaveChanges();
                    
                    LoadMainData();
                }
            }
        }

        private void ComboBox_Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchAndFilter();
        }

        private void ComboBox_Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchAndFilter();
        }

        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchAndFilter();
        }

        private void SearchAndFilter()
        {
            // Сортировка
            List<Product> data = DBEntities.GetInstance().Product.ToList();
            
            // По убыванию
            if (ComboBox_Sort.SelectedIndex == 0)
            {
                data = data.OrderByDescending(entry => entry.Price).ToList();
            }
            // По возрастанию
            else if (ComboBox_Filter.SelectedIndex == 1)
            {
                data = data.OrderBy(entry => entry.Price).ToList();
            }

            // Фильтрация
            if (ComboBox_Filter.SelectedItem != FilterAllObject && ComboBox_Filter.SelectedItem != null)
            {
                data = data.Where(entry => entry.Type == ComboBox_Filter.SelectedItem).ToList();
            }

            // Поиск
            if (!string.IsNullOrEmpty(TextBox_Search.Text))
            {
                data = data.Where(entry => entry.Name.Contains(TextBox_Search.Text)).ToList();
            }

            MainDataList = data;
            DataGrid_Main.ItemsSource = null;
            DataGrid_Main.ItemsSource = MainDataList;
        }

        private void Button_Filters_Clear_Click(object sender, RoutedEventArgs e)
        {
            ComboBox_Sort.SelectedItem = null;
            ComboBox_Filter.SelectedItem = null;
            TextBox_Search.Text = "";

            LoadMainData();
        }

        private void Button_Orders_Click(object sender, RoutedEventArgs e)
        {
            // NavigationService.Navigate();
        }
    }
}