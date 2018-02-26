using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPoolingExample
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
