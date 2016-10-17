using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnDepthChapterOne
{
    class Classtry1
    {
        string name;
        public string Name
        {
            get { return name; }
        }

        decimal price;
        public decimal Price
        {
            get { return price; }
        }

        public Classtry1(string name, decimal price)
        {
            this.name = name;
            this.price = price;
        }

        public static ArrayList GetSample()
        {
            ArrayList list = new ArrayList();
            list.Add(new Classtry1("P1", 100));
            list.Add(new Classtry1("P2", 200));
            return list;
        }

        public override string ToString()
        {
            return string.Format("{0};{1}", name, price);
        }

        public static void print()
        {
            Console.WriteLine("123");
        }

    }
}
