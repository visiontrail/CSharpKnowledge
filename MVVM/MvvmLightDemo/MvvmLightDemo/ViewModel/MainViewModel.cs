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
        public RelayCommand NavToNextPage { get; set; }

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
            NavToNextPage = new RelayCommand(NavNextPage);   // 导航到下一个Page页;
            
            MPage = new MainPage();
            AttachPage = new PageTwo();
        }

        private void NavNextPage()
        {
            
        }
        
    }
}