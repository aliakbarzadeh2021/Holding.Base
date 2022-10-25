
namespace Holding.Base.Infrastructure.Messaging
{
    public class AutoReply : IAutoReply
    {
        private readonly bool _isSuccessful;
        public AutoReply( bool isSuccessful )
        {
            _isSuccessful = isSuccessful;
        }

        public bool IsSuccessful
        {
            get { return _isSuccessful; }
        }
    }
}
