using Holding.Base.Infrastructure.Domain;
using Holding.Base.Infrastructure.Enums;

namespace Holding.Base.DomainModel.ValueObjects
{
    /// <summary>
    /// مدل درس
    /// </summary>
    public class Lesson : ValueObjectBase<Lesson>
    {
        public Lesson(string title, int course, LessonType lessenType)
        {
            Title = title;
            Course = course;
            LessonType = lessenType;
        }

        /// <summary>
        /// نام درس
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// واحد درس
        /// </summary>
        public int Course { get; private set; }

        /// <summary>
        /// نوع درس
        /// </summary>
        public LessonType LessonType { get; private set; }


        public override string ToString()
        {
            return Title;
        }
    }
}
