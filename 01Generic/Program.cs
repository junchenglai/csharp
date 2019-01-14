using System;
using System.Collections.Generic;
using Generic.Extend;

namespace Generic
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int iValue = 123;
                string sValue = "456";
                DateTime dtValue = DateTime.Now;
                object oValue = "789";

                Console.WriteLine("*************** Common *****************");
                CommonMethod.ShowInt(iValue);
                CommonMethod.ShowString(sValue);
                CommonMethod.ShowDateTime(dtValue);

                Console.WriteLine("*************** Object *****************");
                CommonMethod.ShowObject(iValue);
                CommonMethod.ShowObject(sValue);
                CommonMethod.ShowObject(dtValue);
                CommonMethod.ShowObject(oValue);

                Console.WriteLine("*************** Generic ****************");
                CommonMethod.Show<int>(iValue); // 调用泛型，需要指定类型参数
                CommonMethod.Show(iValue); // 如果可以从参数类型推断，可以省略类型参数
                CommonMethod.Show<string>(sValue);
                CommonMethod.Show<DateTime>(dtValue);
                CommonMethod.Show<object>(oValue);

                Console.WriteLine("************** 泛型原理 *****************");
                // 泛型是 .net framework 2.0 推出的，是框架升级的产物
                // 支持基础：
                // 1. 编译器支持类型参数，用占位符表示的
                // 2. CLR升级 CLR 2.0 （.net framework 2.0 - 4.0），运行时类型确定后再替换占位符
                Console.WriteLine(typeof(List<>));
                Console.WriteLine(typeof(Dictionary<,>));

                Console.WriteLine("************** Monitor *****************");
                // 泛型方法的性能与普通方法大致一致，是最好的
                // 可以一个方法满足多个不同的类型
                // object 装箱拆箱过程消耗大量性能
                Monitor.Show();

                Console.WriteLine("*********** GenericCache ***************");
                GenericCacheTest.Show();


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
