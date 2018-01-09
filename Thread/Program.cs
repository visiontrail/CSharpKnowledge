using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLearn
{
    class Program
    {
        static void Main(string[] args)
        {

            //                                                           静态TLS和动态TLS
            //---------------------------------------------------------------------------
            ThreadBasic myStaticTest = new ThreadBasic();                     // 实例化一个对象;
            ThreadStart ts1 = new ThreadStart(myStaticTest.AddXY);

            LocalDataStoreSlot slot = Thread.AllocateNamedDataSlot("slot1");  // 设置一个有名称的槽;
            
            Thread.SetData(slot, 10);                                         // 设置这个槽中的值;
            Thread Dt1 = new Thread(ts1);

            Console.WriteLine("动态TLS：");

            Console.ReadLine();

            Thread t1 = new Thread(ts1);
            Thread t2 = new Thread(ts1);
            Thread t3 = new Thread(ts1);
            Thread t4 = new Thread(ts1);
            Thread t5 = new Thread(ts1);

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();

            //string info = string.Format("非线程私有的X={0}",ThreadBasic_ThreadStaticArr.X);
            //Console.WriteLine(info);
            
            Console.ReadLine();
        }
    }
}
