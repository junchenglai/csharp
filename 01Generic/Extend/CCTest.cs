using System;
using System.Collections.Generic;
using System.Linq;

namespace Generic.Extend
{
    /// <summary>
    /// 所谓协变、逆变，都与泛型相关
    /// .net framework 3.0 提供的
    /// 只能放在接口或者委托的泛型参数前面
    /// out 协变 covariant     修饰返回值
    /// in  逆变 contravariant 修饰传入参数
    /// </summary>
    public class CCTest
    {
        public static void Show()
        {
            {
                Bird bird1 = new Bird();
                Bird bird2 = new Sparrow(); // 子类实例化 
                Sparrow sparrow1 = new Sparrow();
                // Sparrow sparrow2 = new Bird(); // 子类变量不能用父类实例化
            }
            {
                List<Bird> birdList1 = new List<Bird>();
                // List<Bird> birdList2 = new List<Sparrow>(); // error: 语法错误：List<Bird>是一个类，List<Sparrow>也是一个类，两者没有父子关系，List<> 之间没有关系。

                List<Bird> birdList3 = new List<Sparrow>().Select(c => (Bird)c).ToList();
            }
            {
                // 协变：就是可以让右边用上子类
                // out 修饰后，T 只能作为返回值，不能当参数
                IEnumerable<Bird> birdList1 = new List<Bird>();
                IEnumerable<Bird> birdList2 = new List<Sparrow>();

                Func<Bird> func = new Func<Sparrow>(() => null);

                ICustomerListOut<Bird> customerList1 = new CustomerListOut<Bird>();
                ICustomerListOut<Bird> customerList2 = new CustomerListOut<Sparrow>();
                customerList2.Get();
            }

            {
                // 逆变：就是可以让右边用上父类
                // in 修饰后，T 只能作为参数，不能当返回值
                ICustomerListIn<Sparrow> customerList1 = new CustomerListIn<Bird>();
                ICustomerListIn<Sparrow> customerList2 = new CustomerListIn<Sparrow>();
                customerList1.Show(new Sparrow());

                ICustomerListIn<Bird> birdList1 = new CustomerListIn<Bird>();
                birdList1.Show(new Sparrow());
                birdList1.Show(new Bird());

                Action<Sparrow> action = new Action<Bird>((Bird i) => { });
            }

            {
                // 协变与逆变的组合
                IMyList<Sparrow,Bird> myList1 = new MyList<Sparrow,Bird>();
                IMyList<Sparrow,Bird> myList2 = new MyList<Sparrow,Sparrow>(); // 协变
                IMyList<Sparrow,Bird> myList3 = new MyList<Bird,Bird>(); // 逆变
                IMyList<Sparrow,Bird> myList4 = new MyList<Bird,Sparrow>(); // 协变 + 逆变
            }
        }
    }

    public class Bird
    {
        public int Id { get; set; }
    }
    public class Sparrow : Bird
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// out 协变 只能返回结果，不能作为参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomerListOut<out T>
    {
        T Get();
        // void Show(T t); // error 
    }

    public class CustomerListOut<T> : ICustomerListOut<T>
    {
        public T Get() => default(T);
        // public void Show(T t) { }
    }

    /// <summary>
    /// 逆变
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomerListIn<in T>
    {
        // T Get(); // error: 不能作为返回值
        void Show(T t);
    }

    public class CustomerListIn<T> : ICustomerListIn<T>
    {
        // public T Get() => default(T);
        public void Show(T t) { }
    }

    public interface IMyList<in inT, out outT>
    {
        void Show(inT T);
        outT Get();
        outT Do(inT t);
    }

    public class MyList<T1, T2> : IMyList<T1, T2>
    {
        public void Show(T1 t) => Console.WriteLine(t.GetType().Name);
        public T2 Get()
        {
            Console.WriteLine(typeof(T2).Name);
            return default(T2);
        }
        public T2 Do(T1 t)
        {
            Console.WriteLine(t.GetType().Name);
            Console.WriteLine(typeof(T2).Name);
            return default(T2);
        }
    }
}