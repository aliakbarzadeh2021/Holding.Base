using System;

namespace Holding.Base.EntityFrameworkProvider.Write.Exceptions
{
    public class DataProviderAssembliesNotFoundException:Exception
    {
        public override string Message
        {
            get { return "ابتدا اسمبلی مدل های خود را به دیتا پروایدر اضافه کنید"; }
        }
    }

    public class StoredProcedureAssembliesNotFoundException : Exception
    {
        public override string Message
        {
            get { return "ابتدا اسمبلی رویه های خود را به پروایدر اضافه کنید"; }
        }
    }
}
