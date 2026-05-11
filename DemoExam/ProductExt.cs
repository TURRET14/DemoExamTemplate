using System;
using System.IO;

namespace DemoExam
{
    public partial class Product
    {
        public string ImagePathGetSet {
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
