using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DataGridBinding
{
    /// <summary>
    /// Person
    /// </summary>
    public class Person : INotifyPropertyChanged
    {
        #region ... Variables ...
        /// <summary>
        /// Age.
        /// </summary>
        private int mAge = 0;
        /// <summary>
        /// Sex.
        /// </summary>
        private Sex mSex = Sex.Male;
        #endregion ... Variables ...

        #region ... Properties ...
        /// <summary>
        /// Gets or sets Age.
        /// </summary>
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
        /// <summary>
        /// Gets or sets Sex.
        /// </summary>
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
        #endregion ... Properties ...

        #region ... Constructor ...
        /// <summary>
        /// Constructor.
        /// </summary>
        public Person ( )
        { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="age"></param>
        /// <param name="sex"></param>
        public Person ( int age, Sex sex )
        {
            mAge = age;
            mSex = sex;
        }
        #endregion ... Constructor ...

        #region ... Methods ...
        /// <summary>
        /// OnPropertyChanged.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged ( string propertyName )
        {
            if ( PropertyChanged != null )
            {
                PropertyChanged ( this, new PropertyChangedEventArgs ( propertyName ) );
            }
        }
        #endregion ... Methods ...

        #region ... INotifyPropertyChanged Members ...
        /// <summary>
        /// PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion ... INotifyPropertyChanged Members ...
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
