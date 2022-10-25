using Holding.Base.Infrastructure.Enums;

namespace Holding.Base.Infrastructure.Translator
{
    public interface ITranslator
    {
        string Translate(ResourceKey key);
    }
}
