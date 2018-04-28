using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;
using System.Windows;

namespace DataGridBinding
{
    /// <summary>
    /// People.
    /// </summary>
    public class People : INotifyPropertyChanged, ICustomTypeDescriptor
    {
        #region ... Variables ...
        /// <summary>
        /// Name.
        /// </summary>
        private string mName = string.Empty;
        /// <summary>
        /// Person.
        /// </summary>
        private Person mPerson;
        /// <summary>
        /// 
        /// </summary>
        private ICommand mShowCommand;
        #endregion ... Variables ...

        #region ... Properties ...
        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name
        {
            get { return mName; }
            set
            {
                if ( !value.Equals ( mName ) )
                {
                    mName = value;
                    OnPropertyChanged ( "Name" );
                }
            }
        }
        /// <summary>
        /// Gets or sets Person.
        /// </summary>
        public Person Person
        {
            get { return mPerson; }
            set 
            {
                mPerson = value;
                OnPropertyChanged ( "Person" );
            }
        }
        /// <summary>
        /// Gets ShowCommand.
        /// </summary>
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
        #endregion ... Properties ...

        #region ... Constructor ...
        /// <summary>
        /// Constructor.
        /// </summary>
        public People ( )
        { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public People ( string name )
        {
            mName = name;
            mPerson = new Person ( );
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


        private void ExecuteAction()
        {
            string message = string.Empty;
            if (mPerson != null)
            {
                message = string.Format("Name {0}, Age: {1}, Sex {2}.",
                    mName, mPerson.Age, mPerson.Sex);
            }
            MessageBox.Show(message);
        }

        private bool CanExecuteFunc()
        {
            return true;
        }
        #endregion ... Methods ...

        #region ... INotifyPropertyChanged Members ...
        /// <summary>
        /// PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion ... INotifyPropertyChanged Members ...

        #region ... ICustomTypeDescriptor Members ...
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AttributeCollection GetAttributes ( )
        {
            return TypeDescriptor.GetAttributes ( this, true );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetClassName ( )
        {
            return TypeDescriptor.GetClassName ( this, true );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetComponentName ( )
        {
            return TypeDescriptor.GetComponentName ( this, true );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TypeConverter GetConverter ( )
        {
            return TypeDescriptor.GetConverter ( this, true );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EventDescriptor GetDefaultEvent ( )
        {
            return TypeDescriptor.GetDefaultEvent ( this, true );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PropertyDescriptor GetDefaultProperty ( )
        {
            return TypeDescriptor.GetDefaultProperty ( this, true );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="editorBaseType"></param>
        /// <returns></returns>
        public object GetEditor ( Type editorBaseType )
        {
            return TypeDescriptor.GetEditor ( this, editorBaseType, true );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public EventDescriptorCollection GetEvents ( Attribute[] attributes )
        {
            return TypeDescriptor.GetEvents ( this, attributes, true ); 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EventDescriptorCollection GetEvents ( )
        {
            return GetEvents ( null );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public PropertyDescriptorCollection GetProperties ( Attribute[] attributes )
        {
            PropertyDescriptorCollection propertyCollection1 = TypeDescriptor.GetProperties ( mPerson, attributes );
            return propertyCollection1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PropertyDescriptorCollection GetProperties ( )
        {
            return GetProperties ( null );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pd"></param>
        /// <returns></returns>
        public object GetPropertyOwner ( PropertyDescriptor pd )
        {
            return mPerson;
        }

        #endregion ... ICustomTypeDescriptor Members ...
    }
    /// <summary>
    /// People Collection.
    /// </summary>
    public class PeopleCollection : ObservableCollection<People>
    { }

    public class RelayCommand : ICommand
    {
        private Action mExecuteAction;

        private Func<bool> mCanExecuteFunc;

        public RelayCommand(Action executeAction, Func<bool> canExecuteFunc)
        {
            mExecuteAction = executeAction;
            mCanExecuteFunc = canExecuteFunc;
        }

        #region ... ICommand 成员 ...

        public bool CanExecute(object parameter)
        {
            return mCanExecuteFunc.Invoke();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            mExecuteAction.Invoke();
        }

        #endregion ... ICommand 成员 ...
    }
}
