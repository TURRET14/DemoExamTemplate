using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DemoExam
{
    public static class MessageHelper
    {
        /// <summary>
        /// Вывод Сообщения Об Ошибке.
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        public static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Вывод Сообщения Информации.
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        public static void ShowInfoMessage(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Вывод Сообщения Подтверждения.
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <returns>Результат Сообщения Подтверждения</returns>
        public static bool ShowConfirmationMessage(string message)
        {
            if (MessageBox.Show(message, "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
