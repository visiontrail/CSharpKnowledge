using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericCSharp
{
    class Program
    {
        public delegate int IAsnycDelayTime(int ms, int data);
        public delegate string printString();

        public static string ps()
        {
            string a = "aaa";
            return a;
        }

        public static int DelayTimeAsync(int ms, int data)
        {
            int i = 0;
            Console.WriteLine("Async start");
            while(i != data)
            {
                Thread.Sleep(ms);
                ++i;
            }

            Console.WriteLine("Async End!");
            return 10;
        }

        public static void printComplete(IAsyncResult ar)
        {
            if(ar == null)
            {
                return;
            }

        }

        // 类型T必须是继承自ICompareable的类型才行
        static int CompareToDefault<T>(T para) where T : IComparable
        {
            return para.CompareTo(default(T));
        }
        static int func<T>(T para) where T : ITemp
        {
            return 0;
        }


        static void Main(string[] args)
        {
            // ++++泛型尝试;+++++++++++++++++++++++++++++
            List<ITemp> TempList = new List<ITemp>();
            Random ram = new Random();

            for (int i = 0; i < 10; ++i)
            {
                double tempt = ram.NextDouble();
                ITemp tempAdd = new ITemp();
                tempAdd.TempereatureC = tempt * 10;
                TempList.Add(tempAdd);

            }

            TempList.Sort();
            
            foreach(ITemp i in TempList)
            {
                Console.WriteLine(i.TempereatureC);
            }
            // ++++泛型尝试;+++++++++++++++++++++++++++++

            // ++++CompareToDefault+++++++++++++++++++++
            Console.WriteLine("String Compare To Default:"+CompareToDefault<string>("a"));
            Console.WriteLine("String2 Compare To Default:" + CompareToDefault("aaa"));
            Console.WriteLine("int -10 CompareTo Default" + CompareToDefault<int>(-10));
            Console.WriteLine("int 0 Compare To Default" + CompareToDefault<int>(0));
            Console.WriteLine("int 10 Compare To Default" + CompareToDefault<int>(10));
            //Console.WriteLine("Itemp:" + CompareToDefault<Itemp2<string>>(itemp2));
            // ++++CompareToDefault+++++++++++++++++++++

            DerivedClass dc = new DerivedClass("string1", "string2");
            dc.PrintName();


            IAsnycDelayTime ass = DelayTimeAsync;
            // 参数1、参数2：委托调用的参数
            // 参数3（回调）：异步委托完成任务之后，就会调用回调函数
            IAsyncResult ar = ass.BeginInvoke(1000, 10, printComplete, null);

            Thread.Sleep(3000);

            Console.WriteLine("Main Ended~!");

            int res = ass.EndInvoke(ar);

            if(res == 0)
            {
                Console.WriteLine("执行成功");
            }

            printString delstr = ps;
            IAsyncResult irs = delstr.BeginInvoke(null,null);

            string abc = null;
            abc = delstr.EndInvoke(irs);

            Console.ReadLine();
            
        }
    }
}
