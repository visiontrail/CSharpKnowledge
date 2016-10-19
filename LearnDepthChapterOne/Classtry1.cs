using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnDepthChapterOne
{
    class ClassSortCSharpVer1 : IComparer
    {
        public int Compare(object x ,object y)
        {
            ClassCSharpVersion1 instance1 = (ClassCSharpVersion1)x;
            ClassCSharpVersion1 instance2 = (ClassCSharpVersion1)y;
            return instance1.Name.CompareTo(instance2.Name);
        }
    }
    class ClassSort : IComparer<ClassCSharpVersion4>
    {
        public int Compare(ClassCSharpVersion4 x,ClassCSharpVersion4 y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    // C#1版本的编程风格
    class ClassCSharpVersion1
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

        public ClassCSharpVersion1(string name, decimal price)
        {
            this.name = name;
            this.price = price;
        }

        // 该程序有问题，问题在于List中可以是任何类型
        public static ArrayList GetSample()
        {
            ArrayList list = new ArrayList();
            list.Add(new ClassCSharpVersion1("C#1-P1", 100m));
            list.Add(new ClassCSharpVersion1("C#1-P2", 200m));
            list.Add(new ClassCSharpVersion1("C#1-P5", 600m));
            list.Add(new ClassCSharpVersion1("C#1-P4", 300m));
            list.Add(new ClassCSharpVersion1("C#1-P3", 500m));
            // 当误将list添加了一个非Class类型的时候
            //list.Add("123123");
            return list;
        }

        public override string ToString()
        {
            return string.Format("C#Version1--产品名称：{0};价格：{1}", name, price);
        }

        public static void print()
        {
            Console.WriteLine("123");
        }

    }

    class ClassCSharpVersion2
    {
        string name;
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        decimal price;
        public decimal Price
        {
            get { return price; }
            private set { price = value; }
        }

        public ClassCSharpVersion2(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        // 该程序使用了泛型，在List中，只能使用ClassCSharpVersoin2类型进行填充
        public static List<ClassCSharpVersion2> GetSample()
        {
            List<ClassCSharpVersion2> list = new List<ClassCSharpVersion2>();
            list.Add(new ClassCSharpVersion2("P1", 100));
            list.Add(new ClassCSharpVersion2("P2", 200));
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

    class ClassCSharpVersion3
    {
        public string Name
        {
            get;
            private set;
        }

        public decimal Price
        {
            get;
            private set;
        }

        public ClassCSharpVersion3()
        {
        }

        // 该程序使用了泛型，在List中，只能使用ClassCSharpVersoin2类型进行填充
        public static List<ClassCSharpVersion3> GetSample()
        {
            return new List<ClassCSharpVersion3>
            {
                new ClassCSharpVersion3 { Name = "P1", Price = 10m },
                new ClassCSharpVersion3 { Name = "P2", Price = 20m }
            };
        }

        public override string ToString()
        {
            return string.Format("{0};{1}", Name, Price);
        }

        public static void print()
        {
            Console.WriteLine("123");
        }

    }

    class ClassCSharpVersion4
    {
        // 对于C#4来说，在涉及属性和构造函数的时候，允许我们在调用构造函数的时候，指定实参的名称
        // 它为我们提供了在C#3中一样清晰的初始化，而又移除了易变性
        readonly string name;
        public string Name
        {
            get { return name; }
        }

        readonly decimal price;
        public decimal Price
        {
            get { return price; }
        }

        public ClassCSharpVersion4(string name, decimal price)
        {
            this.name = name;
            this.price = price;
        }

        public static List<ClassCSharpVersion4> GetSample()
        {
            return new List<ClassCSharpVersion4>
            {
                new ClassCSharpVersion4(name:"P1",price:30m),
                new ClassCSharpVersion4(name:"P6",price:10m),
                new ClassCSharpVersion4(name:"P4",price:60m),
                new ClassCSharpVersion4(name:"P5",price:50m),
                new ClassCSharpVersion4(name:"P3",price:40m),
                new ClassCSharpVersion4(name:"P2",price:20m)
            };
        }

        public override string ToString()
        {
            return string.Format(" C#Version4--产品名称：{0};产品价格：{1}", Name, Price);
        }

        public static void print()
        {
            Console.WriteLine("123");
        }

    }

}
