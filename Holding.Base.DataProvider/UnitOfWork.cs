
namespace Holding.Base.DataProvider
{
    public class UnitOfWork
    {
        public static IUnitOfWork CreateNew()
        {
            return new DatabaseContext();
        }

        public static void Initialize()
        {
            using ( var context = new DatabaseContext() )
            {
                context.Database.Initialize( force: true );
            }
        }
    }
}
