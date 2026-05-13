using System.Collections.Generic;
using System.Linq;

namespace DemoExam
{
    public partial class DBEntities
    {
        private static Role AdminRole { get; set; } = new DemoExam.Role { Name = "Администратор" };
        private static Role ManagerRole { get; set; } = new DemoExam.Role { Name = "Менеджер" };
        private static Role UserRole { get; set; } = new DemoExam.Role { Name = "Пользователь" };

        private static Type TireType { get; set; } = new DemoExam.Type { Name = "Шина" };
        private static Type DefaultType { get; set; } = new DemoExam.Type { Name = "По умолчанию" };

        public List<Role> Role { get; set; } = new List<Role>() { AdminRole, ManagerRole, UserRole };
        public List<User> User { get; set; } = new List<User>() { new DemoExam.User() { FIO = "Емельяненко Семен Михайлович", Login = "EmelyanenkoSM", Password = "12345", Role = AdminRole } };
        public List<Type> Type { get; set; } = new List<Type>() { TireType, DefaultType };
        public List<Product> Product { get; set; } = new List<Product>() { new DemoExam.Product() { Name = "Шина Обычная", Type = TireType, Price = 15000}, new DemoExam.Product() { Name = "Покрышка Обычная", Type = DefaultType, Price = 3000 }, new DemoExam.Product() { Name = "Колесо", Type = TireType, Price = 7000 } };

        public void SaveChanges() { }
    }
}
