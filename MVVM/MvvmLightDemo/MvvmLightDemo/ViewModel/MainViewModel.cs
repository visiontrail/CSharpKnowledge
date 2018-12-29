using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MvvmLightDemo.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand Button_Click_ChangeText { get; set; }
        public RelayCommand NavToNextPage { get; set; }

        /// <summary>
        /// 显示字符;
        /// </summary>
        private string m_LabelShow;
        public string Label1Show
        {
            get { return m_LabelShow; }

            // MvvmLight实现的Set方法,好处就是不用自己实现RaisePropertyChanged函数了;
            set { Set(ref m_LabelShow, value); }
        }

        /// <summary>
        /// 主标签页;
        /// </summary>
        private Page m_MFrame;
        public Page MFrame
        {
            get { return m_MFrame; }
            set { Set(ref m_MFrame, value); }
        }

        /// <summary>
        /// 附标签页;
        /// </summary>
        private Page m_AttachFrame;
        public Page AttachFrame
        {
            get { return m_AttachFrame; }
            set { Set(ref m_AttachFrame, value); }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Button_Click_ChangeText = new RelayCommand(ChangeText);
            NavToNextPage = new RelayCommand(NavNextPage);

            Label1Show = "ShowOne";
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            MFrame = new MainPage();
            AttachFrame = new NextPage();
        }

        private void NavNextPage()
        {
            base.Cleanup();
        }
        
        void ChangeText()
        {
            Label1Show = "ShowOneChange";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (Count > 65534)
                Count = 0;

            Count++;
            Label1Show = "ShowTimerCount:" + Count.ToString();
        }

        
        private DispatcherTimer timer = new DispatcherTimer();
        private int Count = 0;

    }
}