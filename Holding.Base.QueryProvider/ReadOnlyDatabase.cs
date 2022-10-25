
namespace Holding.Base.QueryProvider
{
    public class ReadOnlyDatabase
    {
        public static IQuery AsReadOnly()
        {
            return new ReadOnlyDatabaseContext();
        }
    }
}
