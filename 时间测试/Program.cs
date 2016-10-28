using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 时间测试
{
    class Program
    {
        static void Main(string[] args)
        {

            Test();
        }

        private static void Test()
        {
            long startTicks = new DateTime(1970, 1, 1, 0, 0, 0).Ticks;
            long ticks = startTicks + 1368864000*10000000L;

            DateTime dt = new DateTime(ticks);

            Console.WriteLine(dt.ToString("yyyy/MM/dd HH:mm:ss"));
            Console.ReadKey();
        }
    }
}
