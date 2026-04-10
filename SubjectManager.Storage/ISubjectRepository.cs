namespace Storage;

using SubjectManager.Model.Entity;
using SubjectManager.Model.View;

public interface ISubjectRepository
{
    IAsyncEnumerable<SubjectEntity>GetAllSubjectsAsync();

    Task<SubjectEntity> GetSubjectByIdAsync(Guid id);

    Task PutSubjectEntityAsync(SubjectView view);

    Task DeleteSubjectAsync(Guid id);
}