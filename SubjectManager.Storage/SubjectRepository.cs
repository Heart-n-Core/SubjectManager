using SubjectManager.Model.Entity;
using SubjectManager.Model.View;

namespace Storage;

public class SubjectRepository : ISubjectRepository
{
    private readonly IStorage _storage;

    public SubjectRepository(IStorage storage)
    {
        _storage = storage;
    }

    public IAsyncEnumerable<SubjectEntity> GetAllSubjectsAsync()
    {
        return _storage.GetSubjectsAsync();
    }

    public async Task<SubjectEntity> GetSubjectByIdAsync(Guid id)
    {
        var entity = await _storage.GetSubjectAsync(id);

        if (entity == null)
            throw new KeyNotFoundException($"Subject with ID {id} not found");

        return entity;
    }

    public async Task PutSubjectEntityAsync(SubjectView view)
    {
        SubjectEntity entity;

        if (view.Id == null || view.Id == Guid.Empty)
        {
            // CREATE
            entity = new SubjectEntity
            {
                Id = Guid.NewGuid()
            };
        }
        else
        {
            // UPDATE
            entity = await _storage.GetSubjectAsync(view.Id.Value);

            if (entity == null)
                throw new KeyNotFoundException($"Subject with ID {view.Id} not found");
        }

        entity.Name = view.Name;
        entity.Credits = view.Credits;
        entity.FieldOfKnowledge = view.FieldOfKnowledge;

        await _storage.SaveSubjectAsync(entity);
    }

    public async Task DeleteSubjectAsync(Guid id)
    {
        var entity = await _storage.GetSubjectAsync(id);

        if (entity == null)
            throw new KeyNotFoundException($"Subject with ID {id} not found");

        //Get and delete all lessons for this subject
        var lessons = await _storage.GetLessonsBySubjectAsync(id);
        var deleteTasks = lessons.Select(lesson => _storage.DeleteLessonAsync(lesson.Id));
        await Task.WhenAll(deleteTasks);
        
        await _storage.DeleteSubjectAsync(id);
    }
}