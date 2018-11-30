using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ch19_P6_AddWithThreads
{
    class Program
    {
        private static AutoResetEvent waitHandle = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            Console.WriteLine("***** Adding with Thread objects *****");
            Console.WriteLine("ID of thread in Main(): {0}",  
                                Thread.CurrentThread.ManagedThreadId);
            // Make an AddParams object to pass to the secondary thread.
            AddParamsClass ap = new AddParamsClass(10, 10);
            Thread thread = new Thread(new ParameterizedThreadStart(Add));
            // This is now a background Thread
            thread.IsBackground = true;
            thread.Start(ap);
            // Force a wait to let other thread finish.
            //Thread.Sleep(5);
            Console.WriteLine(" waiting for the other thread to give green signal to move on ");
            waitHandle.WaitOne();
            Console.WriteLine("Done tun");
            Console.ReadLine();
        }
        static void Add(object data)
        {
            if (data is AddParamsClass)
            {
                Console.WriteLine("ID of thread in Add(): {0}",
                Thread.CurrentThread.ManagedThreadId);
                AddParamsClass ap = (AddParamsClass)data;
                Thread.Sleep(2000);
                Console.WriteLine("{0} + {1} is {2}",
                ap.a, ap.b, ap.a + ap.b);
            }
            // Tell the other thread we are done tun.
            waitHandle.Set();
        }
        static void AddNumbers(int a , int b)
        {
            Console.WriteLine(" ID of thread in Add(): {0}",
                            Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(" Adding two numbers!\n ");
            Thread.Sleep(1000); Console.Write(" . ");
            Thread.Sleep(1000); Console.Write(" . ");
            Thread.Sleep(1000); Console.Write(" . ");
            int ans = a + b;

        }
    }
}
