using System;
using System.Reflection;
using Reflection.DB.Interface;
using Reflection.DB.MySql;
using Reflection.DB.SqlServer;

namespace Reflection
{
    /// <summary>
    /// 1 dll-IL-metadata-反射
    /// 2 反射加载dll，读取 model、类、方法、特性
    /// 3 反射创建对象，反射+简单工厂+配置文件
    /// 4 破坏单例 创建泛型
    /// 5 反射调用实例方法、静态方法、重载方法
    /// 6 调用私有方法 调用泛型方法
    /// 7 反射字段和属性，分别获取值和设置值
    /// 8 反射的好处和局限性
    /// </summary>
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
                    // Assembly assembly2 = Assembly.LoadFile(@"E:\GitHub\csharp\02Reflection\Reflection.DB.MySql\bin\Debug\netstandard2.0\Reflection.DB.MySql.dll"); // 必须要完整路径
                    // Assembly assembly3 = Assembly.LoadFrom("Reflection.DB.MySql.dll"); // 当前路径
                    // Assembly assembly4 = Assembly.LoadFrom(@"E:\GitHub\csharp\02Reflection\Reflection.DB.MySql\bin\Debug\netstandard2.0\Reflection.DB.MySql.dll"); //完整路径

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
                    // 程序是可配置的，通过修改配置就可以自动切换
                    // 没有写死类型，而是通过配置文件执行，反射创建的
                    // 实现类必须是事先已有的，而且在目录下面

                    // 可扩展：完全不修改原有代码，只是增加新的实现、复制、修改配置，则可以支持新功能
                    // 反射的动态加载和动态创建对象，以及配置文件结合
                }

                {
                    Console.WriteLine("********* ctor + parameter **********");
                    Type type = Assembly.Load("Reflection.DB.SqlServer")
                        .GetType("Reflection.DB.SqlServer.ReflectionTest");
                    IDBHelper iDBHelper1 = Activator.CreateInstance(type) as IDBHelper;
                    IDBHelper iDBHelper2 = Activator.CreateInstance(type, new object[] { 123 }) as IDBHelper;
                    IDBHelper iDBHelper3 = Activator.CreateInstance(type, new object[] { "oTest" }) as IDBHelper;

                    foreach (ConstructorInfo ctor in type.GetConstructors())
                    {
                        Console.WriteLine(ctor.Name);
                        foreach (var parameter in ctor.GetParameters())
                        {
                            Console.WriteLine(parameter.ParameterType);
                        }
                    }
                }

                {
                    Console.WriteLine("************ Singleton *************");
                    Singleton singleton = Singleton.GetInstance();
                    Singleton singleton1 = Singleton.GetInstance();
                    Console.WriteLine(ReferenceEquals(singleton, singleton1));

                    Type type = Assembly.Load("Reflection.DB.SqlServer")
                        .GetType("Reflection.DB.SqlServer.Singleton");
                    //Singleton singletonA = Activator.CreateInstance(type) as Singleton; // error: 无法实例化单例

                    // 反射破坏单例 —— 就是反射调用私有构造函数
                    Singleton singletonA = Activator.CreateInstance(type, true) as Singleton;
                    Singleton singletonB = Activator.CreateInstance(type, true) as Singleton;
                    Console.WriteLine(ReferenceEquals(singletonA, singletonB));
                }

                {
                    Console.WriteLine("********** GenericClass ***********");
                    Type type = Assembly.Load("Reflection.DB.SqlServer")
                        .GetType("Reflection.DB.SqlServer.GenericClass`3");
                    // object oGeneric = Activator.CreateInstance(type); // error: 泛型无法直接实例化
                    Type typeMake = type.MakeGenericType(new Type[] { typeof(string), typeof(int), typeof(DateTime) });
                    object oGeneric = Activator.CreateInstance(typeMake);
                }

                #region 泛型方法
                {
                    Console.WriteLine("********** GenericMethod ***********");
                    Type type = Assembly.Load("Reflection.DB.SqlServer")
                        .GetType("Reflection.DB.SqlServer.GenericMethod");
                    object oGeneric = Activator.CreateInstance(type);
                    foreach (MethodInfo item in type.GetMethods())
                    {
                        Console.WriteLine(item.Name);
                    }
                    MethodInfo method = type.GetMethod("Show");
                    MethodInfo methodNew = method.MakeGenericMethod(new Type[] { typeof(int), typeof(string), typeof(DateTime) });
                    methodNew.Invoke(oGeneric, new object[] { 123, "John", DateTime.Now });
                }
                {
                    Console.WriteLine("******* GenericClass&Method ********");
                    Type type = Assembly.Load("Reflection.DB.SqlServer")
                        .GetType("Reflection.DB.SqlServer.GenericDouble`1")
                        .MakeGenericType(typeof(int));
                    object oObject = Activator.CreateInstance(type);
                    MethodInfo method = type.GetMethod("Show").MakeGenericMethod(typeof(string), typeof(DateTime));
                    method.Invoke(oObject, new object[] { 123, "John", DateTime.Now });
                }
                #endregion

                {
                    // 反射创建对象后，知道方法名称，可以直接调用而不需做类型转换
                    // 反射创建了对象实例——知道方法的名称——反射调用方法
                    // dll 名称——类型名称——方法名称——我们就能调用方法
                    // MVC 依赖这种模式——调用 Action
                    // http://localhost:9099/home/index 通过路由解析——会调用 HomeController——Index方法
                    // 相当于浏览器输入时告诉了服务器类型的名称和方法的名称
                    // MVC 加载时会先扫描所有 dll —— 找到所有的控制器 —— 请求来时用Controller 来匹配
                    // 1 MVC的局限性：Action 重载时，反射是无法区分的，只能通过 HttpMethod + 特性 HttpGet/HttpPost 等
                    // 2 AOP 反射调用方法，可以在前后插入逻辑
                    Console.WriteLine("********** GenericMethod ***********");
                    Type type = Assembly.Load("Reflection.DB.SqlServer")
                        .GetType("Reflection.DB.SqlServer.ReflectionTest");
                    object oTest = Activator.CreateInstance(type);
                    // 直接调用 Show1 方法
                    foreach (var method in type.GetMethods())
                    {
                        Console.WriteLine(method.Name);
                        foreach (var parameter in method.GetParameters())
                        {
                            Console.WriteLine($"{parameter.Name} {parameter.ParameterType}");
                        }
                    }
                    {
                        MethodInfo method = type.GetMethod("Show1");
                        method.Invoke(oTest, null);
                    }
                    {
                        MethodInfo method = type.GetMethod("Show2");
                        method.Invoke(oTest, new object[] { 123 });
                    }
                    {
                        MethodInfo method = type.GetMethod("Show3", new Type[] { });
                        method.Invoke(oTest, null);
                    }
                    {
                        MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(int) });
                        method.Invoke(oTest, new object[] { 123 });
                    }
                    {
                        MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(string) });
                        method.Invoke(oTest, new object[] { "John" });
                    }
                    {
                        MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(int), typeof(string) });
                        method.Invoke(oTest, new object[] { 123, "John" });
                    }
                    {
                        MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(string), typeof(int) });
                        method.Invoke(oTest, new object[] { "John", 123 });
                    }
                    {
                        // 静态方法实例可要可不要
                        MethodInfo method = type.GetMethod("Show5");
                        method.Invoke(oTest, new object[] { "John" });
                        method.Invoke(null, new object[] { "John" });
                    }
                }

                #region 反射调用私有方法
                {
                    Console.WriteLine("********* 反射调用私有方法 *********");
                    Type type = Assembly.Load("Reflection.DB.SqlServer")
                        .GetType("Reflection.DB.SqlServer.ReflectionTest");
                    object oTest = Activator.CreateInstance(type);
                    var method = type.GetMethod("Show4", BindingFlags.Instance | BindingFlags.NonPublic);
                    object oReturn = method.Invoke(oTest, new object[] { "John" });
                }
                #endregion

                #region 
                {
                    
                }
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
