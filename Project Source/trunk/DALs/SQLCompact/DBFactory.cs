namespace SQLCompact
{
    public class SQLCEFactory
    {
        private SQLCEFactory()
        {
        }

        private static SQLCEFactory _instance;
        public static SQLCEFactory Instance
        {
            get
            {
                if (_instance == null) _instance = new SQLCEFactory();
                return _instance;
            }
            //private set { _instance = value; }
        }

        public SQLCEEntities DB = new SQLCEEntities();
    }
}
