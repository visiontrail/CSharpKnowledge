using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DataGridBinding
{
    /// <summary>
    /// 一个数据模型;
    /// </summary>
    public class Person : INotifyPropertyChanged
    {
        private int mAge = 0;
        private Sex mSex = Sex.Male;
        
        public int Age
        {
            get { return mAge; }
            set 
            {
                if ( !value.Equals ( mAge ) )
                {
                    mAge = value;
                    OnPropertyChanged ( "Age" );
                }
            }
        }
        public Sex Sex
        {
            get { return mSex; }
            set
            {
                if ( value != mSex )
                {
                    mSex = value;
                    OnPropertyChanged ( "Sex" );
                }
            }
        }
        
        public Person ( )
        {
        }

        public Person ( int age, Sex sex )
        {
            mAge = age;
            mSex = sex;
        }
        
        protected virtual void OnPropertyChanged ( string propertyName )
        {
            if ( PropertyChanged != null )
            {
                PropertyChanged ( this, new PropertyChangedEventArgs ( propertyName ) );
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
    /// <summary>
    /// Sex.
    /// </summary>
    public enum Sex
    {
        Male,
        Female
    }
}
