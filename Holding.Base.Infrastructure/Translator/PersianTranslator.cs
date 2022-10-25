using System.Resources;
using Holding.Base.Infrastructure.Enums;

namespace Holding.Base.Infrastructure.Translator
{
    public class PersianTranslator : ITranslator
    {
        private readonly ResourceManager _resourceManager;
        public PersianTranslator()
        {
            _resourceManager = new ResourceManager( GetType() );
        }

        public string Translate( ResourceKey key )
        {
            return Translate( key.ToString() );
        }

        public string Translate( string key )
        {
            return _resourceManager.GetString( key );
        }
    }
}
