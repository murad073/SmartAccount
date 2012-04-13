namespace SQLCompact
{
    public class DBFactory
    {
        private DBFactory()
        {
        }

        private static DBFactory _instance;
        public static DBFactory Instance
        {
            get
            {
                if (_instance == null) _instance = new DBFactory();
                return _instance;
            }
            //private set { _instance = value; }
        }

        public SQLCEEntities DB = new SQLCEEntities();
    }
}
