using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "LessonType")]
    public enum LessonType
    {
        [Description("دروس آزمایشگاهی")]
        LaboratoryCourses = 1,
        [Description("دروس تئوری")]
        InterpretingCourses = 2,
        [Description("دروس عملی")]
        PracticalCourses = 3,
        [Description("دروس اختیاری")]
        ElectiveCourses = 4,
        [Description("دروس غیر حضوری")]
        NonFaceCourses = 5,
        [Description("دروس تقویتی")]
        RefresherCourses = 6,
        [Description("دورس کنکوری")]
        CoursesExaminee = 7,
        [Description("سایر")]
        Other = 8
    }
} 