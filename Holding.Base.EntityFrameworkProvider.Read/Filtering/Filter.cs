using System.Collections.Generic;

namespace Holding.Base.EntityFrameworkProvider.Read.Filtering
{
    public class Filter : IFilter
    {
        public Filter()
        {
            Fields = new List<KeyValuePair<string, object>>();
        }

        public List<KeyValuePair<string, object>> Fields { get; set; }

        public string OrderByOn { get; set; }

        public string OrderType { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int Condition { get; set; }

        internal ConditionType FielsConditionType
        {
            get
            {
                return (ConditionType)Condition;
            }
        }

        internal OrderByType GetOrderType()
        {
            var type = OrderType.ToLower();
            switch (type)
            {
                case "asc":
                case "1":
                    return OrderByType.Asc;
                case "desc":
                case "2":
                    return OrderByType.Desc;
                default:
                    return OrderByType.None;
            }
        }
    }
}