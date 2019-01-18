using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Reflection.DB.Interface;

namespace Reflection
{
    public class SimpleFactory
    {
        private static IConfigurationRoot config = new ConfigurationBuilder().AddYamlFile("appconfig.yml").Build();
        public static IDBHelper CreaterInstance()
        {
            Assembly assembly = Assembly.Load(config["DBConfig:DllName"]);
            Type type = assembly.GetType(config["DBConfig:TypeName"]);
            object oDBHelper = Activator.CreateInstance(type);
            IDBHelper iDBHelper = oDBHelper as IDBHelper;
            return iDBHelper;
        }
    }
}