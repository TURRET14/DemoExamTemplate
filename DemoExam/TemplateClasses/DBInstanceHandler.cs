namespace DemoExam
{
    public partial class DBEntities
    {
        private static DBEntities Instance { get; set; }

        public static DBEntities GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DBEntities();
            }
            return Instance;
        }
    }
}