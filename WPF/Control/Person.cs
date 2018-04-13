using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF   
{
    public class Person
    {
        public int m_ID
        {
            get;set;
        }

        public string m_Name
        {
            get;set;
        }

        public Person(int id, string name)
        {
            this.m_ID = id;
            this.m_Name = name;
        }
    }
}
