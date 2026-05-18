using System.Collections.Generic;

namespace DemoExam
{
    public partial class Products
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public Types Type { get; set; }
        public string ImagePath { get; set; }
        public List<Materials> ProductMaterial { get; set; }
    }
}