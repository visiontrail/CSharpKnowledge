using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSLThread
{
    public class ThreadPoolLearn
    {
        public static bool threadrun = true;
        public BackgroundWorker bgworker = new BackgroundWorker();
        
        public void startFunc()
        {
            ThreadPool.QueueUserWorkItem((x) => {
                while((bool)x)
                {
                    Console.Write(".");
                    Thread.Sleep(100);
                }
            }, threadrun);
        }

        public void startFuncByBgWorker()
        {
            bgworker.DoWork += Bgworker_DoWork;
            bgworker.WorkerReportsProgress = true;
            bgworker.ProgressChanged += Bgworker_ProgressChanged;
            bgworker.RunWorkerAsync();
        }

        private void Bgworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("Receive Changed!");
        }

        private void Bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(100);
        }
    }
}
