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
                                <ComboBox ItemsSource='{Binding " + iter.Item1 + @"}' SelectedIndex='1'/>
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
                                <ComboBox ItemsSource='{Binding " + iter.Item1 + @".m_AllList}' SelectedIndex='0'/>
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
            this.DynamicDataGrid.SelectionChanged += DynamicDataGrid_SelectionChanged;            // 当选择发生变化的时候;
            this.DynamicDataGrid.GotMouseCapture += DynamicDataGrid_GotMouseCapture;              // 捕获鼠标事件，在该事件中添加拖拽判断;
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
        /// 该事件触发过于频繁，暂时不使用该事件进行处理;
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

        /// <summary>
        /// 当单元格选择发生变化的时候;
        /// TODO:当第一次获取焦点的时候，也会触发该函数;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DynamicDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((e.OriginalSource as ComboBox).SelectedIndex == -1) || e.RemovedItems.Count == 0)
            {
                return;
            }
            else
            {
                try
                {
                    // 由于前端的选择模式被限定在了SingleMode，所以以下默认取第一个就可以;
                    (sender as DataGrid).SelectedCells[0].Item.GetType();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
            try
            {
                // TODO:当前只有ComboBox这样的特殊类型，后续添加新的类型之后，这个还需要修改;
                Console.WriteLine("判断当期是否是第一次进入编辑状态：" + e.OriginalSource.GetType());
                ((sender as DataGrid).SelectedCells[0].Item as DyDataDridModel).JudgePropertyName_ChangeSelection(
                    (sender as DataGrid).SelectedCells[0].Column.Header.ToString(), (e.OriginalSource as ComboBox).SelectedItem);
            }
            catch
            {

            }

        }

        /// <summary>
        /// 获取鼠标事件，拖拽动作的起点;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DynamicDataGrid_GotMouseCapture(object sender, MouseEventArgs e)
        {
            try
            {
                if ((e.OriginalSource as DataGrid).Items.CurrentItem is DyDataDridModel)
                {
                    Console.WriteLine("GotMouseCapture;捕获鼠标除了移动之外的任何事件，传过来的参数数据类型为:" + e.Source.GetType());

                    DataGrid sender_item = e.OriginalSource as DataGrid;                  // 获取当前被操作的控件;
                    foreach (var iter in (e.OriginalSource as DataGrid).SelectedCells)
                    {
                        // 如果鼠标右键是按下的话;
                        if (e.LeftButton == MouseButtonState.Pressed)
                        {
                            // 设置拖拽事件的参数;
                            // 参数一：拖拽事件作用源的控件实例;
                            // 参数二：赋值给拖拽目标的参数实例;
                            // 参数三：拖拽的效果;
                            DragDropEffects myDropEffect = DragDrop.DoDragDrop(sender_item, iter.Item as DyDataDridModel, DragDropEffects.Copy);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
