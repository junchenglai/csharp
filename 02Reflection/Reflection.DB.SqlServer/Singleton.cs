using System;

namespace Reflection.DB.SqlServer
{
    /// <summary>
    /// 单例模式：类
    /// </summary>
    public sealed class Singleton
    {
        private static Singleton _Singleton =null;

        private Singleton() => Console.WriteLine("Singleton 被构造");

        static Singleton() => _Singleton = new Singleton();

        public static Singleton GetInstance() => _Singleton;
    }
}