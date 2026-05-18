using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoExam
{
    public static class ValidationService
    {
        /// <summary>
        /// Проверка строки на пустоту.
        /// </summary>
        /// <param name="value">Значение строки</param>
        /// <param name="title">Название поля строки</param>
        /// <returns>Истина (true): Если строка не пустая. Ложь (false): Если строка пустая.</returns>
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

        /// <summary>
        /// Проверка, является ли число больше нуля.
        /// </summary>
        /// <param name="value">Значение числа</param>
        /// <param name="title">Название поля числа</param>
        /// <returns>Истина (true): Если число больше нуля. Ложь (false): Если число меньше или равно нулю.</returns>
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

        /// <summary>
        /// Проверка, является ли число равным или больше нуля.
        /// </summary>
        /// <param name="value">Значение числа</param>
        /// <param name="title">Название поля числа</param>
        /// <returns>Истина (true): Если число больше или равно нулю. Ложь (false): Если число меньше нуля.</returns>
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
