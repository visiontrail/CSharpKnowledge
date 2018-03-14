using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Delegate
{
    delegate void func();
    class ChapterTwoClass
    {
        public func df1;
        public event EventHandler eh1;
        public static void HandleDemoEvent(object obj, EventArgs args)
        {
            Console.WriteLine("HandleDemoEvent Print:" +obj.ToString());
            return;
        }
        public void func()
        {
            df1();
            eh1(this, new TryClassArgs());
        }
    }
    public class TryClassArgs :EventArgs
    {

    }

    public class TryClass
    {
        public TryClass()
        {
            ChapterTwoClass ctc = new ChapterTwoClass();
            ctc.eh1 += Ctc_eh1;
            ctc.df1 = new func(() => { Console.WriteLine("This is Delegate"); });
            ctc.eh1 += Ctc_eh11;
            ctc.func();
        }

        private void Ctc_eh11(object sender, EventArgs e)
        {
            Thread.Sleep(1232);
            Console.WriteLine("This is evnet2");
        }

        private void Ctc_eh1(object sender, EventArgs e)
        {
            
            Console.WriteLine("This is evnet1");
        }
    }
}
