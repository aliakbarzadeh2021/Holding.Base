using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Holding.Base.Sync.Filters
{
    public abstract class Filter
    {
        public abstract BsonElement CreateFilterElement();
    }
}
