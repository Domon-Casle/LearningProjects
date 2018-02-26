using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelForEach
{
    public class QueueItems
    {
        public ConcurrentQueue<QueueItem> processItems = new ConcurrentQueue<QueueItem>();
        private static int[] LevelInUse = { 0, 0, 0, 0, 0 };
        private static object LevelLock = new object();

        public QueueItems()
        {
            QueueItem toDoItem;

            for (int i = 0; i < 100; i++)
            {
                toDoItem = new QueueItem(i);
                AddToLevel(toDoItem.Level);
                processItems.Enqueue(toDoItem);
            }
        }

        public static void AddToLevel(int level)
        {
            lock (LevelLock)
            {
                LevelInUse[level]++;
            }
        }

        public static void SubFromLevel(int level)
        {
            lock (LevelLock)
            {
                LevelInUse[level]--;
            }
        }

        public static bool IsLevelDone(int level)
        {
            lock (LevelLock)
            {
                if (LevelInUse[level] == 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
