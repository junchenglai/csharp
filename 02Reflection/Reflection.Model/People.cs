using System;

namespace Reflection.Model
{
    /// <summary>
    /// 实体
    /// </summary>
    public class People
    {
        public People() => Console.WriteLine($"{GetType().FullName} 被创建");

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description;
    }
}
