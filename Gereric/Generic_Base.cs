using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gereric
{
    public class Generic_Base<T> where T : otherstring
    {
        public List<string> list { get; set; }  
        public List<T> list2 { get; set; }
        public Dictionary<int,int> dic1; 

        public Generic_Base()
        {
            list = new List<string>();
            list2 = new List<T>();
        }
        static Generic_Base()
        {
            Console.WriteLine("111");
        }

        // 实现一个泛型方法，可以通过约束泛型接口实现相关内容;
        // 这个函数的功能是与T类型的默认值进行比较;
        public static int ComparetoDefalut<T>(T value)
            where T : IComparable<T>
        {
            return value.CompareTo(default(T));
        }
    }

    public class Generic_Base
    {
        public List<string> list { get; set; }
        public Dictionary<int, int> dic1;

        public Generic_Base()
        {
            list = new List<string>();
        }

        public static Dictionary<T1, T2> Of<T1, T2>(T1 first, T2 second)
        {
            Dictionary<T1, T2> dic = new Dictionary<T1, T2>();
            dic.Add(first, second);
            return dic;
        }

    }

    public class otherstring
    {
        public string contents { get; set; }
        public otherstring()
        {

        }
    }

    public class Outer<T>
    {
        public static void funct()
        {

        }
        public class Inner<U,V>
        {
            public static void funct()
            {

            }
            static Inner()
            {
                Console.WriteLine("T:" + typeof(T) + "U:" + typeof(U));
            }
            public Inner()
            {
                Console.WriteLine("Constructure");
            }
            
        }
    }

}
