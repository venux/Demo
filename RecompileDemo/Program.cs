using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RecompileDemo
{
    class Program
    {
        private static int num = 0;

        static void Main(string[] args)
        {
            Stream stream = null;

            TaskTest();

            Console.ReadLine();
        }

        private static async void TaskTest()
        {
            do
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(100);
                    num++;

                    Console.WriteLine(num);
                });
            } while (num < 100);

            Console.WriteLine("结束！");
        }
    }
}
