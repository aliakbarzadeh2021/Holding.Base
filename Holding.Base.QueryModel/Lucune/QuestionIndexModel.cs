using System;

namespace Holding.Base.QueryModel.Lucune
{
    public class QuestionIndexModel : IQuestionIndexModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }
    }
}
