using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    class ChapterTwoClass
    {
        public static void HandleDemoEvent(object obj, EventArgs args)
        {
            Console.WriteLine("HandleDemoEvent Print:" +obj.ToString());
            return;
        }
    }
}
