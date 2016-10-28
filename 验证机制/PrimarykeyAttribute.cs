using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 验证机制
{
    /// <summary>
    /// 主键属性标示
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PrimarykeyAttribute : Attribute
    {
        public string Comment { get; set; }

        public PrimarykeyAttribute()
        {
        
        }
    }
}
