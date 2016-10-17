using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnDepthChapterOne
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal da1 = 34287786090351m;
            int i = 10;
            decimal abc = da1 + i;

            Classtry2 P1 = new Classtry2("P3",100);
            Classtry2 P2;

            Classtry1.print();

            ArrayList list = Classtry1.GetSample();

            string A = list[1].ToString();

            Console.WriteLine(A);

            Console.Read();
            return;
        }
    }

    class Classtry2
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

        public Classtry2(string name, decimal price)
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
