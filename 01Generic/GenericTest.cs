namespace Generic
{
    public class GenericTest
    {
        /// <summary>
        /// 泛型方法：为了一个方法满足不同的类型需求
        /// 例如：一个方法完成多个实体的查询
        ///     一个方法完成不同的类型的数据展示
        ///     任意一个实体，转换成 Json 字符串
        /// 
        /// 多类型参数 不能是关键字 不要与类名称重复
        /// </summary>
        /// <param name="tParameter"></param>
        /// <typeparam name="T"></typeparam>
        public T Show<T, S, Model>(S sParameter)
        {
            GenericClass<int> genericClass = new GenericClass<int>();

            throw new System.Exception();
        }
    }

    /// <summary>
    /// 泛型类：一个类，满足不同类型的需求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericClass<T>
    {

    }

    /// <summary>
    /// 泛型接口：一个接口满足不同类型的需求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface GenericInterface<T>
    {

    }

    /// <summary>
    /// 泛型委托：一个委托满足不同类型的需求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate void GenericDelegate<T>();

    /// <summary>
    /// 子类继承父类需要明确类型
    /// </summary>
    public class ChildClass : GenericClass<int>, GenericInterface<string> { }
    public class GenericChildClass<T, S> : GenericClass<T>, GenericInterface<S> { }
}