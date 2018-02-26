using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime tempDateTime = DateTime.Now;
            Console.WriteLine("Started {0}", DateTime.Now.ToString());
            QueueItems queueItems = new QueueItems();
            QueueItem toDoItem;

            ThreadPool.SetMinThreads(1, 0);
            ThreadPool.SetMaxThreads(10, 0);
            while(queueItems.processItems.TryDequeue(out toDoItem))
            {
                if ((toDoItem.Level == 0) || (QueueItems.IsLevelDone(toDoItem.Level -1)))
                {
                    ThreadPool.QueueUserWorkItem(
                        new WaitCallback(ProcessItem),
                        toDoItem);
                }
                else
                {
                    queueItems.processItems.Enqueue(toDoItem);
                }
            }

            int avaiableThreads = 0;
            int comThreads = 0;
            do
            {
                Thread.Sleep(1000);
                ThreadPool.GetAvailableThreads(out avaiableThreads, out comThreads);
            }
            while (avaiableThreads != 10);
            

            Console.WriteLine("Done {0}", DateTime.Now.ToString());
            Console.WriteLine("Total Time: {0}", DateTime.Now.Subtract(tempDateTime));
            Console.Read();
        }

        private static void ProcessItem(object state)
        {
            QueueItem queueItem = (QueueItem)state;
            Console.WriteLine("Process {0} has done item {1} Level {2}",
                Thread.CurrentThread.GetHashCode(),
                queueItem.QueueId, 
                queueItem.Level);

            Random random = new Random();
            Thread.Sleep(random.Next(100, 2000));

            QueueItems.SubFromLevel(queueItem.Level);
        }
    }
}
