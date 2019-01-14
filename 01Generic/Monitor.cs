using System.Diagnostics;

namespace Generic
{
    public class Monitor
    {
        public static void Show()
        {
            int iValue = 12345;
            long commonSecond = 0;
            long objectSecond = 0;
            long genericSecond = 0;
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                for (int i = 0; i < 100_000_000; i++) ShowInt(iValue);
                watch.Stop();
                commonSecond = watch.ElapsedMilliseconds;
            }
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                for (int i = 0; i < 100_000_000; i++) ShowObject(iValue);
                watch.Stop();
                objectSecond = watch.ElapsedMilliseconds;
            }
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                for (int i = 0; i < 100_000_000; i++) Show(iValue);
                watch.Stop();
                genericSecond = watch.ElapsedMilliseconds;
            }
            System.Console.WriteLine($"commonSecond = {commonSecond}, objectSecond = {objectSecond}, genericSecond = {genericSecond}");
        }

        #region PrivateMethod
        private static void ShowInt(int iParameter) { }
        private static void ShowObject(object oParameter) { }
        private static void Show<T>(T tParameter) { }
        #endregion
    }
}