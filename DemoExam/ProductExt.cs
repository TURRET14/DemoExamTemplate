using System;
using System.IO;
using System.Linq;

namespace DemoExam
{
    public partial class Products
    {
        
        public string ImagePathGetSet {
            // Получение полного пути к изображению объекта (Если путь не задан - Picture.png).
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
            // Установка изображения объекта.
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
            // Получение списка материалов строкой.
            get
            {
                string materials = String.Empty;

                if (ProductMaterial != null)
                {
                    materials = String.Join(", ", ProductMaterial.Select(Entry => Entry.Name));
                }

                return materials;
            }
        }

        public bool IsBigPrice
        {
            // Используется для DataTrigger (Условное оформление).
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
