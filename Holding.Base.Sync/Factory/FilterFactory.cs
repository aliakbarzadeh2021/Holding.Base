using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Holding.Base.Sync.Filters;
using MongoDB.Bson;

namespace Holding.Base.Sync.Factory
{
    public static class FilterFactory
    {
        public static BsonDocument Build(IList<Filter> filters)
        {
            var result = new BsonDocument();

            foreach (var filter in filters)
                result.Add(filter.CreateFilterElement());

            return result;
        }
    }
}
