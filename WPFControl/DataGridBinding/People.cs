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
    /// People的数据模型;
    /// </summary>
    public class People : INotifyPropertyChanged, ICustomTypeDescriptor
    {
        private string mName = string.Empty;
        private Person mPerson;
        private ICommand mShowCommand;                // 一个事件，这个事件用于显示细节;如何关联的呢？
        
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
        public Person Person
        {
            get { return mPerson; }
            set 
            {
                mPerson = value;
                OnPropertyChanged ( "Person" );
            }
        }

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
        
        public People ( )
        { }
        public People ( string name )
        {
            mName = name;
            mPerson = new Person ( );
        }
        
        protected virtual void OnPropertyChanged ( string propertyName )
        {
            if ( PropertyChanged != null )
            {
                PropertyChanged ( this, new PropertyChangedEventArgs ( propertyName ) );
            }
        }
        
        // 
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

        public event PropertyChangedEventHandler PropertyChanged;

        public AttributeCollection GetAttributes ( )
        {
            return TypeDescriptor.GetAttributes ( this, true );
        }

        public string GetClassName ( )
        {
            return TypeDescriptor.GetClassName ( this, true );
        }

        public string GetComponentName ( )
        {
            return TypeDescriptor.GetComponentName ( this, true );
        }

        public TypeConverter GetConverter ( )
        {
            return TypeDescriptor.GetConverter ( this, true );
        }

        public EventDescriptor GetDefaultEvent ( )
        {
            return TypeDescriptor.GetDefaultEvent ( this, true );
        }

        public PropertyDescriptor GetDefaultProperty ( )
        {
            return TypeDescriptor.GetDefaultProperty ( this, true );
        }

        public object GetEditor ( Type editorBaseType )
        {
            return TypeDescriptor.GetEditor ( this, editorBaseType, true );
        }

        public EventDescriptorCollection GetEvents ( Attribute[] attributes )
        {
            return TypeDescriptor.GetEvents ( this, attributes, true ); 
        }

        public EventDescriptorCollection GetEvents ( )
        {
            return GetEvents ( null );
        }

        public PropertyDescriptorCollection GetProperties ( Attribute[] attributes )
        {
            PropertyDescriptorCollection propertyCollection1 = TypeDescriptor.GetProperties ( mPerson, attributes );
            return propertyCollection1;
        }

        public PropertyDescriptorCollection GetProperties ( )
        {
            return GetProperties ( null );
        }

        public object GetPropertyOwner ( PropertyDescriptor pd )
        {
            return mPerson;
        }
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
