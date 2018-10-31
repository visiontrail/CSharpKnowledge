using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF   
{
    public class Person : INotifyPropertyChanged
    {
        private int id;
        public int m_ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                RaisePropertyChanged("m_ID");
            }
        }

        public string m_Name
        {
            get;set;
        }

        public Person()
        {

        }

        public Person(int id, string name)
        {
            this.m_ID = id;
            this.m_Name = name;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Student : Person, INotifyPropertyChanged
    {

        private List<Person> StuList;
        public List<Person> m_StuList
        {
            get
            {
                return StuList;
            }
            set
            {
                StuList = value;
                RaisePropertyChanged("m_StuList");
            }
        }

        public Student()
        {
            StuList = new List<Person>();
            StuList.Add(new Person(1, "G1"));
            StuList.Add(new Person(1, "F1"));
            StuList.Add(new Person(1, "A1"));
            StuList.Add(new Person(1, "G2"));
            StuList.Add(new Person(1, "R1"));
        }

        public Student(int id, string name)
            : base(id,name)
        {

        }

        public Student(int id, string name, string major)
            :base(id,name)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class ClassA : INotifyPropertyChanged
    {
        public string m_A
        {
            get;set;
        }

        public string m_B
        {
            get;set;
        }

        public static string m_sA
        {
            get;set;
        }

        private int iA;
        public int m_iA
        {
            get { return iA; }
            set
            {
                iA = value;

                if(this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("m_iA"));
                }
            }
        }

        public ClassA()
        {
            m_A = "在此可binding这个类实例的一个属性";
            m_B = "这是ClassA一个实例的另一个属性";
            m_sA = "这是ClassA的静态属性";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <summary>
    /// 可以用于ClassA类型对象的Binding过程的数值校验;
    /// </summary>
    class ClassACheckValidation : ValidationRule
    {
        /// <summary>
        /// 在Binding关联数据的过程中，如果设置了ValidationRule的话，会自动调用该函数;
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                int checkval = int.Parse(value.ToString());
                if(checkval < 100 && checkval > 0)
                {
                    return new ValidationResult(true, "");
                }
                else
                {
                    return new ValidationResult(false, "数值超范围");
                }

            }
            catch
            {
                return new ValidationResult(false, "输入数值格式不正确");
            }
        }
    }
}
