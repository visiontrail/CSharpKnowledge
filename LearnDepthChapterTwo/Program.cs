using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
            // ================== 以下是委托的简单用法 =========================
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
            // ================== 委托的简单用法 End =========================

            // ================== C#2使用匿名方法进行委托操作 ================

            // 指定委托类型和方法;
            EventHandler eventhandler;
            eventhandler = new EventHandler(ChapterTwoClass.HandleDemoEvent);
            eventhandler("Hello", EventArgs.Empty);

            // 隐式转换成委托实例;
            EventHandler handler2;
            handler2 = ChapterTwoClass.HandleDemoEvent;
            handler2("hello2", EventArgs.Empty);

            // 用一个匿名方法来指定操作;
            EventHandler handler3;
            handler3 = delegate (object obj, EventArgs e)
            {
                Console.WriteLine("handled anonymously:" +obj.ToString());
            };
            handler3("handler3", EventArgs.Empty);

            // 用匿名方法指定操作的简写形式;
            EventHandler handler4;
            handler4 = delegate
            {
                Console.WriteLine("handled anonymously sample");
            };
            handler4("handler4",EventArgs.Empty);

            // ================== C#2使用匿名方法进行委托操作 End ============


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
