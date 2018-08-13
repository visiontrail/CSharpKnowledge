using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrySomeInterface
{
    /// <summary>
    /// 这个类型可以在客户端程序实现排序功能;
    /// IComparable:
    /// </summary>
    public class SomeComparableClass : IComparable<SomeComparableClass>
    {
        public double m_value { get; set; }
        public string m_name { get; set; }

        int IComparable<SomeComparableClass>.CompareTo(SomeComparableClass OtherValue)
        {
            return m_value.CompareTo(OtherValue.m_value);
        }
    }

    public static class ParticalCompare
    {
        public static int? Compare(IComparer<int> comparer, int a, int b)
        {
            int ret = comparer.Compare(a, b);
            return ret == 0 ? new int?() : ret;
        }

        public static double? Compare(IComparer<SomeComparableClass> comparer, SomeComparableClass a, SomeComparableClass b)
        {
            double ret = comparer.Compare(a, b);
            return ret == 0 ? new double?() :ret;
        }
    }
}
