using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadLearn1
{
    class CLRThreadBasic_Delegate
    {
        delegate int ThereParaDelegate(int a, int b, int c);
        public int Para1
        {
            get; set;
        }

        public CLRThreadBasic_Delegate()
        {
            ThereParaDelegate ThereP = ThereParaFunc;
            IAsyncResult ar = ThereP.BeginInvoke(1, 2, 3, PrintRet, ThereP);
        }

        static int ThereParaFunc(int a, int b, int c)
        {
            int ret = a + b + c;
            Thread.Sleep(1999);
            return ret;
        }

        static void PrintRet(IAsyncResult ar)
        {
            if(ar == null)
            {
                throw new ArgumentException("ar");
            }

            ThereParaDelegate dl = ar.AsyncState as ThereParaDelegate;

            int ret = dl.EndInvoke(ar);
            Console.WriteLine("ret is:" + ret);
        }
    }
}
