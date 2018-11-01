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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.Model;
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
        /// 动态表的所有列信息,该属性必须设置，否则无法正常显示;
        /// 设置该属性之后，动态表就会将所有列对应的模板全部生成;
        /// </summary>
        private DyDataDridModel m_ColumnModel;
        public DyDataDridModel ColumnModel
        {
            get
            {
                return m_ColumnModel;
            }
            set
            {
                m_ColumnModel = value;
                // 获取所有列信息，并将列信息填充到DataGrid当中;
                foreach(var iter in m_ColumnModel.PropertyList)
                {
                    if(iter.Item3 is System.Collections.Generic.List<string>)
                    {
                        DataGridTemplateColumn column = new DataGridTemplateColumn();                   // 单元格是一个template;
                        DataTemplate template = new DataTemplate();                                     // 用一个DataTemplate类型填充;
                        ComboBox box = new ComboBox();

                        // 实在没有什么好办法，直接在CS代码当中直接写DataTemplate的XAML代码;
                        string xaml1 =
                            @"<DataTemplate xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                                                xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                                                xmlns:model='clr-namespace:WPF.Model'>
                                    <ComboBox ItemsSource='{Binding " + iter.Item1 + @"}' SelectedIndex='0'/>
                                 </DataTemplate>";

                        template = XamlReader.Parse(xaml1) as DataTemplate;

                        column.Header = iter.Item2;                               // 填写列名称;
                        column.CellTemplate = template;                           // 将单元格的显示形式赋值;
                        column.Width = 230;                                       // 设置显示宽度;

                        this.DynamicDataGrid.Columns.Add(column);
                    }
                    else if(iter.Item3 is GridCellComboBox)
                    {

                    }
                    else if(iter.Item3 is DateTime)
                    {

                    }
                    else if(iter.Item3 is String)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// 动态表构造函数;
        /// </summary>
        public DyDataGrid()
        {
            InitializeComponent();
            
            this.DynamicDataGrid.BeginningEdit += DynamicDataGrid_BeginningEdit;
        }

        /// <summary>
        /// 单元格开始编辑时;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DynamicDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dynamic temp = e.Column.GetCellContent(e.Row).DataContext as DyDataDridModel;
            // 根据不同的列（既数据类型）改变不同的处理策略;
            temp.JudgePropertyName_StartEditing(e.Column.Header);
        }
    }
}
