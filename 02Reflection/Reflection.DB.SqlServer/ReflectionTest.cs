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
    }
}