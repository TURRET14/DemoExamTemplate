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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBox_Login.Text;
            string password = PasswordBox_Password.Password;

            if (!ValidationService.IsStringNotEmpty(login, "Логин"))
            {
                return;
            }
            if (!ValidationService.IsStringNotEmpty(password, "Пароль"))
            {
                return;
            }

            User user = DBEntities.GetInstance().User.FirstOrDefault(entry => entry.Login == login && entry.Password == password);

            if (user is null)
            {
                MessageHelper.ShowErrorMessage("Неверный логин или пароль!");
            }
            else
            {
                UserData.CurrentUser = user;
                
                NavigationService.Navigate(new ProductPage());
            }
        }
    }
}
