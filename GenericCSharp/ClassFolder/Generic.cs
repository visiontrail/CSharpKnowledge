using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryAnyCSharp
{
    public class ITemp : IComparable
    {
        public double TempereatureC = 0d;

        public int CompareTo(object obj)
        {
            return 0;
        }
    }

    public class Itemp2<T> where T : IComparable<T>
    {

    }

    public class BaseClass
    {
        protected string _className = "BaseClass";

        // BaseClass的其中一个构造函数
        public BaseClass(int i)
        {

        }
        // BaseClass的另外一个构造函数
        public BaseClass(string s)
        {

        }

        public virtual void PrintName()
        {
            Console.WriteLine(_className);
        }
    }

    class DerivedClass : BaseClass
    {
        public string _className = "DerivedClass";
        string s,s2 = null;

        public DerivedClass(string s,string s2) : base(s)
        {
            this.s = s;
            this.s2 = s2;
        }

        public override void PrintName()
        {
            Console.Write("The BaseClass Name is:");
            //调用基类方法;
            base.PrintName();
            Console.WriteLine("This DerivedClass is:" +_className+ "s1 and s2 is {0},{1}", s,s2);
        }
    }
}
