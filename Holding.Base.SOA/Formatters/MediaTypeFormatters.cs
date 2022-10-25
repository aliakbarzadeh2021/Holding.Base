using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Formatting;

namespace Holding.Base.SOA.Formatters
{
    public sealed class MediaTypeFormatters : ReadOnlyCollection<MediaTypeFormatter>
    {
        private static readonly Lazy<MediaTypeFormatters> Lazy =
               new Lazy<MediaTypeFormatters>(() =>
                   new MediaTypeFormatters());

        public static MediaTypeFormatters Instance { get { return Lazy.Value; } }

        private MediaTypeFormatters()
            : base(new List<MediaTypeFormatter> { new JsonMediaTypeFormatter() })
        {

        }
    }
}
