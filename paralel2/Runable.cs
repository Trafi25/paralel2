using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace paralel2
{
    
    class Runable
    {
        private Thread[] threads;
        private int adder = 5;
        private Random random = new Random();
        private MishelAndScoth<int> Queuen;

        public void Start(MishelAndScoth<int> alg, int numThreads)
        {
            Queuen = alg;
            threads = new Thread[numThreads];
            for (var i = 0; i < numThreads; i++)
            {
                threads[i] = new Thread(Filling);
                threads[i].Name = $"stream {i}";
            }
          
        }

         public TimeSpan Runthreds()
        {
            var watcher = new Stopwatch();
            watcher.Start();
            foreach (Thread thread in threads)
            {
                thread.Start();
                Console.WriteLine($"Start: {thread.Name}");
            }           

            watcher.Stop();
            return watcher.Elapsed;
        }

        private void Filling()
        {           
                for (var i = 0; i < adder; i++)
                {
                    var randItem = random.Next(100);
                    Queuen.Push(randItem);
                    Console.WriteLine($"{Thread.CurrentThread.Name} add {randItem} into queue");
                }           
        }
       
    }
}
