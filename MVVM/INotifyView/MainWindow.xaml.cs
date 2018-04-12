using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace INotifyView
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ShowString PageMain = new ShowString();
        
        public MainWindow()
        {
            InitializeComponent();
            // 通过这行语句绑定前端与后台的关系;
            DataContext = PageMain;
            // 还可以通过以下单独绑定的方式进行前后台的关联;
            this.Birthday.SetBinding(TextBox.TextProperty, new Binding("m_Birthday") { Source = PageMain });

            Task a = new Task(() =>
            {
                while(true)
                {
                    Thread.Sleep(1000);
                    PageMain.m_Birthday = DateTime.Now.ToString();
                }
            });

            a.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.text.Text = "Modify the Text";
            PageMain.m_Name = "222";

            // 尝试更换另一个绑定对象;
            ShowString dt2 = new ShowString();
            this.Birthday.SetBinding(TextBox.TextProperty, new Binding("m_Birthday") { Source = dt2 });
            dt2.m_Birthday = DateTime.UtcNow.ToString();
        }
    }

    public class Person
    {
        public string m_Name;
        public DateTime m_Birthday;

        public Person()
        {

        }
    }
}
