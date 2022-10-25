using System;

namespace Holding.Base.QueryModel.Lucune
{
    public class AnswerIndexModel : IAnswerIndexModel
    {

        public Guid AnswerId { get; set; }

        public string Answer { get; set; }
    }
}