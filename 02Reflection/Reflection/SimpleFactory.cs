using System;
using System.Reflection;
using Reflection.DB.Interface;

namespace Reflection
{
    public class SimpleFactory
    {
        public static IDBHelper CreaterInstance()
        {
            Assembly assembly = Assembly.Load("Reflection.DB.MySql");
            Type type = assembly.GetType("Reflection.DB.MySql.MySqlHelper");
            object oDBHelper = Activator.CreateInstance(type);
            IDBHelper iDBHelper = oDBHelper as IDBHelper;
            return iDBHelper;
        }
    }
}