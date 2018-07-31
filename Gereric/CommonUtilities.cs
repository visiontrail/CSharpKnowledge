using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gereric
{
    public class CommonUtilities
    {
    }

    /// <summary>
    /// Effective C# 条目7;
    /// 尽可能的使用泛型方法，而不是泛型类型;
    /// 这样的好处就是可以使得编译器能够快速的选择正确的函数进行调用;
    /// </summary>
    public static class Utils
    {
        public static T MAX<T>(T left, T right)
        {
            return Comparer<T>.Default.Compare(left, right) < 0 ? right : left;
        }
        public static double MAX(double left, double right)
        {
            return Math.Max(left, right);
        }
    }
}
