using SubjectManager.Model.View;
using Storage;
using SubjectManager.Model.Entity;

namespace Services;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;

    public SubjectService(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async IAsyncEnumerable<SubjectView> GetAllSubjectsAsync()
    {
        await foreach (var entity in _subjectRepository.GetAllSubjectsAsync())
        {
            yield return MapToView(entity);
        }
    }

    public async Task<SubjectView> GetSubjectByIdAsync(Guid id)
    {
        var entity = await _subjectRepository.GetSubjectByIdAsync(id);
        return MapToView(entity);
    }

    public async Task CreateSubjectAsync(SubjectView view)
    {
        if (view.Id != null && view.Id != Guid.Empty)
            throw new ArgumentException($"Subject with Id {view.Id} already exists");

        ValidateViewFields(view);

        await _subjectRepository.PutSubjectEntityAsync(view);
    }

    public async Task UpdateSubjectAsync(SubjectView view)
    {
        if (view.Id == null || view.Id == Guid.Empty)
            throw new ArgumentException("Missing Id");

        ValidateViewFields(view);

        // Ensure entity exists
        await _subjectRepository.GetSubjectByIdAsync(view.Id.Value);

        await _subjectRepository.PutSubjectEntityAsync(view);
    }

    public async Task DeleteSubjectAsync(Guid id)
    {
        await _subjectRepository.DeleteSubjectAsync(id);
    }

    private static void ValidateViewFields(SubjectView view)
    {
        if (string.IsNullOrWhiteSpace(view.Name))
            throw new ArgumentException("Name is required");

        if (view.Credits <= 0)
            throw new ArgumentException("Credits must be greater than 0");

        if (view.FieldOfKnowledge <= 0)
            throw new ArgumentException("Field of knowledge is required");
    }

    private SubjectView MapToView(SubjectEntity entity)
    {
        return new SubjectView(
            entity.Name,
            entity.Credits,
            entity.FieldOfKnowledge
        )
        {
            Id = entity.Id
        };
    }
}