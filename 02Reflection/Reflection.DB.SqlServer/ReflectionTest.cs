using System;

namespace Reflection.DB.SqlServer
{
    public class ReflectionTest
    {
        #region 
        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public ReflectionTest() => Console.WriteLine($"这里是${GetType()}无参数构造函数");

        /// <summary>
        /// 有参数构造函数
        /// </summary>
        public ReflectionTest(string name) => Console.WriteLine($"这里是${GetType()}有参数构造函数");
        public ReflectionTest(int id) => Console.WriteLine($"这里是${GetType()}有参数构造函数");
        #endregion

        #region Method
        /// <summary>
        /// 无参方法
        /// </summary>
        public void Show1() => Console.WriteLine($"这里是 {GetType()} 的 Show1。");

        /// <summary>
        /// 有参数方法
        /// </summary>
        public void Show2(int id) => Console.WriteLine($"这里是 {GetType()} 的 Show2。");

        /// <summary>
        /// 重载方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void Show3() => Console.WriteLine($"这里是 {GetType()} 的 Show3。");
        public void Show3(int id) => Console.WriteLine($"这里是 {GetType()} 的 Show3_1。");
        public void Show3(string name) => Console.WriteLine($"这里是 {GetType()} 的 Show3_2。");
        public void Show3(int id, string name) => Console.WriteLine($"这里是 {GetType()} 的 Show3_3。");
        public void Show3(string name, int id) => Console.WriteLine($"这里是 {GetType()} 的 Show3_4。");

        /// <summary>
        /// 静态方法
        /// </summary>
        /// <param name="name"></param>
        public static void Show4 (string name) => Console.WriteLine($"这里是 {typeof(ReflectionTest)} 的 Show4。");
        #endregion
    }
}