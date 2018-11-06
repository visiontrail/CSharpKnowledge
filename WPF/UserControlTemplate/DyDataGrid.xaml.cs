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
                    // 显示列表类型的数据结构;
                    if(iter.Item3 is System.Collections.Generic.List<string>)
                    {
                        DataGridTemplateColumn column = new DataGridTemplateColumn();                   // 单元格是一个template;
                        DataTemplate template = new DataTemplate();                                     // 用一个DataTemplate类型填充;
                        ComboBox box = new ComboBox();
                        
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
                    // 如果单元格内类型是ComboBox类型;
                    else if(iter.Item3 is GridCellComboBox)
                    {
                        DataGridTemplateColumn column = new DataGridTemplateColumn();
                        DataTemplate TextBlockTemplate = new DataTemplate();
                        DataTemplate ComboBoxTemplate = new DataTemplate();
                        
                        string textblock_xaml =
                           @"<DataTemplate xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                                            xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                                            xmlns:model='clr-namespace:WPF.Model'>
                                <TextBlock Text='{Binding " + iter.Item1 + @".name}'/>
                            </DataTemplate>";

                        string combobox_xaml =
                           @"<DataTemplate xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                                            xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                                            xmlns:model='clr-namespace:WPF.Model'>
                                <ComboBox ItemsSource='{Binding " + iter.Item1 + @".m_AllList.Values}' SelectedIndex='0'/>
                             </DataTemplate>";

                        TextBlockTemplate = XamlReader.Parse(textblock_xaml) as DataTemplate;
                        ComboBoxTemplate = XamlReader.Parse(combobox_xaml) as DataTemplate;

                        column.Header = iter.Item2;                                      // 填写列名称;
                        column.CellTemplate = TextBlockTemplate;                         // 将单元格的显示形式赋值;
                        column.CellEditingTemplate = ComboBoxTemplate;                   // 将单元格的编辑形式赋值;
                        column.Width = 230;                                              // 设置显示宽度;

                        this.DynamicDataGrid.Columns.Add(column);
                    }
                    else if(iter.Item3 is DateTime)
                    {

                    }
                    else if(iter.Item3 is String)
                    {

                    }
                    else
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
            
            this.DynamicDataGrid.BeginningEdit += DynamicDataGrid_BeginningEdit;                  // 当表格发生正在编辑的状态;
            this.DynamicDataGrid.LostFocus += DynamicDataGrid_LostFocus;                          // 当表格失去焦点的时候;
            this.DynamicDataGrid.SelectionChanged += DynamicDataGrid_SelectionChanged;
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

        /// <summary>
        /// 单元格失去焦点之后;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DynamicDataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            /* 该事件触发过于频繁，暂时不使用该事件进行处理;
            if(e.OriginalSource is DataGridCell)
            {
                Console.WriteLine("第三步：原先的DataGridCell失去焦点，焦点变为ComboBox " + e.Source.GetType());
                try
                {
                    DataGridCell LostFocusObj = new DataGridCell();
                    DyDataDridModel RetObj = new DyDataDridModel();

                    LostFocusObj = e.OriginalSource as DataGridCell;
                    RetObj = LostFocusObj.DataContext as DyDataDridModel;

                    RetObj.JudgePropertyName_ChangeSelection(LostFocusObj.Column.Header.ToString());
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Exception: " + ex);
                }
            }

            if(e.OriginalSource is ComboBox)
            {
                int a = (e.OriginalSource as ComboBox).SelectedIndex;
                //Console.WriteLine("Selected Index is!!!!!!!!!!!!!!!!!!!:" + a);
            }
            */

        }


        private void DynamicDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((e.OriginalSource as ComboBox).SelectedIndex == -1))
            {
                return;
            }
            else
            {
                try
                {
                    (sender as DataGrid).SelectedCells[0].Item.GetType();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
            try
            {
                Console.WriteLine("第二步：判断选择变化的事件触发" + (e.OriginalSource as ComboBox).SelectedIndex);
                Console.WriteLine("选择改变事件触发，Sender的数据类型是:" + (sender as DataGrid).SelectedCells[0].Item.GetType());

                ((sender as DataGrid).SelectedCells[0].Item as DyDataDridModel).JudgePropertyName_ChangeSelection(
                    (sender as DataGrid).SelectedCells[0].Column.Header.ToString());
            }
            catch
            {

            }

        }
    }
}
