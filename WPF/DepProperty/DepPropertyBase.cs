using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WPF
{
    /// <summary>
    /// 这个类型不是一个控件类型，注意：它是一个普通类型，通过它来从原理说明依赖属性的用处;
    /// 普通类型也可以拥有依赖属性，这个属性的值依赖于其他实例的属性;
    /// </summary>
    class DepPropertyBase : DependencyObject
    {
        // 声明一个依赖属性，这个属性本身没有值，它的数值必须依赖于其他对象的属性;
        // 参数一：用来指明哪个CLR属性作为这个依赖属性的包装器;
        // 参数二：依赖属性存储的数据类型;
        // 参数三：依赖属性的宿主类型是什么;
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register(
            "Name", typeof(string), typeof(DepPropertyBase));

        // 微软规定，CLR属性要与定义依赖属性时候的Register中的名称一致;
        public string Name
        {
            get{ return (string)GetValue(NameProperty); }
            set{ SetValue(NameProperty, value); }
        }

        // 借用SetBinding的方法，可以将依赖属性与Binding对象关联;
        public BindingExpressionBase SetBinding(DependencyProperty dp, BindingBase bd)
        {
            return BindingOperations.SetBinding(this, dp, bd);
        }

    }
}
