using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreadLearn1;

namespace ThreadLearn
{
    class Program
    {
        static void Main(string[] args)
        {

            //                                                                      静态TLS和动态TLS
            //--------------------------------------------------------------------------------------
            ThreadBasic_DyTLS DTLSData = new ThreadBasic_DyTLS();              // 实例化一个对象;
            LocalDataStoreSlot slot = Thread.AllocateNamedDataSlot("slot");    // 设置一个有名称的槽;

            Thread Dt1 = new Thread(() => { Thread.SetData(slot, 10); DTLSData.SetDyThreadData(); });
            Thread Dt2 = new Thread(() => { Thread.SetData(slot, 20); DTLSData.SetDyThreadData(); });

            Dt1.Start();
            Dt1.Join();
            Console.WriteLine("动态TLS，用槽赋值:" + DTLSData.threadX);
            Dt2.Start();
            Dt2.Join();
            Console.WriteLine("动态TLS，用槽赋值:" + DTLSData.threadX);
            
            // 进行下一个线程实验;
            Console.ReadLine();
            
            ThreadBasic_StaticTLS StaticTLSData = new ThreadBasic_StaticTLS();                     // 实例化一个对象;
            ThreadStart ts1 = new ThreadStart(StaticTLSData.AddXY);

            Thread t1 = new Thread(ts1);
            Thread t2 = new Thread(ts1);
            Thread t3 = new Thread(ts1);
            Thread t4 = new Thread(ts1);
            Thread t5 = new Thread(ts1);
            Thread t6 = new Thread(ts1);
            Thread t7 = new Thread(ts1);
            Thread t8 = new Thread(ts1);
            Thread t9 = new Thread(ts1);
            Thread t10 = new Thread(ts1);
//             Thread t11 = new Thread(ts1);
//             Thread t12 = new Thread(ts1);
//             Thread t13 = new Thread(ts1);
//             Thread t14 = new Thread(ts1);
//             Thread t15 = new Thread(ts1);
//             Thread t16 = new Thread(ts1);
//             Thread t17 = new Thread(ts1);
//             Thread t18 = new Thread(ts1);
//             Thread t19 = new Thread(ts1);
//             Thread t20 = new Thread(ts1);

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            t6.Start();
            t7.Start();
            t8.Start();
            t9.Start();
            t10.Start();
//             t11.Start();
//             t12.Start();
//             t13.Start();
//             t14.Start();
//             t15.Start();
//             t16.Start();
//             t17.Start();
//             t18.Start();
//             t19.Start();
//             t20.Start();

            Thread.Sleep(20);
            
            Console.ReadLine();

            ThreadMonitor a = new ThreadMonitor();
            
            Thread Mt1 = new Thread(a.VisitObjMonitor);
            Thread Mt2 = new Thread(a.VisitObjLock);

            Mt1.Start();
            Thread.Sleep(10);
            Mt2.Start();


            CLRThreadBasic_Delegate cb = new CLRThreadBasic_Delegate();

        }
    }
}
