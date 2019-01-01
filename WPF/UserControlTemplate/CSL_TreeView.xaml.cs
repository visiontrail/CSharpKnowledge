using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.Control;
using WPF;

namespace WPF.UserControlTemplate
{
    /// <summary>
    /// CSL_TreeView.xaml 的交互逻辑
    /// </summary>
    public partial class CSL_TreeView : UserControl, I_CLS_UserControl
    {
        /// <summary>
        /// 保存属性结构内容;
        /// </summary>
        private XMLTreeViewControl TreeView_Content;

        public CSL_TreeView()
        {
            InitializeComponent();
            TreeView_Content = new XMLTreeViewControl();

            this.TreeViewReadFromXMLFile.ItemsSource = TreeView_Content.items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateBelongControl(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
