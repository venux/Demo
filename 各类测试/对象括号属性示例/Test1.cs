using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 各类测试.对象括号属性示例
{
    public class Test1
    {
        private Dictionary<string, object> dic = new Dictionary<string, object>();

        public object this[string name]
        {
            get
            {
                return this.dic[name];
            }
        }

        public Test1()
        {
            dic.Add("a", 0);
            dic.Add("b", "b");
            dic.Add("c", System.DateTime.Now.ToShortDateString());
            dic.Add("d", null);
        }
    }
}
