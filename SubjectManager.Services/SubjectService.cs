using SubjectManager.Model.View;
using Services.Storage;

namespace Services;

public class SubjectService
{
    public List<SubjectView> getAllSubjects()
    {
        return SubjectRepository.GetAllSubjects();
    }
    
    public SubjectView GetSubjectById(Guid id)
    {
        var view = SubjectRepository.GetSubjectById(id);
        return view ?? throw new KeyNotFoundException($"Subject with ID {id} not found");
    }

    public void CreateSubject(SubjectView view)
    {
        if (view.Id!=null)
        {
            throw new ArgumentException("Subject with Id " + view.Id + " already exists");
        }
        validateViewFields(view);
        SubjectRepository.PutSubjectEntity(view);
    }

    public void UpdateSubject(SubjectView view)
    {
        if (view.Id==null)
        {
            throw new ArgumentException("Missing Id");
        }
        validateViewFields(view);
        var ex = SubjectRepository.GetSubjectById(view.Id.Value);
        if (ex == null)
        {
            throw new KeyNotFoundException($"Subject with ID {view.Id} not found");
        }
        SubjectRepository.PutSubjectEntity(view);
    }

    public static void deleteSubject(Guid id)
    {
        //TODO delete Subject
    }

    private static void validateViewFields(SubjectView view)
    {
        //TODO validate Subject
    }
}