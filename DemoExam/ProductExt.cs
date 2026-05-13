using System;
using System.IO;

namespace DemoExam
{
    public partial class Product
    {
        
        public string ImagePathGetSet {
            // Свойство для получения полного пути к изображению продукта. Если путь к изображению не указан, возвращает путь к изображению по умолчанию.
            get
            {
                if (!String.IsNullOrEmpty(ImagePath))
                {
                    return $"{Environment.CurrentDirectory}\\Images\\{ImagePath}";
                }
                else
                {
                    return "/Picture.png";
                }
            }
            // Свойство для установки изображения продукта.
            set
            {
                string filename = Path.GetFileName(value);
                string newpath = $"{Environment.CurrentDirectory}\\Images\\{filename}";

                if (!File.Exists(newpath))
                {
                    File.Copy(value, newpath);
                }
                ImagePath = filename;
            }
        }

        public bool IsBigPrice
        {
            get
            {
                if (Price >= 5000)
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
}
