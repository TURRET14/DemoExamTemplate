using System.Collections.Generic;

namespace DemoExam
{
    public partial class DBEntities
    {
        public List<User> User { get; set; } = new List<User>();
        public List<Role> Role { get; set; } = new List<Role>();
        public List<Product> Product { get; set; } = new List<Product>();
        public List<Type> Type { get; set; } = new List<Type>();

        public void SaveChanges() { }
    }
}
