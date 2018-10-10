using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    /// <summary>
    /// 在DataGrid单元格内填充的类型
    /// GridCell应该有多种类型的呈现形式，即有不同样式的
    /// </summary>
    public class GridCell
    {
        /// <summary>
        /// 显示在单元格内的内容;
        /// </summary>
        public string name { get; set; }

        public void EditingCalback()
        {
            Console.WriteLine("GridCell Editing Callback:" + name);
        }
    }

    public class GridCellCombox
    {
        public List<string> name_list { get; set; }
    }
}
