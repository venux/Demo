using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 各类测试.方法重载示例
{
    public class Test3
    {
        public static void Test()
        {
            C c = new C();

            A a = new A();
            c.Test(a);

            B b = new B();
            c.Test(b);

            A ab = new B();
            c.Test(ab);
        }
    }
    
    class A
    {
    }

    class B : A
    {
    }

    class C
    {
        public void Test(A a)
        {
            Console.WriteLine("a");
        }

        public void Test(B b)
        {
            Console.WriteLine("b");
        }
    }
}
