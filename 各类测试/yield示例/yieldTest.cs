using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 各类测试.yield示例
{
    public class yieldTest
    {
        public static void Test()
        {
            int i = 0;
            var nums= Power(2, 8);

            Console.WriteLine("-----------{0}--------------", i);
            i++;

            // Display powers of 2 up to the exponent of 8:
            foreach (int index in nums)
            {
                Console.WriteLine("{0} ", index);
            }
        }

        public static System.Collections.Generic.IEnumerable<int> Power(int number, int exponent)
        {
            int result = 1;

            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }
        }

        // Output: 2 4 8 16 32 64 128 256
    }
}
