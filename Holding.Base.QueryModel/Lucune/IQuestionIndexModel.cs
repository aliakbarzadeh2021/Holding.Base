using System;

namespace Holding.Base.QueryModel.Lucune
{
    public interface IQuestionIndexModel
    {
        Guid Id { get; }

        string Text { get; }
    }
}