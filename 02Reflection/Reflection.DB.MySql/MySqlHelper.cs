using System;
using Reflection.DB.Interface;

namespace Reflection.DB.MySql
{
    public class MySqlHelper : IDBHelper
    {
        public MySqlHelper() => Console.WriteLine($"{GetType().Name}被构造");
        public void Query() => Console.WriteLine($"{GetType().Name}");
    }
}
