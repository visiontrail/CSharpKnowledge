using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    public class GridCell
    {
        public string name { get; set; }
        public void EditingCalback()
        {
            Console.WriteLine("GridCell Editing Callback:" + name);
        }

        

    }
}
