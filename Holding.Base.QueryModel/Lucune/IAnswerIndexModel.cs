using System;

namespace Holding.Base.QueryModel.Lucune
{
    public interface IAnswerIndexModel
    {
        Guid AnswerId { get; }

        string Answer { get; }
    }
}