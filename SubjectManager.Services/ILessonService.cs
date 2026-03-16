namespace Services;

using SubjectManager.Model.View;

public interface ILessonService
{
    List<LessonView> GetAllLessons();

    List<LessonView> GetAllLessonsBySubjectId(Guid subjectId);

    LessonView GetLessonById(Guid id);

    void CreateLesson(LessonView view);

    void UpdateLesson(LessonView view);

    void DeleteLesson(Guid id);
}