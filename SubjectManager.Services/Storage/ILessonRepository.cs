namespace Services.Storage;

using SubjectManager.Model.Entity;
using SubjectManager.Model.View;

public interface ILessonRepository
{
    List<LessonView> GetAllLessons();

    List<LessonView> GetAllLessonsBySubjectId(Guid subjectId);

    LessonView? GetLessonById(Guid id);

    LessonEntity PutLessonEntity(LessonView view);
    
    void DeleteLessonEntity(Guid id);
}