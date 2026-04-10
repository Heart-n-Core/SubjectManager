namespace Storage;

using SubjectManager.Model.Entity;
using SubjectManager.Model.View;

public interface ILessonRepository
{
    Task<IEnumerable<LessonEntity>> GetAllLessonsBySubjectId(Guid subjectId);

    Task<LessonEntity> GetLessonById(Guid id);

    Task PutLessonEntity(LessonView view);
    
    Task DeleteLessonEntity(Guid id);
}