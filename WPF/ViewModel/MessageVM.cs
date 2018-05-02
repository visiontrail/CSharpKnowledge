using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;


namespace WPF.ViewModel
{
    /// <summary>
    /// MessageModel对应的VM显示层
    /// </summary>
    public class MessageVM
    {
        // 存放所有消息内容的地方;
        private volatile ObservableCollection<MessageModel> m_messagelist;
        public ObservableCollection<MessageModel> messagelist
        {
            get
            {
                return m_messagelist;
            }
            set
            {
                m_messagelist = value;
            }
        }

        public MessageVM()
        {
            messagelist = new ObservableCollection<MessageModel>();
        }
    }
    
}
