using Services;

namespace SubjectManager.UserInterface;

public static class AppServices
{
    public static ISubjectService SubjectService { get; set; } = null!;
    public static ILessonService LessonService { get; set; } = null!;
}