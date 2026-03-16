namespace Services.Storage;

using SubjectManager.Model.Entity;
using SubjectManager.Model.View;

public interface ISubjectRepository
{
    List<SubjectView> GetAllSubjects();

    SubjectView? GetSubjectById(Guid id);

    SubjectEntity PutSubjectEntity(SubjectView view);

    void DeleteSubject(Guid id);
}