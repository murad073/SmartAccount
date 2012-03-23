using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQL2K8
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

        public SmartAccountEntities DB = new SmartAccountEntities();
    }
}
