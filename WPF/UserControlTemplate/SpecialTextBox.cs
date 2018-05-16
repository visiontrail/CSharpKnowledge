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
    /// 一个自定义的TextBox,通过这个控件学习依赖属性;
    /// </summary>
    public class SpecialTextBox : TextBox
    {
        public SpecialTextBox() : base()
        {
        }

        public static readonly DependencyProperty DTextProperty =
            DependencyProperty.Register("DText", typeof(string), typeof(string));

    }
}
