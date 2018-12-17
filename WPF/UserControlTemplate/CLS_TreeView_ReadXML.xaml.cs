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
using WPF.Model;
using WPF;
using WPF.Control;
using System.Collections.ObjectModel;

namespace WPF.UserControlTemplate
{
    /// <summary>
    /// 可更新其他控件的TreeView;
    /// </summary>
    public partial class CLS_TreeView_ReadXML : UserControl, I_CLS_UserControl
    {
        public XMLTreeViewControl TreeView_Content;    // 用于保存TreeView当中的内容;
        public ListBox BelongControl;                 // 关联附属控件;
        private ObservableCollection<ListViewSearchResult> ret = new ObservableCollection<ListViewSearchResult>(); // 用于保存搜索结果
        
        public CLS_TreeView_ReadXML()
        {
            InitializeComponent();
            TreeView_Content = new XMLTreeViewControl();

            this.TreeViewReadFromXMLFile.ItemsSource = TreeView_Content.items;
        }

        /// <summary>
        /// 搜索更新附属控件的显示内容;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateBelongControl(object sender, EventArgs e)
        {
            Console.WriteLine("Receive the SearchText Notify Event" + (e as SearchButtonEventArgs).SearchContent);

            if(string.IsNullOrEmpty((e as SearchButtonEventArgs).SearchContent))
            {
                return;
            }

            ret.Clear();
            ret = TreeView_Content.finditems(TreeView_Content.items, (e as SearchButtonEventArgs).SearchContent);
            this.BelongControl.ItemsSource = ret;
        }
    }
}
