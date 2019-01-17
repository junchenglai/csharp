using System;
using System.Reflection;
using Reflection.DB.Interface;
using Reflection.DB.MySql;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region Common
                {
                    Console.WriteLine("*************** Common *****************");
                    IDBHelper iDBHelper = new MySqlHelper();
                    iDBHelper.Query();
                }
                #endregion

                #region Reflection
                {
                    Console.WriteLine("************* Reflection ***************");
                    Assembly assembly1 = Assembly.Load("Reflection.DB.MySql"); // 1 动态加载 一个完整的 dll 名称不需要后缀（从 exe 所在的路径进行查找）
                    Assembly assembly2 = Assembly.LoadFile(@"E:\GitHub\csharp\02Reflection\Reflection.DB.MySql\bin\Debug\netstandard2.0\Reflection.DB.MySql.dll"); // 必须要完整路径
                    // Assembly assembly3 = Assembly.LoadFrom("Reflection.DB.MySql/Reflection.DB.MySql.dll"); // 当前路径
                    Assembly assembly4 = Assembly.LoadFrom(@"E:\GitHub\csharp\02Reflection\Reflection.DB.MySql\bin\Debug\netstandard2.0\Reflection.DB.MySql.dll"); //完整路径

                    foreach (var type in assembly1.GetTypes())
                    {
                        Console.WriteLine(type.Name);
                        foreach (var method in type.GetMethods())
                        {
                            Console.WriteLine(method.Name);
                        }
                    }
                }
                #endregion
                {
                    Assembly assembly = Assembly.Load("Reflection.DB.MySql"); // 1 动态加载
                    Type type = assembly.GetType("Reflection.DB.MySql.MySqlHelper"); // 2 获取类型 完整类型名称

                    object oDBHelper = Activator.CreateInstance(type); // 3 创建对象
                    // oDBHelper.Query(); // 无法直接调用方法，编译器不允许
                    // C# 是一种强类型语言，静态语言，编译时就确定好类型保证安全

                    dynamic dDBHelper = Activator.CreateInstance(type);
                    dDBHelper.Query(); // dynamic 跳过编译器检查，运行时才检查

                    IDBHelper iDBHelper = oDBHelper as IDBHelper; // 4 类型转换 不报错，类型不对就返回 null
                    iDBHelper.Query(); // 5 方法调用
                }

                {
                    Console.WriteLine("**** Reflection + Factory + Config ****");
                    IDBHelper iDBHelper = SimpleFactory.CreaterInstance();
                    iDBHelper.Query();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
