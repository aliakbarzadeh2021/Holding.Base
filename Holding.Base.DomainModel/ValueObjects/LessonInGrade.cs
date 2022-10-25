using Holding.Base.Infrastructure.Domain;
using Holding.Base.Infrastructure.Enums;

namespace Holding.Base.DomainModel.ValueObjects
{
    /// <summary>
    /// لیست دروس هر پایه تحصیلی 
    /// </summary>
    public sealed class LessonInGrade : ValueObjectBase<LessonInGrade>
    {
        /// <summary>
        /// تابع سازنده
        /// </summary>
        /// <param name="lesson"></param>
        /// <param name="grade"></param>
        /// <param name="unit"></param>
        public LessonInGrade(Lesson lesson, Grade grade, int unit = 1)
        {
            Lesson = lesson;
            Grade = grade;
            Unit = unit;
            Science = Science.Empty;
        }

        /// <summary>
        /// تابع سازنده
        /// </summary>
        /// <param name="lesson"></param>
        /// <param name="grade"></param>
        /// <param name="science"></param>
        /// <param name="unit"></param>
        public LessonInGrade(Lesson lesson, Grade grade, Science science, int unit = 1)
        {
            Lesson = lesson;
            Grade = grade;
            Unit = unit;
            Science = science;
        }

        /// <summary>
        /// رشته تحصیلی
        /// </summary>
        public Science Science { get; private set; }

        /// <summary>
        ///  دروس
        /// </summary>
        public Lesson Lesson { get; private set; }

        /// <summary>
        ///  پایه تحصیلی
        /// </summary>
        public Grade Grade { get; private set; }

        /// <summary>
        /// واحد درس
        /// </summary>
        public int Unit { get; private set; }

        public override bool Equals(LessonInGrade other)
        {
            return Lesson.Title == other.Lesson.Title
                && Grade == other.Grade
                && Unit == other.Unit
                && Science.Equals(other.Science);
        }
    }
}
