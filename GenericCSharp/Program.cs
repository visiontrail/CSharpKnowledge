using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrySomeInterface;

namespace TryAnyCSharp
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


        static public bool FindSomethong<T>(T obj)
        {
            return true;
        }

        static void Main(string[] args)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();

            // ++++泛型尝试;+++++++++++++++++++++++++++++
            List<ITemp> TempList = new List<ITemp>();
            Random ram = new Random();

            Person person = new Person("name", DateTime.Now);
            Student stu = new Student("stu", DateTime.Now);
            Person person2 = stu;

            Console.WriteLine("得到得类型;"+ person2.GetType());

            string aaa = "a.2.3.4";
            Console.WriteLine(asciiEncoding.GetBytes(aaa)[0]);

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

            TempList.Find(obj => obj.TempereatureC == 10);

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

            foreach(var iter1 in Enumerable.Range(10, 20))
            {
                Console.WriteLine("Enumerable Range" + iter1);
            }

            Console.WriteLine("---------------------------");

            // 类Enumerable的使用;
            // 这个delegate是一个x作为入参，返回一个bool类型的匿名函数;
            IEnumerable<int> ret = Enumerable.Range(10, 20).Where(delegate (int x) { return x < 20; });

            ret = Enumerable.Range(10, 20).Where(underTwenty);
            foreach (var iter1 in ret)
            {
                Console.WriteLine("Enumerable Range delegate" + iter1);
            }

            var ret2 = Enumerable.Range(10, 40).Where(x => x % 2 != 0);
            foreach(var iter in ret2)
            {
                Console.WriteLine("Enumerable Range Lambda" +iter);
            }

            bool? a = Comparer.Equals(ret, ret2);

            Person person1 = new Person("Guoliang", new DateTime(1988, 9, 25));
            Person personDie = new Person("Paul", new DateTime(1973, 10, 2));

            personDie.DeathDay = new DateTime(2016, 10, 11);

            Console.WriteLine("Guoliang Age:" + person1.Age.Days);
            Console.WriteLine("Paul Age:" + personDie.Age.Days / 365);
            
            Console.ReadKey();
            
        }
        
        static bool underTwenty(int x)
        {
            return x < 20;
        }
    }
}
