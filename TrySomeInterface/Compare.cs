using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrySomeInterface
{
    /// <summary>
    /// 这个类型可以在客户端程序实现排序功能;
    /// </summary>
    public class Compare : IComparable<Compare>
    {
        public double m_value { get; set; }
        public string m_name { get; set; }

        int IComparable<Compare>.CompareTo(Compare OtherValue)
        {
            return m_value.CompareTo(OtherValue);
        }
    }
}
