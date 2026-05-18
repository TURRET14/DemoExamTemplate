using System.Collections.Generic;
using System.Linq;

namespace DemoExam
{
    public partial class DBEntities
    {
        private static Roles AdminRole { get; set; } = new DemoExam.Roles { Name = "Администратор" };
        private static Roles ManagerRole { get; set; } = new DemoExam.Roles { Name = "Менеджер" };
        private static Roles UserRole { get; set; } = new DemoExam.Roles { Name = "Пользователь" };

        private static Types TireType { get; set; } = new DemoExam.Types { Name = "Шина" };
        private static Types DefaultType { get; set; } = new DemoExam.Types { Name = "По умолчанию" };

        private static Materials RubberMaterial { get; set; } = new DemoExam.Materials { Name = "Резина" };
        private static Materials MetalMaterial { get; set; } = new DemoExam.Materials { Name = "Металл" };

        public List<Roles> Role { get; set; } = new List<Roles>() { AdminRole, ManagerRole, UserRole };
        public List<Users> User { get; set; } = new List<Users>() { new DemoExam.Users() { FIO = "Емельяненко Семен Михайлович", Login = "EmelyanenkoSM", Password = "12345", Role = AdminRole } };
        public List<Types> Type { get; set; } = new List<Types>() { TireType, DefaultType };
        public List<Products> Product { get; set; } = new List<Products>() { new DemoExam.Products() { Name = "Шина Обычная", Type = TireType, Price = 15000, ProductMaterial = new List<Materials> { RubberMaterial, MetalMaterial } }, new DemoExam.Products() { Name = "Покрышка Обычная", Type = DefaultType, Price = 3000, ProductMaterial = new List<Materials> { RubberMaterial } }, new DemoExam.Products() { Name = "Колесо", Type = TireType, Price = 7000, ProductMaterial = new List<Materials> { MetalMaterial } } };

        public void SaveChanges() { }
    }
}
