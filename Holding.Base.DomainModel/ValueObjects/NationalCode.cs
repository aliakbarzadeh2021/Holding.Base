using System;
using System.Globalization;
using Holding.Base.DomainModel.SeedWorks.Validators;
using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.DomainModel.ValueObjects
{
    /// <summary>
    /// کلاس کدملی
    /// </summary>
    public class NationalCode : ValueObjectBase<NationalCode>
    {
        public static readonly NationalCode Empty = default(NationalCode);
        public NationalCode(string code)
        {
            Code = code;
            Validate();
        }

        /// <summary>
        /// کدملی
        /// </summary>
        public string Code { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}", Code);
        }

        protected override sealed void Validate()
        {
            if (!Code.ToString(CultureInfo.InvariantCulture).IsValidNationalCode())
            {
                throw new ArgumentNullException();
            }
        }

    }
}
