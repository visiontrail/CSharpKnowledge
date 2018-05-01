using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF.Model
{
    /// <summary>
    /// 一个模拟网络消息的数据模型;
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// 可触发的命令:显示消息内容;
        /// </summary>
        private ICommand mShowCommand;
        public ICommand ShowCommand
        {
            get
            {
                if (mShowCommand == null)
                {
                    mShowCommand = new RelayCommand(() =>
                    {
                        ExecuteAction();
                    },
                    () => CanExecuteFunc());
                }
                return mShowCommand;
            }
        }
        private bool CanExecuteFunc()
        {
            return true;
        }
        private void ExecuteAction()
        {
            Console.WriteLine("Content is" + this.m_content);
        }

        /// <summary>
        /// 消息编号;
        /// </summary>
        private string No;
        public string m_No
        {
            get { return No; }
            set { No = value; }
        }

        /// <summary>
        /// 时间戳;
        /// </summary>
        private DateTime time;
        public DateTime m_time
        {
            get { return time; }
            set { time = value; }
        }

        /// <summary>
        /// 消息内容;
        /// </summary>
        private string content;
        public string m_content
        {
            get { return content; }
            set { content = value; }
        }

        /// <summary>
        /// 消息的源IP地址;
        /// </summary>
        private string source;
        public string m_source
        {
            get { return source; }
            set { source = value; }
        }

        /// <summary>
        /// 消息的目的IP地址;
        /// </summary>
        private string dest;
        public string m_dest
        {
            get { return dest; }
            set { dest = value; }
        }
    }

    public class RelayCommand : ICommand
    {
        private Action mExecuteAction;              // 执行命令;
        private Func<bool> mCanExecuteFunc;         // 命令是否可以执行;

        public RelayCommand(Action executeAction, Func<bool> canExecuteFunc)
        {
            mExecuteAction = executeAction;
            mCanExecuteFunc = canExecuteFunc;
        }
      
        public bool CanExecute(object parameter)
        {
            return mCanExecuteFunc.Invoke();
        }

        public void Execute(object parameter)
        {
            mExecuteAction.Invoke();
        }

        public event EventHandler CanExecuteChanged;
        
    }
}
