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

namespace INotifyView
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //public ShowString PageMain = new ShowString();
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = PageMain;       // 通过这行语句绑定前端与后台的关系;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.text.Text = "Modify the Text";
            PageMain.m_Name = "222";
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
