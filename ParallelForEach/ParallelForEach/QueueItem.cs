using System;

namespace ParallelForEach
{
    public class QueueItem
    {

        public int QueueId { get; set; }
        public int Level { get; set; }
        private static Random random = new Random();

        public QueueItem(int id)
        {
            this.QueueId = id;
            Level = random.Next(0, 5);
        }
    }
}
