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
using System.Windows.Input;
using CSLThread;
using System.ComponentModel;
using WPF.EventRoute;
using WPF.TryAnyCSharp;
using System.Windows.Markup;

namespace WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑;
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Person> persons = new List<Person>();                                                  // 最简单的控件Binding实验数据;
        public DataGridVM m_DgVm;                                                                          // DataGrid的VM层，所有针对DataGrid的操作，全部使用这个类型;
        public MessageVM m_MessageVM =  new MessageVM();                                                   // MessageVM层，用来显示MessageModel的;
        ObservableCollection<DataGridCustomer> listbase = new ObservableCollection<DataGridCustomer>();    // DataGrid的基础，数据;

        List<DyDataDridModel> list = new List<DyDataDridModel>();                                          // 用来在DataGrid中显示的实验数据; 
        ObservableCollection<DyDataDridModel> list2 = new ObservableCollection<DyDataDridModel>();         // 用来在DataGrid中显示的实验数据; 

        ThreadPoolLearn tp = new ThreadPoolLearn();

        private TimeTicked TimeTicked = TimeTicked.NeverTicked;
        private TwoTimeSpan ttp = new TwoTimeSpan();

        public MainWindow()
        {
            InitializeComponent();
            DrawPolyLine();                     // 用WPF画线;

            // 以下是使用Binding关联命令的基础使用方法;
            InitPersons();                      // 首先初始化一个容器——Person列表;
            BindingBaseKnowledge();             // 解释说明Binding的基本原理;
            BindingToProperty();                // 将类型的属性Binding到WPF控件当中;
            BindingToFunction();                // 将类型的方法Binding到WPF控件当中;
            
            // 以下是使用控件DataGrid的方法;
            InitaListToDataGridColumn();        // 将用户自定义的List添加到DataGrid当中;
            InitaListToDataGridWithEvnet();     // 将用户自定义的List添加到DataGrid当中，并包含对DataGrid单元格的事件操作;
            InitMessageToDataGrid();            // 利用MVVM模式，将同样的数据同时写入两个DataGrid当中;
            InitDynamicClassToDataGrid();       // 向一个动态类型添加属性后，关联一个DataGrid,为这个DataGrid动态加载类型;

            // 以下是使用控件TreeView的方法;
            InitTreeViewComposite();            // 直接使用一个组合模式的实例填入到TreeView当中;
            
            // WPF中的ItemsSource是可以使用LINQ进行查询筛选的;
            this.StuList.ItemsSource = from stu in this.stus.m_StuList where stu.m_Name.StartsWith("G") select stu;

            // 路由事件;
            Interface_Grid.AddHandler(Button.ClickEvent, new RoutedEventHandler((object sender, RoutedEventArgs e) =>
            {
                Console.WriteLine("The Grid RouteEvent Receive a Button Event and the Button is " + (e.OriginalSource as Button).Content.ToString());
            }));
            
        }
        

        #region 纯前端操作
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
        /// 当ListBox控件选择发生变化时;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.PersonID.Text = (this.PersonList.SelectedItem as Person).m_ID.ToString();
        }

        private void GetDepProperty(object sender, RoutedEventArgs e)
        {
            SpecialTextBox st = new SpecialTextBox();
            st.SetValue(SpecialTextBox.DTextProperty, this.text1.Text);              // 为这个依赖属性设置对象;
            MessageBox.Show(st.GetValue(SpecialTextBox.DTextProperty) as string);
        }
        #endregion

        #region Binding的基本使用方法
        /// <summary>
        /// 有关于Binding的基础原理;
        /// </summary>
        private void BindingBaseKnowledge()
        {
            // Binding可以形象比喻成UI元素和逻辑对象之间的高速公路\桥梁;
            // 既可以控制数据的流转方向，还可以在上边校验数据的正确性等等操作;

            ClassA Ca = new ClassA();
            Ca.m_iA = 10;

            Binding binding = new Binding();                       // 实例化了一个binding,即一座桥梁;
            binding.Source = Ca;                                   // 为Binding指定数据源;
            binding.Path = new PropertyPath("m_iA");               // 为Binding指定访问路径;

            // 指定Binding的访问目标，目标的属性路径和binding对象;
            BindingOperations.SetBinding(this.BindingBase, Label.ContentProperty, binding);
            this.BindingBase.SetBinding(Label.ContentProperty, "m_iA");
        }

        /// <summary>
        /// 初始化实验数据Persons;
        /// </summary>
        private void InitPersons()
        {
            persons.Add(new Person(11, "GuoLiang"));
            persons.Add(new Person(2, "RouPao"));
            persons.Add(new Person(5, "WangCY"));

            ClassA Ca = new ClassA();

            Ca.m_iA = 10;
            this.BindingLabel.DataContext = Ca;
            this.BindingLabel2.DataContext = Ca;

            Task tsk = new Task(() =>
            {
                while(true)
                {
                    Thread.Sleep(2000);
                    Ca.m_iA++;
                }
            });
            tsk.Start();
        }

        /// <summary>
        /// WPF控件关联类型中的属性
        /// </summary>
        private void BindingToProperty()
        {
            //_____________________________________________________________以下是使用Binding关联XAML和C#属性的实验
            // 这个是ItemsControl类的控件使用binding关联数据源的方法;
            this.PersonList.ItemsSource = persons;                                    // 指定ItemsSource;
            this.PersonList.SelectionChanged += PersonList_SelectionChanged;          // 当控件选择发生变化时;

            this.AllControlUsage.DataContext = persons;                                   // 如果没有明确的ItemsSource
            this.PersonList2.DisplayMemberPath = "m_Name";
            
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
        #endregion
        
        #region 有关DagaGrid控件的使用方法
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

            // 以下是表格事件;
            this.CustomerDataGrid_AddEvent.BeginningEdit += CustomerDataGrid_AddEvent_BeginningEdit;              // 事件一：单元格开始编辑事件;
            this.CustomerDataGrid_AddEvent.SelectionChanged += CustomerDataGrid_AddEvent_SelectionChanged;        // 事件二：单元格选择出现变化时;
            this.CustomerDataGrid_AddEvent.GotFocus += CustomerDataGrid_AddEvent_GotFocus;                        // 事件三：DataGrid表格点击单元格获取焦点时;

            // 以下是鼠标事件;
            this.CustomerDataGrid_AddEvent.MouseMove += CustomerDataGrid_AddEvent_MouseMove;                      // 事件四：鼠标移动到某个单元格上时触发（实验函数增加了鼠标拖动效果）;
            this.CustomerDataGrid_AddEvent.GotMouseCapture += CustomerDataGrid_AddEvent_GotMouseCapture;          // 事件五：使用这个事件事件鼠标拖拽更加稳定;

            this.CustomerDataGrid_AddEvent.MouseLeftButtonDown += CustomerDataGrid_AddEvent_MouseLeftButtonDown;  // 事件六：鼠标左键点击事件，这个事件只针对DataGrid整个表格;
            this.CustomerDataGrid_AddEvent.MouseEnter += CustomerDataGrid_AddEvent_MouseEnter;                    // 事件七：鼠标进入整个表格时触发，且只触发一次;

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

        /// <summary>
        /// 事件三：DataGrid表格点击单元格获取焦点的事件;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerDataGrid_AddEvent_GotFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("GotFocus;函数参数e反馈的实体是单元格内数据类型:" + e.OriginalSource.GetType());

        }

        /// <summary>
        /// 事件四：鼠标经过这个单元格时;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerDataGrid_AddEvent_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.OriginalSource is DataGridCell)
                {
                    Console.WriteLine("MouseMove;函数参数e反馈的实体是单元格内数据类型:" +
                        ((e.OriginalSource as DataGridCell).DataContext as DataGridWithEvent).column1.name);

                    DataGridCell item = e.OriginalSource as DataGridCell;
                    DataGridWithEvent data = (e.OriginalSource as DataGridCell).DataContext as DataGridWithEvent;

                    // 鼠标移动到某个目标后，如果点击鼠标左键，则添加鼠标拖拽事件;
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        //DragDropEffects myDropEffect = DragDrop.DoDragDrop(item, item.DataContext, DragDropEffects.Copy);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("用户鼠标拖拽事件不规范;" + ex.ToString());
            }
            
        }

        /// <summary>
        /// 事件五：鼠标选中事件;
        /// 用这个事件作为鼠标拖拽事件的起点更为合适;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerDataGrid_AddEvent_GotMouseCapture(object sender, MouseEventArgs e)
        {
            try
            {
                if ((e.OriginalSource as DataGrid).Items.CurrentItem is DataGridWithEvent)
                {
                    Console.WriteLine("GotMouseCapture;捕获鼠标除了移动之外的任何事件，传过来的参数数据类型为:" + e.Source.GetType());
                    DataGrid sender_item = e.OriginalSource as DataGrid;
                    foreach (var iter in (e.OriginalSource as DataGrid).SelectedCells)
                    {
                        if (e.LeftButton == MouseButtonState.Pressed)
                        {
                            DragDropEffects myDropEffect = DragDrop.DoDragDrop(sender_item, iter.Item as DataGridWithEvent, DragDropEffects.Copy);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// 事件六：鼠标左键按下的事件;
        /// 这个鼠标事件只针对没有填充数据的单元格的表格区域;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerDataGrid_AddEvent_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("MouseLeftButtonDown;函数参数sender反馈的数据类型是:" + sender.GetType());
            Console.WriteLine("MouseLeftButtonDown;函数参数e.Source反馈的数据类型是:" + e.Source.GetType());
            Console.WriteLine("MouseLeftButtonDown;函数参数e.OriginalSource反馈的数据类型是:" + e.OriginalSource.GetType());
        }

        /// <summary>
        /// 事件七：鼠标进入整个DataGrid表格后就会触发;
        /// 只触发一次,直到下一次鼠标再次进入表格的时候才会再次触发;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerDataGrid_AddEvent_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Console.WriteLine("MouseEnter;函数参数sender反馈的数据类型是:" + sender.GetType());
            Console.WriteLine("MouseEnter;函数参数e.Source反馈的数据类型是:" + e.Source.GetType());
            Console.WriteLine("MouseEnter;函数参数e.OriginalSource反馈的数据类型是:" + e.OriginalSource.GetType());
        }

        /// <summary>
        /// 接收鼠标拖拽事件;
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
        private void InitDynamicClassToDataGrid()
        {
            // ____________________________________________以下是通过简单的生成
            // 动态添加内容,i表示有多少行,目前的处理就是添加i行相同的内容;
            for (int i = 0; i <= 5; i++)
            {
                dynamic model = new DyDataDridModel();

                // 向单元格内动态添加属性及其;
                // 参数1：动态类型的类型名;
                // 参数2：动态类型的实例;
                // 参数3：列名称;
                model.AddProperty("property2", new GridCell() { name = "显示内容1" }, "列2");
                model.AddProperty("property0", new GridCell() { name = "显示内容2" }, "列0");
                model.AddProperty("property1", new GridCell() { name = "显示内容3" }, "列1");

                list.Add(model);
            }
            // 此处将所有的列信息添加到DataGrid当中;
            for (int i = 0; i <= 2; i++)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = "列" + i;
                column.Binding = new Binding("property" + i + ".name");
                this.MsgDataGrid_AutoGenCol.Columns.Add(column);
            }

            this.MsgDataGrid_AutoGenCol.ItemsSource = list;
            this.MsgDataGrid_AutoGenCol.BeginningEdit += MsgDataGrid_AutoGenCol_BeginningEdit;

            

            // ____________________________________________以下是丰富了单元格内容的DataGrid(在单元格内添加下拉框);
            // 后续将该模式改造成工厂模式;
            List<string> name_list = new List<string>();
            name_list.Add("Combox_Content1");
            name_list.Add("Combox_Content2");
          
            // 在表格中添加3行内容相同的内容;
            for (int i = 0; i <= 2; i++)
            {

                dynamic model2 = new DyDataDridModel();                                                     // 创建一个动态模型;
                model2.AddProperty("p1", name_list, "列1");
                model2.AddProperty("p2", DateTime.Now, "列2");
                model2.AddProperty("p3", "显示内容", "列3");
                
                list2.Add(model2);

                if(i == 2)
                {
                    // 为表格添加列信息;
                    for (int j = 0; j <= 2; j++)
                    {
                        if (j == 0)
                        {
                            DataGridTemplateColumn column = new DataGridTemplateColumn();                   // 单元格是一个template;
                            DataTemplate template = new DataTemplate();                                     // 用一个DataTemplate类型填充;
                            ComboBox box = new ComboBox();

                            // 实在没有什么好办法，直接在CS代码当中直接写DataTemplate的XAML代码;
                            string xaml1 =
                                @"<DataTemplate xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                                                xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                                                xmlns:model='clr -namespace:WPF.Model'>
                                    <ComboBox ItemsSource='{Binding p"+(j + 1) +@"}' SelectedIndex='0'/>
                                 </DataTemplate>";

                            template = XamlReader.Parse(xaml1) as DataTemplate;
                            
                            column.Header = "列" + j;                               // 填写列名称;
                            column.CellTemplate = template;                         // 将单元格的显示形式赋值;
                            column.Width = 100;                                     // 设置显示宽度;

                            this.MsgDataGrid_AutoGenColandCellMore.Columns.Add(column);
                        }
                        else if (j == 1)
                        {
                            DataGridTextColumn DGText = new DataGridTextColumn();
                            DGText.Header = "列" + j;
                            DGText.Binding = new Binding("p" + (j + 1).ToString());
                            this.MsgDataGrid_AutoGenColandCellMore.Columns.Add(DGText);
                        }
                        else if (j == 2)
                        {
                            DataGridTextColumn DGText = new DataGridTextColumn();
                            DGText.Header = "列" + j;
                            DGText.Binding = new Binding("p" + (j + 1).ToString());
                            this.MsgDataGrid_AutoGenColandCellMore.Columns.Add(DGText);
                        }

                    }
                }

            }
            

            this.MsgDataGrid_AutoGenColandCellMore.DataContext = list2;
        }


        private void MsgDataGrid_AutoGenCol_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            Console.WriteLine("开始编辑单元格;函数参数e反馈的实体可以是单元格内数据类型" + (e.Column.GetCellContent(e.Row)).DataContext.GetType());
            Console.WriteLine("当前选中单元格列名:" + e.Column.Header + ",选中行数：" + e.Row.GetIndex());
            dynamic temp = e.Column.GetCellContent(e.Row).DataContext as DyDataDridModel;
            temp.JudgePropertyName_StartEditing(e.Column.Header);
        }

        /// <summary>
        /// DataGrid的模拟数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> list_detail = new List<string>();
            list_detail.Add("This is Message Detail Content");
            list_detail.Add("This is anoher Detail Conntent");
            Task a = new Task(() =>
            {
                int temp = 0;
                while (true)
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

            listbase.Add(new DataGridCustomer()
            {
                FirstName = "Add",
                LastName = "Person",
                Email = new Uri("Http://baidu.com"),
                IsMember = false,
                cell = new GridCell() { name = "cell" }
            });
        }
        #endregion

        #region 有关Treeview以及解析XML
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
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TryEnumerable(object sender, RoutedEventArgs e)
        {
            List<Iterator_Try> list = new List<Iterator_Try>();
        }

        /// <summary>
        /// 一个进度条，使用线程池进行后台处理;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartBackgroundProgress(object sender, RoutedEventArgs e)
        {
            this.psb.Maximum = 100;
            this.psb.Minimum = 0;

            tp.bgworker.DoWork += Bgworker_DoWork;                             // 注册处理函数，但不会执行;
            tp.bgworker.WorkerReportsProgress = true;                          // 线程是否可以在中途汇报进度;
            tp.bgworker.WorkerSupportsCancellation = true;                     // 线程是否可以被随时终止(调用CancelAsync终止线程);
            tp.bgworker.ProgressChanged += Bgworker_ProgressChanged;           // 当线程中调用了ReportProgress函数时，会触发该事件;
            tp.bgworker.RunWorkerCompleted += Bgworker_RunWorkerCompleted;     // 当线程函数返回时，会触发该事件;
            tp.bgworker.RunWorkerAsync(100);                                   // 启动线程，并向线程传入参数;
        }

        /// <summary>
        /// BackgroundWorker后台处理的入口，通过调用RunWorkerAsync进入;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">RunWorkerAsync函数可以带上参数;</param>
        private void Bgworker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int i = (int)e.Argument;
            Console.WriteLine("接收到的数值;" + i);

            for(int iter = 0; iter < i; iter++)
            {
                iter++;
                Thread.Sleep(200);
                (sender as BackgroundWorker).ReportProgress(iter);

                if((sender as BackgroundWorker).CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
            }

        }

        /// <summary>
        /// BackgroundWorker后台处理发生变化的时候，调用该函数;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bgworker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.psb.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// BackgroundWorker取消事件;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopBackgroundProgress(object sender, RoutedEventArgs e)
        {
            tp.bgworker.CancelAsync();
        }

        /// <summary>
        /// 当BackgroundWorker后台处理完成后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bgworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Progress Complete!");
        }

        private void StartEquatable_1(object sender, RoutedEventArgs e)
        {
            person_Non_Equatable per1 = new person_Non_Equatable("G1", 19);
            person_Non_Equatable per2 = new person_Non_Equatable("G1", 19);

            Console.WriteLine(per1.GetHashCode());
            Console.WriteLine(per2.GetHashCode());

            bool ret = per1.Equals(per2);
            if(ret)
            {
                Console.WriteLine("在不使用IEquatable的情况下，比较相等");
            }
            else
            {
                Console.WriteLine("在不使用IEquatable的情况下，比较不相等");
            }
        }

        private void StartEquatable_2(object sender, RoutedEventArgs e)
        {
            person_Non_Equatable_OverrideHashCode per1 = new person_Non_Equatable_OverrideHashCode("G2", 19);
            person_Non_Equatable_OverrideHashCode per2 = new person_Non_Equatable_OverrideHashCode("G2", 19);

            Console.WriteLine(per1.GetHashCode());
            Console.WriteLine(per2.GetHashCode());

            bool ret = per1.Equals(per2);      // 看来Equals方法不是通过GetHashCode判断的;
            if (ret)
            {
                Console.WriteLine("在不使用IEquatable的情况下，比较相等");
            }
            else
            {
                Console.WriteLine("在不使用IEquatable的情况下，比较不相等");
            }
        }

        private void StartEquatable_3(object sender, RoutedEventArgs e)
        {
            person_Equatable per1 = new person_Equatable("G1", 19);
            person_Equatable per2 = new person_Equatable("G1", 19);

            Console.WriteLine(per1.GetHashCode());
            Console.WriteLine(per2.GetHashCode());

            bool ret = per1.Equals(per2);
            if (ret)
            {
                Console.WriteLine("在不使用IEquatable的情况下，比较相等");
            }
            else
            {
                Console.WriteLine("在不使用IEquatable的情况下，比较不相等");
            }
        }


        private void StartEquatable_4(object sender, RoutedEventArgs e)
        {
            int?ret = ParticalCompare.Compare(Comparer<int>.Default, 20, 20);
            Console.WriteLine("用ICompare作为函数入参;" + ret);

            SomeComparableClass a = new SomeComparableClass();
            SomeComparableClass b = new SomeComparableClass();
            a.m_value = 30;
            b.m_value = 20;
            double? ret2 = ParticalCompare.Compare(Comparer<SomeComparableClass>.Default, a, b);
            Console.WriteLine("用ICompare作为函数入参;" + ret2);
        }

        private void StartForeach_1(object sender, RoutedEventArgs e)
        {
            Person2 p1 = new Person2("John", "Smith");
            Person2 p2 = new Person2("John2", "Smith2");
            Person2 p3 = new Person2("John3", "Smith3");

            People list = new People();
            list.Add(p1);
            list.Add(p2);
            list.Add(p3);
            
            foreach(Person2 itor in list)
            {
                Console.WriteLine("foreach:" + itor.firstName);
            }
        }

        private void RptTime(object sender, ReportTimeEvtArgs e)
        {
            Console.WriteLine("路由内容;" + (sender as FrameworkElement).Name + "点击时间;"+ e.ClickTime.ToString());
        }

        private void TimeTick(object sender, RoutedEventArgs e)
        {
            
            if(this.TimeTicked == TimeTicked.NeverTicked)
            {
                this.Time_Span_Button.Content = "停止计时";
                ttp.time_one = DateTime.Now;
                this.TimeTicked = TimeTicked.PressedStart;
            }
            else if(this.TimeTicked == TimeTicked.PressedStart)
            {
                this.Time_Span_Button.Content = "开始计时";
                ttp.time_two = DateTime.Now;
                this.TimeTicked = TimeTicked.NeverTicked;
                this.time_span_second.Content = ttp.TimeDelaySecond.ToString();
            }
        }

    }
}
