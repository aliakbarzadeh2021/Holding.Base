using System.Collections.Generic;
using System.Data;

namespace Holding.Base.Bus.Configuration
{
    public class MsmqSettings : IMsmqSettings
    {
        private static readonly Dictionary<string, string> Queues;
        static MsmqSettings()
        {
            Queues = new Dictionary<string, string>();
        }

        public static Dictionary<string, string> AllQueues
        {
            get { return Queues; }
        }

        public IMsmqSettings PrepareQueue(IMessageQueue queue)
        {
            if (Queues.ContainsKey(queue.Name))
                throw new DuplicateNameException("Queue name is duplicated");

            Queues.Add(queue.Name, queue.Address);
            return this;
        }

        public static IMsmqSettings Configure()
        {
            return new MsmqSettings();
        }
    }
}

