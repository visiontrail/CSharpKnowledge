using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    /// <summary>
    /// DataGrid单元格内数据类型的抽象函数;
    /// </summary>
    public abstract class AbsDataGridCell
    {
        /// <summary>
        /// DataGrid加载的时候，要显示在单元格内的内容;
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 当单元格被编辑的时候的事件触发;
        /// </summary>
        public abstract void EditingCallback();

        /// <summary>
        /// 当表格失去焦点后;
        /// </summary>
        public abstract void LostFocusCallback();

        /// <summary>
        /// 当表格的单元格选择发生变化的时候;
        /// </summary>
        public abstract void SelectionCellChanged();
    }

    /// <summary>
    /// 在DataGrid单元格内填充字符类型的数据;
    /// GridCell应该有多种类型的呈现形式，即有不同样式的
    /// </summary>
    public class GridCell : AbsDataGridCell
    {
        public override void EditingCallback()
        {
            Console.WriteLine("String GridCell Editing Callback:" + name);
        }

        public override void LostFocusCallback()
        {
            Console.WriteLine("String GridCell LostFocus CallBack");
        }

        public override void SelectionCellChanged()
        {
            Console.WriteLine("String GridCell SelectionCellChanged CallBack");
        }
    }

    /// <summary>
    /// 在DataGrid中显示枚举类型的数据模型;
    /// </summary>
    public class GridCellComboBox : AbsDataGridCell
    {
        /// <summary>
        /// 该单元格内ComboBox要显示的所有内容;
        /// </summary>
        public Dictionary<int, string> m_AllList { get; set; }

        /// <summary>
        /// 当前显示的内容，添加这个;
        /// </summary>
        public int m_CurContent { get; set; }
        

        /// <summary>
        /// 正在编辑当中的回调事件;
        /// 当鼠标停留超过2秒的时候，以气泡的形式显示其枚举类型的数字;
        /// </summary>
        public override void EditingCallback()
        {
            Console.WriteLine("第一步：判断开始编辑的事件触发:" + m_CurContent);
        }

        public override void LostFocusCallback()
        {
            Console.WriteLine("ComboBox LostFocus CallBack");
        }

        public override void SelectionCellChanged()
        {
            Console.WriteLine("ComboBox GridCell SelectionCellChanged CallBack");
        }
    }
}
