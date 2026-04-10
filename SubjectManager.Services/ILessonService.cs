namespace Services;

using SubjectManager.Model.View;

public interface ILessonService
{
    Task<IEnumerable<LessonView>> GetAllLessonsBySubjectIdAsync(Guid subjectId);

    Task<LessonView> GetLessonByIdAsync(Guid id);

    Task CreateLessonAsync(LessonView view);

    Task UpdateLessonAsync(LessonView view);

    Task DeleteLessonAsync(Guid id);
}