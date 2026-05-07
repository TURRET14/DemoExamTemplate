using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoExam
{
    public static class ValidationService
    {
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
    }
}
