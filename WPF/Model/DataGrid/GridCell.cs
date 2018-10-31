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
        public abstract void EditingCallback();
    }

    /// <summary>
    /// 在DataGrid单元格内填充字符类型的数据;
    /// GridCell应该有多种类型的呈现形式，即有不同样式的
    /// </summary>
    public class GridCell : AbsDataGridCell
    {
        /// <summary>
        /// 编辑字符类型单元格时候的回调;
        /// </summary>
        public override void EditingCallback()
        {
            Console.WriteLine("String GridCell Editing Callback:" + name);
        }
    }

    /// <summary>
    /// 在DataGrid中显示枚举类型的数据模型;
    /// </summary>
    public class GridCellComboBox : AbsDataGridCell
    {
        public Dictionary<int, string> m_AllList { get; set; }
        public int m_CurContent { get; set; }
        public List<string> m_AllListString { get; set; }

        /// <summary>
        /// 正在编辑当中的回调事件;
        /// 当鼠标停留超过2秒的时候，以气泡的形式显示其枚举类型的数字;
        /// </summary>
        public override void EditingCallback()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 鼠标双击事件回调;
        /// 鼠标双击后，才会出现ComboBox下拉框选项;
        /// </summary>
        public void MouseDoubleClickCallBack()
        {

        }

        /// <summary>
        /// 当选择完成之后，单元格失去焦点后的事件回调;
        /// 选择完成后，向后台服务器发送请求;
        /// </summary>
        public void SelectedCallBack()
        {

        }
    }

    /// <summary>
    /// 在DataGrid中显示BIT类型的数据模型;
    /// </summary>
    public class GridCellBit : GridCell
    {

    }
}
