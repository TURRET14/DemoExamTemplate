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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Метод перехода на страницу авторизации и очистки текущего пользователя.
        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            UserData.CurrentUser = null;

            Frame_Main.NavigationService.Navigate(new LoginPage());
        }

        // Метод для обработки нажатия кнопки "Назад" и возврата на предыдущую страницу, если это возможно.
        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            if (Frame_Main.NavigationService.CanGoBack)
            {
                Frame_Main.NavigationService.GoBack();
            }
        }

        // Метод для обновления отображения информации о текущем пользователе при навигации на новую страницу.
        private void Frame_Main_Navigated(object sender, NavigationEventArgs e)
        {
            if (UserData.CurrentUser != null)
            {
                TextBlock_FIO.Text = UserData.CurrentUser.FIO;
            }
            else
            {
                TextBlock_FIO.Text = "Гость";
            }
        }
    }
}
