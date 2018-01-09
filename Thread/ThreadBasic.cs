using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadLearn
{
    class ThreadBasic
    {
        public static int X = 0;                 // 处于共享状态的静态类型X
        [ThreadStatic]
        public static int threadStaticX = 0;     // 每个线程都拥有自己副本的threadStaticX
        public int threadX = 0;

        public void AddXY()
        {
            for (int i = 0; i < 10000; i++)
            {
                X++;
                threadStaticX++;

                Thread current = Thread.CurrentThread;

                string info = string.Format("threadID:{0} 非线程私有的X={1}; [ThreadStatic]线程私有的threadStaticX={2}", 
                                            current.ManagedThreadId, X, threadStaticX);
                Console.WriteLine(info);
            }

        }

    }
    
}
