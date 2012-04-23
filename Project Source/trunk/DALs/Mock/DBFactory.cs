namespace Mock
{
    public class DBFactory
    {
        private DBFactory()
        {
        }

        private static DBFactory _instance = new DBFactory();
        public static DBFactory Instance
        {
            get
            {
                return _instance;
            }
        }

        public SmartAccountEntities DB = new SmartAccountEntities();
    }
}
