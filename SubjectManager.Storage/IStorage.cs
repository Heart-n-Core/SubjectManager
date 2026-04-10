using SubjectManager.Model.Entity;

namespace Storage;

public interface IStorage
{
        IAsyncEnumerable<SubjectEntity> GetSubjectsAsync();
        Task<SubjectEntity> GetSubjectAsync(Guid SubjectId);
        Task SaveSubjectAsync(SubjectEntity subject);
        Task DeleteSubjectAsync(Guid SubjectId);
        
        
        
        Task<IEnumerable<LessonEntity>> GetLessonsBySubjectAsync(Guid SubjectId);
        Task<LessonEntity> GetLessonAsync(Guid LessonId);
        // Task<int> GetLessonsBySubjectCountAsync(Guid Subjectid);
        Task SaveLessonAsync(LessonEntity Lesson);
        Task DeleteLessonAsync(Guid LessonId);
}