using System;

namespace Holding.Base.QueryModel.Lucune
{
    public class PostIndexModel : IPostIndexModel
    {

        public Guid PostId { get; set; }

        public Guid BlogId { get; set; }

        public string Context { get; set; }
    }
}
