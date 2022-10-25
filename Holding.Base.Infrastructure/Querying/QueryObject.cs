using System.Collections.Generic;

namespace Holding.Base.Infrastructure.Querying
{
    public class QueryObject
    {
        private IList<QueryObject> _subQueries = new List<QueryObject>();
        private IList<Criterion> _criteria = new List<Criterion>();

        public IEnumerable<Criterion> Criteria
        {
            get { return _criteria; }
        }

        public IEnumerable<QueryObject> SubQueries
        {
            get { return _subQueries; }
        }

        public void AddSubQuery(QueryObject subQuery)
        {
            _subQueries.Add(subQuery);
        }

        public void Add(Criterion criterion)
        {
            _criteria.Add(criterion);
        }

        public QueryOperator QueryOperator { get; set; }

        public OrderByClause OrderByProperty { get; set; }
    }

}
