using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 各类测试.Null判断写法示例
{
    /*
     * 在C#、JAVA中两者写法完全无区别；
     * 在别的语言中，推荐第二种写法，防止==漏写成=，而编译器无法发现该问题，后种写法编译器能发现问题。
     */
    public class Test2
    {
        public static void Test()
        {
            DateTime now = System.DateTime.Now;

            if (now == null)
            {
            }

            if (null == now)
            {
            }
        }
    }
}
