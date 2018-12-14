using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF.UserControlTemplate
{
    /// <summary>
    /// 负责搜索的Text输入框;
    /// 解耦和第一级别：重写控件方法;
    /// </summary>
    class SearchTextBox : TextBox
    {
        // 目标作用的UI控件;
        public FrameworkElement Target_element;

        // 当搜索框输入发生改变时;
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            Console.WriteLine("用户输入内容发生改变:" + (e.OriginalSource as TextBox).Text.ToString());
            Console.WriteLine("用户行为:" + e.UndoAction);
            foreach(TextChange change in e.Changes)
            {
                Console.WriteLine("Offset:" + change.Offset + " AddedLength:" + change.AddedLength);
            }

            // 在这里写作用在Target_element的真正执行方法;
        }
    }
}
