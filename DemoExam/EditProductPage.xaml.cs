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
        public Product SelectedObject {  get; set; }
        private bool IsNewObject { get; set; }

        public List<Type> ComboBoxTypes { get; set; }
        
        // Конструктор добавления нового объекта.
        public EditProductPage()
        {
            InitializeComponent();
            SelectedObject = new Product();
            IsNewObject = true;
        }

        // Конструктор для редактирования существующего объекта.
        public EditProductPage(Product selected)
        {
            InitializeComponent();
            SelectedObject = selected;
            IsNewObject = false;
        }

        // Метод для загрузки данных при загрузке страницы и отображения их в интерфейсе.
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

        // Метод для обработки нажатия кнопки изменения изображения, который открывает диалоговое окно для выбора нового изображения и обновляет отображаемое изображение.
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

        // Метод для обработки нажатия кнопки очистки изображения, который удаляет путь к изображению продукта и обновляет отображаемое изображение.
        private void Button_ClearImage_Click(object sender, RoutedEventArgs e)
        {
            SelectedObject.ImagePath = null;
            Image_Main.GetBindingExpression(Image.SourceProperty).UpdateTarget();
        }

        // Метод для обработки нажатия кнопки сохранения, который проверяет правильность введенных данных, сохраняет изменения в базе данных и возвращается на предыдущую страницу.
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
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

        // Метод для проверки правильности введенных данных.
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
