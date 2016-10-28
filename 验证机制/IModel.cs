using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 验证机制
{
    public interface IModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Primarykey(Comment = "主键标示")]
        string ID { get; set; }
    }
}
