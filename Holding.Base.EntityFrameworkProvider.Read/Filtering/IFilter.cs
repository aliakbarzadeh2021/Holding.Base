using System.Collections.Generic;

namespace Holding.Base.EntityFrameworkProvider.Read.Filtering
{
    public interface IFilter
    {
        List<KeyValuePair<string, object>> Fields { get; set; }

        string OrderByOn { get; set; }

        string OrderType { get; set; }

        int PageSize { get; set; }

        int PageIndex { get; set; }

        int Condition { get; set; }
    }
}