using System;

namespace Generic
{
    public class GenericConstraint
    {
        public static void ShowObject(object oParameter)
        {
            Console.WriteLine($"This is {typeof(GenericConstraint).Name}, parameter = {oParameter}, type = {oParameter.GetType().Name}");
            People people = (People)oParameter;
            Console.WriteLine($"{people.Id} {people.Name}");
        }

        /// <summary>
        /// 没有约束，很受局限
        /// where T : BaseModel
        /// 基类约束：
        /// 1 把 T 当成基类
        /// 2 T 必须是 People 或其子类
        /// </summary>
        /// <param name="tParameter"></param>
        /// <typeparam name="T"></typeparam>
        public static void Show<T>(T tParameter)
            where T : People // 基类约束
            // where T : ISports // 接口约束
        {
            Console.WriteLine($"This is {typeof(GenericConstraint).Name}, parameter = {tParameter}, type = {tParameter.GetType().Name}");
            Console.WriteLine($"{tParameter.Id} {tParameter.Name}");
            // tParameter.Pingpang();
        }

        public T GetT<T>(T t)
            where T : 
            class // 引用类型约束
            // where T : struct // 值类型约束
            // where T : new() // 无参数构造函数约束
            // where T : int // error: 密封类不能约束，因为没有意义
        {
            return default(T); // default 是个关键字，会根据 T 的类型去获得一个默认值
            // return new T();
        }
    }
}