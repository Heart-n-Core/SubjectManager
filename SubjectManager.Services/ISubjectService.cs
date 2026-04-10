namespace Services;

using SubjectManager.Model.View;

public interface ISubjectService
{

    IAsyncEnumerable<SubjectView>GetAllSubjectsAsync();

    Task<SubjectView> GetSubjectByIdAsync(Guid id);

    Task CreateSubjectAsync(SubjectView view);
    Task UpdateSubjectAsync(SubjectView view);

    Task DeleteSubjectAsync(Guid id);
}