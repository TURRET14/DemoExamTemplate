using Microsoft.Win32;
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
    /// Логика взаимодействия для EditProductPage.xaml
    /// </summary>
    public partial class EditProductPage : Page
    {
        public Products SelectedObject {  get; set; }
        private bool IsNewObject { get; set; }

        public List<Types> ComboBoxTypes { get; set; }
        
        // Конструктор - Для нового объекта.
        public EditProductPage()
        {
            InitializeComponent();
            SelectedObject = new Products();
            IsNewObject = true;
        }

        // Конструктор - Для редактирования указанного объекта.
        public EditProductPage(Products selected)
        {
            InitializeComponent();
            SelectedObject = selected;
            IsNewObject = false;
        }

        // Загрузка данных в ComboBox.
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBoxTypes = DBEntities.GetInstance().Type.ToList();
            }
            catch
            {
                MessageHelper.ShowErrorMessage("Ошибка при получении данных из базы данных!");
                return;
            }

            DataContext = null;
            DataContext = this;
        }

        // Смена картинки объекта.
        private void Button_ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Ofd = new OpenFileDialog();
            Ofd.Filter = "(*.png, *.jpg, *.jpeg)|*.png;*.jpg;*.jpeg";
            if (Ofd.ShowDialog() == true)
            {
                SelectedObject.ImagePathGetSet = Ofd.FileName;
                
                Image_Main.GetBindingExpression(Image.SourceProperty).UpdateTarget();
            }
        }

        // Очистка картинки объекта.
        private void Button_ClearImage_Click(object sender, RoutedEventArgs e)
        {
            SelectedObject.ImagePath = null;
            Image_Main.GetBindingExpression(Image.SourceProperty).UpdateTarget();
        }

        // Сохранение объекта.
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                // Если объект новый - Сначала добавляем его в базу данных.
                if (IsNewObject)
                {
                    DBEntities.GetInstance().Product.Add(SelectedObject);
                }

                DBEntities.GetInstance().SaveChanges();
            }
            catch
            {
                MessageHelper.ShowErrorMessage("Ошибка при сохранении данных!");
                return;
            }


            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        // Проверка введенных данных.
        private bool ValidateInput()
        {
            if (!ValidationService.IsStringNotEmpty(SelectedObject.Name, "Название"))
            {
                return false;
            }
            if (!ValidationService.IsNumberGreaterThanOrEqualToZero(SelectedObject.Price, "Цена"))
            {
                return false;
            }
            if (SelectedObject.Type is null)
            {
                MessageHelper.ShowErrorMessage("Выберите Тип!");
                return false;
            }

            return true;
        }
    }
}
