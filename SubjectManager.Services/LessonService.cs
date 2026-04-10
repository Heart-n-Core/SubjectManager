using Storage;
using SubjectManager.Model.Entity;
using SubjectManager.Model.View;

namespace Services;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _lessonRepository;

    public LessonService(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<IEnumerable<LessonView>> GetAllLessonsBySubjectIdAsync(Guid subjectId)
    {
        var entities = await _lessonRepository.GetAllLessonsBySubjectId(subjectId);
        return entities.Select(MapToView);
    }

    public async Task<LessonView> GetLessonByIdAsync(Guid id)
    {
        var entity = await _lessonRepository.GetLessonById(id);
        return MapToView(entity);
    }

    public async Task CreateLessonAsync(LessonView view)
    {
        if (view.Id != null && view.Id != Guid.Empty)
            throw new ArgumentException($"Lesson with Id {view.Id} already exists");

        ValidateViewFields(view);

        await _lessonRepository.PutLessonEntity(view);
    }

    public async Task UpdateLessonAsync(LessonView view)
    {
        if (view.Id == null || view.Id == Guid.Empty)
            throw new ArgumentException("Missing Id");

        ValidateViewFields(view);

        // ensure exists
        await _lessonRepository.GetLessonById(view.Id.Value);

        await _lessonRepository.PutLessonEntity(view);
    }

    public async Task DeleteLessonAsync(Guid id)
    {
        await _lessonRepository.DeleteLessonEntity(id);
    }

    private static void ValidateViewFields(LessonView view)
    {
        if (view.SubjectId == Guid.Empty)
            throw new ArgumentException("SubjectId is required");

        if (string.IsNullOrWhiteSpace(view.Topic))
            throw new ArgumentException("Topic is required");

        if (view.BeginDate >= view.EndDate)
            throw new ArgumentException("BeginDate must be before EndDate");
    }

    private LessonView MapToView(LessonEntity entity)
    {
        return new LessonView(
            entity.SubjectId,
            entity.Topic,
            entity.LessonType,
            entity.BeginDate,
            entity.EndDate
        )
        {
            Id = entity.Id
        };
    }
}