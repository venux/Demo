using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 验证机制
{
    /// <summary>
    /// 验证帮助类
    /// </summary>
    public class ValidatorHelper<T>
    {
        public ValidatorHelper<T> Validator(Predicate<T> predicate)
        {
            return this;
        }
    }
}
