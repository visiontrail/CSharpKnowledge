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
    /// 解耦和第一级别：重写控件类型;
    /// </summary>
    class CLS_SearchTextBox : TextBox
    {
        // 目标作用的UI控件;
        public I_CLS_UserControl Target_element;

        // 当搜索框输入发生改变时;
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            // 能获取到的信息;
            Console.WriteLine("用户输入内容发生改变:" + (e.OriginalSource as TextBox).Text.ToString());
            Console.WriteLine("用户行为:" + e.UndoAction);
            foreach(TextChange change in e.Changes)
            {
                Console.WriteLine("Offset:" + change.Offset + " AddedLength:" + change.AddedLength);
            }

            // 在这里写作用在Target_element的真正执行方法;
            if(Target_element != null)
            {
                Target_element.UpdateBelongControl(this, new SearchButtonEventArgs() {
                    SearchContent = (e.OriginalSource as TextBox).Text.ToString(),
                    SearchUndoAction = e.UndoAction,
                    SearchTextChange = e.Changes
                });
            }
                
        }
    }

    public class SearchButtonEventArgs : EventArgs
    {
        /// <summary>
        /// 用户搜索的字符;
        /// </summary>
        public string SearchContent;

        /// <summary>
        /// 用户的行为;
        /// </summary>
        public UndoAction SearchUndoAction;

        /// <summary>
        /// 字符改变情况;
        /// </summary>
        public ICollection<TextChange> SearchTextChange;
    }
}
