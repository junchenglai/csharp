using System;
using System.Collections.Generic;
using System.Threading;

namespace Generic.Extend
{
    /// <summary>
    /// 泛型缓存
    /// </summary>
    public class GenericCacheTest
    {
        public static void Show()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(GenericCache<int>.GetCache());
                Thread.Sleep(10);
                Console.WriteLine(GenericCache<long>.GetCache());
                Thread.Sleep(10);
                Console.WriteLine(GenericCache<DateTime>.GetCache());
                Thread.Sleep(10);
                Console.WriteLine(GenericCache<string>.GetCache());
                Thread.Sleep(10);
                Console.WriteLine(GenericCache<GenericCacheTest>.GetCache());
                Thread.Sleep(10);
            }
        }
    }

    /// <summary>
    /// 字典缓存，静态属性常驻缓存
    /// </summary>
    public class DictionaryCache
    {
        private static Dictionary<Type, string> _TypeTimeDictionary = null;
        static DictionaryCache()
        {
            Console.WriteLine("This is DictionaryCache 静态构造函数");
            _TypeTimeDictionary = new Dictionary<Type, string>();
        }
        public static string GetCache<T>()
        {
            Type type = typeof(Type);
            if (!_TypeTimeDictionary.ContainsKey(type))
                _TypeTimeDictionary[type] = $"{typeof(T).FullName}_{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}";
            return _TypeTimeDictionary[type];
        }
    }

    /// <summary>
    /// 泛型缓存：
    ///     每个不同的T，都会生成一份不同的副本
    ///     适合不同的类型，需要缓存一份数据的场景，效率高
    ///     泛型缓存比字典缓存效率高好几百倍
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericCache<T>
    {
        static string _TypeTime = "";
        static GenericCache()
        {
            Console.WriteLine("This is GenericCache 静态构造函数");
            _TypeTime = $"{typeof(T).FullName}_{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}";
        }
        public static string GetCache()
        {
            return _TypeTime;
        }
    }
}