using Services.Storage;
using SubjectManager.Model.View;

namespace Services;

public class LessonService : ILessonService
{
    private ILessonRepository _lessonRepository;

    public LessonService(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }
    
    public List<LessonView> GetAllLessons()
    {
        return _lessonRepository.GetAllLessons();
    }

    public List<LessonView> GetAllLessonsBySubjectId(Guid subjecId)
    {
        return _lessonRepository.GetAllLessonsBySubjectId(subjecId);
    }

    public LessonView GetLessonById(Guid id)
    {
        var view = _lessonRepository.GetLessonById(id);
        return view ?? throw new KeyNotFoundException($"Lesson with ID {id} not found");
    }

    public void CreateLesson(LessonView view)
    {
        if (view.Id!=null)
        {
            throw new ArgumentException("Lesson with Id " + view.Id + " already exists");
        }
        ValidateViewFields(view);
        _lessonRepository.PutLessonEntity(view);
    }

    public void UpdateLesson(LessonView view)
    {
        if (view.Id==null)
        {
            throw new ArgumentException("Missing Id");
        }
        ValidateViewFields(view);
        var ex = _lessonRepository.GetLessonById(view.Id.Value);
        if (ex == null)
        {
            throw new KeyNotFoundException($"Lesson with ID {view.Id} not found");
        }
        _lessonRepository.PutLessonEntity(view);
    }

    public void DeleteLesson(Guid id)
    {
        _lessonRepository.DeleteLessonEntity(id);
    }

    private static void ValidateViewFields(LessonView view)
    {
        //TODO validate lesson
    }
}