namespace Services;

using SubjectManager.Model.View;

public interface ISubjectService
{
    List<SubjectView> GetAllSubjects();

    SubjectView GetSubjectById(Guid id);

    void CreateSubject(SubjectView view);

    void UpdateSubject(SubjectView view);

    void DeleteSubject(Guid id);
}