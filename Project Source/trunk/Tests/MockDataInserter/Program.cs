using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockDataInserter
{
    class Program
    {
        private static string _connectionString = @"Data Source=D:\My Projects\Practice self\.net projects\office Github SmartAccount\Project Source\trunk\Tests\BLL.LedgerManagement.Test\bin\Debug\SmartAccountEntities.sdf";

        static void Main(string[] args)
        {
            DataInserter dataInserter = new DataInserter(_connectionString);
            dataInserter.InsertDataInDB();
            Console.WriteLine("Data successfully inserted");
            Console.ReadKey();
        }
    }
}
