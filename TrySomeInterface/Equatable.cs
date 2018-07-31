using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrySomeInterface
{
    public class Try_Equatable<T> : IEquatable<Try_Equatable<T>>
    {
        private readonly IEqualityComparer<Try_Equatable<T>> comparer1 = EqualityComparer<Try_Equatable<T>>.Default;


        public bool Equals(Try_Equatable<T> other)
        {
            throw new NotImplementedException();
        }
    }

    public class person_Non_Equatable
    {
        public string Name;
        public int Age;

        public person_Non_Equatable(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }
    }

    public class person_Non_Equatable_OverrideHashCode
    {
        public string Name;
        public int Age;

        public person_Non_Equatable_OverrideHashCode(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("看看String相同的Hashcode："+this.Name.GetHashCode());
            int ret = this.Name.GetHashCode() * 10 + this.Age.GetHashCode();
            return ret;
        }
    }

    public class person_Equatable : IEquatable<person_Equatable>
    {
        public string Name;
        public int Age;

        public person_Equatable(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public bool Equals(person_Equatable other)
        {
            return this.Name == other.Name && this.Age == other.Age;
        }
    }
}
