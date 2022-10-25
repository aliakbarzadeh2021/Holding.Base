using System;

namespace Holding.Base.QueryModel.Lucune
{
    public interface IPostIndexModel
    {
        Guid PostId { get; }

        Guid BlogId { get; }

        string Context { get; }
    }
}