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

namespace WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑;
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Person> persons = new List<Person>();

        // DataGrid的显示层;
        public DataGridVM m_DgVm
        {
            get;set;
        }

        public MainWindow()
        {
            InitializeComponent();
            //DrawPoin();
            DrawPolyLine();
            InitPersons();
            InitStus();
            TryAddDataGridColumn();

            //_________________________以下是使用Binding关联XAML和C#代码的实验_____
            
            // 这个是ItemControl类的控件使用binding关联数据源的方法;
            this.PersonList.ItemsSource = persons;
            this.PersonList.DisplayMemberPath = "m_Name";
            this.PersonList.SelectionChanged += PersonList_SelectionChanged;
            
            // 通过SetBinding方法也可以Binding现有类(ListBox)的属性;
            this.PersonID.SetBinding(TextBox.TextProperty, new Binding("m_ID") {
                Source = this.PersonList.ItemsSource,
                BindsDirectlyToSource = true
            });

            ClassA.m_sA = "静态属性";

            // 以下是通过ObjectDataProvider进行命令的binding
            ObjectDataProvider odp = new ObjectDataProvider();
            odp.ObjectInstance = new Calc();
            odp.MethodName = "Add";
            odp.MethodParameters.Add("0");
            odp.MethodParameters.Add("0");

            // 方法的第一个参数;
            this.CalcX.SetBinding(TextBox.TextProperty, new Binding("MethodParameters[0]") {
                Source = odp,
                BindsDirectlyToSource = true,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            // 方法的第二个参数;
            this.CalcY.SetBinding(TextBox.TextProperty, new Binding("MethodParameters[1]")
            {
                Source = odp,
                BindsDirectlyToSource = true,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            this.CalcRes.SetBinding(TextBox.TextProperty, new Binding(".")
            {
                Source = odp
            });

        }

        // 添加列;
        private void TryAddDataGridColumn()
        {
            List<string> collist = new List<string>();
            
            
            collist.Add("c1");
            collist.Add("c2");

            m_DgVm = new DataGridVM(collist);
            m_DgVm.ColumnListChanged += M_DgVm_ColumnListChanged;
            m_DgVm.m_ColumnNameList.ListChanged += M_ColumnNameList_ListChanged;
            MessageDataGrid.DataContext = m_DgVm;
            foreach (string iter in m_DgVm.m_ColumnNameList.list)
            {
                DataGridTextColumn temp = new DataGridTextColumn();
                temp.Header = iter;
                MessageDataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = iter
                });
            }
        }

        private void M_ColumnNameList_ListChanged(object sender, EventArgs e)
        {
            MessageDataGrid.Columns.Clear();
            foreach (string iter in m_DgVm.m_ColumnNameList.list)
            {
                DataGridTextColumn temp = new DataGridTextColumn();
                temp.Header = iter;
                MessageDataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = iter
                });
            }
        }

        private void M_DgVm_ColumnListChanged(object sender, EventArgs e)
        {
            foreach (string iter in m_DgVm.m_ColumnNameList.list)
            {
                MessageDataGrid.Columns.Clear();
                DataGridTextColumn temp = new DataGridTextColumn();
                temp.Header = iter;
                MessageDataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = iter
                });
            }
        }

        private void PersonList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.PersonID.Text = (this.PersonList.SelectedItem as Person).m_ID.ToString();
        }

        private void InitStus()
        {
            // 使用LINQ查询;
            this.StuList.ItemsSource = from stu in this.stus.m_StuList where stu.m_Name.StartsWith("G") select stu;
        }

        private void InitPersons()
        {
            persons.Add(new Person(11, "Guoliang"));
            persons.Add(new Person(2, "Roupao"));
            persons.Add(new Person(5, "WangCY"));
        }

        public void DrawPoin()
        {
            Canvas myParentCanvas;
            Canvas myCanvas1;
            Canvas myCanvas2;
            Canvas myCanvas3;

            myParentCanvas = new Canvas();
            myParentCanvas.Width = 400;
            myParentCanvas.Height = 400;

            // Define child Canvas elements
            myCanvas1 = new Canvas();
            myCanvas1.Background = Brushes.Red;
            myCanvas1.Height = 100;
            myCanvas1.Width = 100;
            Canvas.SetTop(myCanvas1, 0);
            Canvas.SetLeft(myCanvas1, 0);

            myCanvas2 = new Canvas();
            myCanvas2.Background = Brushes.Green;
            myCanvas2.Height = 100;
            myCanvas2.Width = 100;
            Canvas.SetTop(myCanvas2, 100);
            Canvas.SetLeft(myCanvas2, 100);

            myCanvas3 = new Canvas();
            myCanvas3.Background = Brushes.Blue;
            myCanvas3.Height = 50;
            myCanvas3.Width = 50;
            Canvas.SetTop(myCanvas3, 50);
            Canvas.SetLeft(myCanvas3, 50);

            // Add child elements to the Canvas' Children collection
            myParentCanvas.Children.Add(myCanvas1);
            myParentCanvas.Children.Add(myCanvas2);
            myParentCanvas.Children.Add(myCanvas3);

            // Add the parent Canvas as the Content of the Window Object
            this.Content = myParentCanvas;
        }

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

        private void ChangeCol_Click(object sender, RoutedEventArgs e)
        {
            this.m_DgVm.m_ColumnNameList.Add("C3");
            Console.WriteLine("123");
        }
    }
}
