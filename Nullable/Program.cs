using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nullable
{
    class Program
    {
        /// <summary>
        /// 引用类型本质是个指针，所以天生具备可空性质;
        /// 所以，对于可空类型，就是完全针对值类型设计出来的;
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Nullable<int> inta = null;
            int ret = inta.GetValueOrDefault();   // 获取可空类型的值或者返回默认值;
            Console.WriteLine("可以通过HasValue判断是否是空值:" + inta.HasValue + "调用GetValueOrDefalut的结果:" + ret);

            int? intb = null;                     // Nullable的另外一种写法，微软的语法糖;
            int ret2 = intb ?? 100;               // 一种判断是否为空值，且赋值的语法糖;
            Console.WriteLine("双问号??表示如果为空，则执行后边语句;" + ret2 );

            //_______________________________??操作符的有趣用法;
            int? Condition_1 = null;
            int? Condition_2 = 100;
            int? Condition_3 = 10;

            int? ret3 = Condition_1 ?? Condition_2 ?? Condition_3;
            Console.WriteLine("从左到右，依次验证;" + ret3);

            Console.ReadKey();
        }
    }
}
