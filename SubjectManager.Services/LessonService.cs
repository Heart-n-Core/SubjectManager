using SubjectManager.Model.Entity;
using SubjectManager.Model.View;
using Services.Storage;

namespace Services;

public class LessonService
{
    public List<LessonView> GetAllLessons()
    {
        return LessonRepository.GetAllLessons();
    }

    public List<LessonView> GetAllLessonsBySubjectId(Guid subjecId)
    {
        return LessonRepository.GetAllLessonsBySubjectId(subjecId);
    }

    public LessonView GetLessonById(Guid id)
    {
        var view = LessonRepository.GetLessonById(id);
        return view ?? throw new KeyNotFoundException($"Lesson with ID {id} not found");
    }

    public void CreateLesson(LessonView view)
    {
        if (view.Id!=null)
        {
            throw new ArgumentException("Lesson with Id " + view.Id + " already exists");
        }
        validateViewFields(view);
        LessonRepository.PutLessonEntity(view);
    }

    public void UpdateLesson(LessonView view)
    {
        if (view.Id==null)
        {
            throw new ArgumentException("Missing Id");
        }
        validateViewFields(view);
        var ex = LessonRepository.GetLessonById(view.Id.Value);
        if (ex == null)
        {
            throw new KeyNotFoundException($"Lesson with ID {view.Id} not found");
        }
        LessonRepository.PutLessonEntity(view);
    }

    public static void deleteLesson(Guid id)
    {
        //TODO delete lesson
    }

    private static void validateViewFields(LessonView view)
    {
        //TODO validate lesson
    }
}