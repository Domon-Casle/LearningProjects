using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelForEach
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime tempDateTime = DateTime.Now;
            Console.WriteLine("Started {0}", DateTime.Now.ToString());
            QueueItems queueItems = new QueueItems();

            while (queueItems.processItems.Count() > 0)
            {
                Parallel.For(0, queueItems.processItems.Count(),
                    index =>
                    {
                        QueueItem toDoItems;
                        queueItems.processItems.TryDequeue(out toDoItems);
                        if (toDoItems != null)
                        {
                            if (toDoItems.Level == 0 || QueueItems.IsLevelDone(toDoItems.Level - 1))
                            {
                                ProcessItem(toDoItems);
                            }
                            else
                            {
                                queueItems.processItems.Enqueue(toDoItems);
                            }
                        }
                    });
            }

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
