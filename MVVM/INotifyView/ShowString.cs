using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;

namespace INotifyView
{
    public class ShowString : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        public string m_Name
        {
            get
            {
                return _name;
            }
            // 当m_Name发生变化的时候，PropertyChanged就会被激发,以通知UI层;
            set
            {
                _name = value;
                RaisePropertyChanged("m_Name");
            }
        }

        private string _brithday;
        public string m_Birthday
        {
            get
            {
                return _brithday;
            }
            set
            {
                _brithday = value;
                RaisePropertyChanged("m_Birthday");
            }
        }
       
        /// <summary>
        /// 通知给UI层，哪个属性发生了变化;
        /// 当然，这个函数也可以独立出来，让所有想要称为VM层的类型继承，这样就不用重复写多次了;
        /// 这种绑定的方式有个缺点，就是它通过字符串索引，这样一来，首先不能包含重复的名称了，再有如果写错，也就绑定不上了;
        /// </summary>
        /// <param name="propertyName">改变了数值的属性</param>
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 构造函数，初始化属性;
        /// </summary>
        public ShowString()
        {
            this.m_Name = "1111";
        }
    }
}
