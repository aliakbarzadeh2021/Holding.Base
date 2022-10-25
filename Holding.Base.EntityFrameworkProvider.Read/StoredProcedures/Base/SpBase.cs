using System.Linq;
using System.Reflection;
using Holding.Base.EntityFrameworkProvider.Read.Attributes;

namespace Holding.Base.EntityFrameworkProvider.Read.StoredProcedures.Base
{
    public abstract class SpBase<T>
    {
        private static string _parameters;
        private static string _storedProcedureName;
        public static string Parameters
        {
            get
            {
                if (string.IsNullOrEmpty(_parameters))
                    _parameters = CreateParameters();
                return _parameters;
            }
        }

        public static string StoredProcedureName
        {
            get
            {
                if (string.IsNullOrEmpty(_storedProcedureName))
                    _storedProcedureName = GetStoredProcedureName();
                return _storedProcedureName;
            }
        }

        public static string StoredProcedureWithParams
        {
            get { return string.Format("{0} {1}", StoredProcedureName, Parameters); }
        }

        private static string GetStoredProcedureName()
        {
            var attr = typeof(T).CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(StoredProcedureAttribute));
            return attr.ConstructorArguments[0].Value.ToString();
        }

        private static string CreateParameters()
        {
            return
                typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(f => f.CustomAttributes.Any(x => x.AttributeType == typeof(SpParameterAttribute)))
                    .Select(fieldInfo => fieldInfo.CustomAttributes.FirstOrDefault().NamedArguments[0].TypedValue.Value.ToString())
                    .Aggregate(string.Empty, (current, parameterName) => current + string.Format("{0}",
                         (current == string.Empty ? string.Empty : ",") + "@" + parameterName));
        }
    }
}
