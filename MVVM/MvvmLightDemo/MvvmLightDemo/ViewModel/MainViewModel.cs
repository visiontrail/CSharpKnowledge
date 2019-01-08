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
        /// <summary>
        /// 导航到下一页面按钮的依赖命令;
        /// </summary>
        public RelayCommand NavToNextPage { get; set; }

        /// <summary>
        /// 导航到上一页面按钮的依赖命令;
        /// </summary>
        public RelayCommand NavToPrePage { get; set; }

        /// <summary>
        /// 主页Frame;
        /// </summary>
        private Frame m_MainWindowFrame;
        public Frame MainWindowFrame
        {
            get { return m_MainWindowFrame; }
            set { Set(ref m_MainWindowFrame, value); }
        }
        
        /// <summary>
        /// 主标签页;
        /// </summary>
        private Page m_MPage;
        public Page MPage
        {
            get { return m_MPage; }
            set { Set(ref m_MPage, value); }
        }

        /// <summary>
        /// 附标签页;
        /// </summary>
        private Page m_AttachPage;
        public Page AttachPage
        {
            get { return m_AttachPage; }
            set { Set(ref m_AttachPage, value); }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            NavToNextPage = new RelayCommand(NavNextPage);          // 导航到下一个Page页;
            NavToPrePage = new RelayCommand(NavPrePage);    // 返回到上一个Page页;

            MainWindowFrame = new Frame();
            MPage = new MainPage();
            AttachPage = new PageTwo();

            // 将Frame自带的导航条隐藏;
            MainWindowFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            MainWindowFrame.Content = MPage;
        }
        
        /// <summary>
        /// 处理点击下一页;
        /// </summary>
        private void NavNextPage()
        {
            MainWindowFrame.Content = AttachPage;
        }

        /// <summary>
        /// 处理点击上一页;
        /// </summary>
        private void NavPrePage()
        {
            MainWindowFrame.Content = MPage;
        }

    }
}