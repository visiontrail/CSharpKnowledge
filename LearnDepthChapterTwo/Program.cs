using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnDepthChapterTwo
{
    // 声明了一个委托;
    // 这个委托表示一个string类型的入参，并且返回值为void的函数委托;
    delegate void StringProcessor(string str);
    delegate void StringProcessor2(string str);

    class Program
    {
        static void Main(string[] args)
        {
            // 创建委托类型的实例;
            // 将委托实例就相当于创建了一个属性，这个属性可以为起“赋值”，即订阅对应的方法，且可以订阅多个方法;
            StringProcessor printstring;
            StringProcessor2 printstring2;
            // 创建委托实例，使用静态方法;
            printstring = new StringProcessor(Program.PrintString);
            printstring2 = new StringProcessor2(Program.PrintString);
            printstring2 += Program.PrintString2;
            printstring("Delegate~printstring");
            printstring2("Delegate~printstring2");

            // 创建委托类型的实例;这个对象被称为操作的目标
            StringProcessor printstringinstance;
            // 创建委托实例，使用实例方法;
            ClassChapter2Instance C2 = new ClassChapter2Instance();
            printstringinstance = new StringProcessor(C2.PrintStringInstance);
            printstringinstance += C2.PrintStringInstance2;
            printstringinstance("Delegate~printstringinstance");

            // 静态类型和动态类型;
            object o = "length";
            // 此处编译不会通过，以为String类型才有Length属性
            //int OLength = o.Length;
            Console.WriteLine(((string)o).Length);

            Console.ReadLine();

            return;

        }

        // 一个符合委托声明的函数实现;
        static void PrintString(string str)
        {
            Console.WriteLine("顺序执行静态方法PrintString第一次:" + str);
        }

        static void PrintString2(string str)
        {
            Console.WriteLine("顺序执行静态方法PrintString第二次:" + str);
        }
    }

    class ClassChapter2Instance
    {
        public void PrintStringInstance(string str)
        {
            Console.WriteLine("执行实例化方法PrintStringInstance第一次:" + str);
        }

        public void PrintStringInstance2(string str)
        {
            Console.WriteLine("执行实例化方法PrintStringInstance第二次:" + str);
        }
    }
}
