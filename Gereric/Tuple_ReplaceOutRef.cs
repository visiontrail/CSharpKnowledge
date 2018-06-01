using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gereric
{
    /// <summary>
    /// Effective C# 条目9：使用元组替代out\ref
    /// 由于out\ref有着种种限制，比如不支持隐式类型转换，无法做到里氏替换原则;
    /// </summary>
    internal class Tuple_ReplaceOutRef
    {
        public Tuple<string, double> FindNearCityTemp(string Locate)
        {
            string city = Locate;
            double temp = 10f;
            Tuple<string, double> t = new Tuple<string, double>(city, temp);
            
            return t;
        }
    }
}
