using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
