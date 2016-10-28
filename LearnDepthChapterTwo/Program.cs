using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnDepthChapterTwo
{
    // 声明了一个委托;
    delegate void StringProcessor(string str);

    class Program
    {
        static void Main(string[] args)
        {
            // 创建委托类型的实例;
            StringProcessor printstring;
            // 创建委托实例，使用静态方法;
            printstring = new StringProcessor(Program.PrintString);
            printstring("Delegate~printstring");

            // 创建委托类型的实例;这个对象被称为操作的目标
            StringProcessor printstringinstance;
            // 创建委托实例，使用实例方法;
            ClassChapter2Instance C2 = new ClassChapter2Instance();
            printstringinstance = new StringProcessor(C2.PrintStringInstance);
            printstringinstance("Delegate~printstringinstance");

            Console.ReadLine();

            return;

        }

        // 一个符合委托声明的函数实现;
        static void PrintString(string str)
        {
            Console.WriteLine("PrintString:" + str);
        }
    }

    class ClassChapter2Instance
    {
        public void PrintStringInstance(string str)
        {
            Console.WriteLine("PrintStringInstance:" + str);
        }
    }
}
