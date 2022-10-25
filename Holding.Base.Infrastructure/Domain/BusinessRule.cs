namespace Holding.Base.Infrastructure.Domain
{
    public class BusinessRule
    {
        private string _property;
        private string _rule;

        public BusinessRule(string property, string rule)
        {
            this._property = property;
            this._rule = rule;
        }

        public string Property
        {
            get { return _property; }
            set { _property = value; }
        }

        public string Rule
        {
            get { return _rule; }
            set { _rule = value; }
        }
    }

    ///// <summary>
    ///// Data annotation validation attribute bussines rule
    ///// </summary>
    //public class ValidationAttributeBusinessRule : BusinessRule
    //{
    //    public ValidationAttribute Attribute { get; private set; }

    //    /// <summary>
    //    /// Constructor
    //    /// </summary>
    //    /// <param name="property">property name</param>
    //    /// <param name="rule">rule</param>
    //    /// <param name="attribute"></param>
    //    public ValidationAttributeBusinessRule(string property, string rule, ValidationAttribute attribute)
    //        : base(property, rule)
    //    {
    //        Attribute = attribute;
    //    }
    //}
}

