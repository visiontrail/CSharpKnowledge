using System;
using System.Collections.Generic;
using System.Data;
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
using System.Threading;
using WPF.UserControlTemplate;
using WPF.Model;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑;
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Person> persons = new List<Person>();
        public DataGridVM m_DgVm;                              // DataGrid的VM层，所有针对DataGrid的操作，全部使用这个类型;
        public MessageVM m_MessageVM;                          // MessageVM层，用来显示MessageModel的;

        public MainWindow()
        {
            InitializeComponent();
            DrawPolyLine();                 // WPF画线;
            InitPersons();                  // 初始化Person列表;
            InitaListToDataGridColumn();    // 小实验，将用户自定义的List添加到DataGrid的List当中;

            m_MessageVM = new MessageVM();
            InitMessageToDataGrid();        // 小实验，将数据通过另一个线程填入到DataGrid当中;
            InitMessageToDataGrid2();
            
            InitTreeViewComposite();
            
            // WPF中的ItemsSource是可以使用LINQ进行查询的;
            this.StuList.ItemsSource = from stu in this.stus.m_StuList where stu.m_Name.StartsWith("G") select stu;

            //_____________________________________________________________以下是使用Binding关联XAML和C#属性的实验
            // 这个是ItemsControl类的控件使用binding关联数据源的方法;
            this.PersonList.ItemsSource = persons;                                    // 指定ItemsSource;
            this.PersonList.DisplayMemberPath = "m_Name";                             // 指定显示的属性;
            this.PersonList.SelectionChanged += PersonList_SelectionChanged;          // 当控件选择发生变化时;
            
            // 通过SetBinding方法也可以Binding现有类(ListBox)的属性;
            this.PersonID.SetBinding(TextBox.TextProperty, new Binding("m_ID") {
                Source = this.PersonList.ItemsSource,
                BindsDirectlyToSource = true
            });

            //_____________________________________________________________以下是使用Binding关联XAML和C#命令的实验
            // 以下是通过ObjectDataProvider进行命令的binding
            ObjectDataProvider odp = new ObjectDataProvider();
            odp.ObjectInstance = new Calc();
            odp.MethodName = "Add";
            odp.MethodParameters.Add("0");
            odp.MethodParameters.Add("0");

            // 方法的第一个参数;
            this.CalcX.SetBinding(TextBox.TextProperty, new Binding("MethodParameters[0]") {
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

        private void InitMessageToDataGrid2()
        {
            this.MsgDataGrid2.m_context = m_MessageVM.messagelist;
        }

        private void InitMessageToDataGrid()
        {
            this.MsgDataGrid.DataContext = m_MessageVM.messagelist;
        }

        private void InitaListToDataGridColumn()
        {
            List<string> colums = new List<string>();
            colums.Add("column1");
            colums.Add("column2");
            colums.Add("column3");

            m_DgVm = new DataGridVM(colums);
            m_DgVm.m_ColumnNameList.ListChanged += M_ColumnNameList_ListChanged;

            foreach (string iter in m_DgVm.m_ColumnNameList.m_list)
            {
                MessageDataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = iter
                });
            }
        }
        
        /// <summary>
        /// DataGridVM层列表发生变化时,将变化添加到View中;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_ColumnNameList_ListChanged(object sender, EventArgs e)
        {
            MessageDataGrid.Columns.Clear();

            foreach (string iter in m_DgVm.m_ColumnNameList.m_list)
            {
                MessageDataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = iter,
                    Width = iter.Length * 10
                });
            }
        }

        /// <summary>
        /// 实验一下添加一列;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeCol_Click(object sender, RoutedEventArgs e)
        {
            this.m_DgVm.m_ColumnNameList.Add("CCCCCCCCCCC3");
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
    
        private void GetDepProperty(object sender, RoutedEventArgs e)
        {
            SpecialTextBox st = new SpecialTextBox();
            st.SetValue(SpecialTextBox.DTextProperty, this.text1.Text);              // 为这个依赖属性设置对象;
            MessageBox.Show(st.GetValue(SpecialTextBox.DTextProperty) as string);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
                            m_dest = "172.27.0.2"
                        });
                    });
                    Thread.Sleep(1233);
                }
            });

            a.Start();
        }
    }
}
