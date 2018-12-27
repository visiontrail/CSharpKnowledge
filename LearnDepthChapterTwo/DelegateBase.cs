using Delegate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearnDepthChapterTwo
{
    public class DelegateBase
    {
        public static void AllBaseUsage()
        {
            // ================== 以下是委托的简单用法 =========================
            // 创建委托类型的实例;
            // 将委托实例就相当于创建了一个属性，这个属性可以为起“赋值”，即订阅对应的方法，且可以订阅多个方法;
            StringProcessor printstring = PrintString2;
            StringProcessor2 printstring2;
            // 创建委托实例，使用静态方法;
            printstring = new StringProcessor(PrintString2);
            printstring2 = new StringProcessor2(PrintString);
            printstring += PrintString;


            printstring("Delegate~printstring");

            printstring2.BeginInvoke("print111111111111", new AsyncCallback((IAsyncResult ar) =>
            {
                Thread.Sleep(3231);
                Console.WriteLine("111111" + ar.AsyncState.ToString());
            }), "");

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
                Console.WriteLine("handled anonymously:" + obj.ToString());
            };
            handler3("handler3", EventArgs.Empty);

            // 用匿名方法指定操作的简写形式;
            EventHandler handler4;
            handler4 = delegate
            {
                Console.WriteLine("handled anonymously sample");
            };
            handler4("handler4", EventArgs.Empty);

            // ================== C#2使用匿名方法进行委托操作 End ============

            //                                                   表达式树(MSDN例子);
            // ---------------------------------------------------------------------
            // 声明一个表达式树,这个树形有两个节点;
            BlockExpression blockExpr = Expression.Block(
                Expression.Call(
                    null,
                    typeof(Console).GetMethod("Write", new Type[] { typeof(String) }),
                    Expression.Constant("Hello ")
                   ),
                Expression.Call(
                    null,
                    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(String) }),
                    Expression.Constant("World!")
                    ),
                Expression.Constant(42)
            );

            Console.WriteLine("The result of executing the expression tree:");
            // The following statement first creates an expression tree,
            // then compiles it, and then executes it.           
            var result = Expression.Lambda<Func<int>>(blockExpr).Compile()();

            // Print out the expressions from the block expression.
            Console.WriteLine("The expressions from the block expression:");

            foreach (var expr in blockExpr.Expressions)
                Console.WriteLine(expr.ToString());

            // Print out the result of the tree execution.
            Console.WriteLine("The return value of the block expression:");
            Console.WriteLine(result);
          
        }

        // 一个符合委托声明的函数实现;
        static void PrintString(string str)
        {
            Thread.Sleep(3111);
            Console.WriteLine("顺序执行静态方法PrintString第一次:" + str);
        }

        static void PrintString2(string str)
        {
            Console.WriteLine("顺序执行静态方法PrintString第二次:" + str);
        }
    }
}
