using System;
using Reflection.DB.Interface;

namespace Reflection.DB.Oracle
{
    public class OracleHelper : IDBHelper
    {
        public OracleHelper() => Console.WriteLine($"{GetType().Name}被构造");
        public void Query() => Console.WriteLine($"{GetType().Name}");
    }
}
