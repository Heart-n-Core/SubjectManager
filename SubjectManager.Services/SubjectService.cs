using SubjectManager.Model.View;
using Services.Storage;

namespace Services;

public class SubjectService : ISubjectService
{
    private ISubjectRepository _subjectRepository;

    // public SubjectService(ISubjectRepository subjectRepository, ILessonRepository lessonRepository)
    public SubjectService(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }
    
    public List<SubjectView> GetAllSubjects()
    {
        return _subjectRepository.GetAllSubjects();
    }
    
    public SubjectView GetSubjectById(Guid id)
    {
        var view = _subjectRepository.GetSubjectById(id);
        return view ?? throw new KeyNotFoundException($"Subject with ID {id} not found");
    }

    public void CreateSubject(SubjectView view)
    {
        if (view.Id!=null)
        {
            throw new ArgumentException("Subject with Id " + view.Id + " already exists");
        }
        ValidateViewFields(view);
        _subjectRepository.PutSubjectEntity(view);
    }

    public void UpdateSubject(SubjectView view)
    {
        if (view.Id==null)
        {
            throw new ArgumentException("Missing Id");
        }
        ValidateViewFields(view);
        var ex = _subjectRepository.GetSubjectById(view.Id.Value);
        if (ex == null)
        {
            throw new KeyNotFoundException($"Subject with ID {view.Id} not found");
        }
        _subjectRepository.PutSubjectEntity(view);
    }

    public void DeleteSubject(Guid id)
    {
        _subjectRepository.DeleteSubject(id);
    }

    private static void ValidateViewFields(SubjectView view)
    {
        //TODO validate Subject
    }
}