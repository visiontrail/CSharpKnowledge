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
using WPF.ViewModel;

namespace WPF.UserControlTemplate
{
    /// <summary>
    /// DyDataGrid.xaml 的交互逻辑
    /// DyDataGrid主要用来存储
    /// </summary>
    public partial class DyDataGrid : UserControl
    {
        /// <summary>
        /// DataGridVM有两个属性
        /// m_ColumnNameList用来保存所有的列名;
        /// m_ColumnContent用来保存所有的内容;
        /// </summary>
        public DataGridVM m_dataGridVM;                                    // DataGrid的VM层;
        
        public DyDataGrid()
        {
            InitializeComponent();
            m_dataGridVM = new DataGridVM(new List<string>());
            m_dataGridVM.m_ColumnNameList.ListChanged += M_ColumnNameList_ListChanged;
            m_dataGridVM.m_ColumnContent.ListChanged += M_ColumnContent_ListChanged;
        }

        private void M_ColumnContent_ListChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// DataGridVM层列表发生变化时,将变化添加到View中;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_ColumnNameList_ListChanged(object sender, EventArgs e)
        {
        }
    }
}
