
namespace Holding.Base.Bus.Configuration
{
    public class MessageQueue : IMessageQueue
    {
        private readonly string _name;
        private readonly string _address;
        public MessageQueue(string name, string address)
        {
            _name = name;
            _address = address;
        }

        public string Name
        {
            get { return _name; }
        }    
        public string Address
        {
            get { return _address; }
        }        
    }
}
