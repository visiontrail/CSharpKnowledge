using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF.Model
{
    public class MessageModel
    {
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
            Console.WriteLine("123");
            Console.WriteLine("Content is" + this.m_content);
        }

        private string No;
        public string m_No
        {
            get { return No; }
            set { No = value; }
        }

        private DateTime time;
        public DateTime m_time
        {
            get { return time; }
            set { time = value; }
        }

        private string content;
        public string m_content
        {
            get { return content; }
            set { content = value; }
        }

        private string source;
        public string m_source
        {
            get { return source; }
            set { source = value; }
        }

        private string dest;
        public string m_dest
        {
            get { return dest; }
            set { dest = value; }
        }
    }

    public class RelayCommand : ICommand
    {
        private Action mExecuteAction;

        private Func<bool> mCanExecuteFunc;

        public RelayCommand(Action executeAction, Func<bool> canExecuteFunc)
        {
            mExecuteAction = executeAction;
            mCanExecuteFunc = canExecuteFunc;
        }
      
        public bool CanExecute(object parameter)
        {
            return mCanExecuteFunc.Invoke();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            mExecuteAction.Invoke();
        }
    }
}
