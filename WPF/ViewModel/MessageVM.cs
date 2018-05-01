using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Model;

namespace WPF.ViewModel
{
    /// <summary>
    /// MessageModel对应的VM显示层
    /// </summary>
    public class MessageVM
    {
        public List<MessageModel> messagelist = new List<MessageModel>();

        public MessageVM()
        {

        }
    }
}
