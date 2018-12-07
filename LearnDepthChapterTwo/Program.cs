using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Linq.Expressions;


namespace Delegate
{
    // 声明了一个委托;
    // 这个委托表示一个string类型的入参，并且返回值为void的函数委托;
    delegate void StringProcessor(string str);
    delegate void StringProcessor2(string str);

    class Program
    {
        private static List<string> strlist = new List<string>();
        static void Main(string[] args)
        {
            // _________________________________________在自己的类中加入委托;
            TryClass a = new TryClass();

            DelegateClass dc = new DelegateClass();
            dc.PrintStringdele += new PrintString((prt_str) => {
                Console.WriteLine("创建一个委托链，这是第一次" + prt_str);
            });
            dc.PrintStringdele += new PrintString((prt_str) => {
                Console.WriteLine("创建一个委托链，这是第二次" + prt_str);
            });
            dc.m_member1 = "Muti_Delegate";
            dc.PrintAllDel("Muti_Delegate");
            
            // ______________BCL提供的很多类中都有委托的应用，以下应用了匿名函数作为实例化的委托
            strlist.Add("1"); strlist.Add("2"); strlist.Add("3");
            List<string> findret = strlist.FindAll(new Predicate<string>(delegate (string instring) {
                if (instring == "1")
                    return true;
                else
                    return false;
            }));

            // _____________BCL提供了很多类中都有委托应用，以下应用了Lambda表达式作为实例化的委托
            strlist.ForEach((str) =>
            {
                Console.WriteLine("in ForEach " + str);
            });
            
            // 最终让窗口停等;
            Console.ReadLine();

            return;

        }
        
    }

    class ClassChapter2Instance
    {
        public void PrintStringInstance(string str)
        {
            Console.WriteLine("执行实例化方法PrintStringInstance第一次:" + str);
        }

        public void PrintStringInstance2(string str)
        {
            List<int> a = new List<int>();
            List<string> c = new List<string>();

            List<string> matches = c.FindAll(match => match == "10");
            a.ForEach(x => Console.WriteLine("print" + x.ToString()));
            c.ForEach(x => Console.WriteLine("print" + x));

            Console.WriteLine("执行实例化方法PrintStringInstance第二次:" + str);
        }
    }
}
