using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadLearn
{
    class ThreadBasic_DyTLS
    {
        public static int X = 0;                 // 处于共享状态的静态类型X
        [ThreadStatic]
        public static int threadStaticX = 0;     // 每个线程都拥有自己副本的threadStaticX
        public int threadX = 0;

        public void SetDyThreadData()
        {
            LocalDataStoreSlot dataslot = Thread.GetNamedDataSlot("slot");
            threadX = (int)Thread.GetData(dataslot);
            
        }

    }

    /// <summary>
    /// 有关线程同步，临界区操作的相关方法;
    /// </summary>
    class ThreadBasic_StaticTLS
    {
        public static int X = 0;                          // 处于共享状态的静态类型X,多个线程访问后,存在同步问题;

        [ThreadStatic]
        public static int threadStaticX = 0;              // 每个线程都拥有自己副本的threadStaticX

        private object objlock;                           // 我是一把锁,我可以锁住一个对象，让其他线程无法访问这个对象;
        private readonly object objlock2 = new object();  // 效果相同;

        public event EventHandler NotifyProgress;         // 对外通知进度的事件;

        private object GetSynchandle()
        {
            Interlocked.CompareExchange(ref this.objlock, new object(), null);
            return objlock;
        }

        public void AddXY()
        {
            for (int i = 0; i < 200; i++)
            {
                // 如果注释掉lock的话，几乎100%会出同步问题;
                // 我测试用的PC配置较高，单线程效率较高，如果配置一般般的电脑，可以减少线程数量或者减少循环次数;
                lock(GetSynchandle())
                {
                    Thread.Sleep(20);
                    X++;
                }

                if(NotifyProgress != null)
                    NotifyProgress(this, EventArgs.Empty);    // 对外通知进度;

                threadStaticX++;

                Thread current = Thread.CurrentThread;
                string info = string.Format("threadID:{0} 非线程私有的X={1}; [ThreadStatic]线程私有的threadStaticX={2}",
                                            current.ManagedThreadId, X, threadStaticX);
                Console.WriteLine(info);
            }
        }
    }

    class ThreadMonitor
    {
        static object obj_Monitor = new object();
        static int obj_Int = 10;

        public void VisitObjMonitor()
        {
            Monitor.Enter(obj_Monitor);

            try
            {
                // DoSomething so long about 10s
                Console.WriteLine("Do Something about 5s by VisitObjMonitor");
                Thread.Sleep(5000);
            }
            finally
            {
                // 只要Enter方法成功了，那么总会调用Exit();
                Monitor.Exit(obj_Monitor);
            }
        }

        public void VisitObjLock()
        {
            lock(obj_Monitor)
            {
                // DoSomething
                Console.WriteLine("Do Something Critical Zone VisitObjLock");
            }
        }

        public void VisitValueMonitor()
        {
            Monitor.Enter(obj_Int as object);

            lock(obj_Int as object)
            {

            }
        }

        public void VisitObjMonitor_TryEnter()
        {
            while (!Monitor.TryEnter(obj_Monitor))
            {
                // 尝试进入的时候，可以干掉别的，比如打印个等待符号什么的;
                Console.Write(".");
                Thread.Sleep(100);
            }
            try
            {
                // 执行临界操作;
                Console.WriteLine();
                Console.WriteLine("Do Something Critical Zone VisitObjMonitor_TryEnter");
            }
            finally
            {
                Monitor.Exit(obj_Monitor);
            }
        }

    }

}
