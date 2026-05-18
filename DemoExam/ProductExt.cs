using System;
using System.IO;
using System.Linq;

namespace DemoExam
{
    public partial class Products
    {
        
        public string ImagePathGetSet {
            // Свойство для получения полного пути к изображению продукта. Если путь к изображению не указан, возвращает путь к изображению по умолчанию.
            get
            {
                if (!String.IsNullOrEmpty(ImagePath))
                {
                    return Path.Combine(Environment.CurrentDirectory, ImagePath);
                }
                else
                {
                    return "/Picture.png";
                }
            }
            // Свойство для установки изображения продукта.
            set
            {
                string filename = Path.Combine("Images", Path.GetFileName(value));
                string newpath = Path.Combine(Environment.CurrentDirectory, filename);
                if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Images")))
                {
                    Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Images"));
                }
                if (!File.Exists(newpath))
                {
                    File.Copy(value, newpath);
                }
                ImagePath = filename;
            }
        }

        public string MaterialsString
        {
            get
            {
                return String.Join(", ", ProductMaterial.Select(Entry => Entry.Name));
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
