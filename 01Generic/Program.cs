using System;
using System.Collections.Generic;
using Generic.Extend;

namespace Generic
{
    class Program
    {
        /// <summary>
        /// 1 引入泛型：延迟声明
        /// 2 如何声明和使用泛型
        /// 3 泛型的好处和原理
        /// 4 泛型类、泛型方法、泛型接口、泛型委托
        /// 5 泛型约束
        /// 6 协变、逆变
        /// 7 泛型缓存
        /// </summary>
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

                Console.WriteLine("******** GenericConstraint *************");
                People people = new People() { Id = 001, Name = "Jone" };
                Chinese chinese = new Chinese() { Id = 002, Name = "Mike" };
                Hubei hubei = new Hubei() { Id = 003, Name = "Mirry" };
                Japanese japanese = new Japanese() { Id = 004, Name = "Jakey" };

                GenericConstraint.ShowObject(people);
                GenericConstraint.ShowObject(chinese);
                GenericConstraint.ShowObject(hubei);
                // error: 编译通过，运行时报错
                // 没有约束，任何类型都能传递进来，所以不安全
                // GenericConstraint.ShowObject(japanese);

                GenericConstraint.Show(people);
                GenericConstraint.Show(chinese);
                GenericConstraint.Show(hubei);
                // error: 编译不通过
                // GenericConstraint.Show(Japanese);

                Console.WriteLine("******** 协变、逆变 *************");
                CCTest.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
