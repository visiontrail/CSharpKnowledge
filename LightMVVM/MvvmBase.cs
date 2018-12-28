using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LightMVVM
{
    /// <summary>
    /// 写一个一个轻量级MVVM框架，以帮助理解MVVM到底是什么;
    /// INotifyPropertyChanged接口实现了当属性发生变化的时候，可以及时发出通知
    /// </summary>
    public class MvvmBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 属性发生改变时调用该方法发出通知;
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void SetAndNotifyIfChanged<T>(string propertyName, ref T oldValue, T newValue)
        {
            if (oldValue == null && newValue == null) return;
            if (oldValue != null && oldValue.Equals(newValue)) return;
            if (newValue != null && newValue.Equals(oldValue)) return;
            oldValue = newValue;
            RaisePropertyChanged(propertyName);
        }
        
    }
}
