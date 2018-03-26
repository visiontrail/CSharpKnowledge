using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnDepthChapterTwo
{
    public class Lambda
    {
        public string Name { get; set; }
        public Lambda(string name)
        {
            Name = name;
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
