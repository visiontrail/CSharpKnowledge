using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    public class Lambda
    {
        public string Name { get; set; }
        public Lambda(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return this.Name;
        }
        public static Lambda operator +(Lambda a, Lambda b)
        {
            Lambda ret = new Lambda("");
            ret.Name = a.Name + b.Name;
            return ret;
        }
    }

    public class InstanceLambda
    {
        public List<Lambda> list { get; set; }

        public InstanceLambda()
        {
            list = new List<Lambda>
            {
                new Lambda("1"),
                new Lambda("2"),
                new Lambda("3")
            };
        }

        void Func_Action()
        {
            list.ForEach(x => Console.WriteLine("Delegate Action" + x.Name));
        }
    }
}
