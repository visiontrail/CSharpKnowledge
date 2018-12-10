using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF
{
    /// <summary>
    /// 这个类型不是一个控件类型，它是一个普通类型;
    /// 普通类型也可以拥有依赖属性，这个属性的值依赖于其他实例的属性;
    /// </summary>
    class DepPropertyBase : DependencyObject
    {
        // 一个依赖属性，这个属性是依赖于其他属性的;
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register(
            "Name", typeof(string), typeof(DepPropertyBase));
    }
}
