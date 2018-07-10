using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryAnyCSharp
{
    public class Person
    {
        public string Name;
        public DateTime Birthday;
        public DateTime? DeathDay;
        public TimeSpan Age
        {
            get
            {
                if (!DeathDay.HasValue)
                    return DateTime.Now - Birthday;
                else
                    return DeathDay.Value - Birthday;
            }
        }
        

        public Person(string name, DateTime birthday)
        {
            this.Name = name;
            this.Birthday = birthday;
        }
    }

    public class Student : Person
    {
        public Student(string name, DateTime birthday) : base(name,birthday)
        {

        }

        string number;
    }
    
}
