using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF
{
    /// <summary>
    /// 自定义用户控件需要实现的接口;
    /// </summary>
    public interface I_CLS_UserControl
    {
        /// <summary>
        /// 通知其附属控件更新内容;
        /// </summary>
        /// <param name="sender">控件源</param>
        /// <param name="e">内容</param>
        void UpdateBelongControl(object sender, EventArgs e);
    }
}
