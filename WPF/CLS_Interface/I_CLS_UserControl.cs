using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    public interface I_CLS_UserControl
    {
        /// <summary>
        /// 更新附属控件;
        /// </summary>
        /// <param name="sender">源控件</param>
        /// <param name="e">目标控件</param>
        void UpdateBelongControl(object sender, EventArgs e);
    }
}
