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
            get { return _instance ?? (_instance = new DBFactory()); }
        }

        public SmartAccountEntities DB = new SmartAccountEntities();
    }
}
