
namespace Holding.Base.Bus.Configuration
{
    public interface IMsmqSettings
    {     
        IMsmqSettings PrepareQueue( IMessageQueue queue );        
    }
}
