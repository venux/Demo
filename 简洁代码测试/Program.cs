using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 简洁代码测试
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>()
            {
                "A",
                "B",
                "C",
                "D",
                "E",
                "G",
                "G"
            };

            string result = "";

            list.ForEach((item) => result += item);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
