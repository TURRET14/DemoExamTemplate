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
        }

        // Метод для загрузки данных при загрузке страницы и проверки прав доступа текущего пользователя.
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFilterData();
            LoadMainData();
            SearchAndFilter();

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

        // Методы для загрузки данных из базы данных и отображения их в интерфейсе.
        private void LoadMainData()
        {
            try
            {
                MainDataList = DBEntities.GetInstance().Product.ToList();
            }
            catch
            {
                MessageHelper.ShowErrorMessage("Ошибка при получении данных из базы данных!");
                return;
            }
            
            ListBox_Main.ItemsSource = null;
            ListBox_Main.ItemsSource = MainDataList;
        }

        private void LoadFilterData()
        {
            try
            {
                FilterDataList = DBEntities.GetInstance().Type.ToList();
            }
            catch
            {
                MessageHelper.ShowErrorMessage("Ошибка при получении данных из базы данных!");
                return;
            }
            FilterDataList.Insert(0, FilterAllObject);
            
            ComboBox_Filter.ItemsSource = null;
            ComboBox_Filter.ItemsSource = FilterDataList;
        }

        // Метод перехода на страницу редактирования для добавления нового объекта.
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProductPage());
        }

        // Метод для удаления выбранного объекта из базы данных с подтверждением действия.
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            Product selected = ListBox_Main.SelectedItem as Product;
            if (!(selected is null))
            {
                if (MessageHelper.ShowConfirmationMessage("Вы уверены, что хотите удалить товар?"))
                {
                    try
                    {
                        DBEntities.GetInstance().Product.Remove(selected);
                        DBEntities.GetInstance().SaveChanges();
                    }
                    catch
                    {
                        MessageHelper.ShowErrorMessage("Ошибка при удалении данных из базы данных!");
                        return;
                    }

                    LoadMainData();
                }
            }
        }
        // Методы для сортировки, фильтрации и поиска данных в интерфейсе.
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

        // Метод для применения сортировки, фильтрации и поиска к данным и обновления отображения в интерфейсе.
        private void SearchAndFilter()
        {
            List<Product> data;
            try
            {
                data = DBEntities.GetInstance().Product.ToList();
            }
            catch
            {
                MessageHelper.ShowErrorMessage("Ошибка при получении данных из базы данных!");
                return;
            }

            // Сортировка
            // По убыванию
            if (ComboBox_Sort.SelectedIndex == 0)
            {
                data = data.OrderByDescending(entry => entry.Price).ToList();
            }
            // По возрастанию
            else if (ComboBox_Sort.SelectedIndex == 1)
            {
                data = data.OrderBy(entry => entry.Price).ToList();
            }

            // Фильтрация
            if (ComboBox_Filter.SelectedItem != FilterAllObject && ComboBox_Filter.SelectedItem != null)
            {
                data = data.Where(entry => entry.Type == ComboBox_Filter.SelectedItem).ToList();
            }

            // Поиск
            if (!String.IsNullOrEmpty(TextBox_Search.Text))
            {
                data = data.Where(entry => entry.Name.Contains(TextBox_Search.Text)).ToList();
            }

            MainDataList = data;
            ListBox_Main.ItemsSource = null;
            ListBox_Main.ItemsSource = MainDataList;
        }

        // Метод для сброса сортировки, фильтрации и поиска и обновления отображения данных в интерфейсе.
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

        // Метод для перехода на страницу редактирования для изменения существующего объекта при двойном клике на элементе списка.
        private void ListBox_Main_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Product selected = ListBox_Main.SelectedItem as Product;

            if (selected != null)
            {
                if (UserData.CurrentUser != null && UserData.CurrentUser.Role != null)
                {
                    if (UserData.CurrentUser.Role.Name == "Администратор")
                    {
                        NavigationService.Navigate(new EditProductPage(selected));
                    }
                }
            }
        }
    }
}