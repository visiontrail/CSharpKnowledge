using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gereric
{
    class Program
    {

        static Converter<string, int> conv;
        static void Main(string[] args)
        {
            Generic_Base gb = new Generic_Base();
            Generic_Base<otherstring> gb2 = new Generic_Base<otherstring>();
            // 由于类型约束，编译器报错;
            //Generic_Base<string> gb3 = new Generic_Base<string>();
            List<int> l = new List<int>();
            Dictionary<string, int> dic = new Dictionary<string, int>();
            List<Generic_Base> list = new List<Generic_Base>();

            // ________首先通过使用.NET提供的官方泛型类和泛型方法熟悉泛型___
            conv = deleConvert;
            gb.list.Add("1");gb.list.Add("2");
            list.Add(gb);
            list.Add(gb);

            // ConvertAll是List泛型类提供的泛型方法
            // 其作用是将List容器当中的元素全部转换为用户指定的类型
            // 转换方法需要用户通过委托自定义;
            l = gb.list.ConvertAll<int>(conv);
            l = gb.list.ConvertAll<int>((ele) => int.Parse(ele));
            l = gb.list.ConvertAll((ele) => int.Parse(ele));
            l = gb.list.ConvertAll<int>(delegate (string ele)
            {
                return int.Parse(ele);
            });

            Generic_Base.Of(1, 2);
            dic = Generic_Base.Of<string, int>("1", 2);
            dic = Generic_Base.Of("1", 2);
            Outer<int>.Inner<string, DateTime>.funct();
            
            foreach(Generic_Base iter in list)
            {
                Console.WriteLine(iter.list[0].ToString());
            }
            
        }

        private static int deleConvert(string a)
        {
            return int.Parse(a);
        }
    }
}
