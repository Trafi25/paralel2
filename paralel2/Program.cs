using System;
using System.Threading;
using System.Threading.Tasks;

namespace paralel2
{
    class Program
    {
        static void Main(string[] args)
        {

            var test = new MishelAndScoth<int>();
            Runable run = new Runable();
            run.Start(test, 5);
            run.Runthreds();
            PrintQueueForm(test);
            int help;
            test.Delete(out help);
            Console.WriteLine($"{help} was removed");
            PrintQueueForm(test);
        }

        private static void PrintQueueForm<T>(MishelAndScoth<T> test) 
        {
            Console.WriteLine("________________OUTPUT_______________");                 
            var elem = test.Head.Next;
            for(int i=0; elem != null; i++ )         
            {
                Console.WriteLine($"Elem number {i} has {elem.Data}");
                elem = elem.Next;               
            }
            Console.WriteLine("________________OUTPUT_______________");
        }
    }
}
