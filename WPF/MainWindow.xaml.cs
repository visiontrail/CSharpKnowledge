using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF.ViewModel;
using System.Threading;
using WPF.UserControlTemplate;
using WPF.Model;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using WPF.Control;
using TrySomeInterface;

namespace WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑;
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Person> persons = new List<Person>();            // 最简单的控件Binding实验数据;
        public DataGridVM m_DgVm;                                    // DataGrid的VM层，所有针对DataGrid的操作，全部使用这个类型;
        public MessageVM m_MessageVM =  new MessageVM();             // MessageVM层，用来显示MessageModel的;
        ObservableCollection<DataGridCustomer> listbase = 
            new ObservableCollection<DataGridCustomer>();            // DataGrid的基础，数据;
        List<DyDataDridModel> list = new List<DyDataDridModel>();    // 用来在DataGrid中显示的实验数据; 

        public MainWindow()
        {
            InitializeComponent();
            DrawPolyLine();                     // 用WPF画线;
            
            // 以下是使用Binding关联命令的使用方法;
            InitPersons();                      // 首先初始化一个容器——Person列表;
            BindingToProperty();                // 将类型的属性Binding到WPF控件当中;
            BindingToFunction();                // 将类型的方法Binding到WPF控件当中;
            
            // 以下是使用控件DataGrid的方法;
            InitaListToDataGridColumn();        // 将用户自定义的List添加到DataGrid的List当中;
            InitaListToDataGridWithEvnet();     // 将用户自定义的List添加到DataGrid当中，并包含对DataGrid单元格的事件操作;
            InitMessageToDataGrid();            // 将同样的数据同时写入两个DataGrid当中;
            InitMessageToDataGrid_Dynamic();    // 小实验，向一个动态类型添加属性后，关联一个DataGrid;

            // 以下是使用控件TreeView的方法;
            InitTreeViewComposite();            // 直接使用一个组合模式的实例填入到TreeView当中;
            
            // WPF中的ItemsSource是可以使用LINQ进行查询筛选的;
            this.StuList.ItemsSource = from stu in this.stus.m_StuList where stu.m_Name.StartsWith("G") select stu;
        }

        /// <summary>
        /// WPF可以实现简单的绘图功能;
        /// </summary>
        public void DrawPolyLine()
        {
            Polyline LineSeries = new Polyline();
            LineSeries.Points.Add(new Point(0, 11));
            LineSeries.Points.Add(new Point(10, 21));
            LineSeries.Points.Add(new Point(10, 31));
            LineSeries.Points.Add(new Point(120, 31));

            LineSeries.Stroke = Brushes.Blue;      // 颜色;
            LineSeries.StrokeThickness = 13;       // 粗细;

            this.canvas1.Children.Add(LineSeries);
        }


        /// <summary>
        /// WPF控件关联类型中的属性
        /// </summary>
        private void BindingToProperty()
        {
            //_____________________________________________________________以下是使用Binding关联XAML和C#属性的实验
            // 这个是ItemsControl类的控件使用binding关联数据源的方法;
            this.PersonList.ItemsSource = persons;                                    // 指定ItemsSource;
            this.PersonList.DisplayMemberPath = "m_Name";                             // 指定显示的属性;
            this.PersonList.SelectionChanged += PersonList_SelectionChanged;          // 当控件选择发生变化时;

            // 通过SetBinding方法也可以Binding现有类(ListBox)的属性;
            this.PersonID.SetBinding(TextBox.TextProperty, new Binding("m_ID")
            {
                Source = this.PersonList.ItemsSource,
                BindsDirectlyToSource = true
            });
        }

        /// <summary>
        /// WPF控件关联类型中的方法
        /// </summary>
        private void BindingToFunction()
        {
            //_____________________________________________________________以下是使用Binding关联XAML和C#命令的实验
            // 以下是通过ObjectDataProvider进行命令的binding
            ObjectDataProvider odp = new ObjectDataProvider();
            odp.ObjectInstance = new Calc();
            odp.MethodName = "Add";
            odp.MethodParameters.Add("0");
            odp.MethodParameters.Add("0");

            // 方法的第一个参数;
            this.CalcX.SetBinding(TextBox.TextProperty, new Binding("MethodParameters[0]")
            {
                Source = odp,                                                         // Source源是ObjectDataProvider
                BindsDirectlyToSource = true,                                         // 这个值是相对于ObjectDataProvider的; 
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged             // 当TextProperty发生改变时;
            });

            // 方法的第二个参数;
            this.CalcY.SetBinding(TextBox.TextProperty, new Binding("MethodParameters[1]")
            {
                Source = odp,                                                         // Source源是ObjectDataProvider
                BindsDirectlyToSource = true,                                         // 这个值是相对于ObjectDataProvider的; 
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged             // 当TextProperty发生改变时;
            });

            this.CalcRes.SetBinding(TextBox.TextProperty, new Binding(".")
            {
                Source = odp
            });
        }

        /// <summary>
        /// 构建一个组合模式的树形结构，并显示在Treeview当中;
        /// </summary>
        private void InitTreeViewComposite()
        {
            List<TreeViewComposite> root = new List<TreeViewComposite>();
            
            TreeViewComposite t1 = new TreeViewComposite() { m_ItemName = "Root" };
            TreeViewComposite t2 = new TreeViewComposite() { m_ItemName = "T1" };
            TreeViewComposite t3 = new TreeViewComposite() { m_ItemName = "T2" };
            TreeViewComposite t4 = new TreeViewComposite() { m_ItemName = "T3" };

            root.Add(t1);
            t1.m_SubList.Add(t2);
            t1.m_SubList.Add(t3);
            t2.m_SubList.Add(t4);
            t2.m_SubList.Add(t4);
            t2.m_SubList.Add(t4);
            t2.m_SubList.Add(t4);

            this.tv_composite.ItemsSource = root;
        }
        
        //_________________________________________________________________________________________以下是DataGrid控件的使用方法;
        /// <summary>
        /// DagaGrid最简单的用法，将一个容器中的内容添加到一个DataGrid控件当中;
        /// </summary>
        private void InitaListToDataGridColumn()
        {
            listbase = InitCustomerData.InitData();
            this.CustomerDataGrid.DataContext = listbase;
        }

        /// <summary>
        /// 向DataGrid当中添加对应的事件;
        /// </summary>
        private void InitaListToDataGridWithEvnet()
        {
            ObservableCollection<DataGridWithEvent> list = InitDataGridWithEventData.InitData();
            this.CustomerDataGrid_AddEvent.DataContext = list;

            this.CustomerDataGrid_AddEvent.BeginningEdit += CustomerDataGrid_AddEvent_BeginningEdit;        // 事件一：单元格开始编辑事件;
            this.CustomerDataGrid_AddEvent.SelectionChanged += CustomerDataGrid_AddEvent_SelectionChanged;  // 事件二：单元格选择出现变化时;

            // 以下是鼠标事件;
            this.CustomerDataGrid_AddEvent.MouseMove += CustomerDataGrid_AddEvent_MouseMove;                // 事件三：鼠标移动到某个元素上时;
            this.CustomerDataGrid_AddEvent.GotFocus += CustomerDataGrid_AddEvent_GotFocus;
            this.CustomerDataGrid_AddEvent.LostMouseCapture += CustomerDataGrid_AddEvent_LostMouseCapture;
            this.CustomerDataGrid_AddEvent.MouseEnter += CustomerDataGrid_AddEvent_MouseEnter;


            // 另一个元素接收鼠标拖拽事件;
            this.ReceiveDataLabel.AllowDrop = true;
            this.ReceiveDataLabel.Drop += ReceiveDataLabel_Drop;
            
        }
        
        /// <summary>
        /// 事件一：当单元给被编辑时;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerDataGrid_AddEvent_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            Console.WriteLine("开始编辑单元格;函数参数e反馈的实体是单元格内数据类型:" + (e.Column.GetCellContent(e.Row)).DataContext.GetType());

            DataGridWithEvent callbacktemp = e.Column.GetCellContent(e.Row).DataContext as DataGridWithEvent;   // 获取了填写单元格的类型实例;
            callbacktemp.JudegePropertyCall_CellEditing(e.Column.Header as string);
        }

        /// <summary>
        /// 事件二：单元格选择出现变化时;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerDataGrid_AddEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("SelectionChanged;函数参数e反馈的实体是单元格内数据类型:" + e.AddedItems.Count);
        }

        private void CustomerDataGrid_AddEvent_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                if (e.OriginalSource is DataGridCell)
                {
                    Console.WriteLine("MouseMove;函数参数e反馈的实体是单元格内数据类型:" +
                        ((e.OriginalSource as DataGridCell).DataContext as DataGridWithEvent).column1.name);

                    DataGridCell item = e.OriginalSource as DataGridCell;
                    DataGridWithEvent data = (e.OriginalSource as DataGridCell).DataContext as DataGridWithEvent;

                    // 在MouseMove事件当中可以添加鼠标拖拽事件;
                    if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                    {
                        DragDropEffects myDropEffect = DragDrop.DoDragDrop(item, item.DataContext, DragDropEffects.Copy);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            
        }


        private void CustomerDataGrid_AddEvent_GotFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("GotFocus;函数参数e反馈的实体是单元格内数据类型:" + e.OriginalSource.GetType());
        }
        
        
        private void CustomerDataGrid_AddEvent_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // 当鼠标进入到整个DataGrid表格的时候，触发该事件;
        }

        private void CustomerDataGrid_AddEvent_LostMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Console.WriteLine("LostMouseCapture;函数参数e反馈的实体是单元格内数据类型:" + e.Source.GetType());
        }

        /// <summary>
        /// 接收鼠标事件;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReceiveDataLabel_Drop(object sender, DragEventArgs e)
        {
            DataGridWithEvent cell = e.Data.GetData(typeof(DataGridWithEvent)) as DataGridWithEvent;
            (sender as Label).Content += ":" + cell.column1.name;

            Console.WriteLine(cell.column1.name);
        }


        /// <summary>
        /// 同一个数据源可以显示到两个不同的DataGrid控件当中;
        /// </summary>
        private void InitMessageToDataGrid()
        {
            this.MsgDataGrid.DataContext = m_MessageVM.messagelist;
            this.MsgDataGrid2.m_context = m_MessageVM.messagelist;
        }

        /// <summary>
        /// 为一个表格动态添加列以及内容;
        /// 表内所有的内容都是在运行时添加的，而不是预先定义好的;
        /// </summary>
        private void InitMessageToDataGrid_Dynamic()
        {
            // 动态添加内容,i表示有多少行;
            for (int i = 0; i <= 5; i++)
            {
                dynamic model = new DyDataDridModel();

                // 向单元格内添加内容;
                model.AddProperty("property2", new GridCell() { name = "343" }, "列2");
                model.AddProperty("property0", new GridCell() { name = "123" }, "列0");
                model.AddProperty("property1", new GridCell() { name = "321" }, "列1");

                list.Add(model);
            }
            for (int i = 0; i <= 2; i++)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = "列" + i;
                column.Binding = new Binding("property" + i + ".name");
                this.MsgDataGrid_AutoGenCol.Columns.Add(column);
            }

            this.MsgDataGrid_AutoGenCol.ItemsSource = list;
            this.MsgDataGrid_AutoGenCol.BeginningEdit += MsgDataGrid_AutoGenCol_BeginningEdit;
        }


        private void MsgDataGrid_AutoGenCol_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            Console.WriteLine("开始编辑单元格;函数参数e反馈的实体可以是单元格内数据类型" + (e.Column.GetCellContent(e.Row)).DataContext.GetType());
            Console.WriteLine("当前选中单元格列名:" + e.Column.Header + ",选中行数：" + e.Row.GetIndex());
            dynamic temp = e.Column.GetCellContent(e.Row).DataContext as DyDataDridModel;
            temp.JudgePropertyName_StartEditing(e.Column.Header);
        }

        /// <summary>
        /// 当ListBox控件选择发生变化时;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.PersonID.Text = (this.PersonList.SelectedItem as Person).m_ID.ToString();
        }

        /// <summary>
        /// 初始化Person;
        /// </summary>
        private void InitPersons()
        {
            persons.Add(new Person(11, "GuoLiang"));
            persons.Add(new Person(2, "RouPao"));
            persons.Add(new Person(5, "WangCY"));
        }
        
        private void GetDepProperty(object sender, RoutedEventArgs e)
        {
            SpecialTextBox st = new SpecialTextBox();
            st.SetValue(SpecialTextBox.DTextProperty, this.text1.Text);              // 为这个依赖属性设置对象;
            MessageBox.Show(st.GetValue(SpecialTextBox.DTextProperty) as string);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> list_detail = new List<string>();
            list_detail.Add("This is Message Detail Content");
            list_detail.Add("This is anoher Detail Conntent");
            Task a = new Task(()=>
            {
                int temp = 0;
                while(true)
                {
                    temp = temp + 1;
                    Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                    {
                        m_MessageVM.messagelist.Add(new MessageModel()
                        {

                            m_No = temp.ToString(),
                            m_time = DateTime.Now,
                            m_content = "content",
                            m_source = "172.27.0.1",
                            m_dest = "172.27.0.2",
                            m_content_detail = list_detail
                            
                        });
                    });
                    Thread.Sleep(1233);
                }
            });

            a.Start();

            listbase.Add(new DataGridCustomer() {
                FirstName ="Add",
                LastName ="Person",
                Email =new Uri("Http://baidu.com"),
                IsMember =false,
                cell = new GridCell() { name = "cell" }
            });
        }

        private void StartParseXML(object sender, RoutedEventArgs e)
        {
            XMLTreeViewControl a = new XMLTreeViewControl();
            this.TreeViewReadFromXMLFile.ItemsSource = a.items;

            foreach (TreeViewComposite item in a.items)
            {
                Console.WriteLine(item.m_ItemName.ToString());
            }

            // 借用这个按钮做DataGrid的小实验;
            foreach (DataGridColumn iter in this.MsgDataGrid_AutoGenCol.Columns)
            {
                Console.WriteLine(iter.Header as string);
            }
        }

        private void TryEnumerable(object sender, RoutedEventArgs e)
        {
            List<Iterator_Try> list = new List<Iterator_Try>();
            list.Add(new Iterator_Try() { value1 = 1, value2 = 3 });
            list.Add(new Iterator_Try() { value1 = 2, value2 = 2 });
            list.Add(new Iterator_Try() { value1 = 3, value2 = 1 });
        }

        // ____________________________________________________________________________________________以下是有关DataGrid有用事件的实验
        private void MsgDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            Console.WriteLine("开始编辑单元格;函数参数e反馈的实体可以是单元格内数据类型" + (e.Column.GetCellContent(e.Row)).DataContext.GetType());
            Console.WriteLine("开始编辑单元格;函数参数e反馈的实体可以是单元格内数据类型的数据" + ((e.Column.GetCellContent(e.Row)).DataContext as MessageModel).m_content);
        }
    }
}
