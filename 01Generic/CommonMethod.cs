using System;

namespace Generic
{
    public class CommonMethod
    {

        /// <summary>
        /// 打印一个 int 值
        /// 
        /// 声明方法时，指定了参数类型，确定了只能传递某个类型
        /// </summary>
        /// <param name = "iParameter">int 型参数</param>
        public static void ShowInt(int iParameter) =>
            Console.WriteLine($"This is {typeof(CommonMethod).Name}, parameter = {iParameter}, type = {iParameter.GetType().Name}");

        /// <summary>
        /// 打印一个 string 值
        /// </summary>
        /// <param name="sParameter">string 型参数</param>
        public static void ShowString(string sParameter) =>
            Console.WriteLine($"This is {typeof(CommonMethod).Name}, parameter = {sParameter}, type = {sParameter.GetType().Name}");

        /// <summary>
        /// 打印一个 DateTime 值
        /// </summary>
        /// <param name="dtParameter">DateTime 型参数</param>
        public static void ShowDateTime(DateTime dtParameter) =>
            Console.WriteLine($"This is {typeof(CommonMethod).Name}, parameter = {dtParameter}, type = {dtParameter.GetType().Name}");

        /// <summary>
        /// 打印个 object 值
        /// 1. 任何父类出现的地方，都可以用子类来替代
        /// 2. object 是一切类型的父类
        /// 
        /// 两个问题：
        /// 1. 装箱拆箱，性能损耗
        ///     传入一个 int 值（栈）
        ///     object 又在堆里面，如果把 int 传递进来，则会把值从栈里面复制到堆里（装箱）
        ///     使用的时候，又需要用对象值，又会从堆复制到栈中（拆箱）
        /// 2. 类型安全问题
        ///     因为传递的对象是没有限制的
        /// </summary>
        /// <param name="oParameter"></param>
        public static void ShowObject(object oParameter) =>
            Console.WriteLine($"This is {typeof(CommonMethod).Name}, parameter = {oParameter}, type = {oParameter.GetType().Name}");

        /// <summary>
        /// 泛型方法：方法名称后面加上尖括号，里面是类型参数
        ///         类型参数实际上就是一个类型T声明，方法就可以用这个类型T了
        /// 
        /// 泛型方法声明时，并没有写死类型，等调用时再指定
        /// 设计思想 —— 延迟声明：推迟一切可以推迟的
        /// </summary>
        /// <param name="tParameter"></param>
        /// <typeparam name="T"></typeparam>
        public static void Show<T>(T tParameter) =>
            Console.WriteLine($"This is {typeof(CommonMethod).Name}, parameter = {tParameter}, type = {tParameter.GetType().Name}");
    }
}