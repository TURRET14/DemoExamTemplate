using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoExam
{
    public static class ValidationService
    {
        // Класс для валидации данных. Содержит статические методы для проверки различных условий и отображения сообщений об ошибках.
        public static bool IsStringNotEmpty(string value, string title)
        {
            if (String.IsNullOrEmpty(value))
            {
                MessageHelper.ShowErrorMessage($"Поле {title} не заполнено!");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsNumberGreaterThanZero(double value, string title)
        {
            if (value <= 0)
            {
                MessageHelper.ShowErrorMessage($"Поле {title} должно быть больше нуля!");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsNumberGreaterThanOrEqualToZero(double value, string title)
        {
            if (value < 0)
            {
                MessageHelper.ShowErrorMessage($"Поле {title} должно быть больше или равно нулю!");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
