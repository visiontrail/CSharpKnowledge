using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

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

        private string m_LabelShow;
        public string Label1Show
        {
            get { return m_LabelShow; }

            // MvvmLight实现的Set方法,好处就是不用自己实现RaisePropertyChanged函数了;
            set { Set(ref m_LabelShow, value); }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Button_Click_ChangeText = new RelayCommand(ChangeText);
            Label1Show = "ShowOne";
        }


        void ChangeText()
        {
            Console.WriteLine("1111111111");
            Label1Show = "ShowOneChange";
        }
        
    }
}