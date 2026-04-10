using SubjectManager.Model.Entity;
using SubjectManager.Model.View;

namespace Storage;

public class LessonRepository : ILessonRepository
{
    private readonly IStorage _storage;

    public LessonRepository(IStorage storage)
    {
        _storage = storage;
    }

    public async Task<IEnumerable<LessonEntity>> GetAllLessonsBySubjectId(Guid subjectId)
    {
        return await _storage.GetLessonsBySubjectAsync(subjectId);
    }

    public async Task<LessonEntity> GetLessonById(Guid id)
    {
        var entity = await _storage.GetLessonAsync(id);

        if (entity == null)
            throw new KeyNotFoundException($"Lesson with ID {id} not found");

        return entity;
    }

    public async Task PutLessonEntity(LessonView view)
    {
        LessonEntity entity;

        if (view.Id == null || view.Id == Guid.Empty)
        {
            // CREATE
            entity = new LessonEntity
            {
                Id = Guid.NewGuid(),
                SubjectId = view.SubjectId
            };
        }
        else
        {
            // UPDATE
            entity = await _storage.GetLessonAsync(view.Id.Value);

            if (entity == null)
                throw new KeyNotFoundException($"Lesson with ID {view.Id} not found");
        }

        entity.Topic = view.Topic;
        entity.LessonType = view.LessonType;
        entity.BeginDate = view.BeginDate;
        entity.EndDate = view.EndDate;

        await _storage.SaveLessonAsync(entity);
    }

    public async Task DeleteLessonEntity(Guid id)
    {
        var entity = await _storage.GetLessonAsync(id);

        if (entity == null)
            throw new KeyNotFoundException($"Lesson with ID {id} not found");

        await _storage.DeleteLessonAsync(id);
    }
}