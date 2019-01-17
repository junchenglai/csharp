using System;
using Reflection.DB.Interface;

namespace Reflection.DB.SqlServer
{
    public class SqlServerHelper : IDBHelper
    {
        public SqlServerHelper() => Console.WriteLine($"{GetType().Name}被构造");
        public void Query() => Console.WriteLine($"{GetType().Name}");
    }
}
