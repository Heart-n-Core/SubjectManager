using SubjectManager.Model.Entity;
using SubjectManager.Model.View;

namespace Services.Storage;

public class SubjectRepository : ISubjectRepository
{
    private ILessonRepository _lessonRepository;

    public SubjectRepository(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }
    
    public List<SubjectView> GetAllSubjects()
    {
        return PrimitiveStorage.Subjects.Select(mapToView).ToList();
    }

    public SubjectView? GetSubjectById(Guid id)
    {
        var entity = PrimitiveStorage.Subjects.SingleOrDefault(x => x.Id == id);
        return entity == null ? null : mapToView(entity);
    }

    private SubjectView mapToView(SubjectEntity entity)
    {
        var lessons = _lessonRepository.GetAllLessonsBySubjectId(entity.Id);
        var view = new SubjectView(
            entity.Name,
            entity.Credits,
            entity.FieldOfKnowledge
        );
        view.Id = entity.Id;
        view.Lessons = lessons;
        return view;
    }

    public SubjectEntity PutSubjectEntity(SubjectView view)
    {
        var entity = view.Id==null ? new SubjectEntity() : PrimitiveStorage.Subjects.Single(x => x.Id == view.Id);
        
        entity.Name = view.Name;
        entity.Credits = view.Credits;
        entity.FieldOfKnowledge = view.FieldOfKnowledge;
        
        if (view.Id==null)
            PrimitiveStorage.Subjects.Add(entity);
        
        return entity;
    }
    
    public void DeleteSubject(Guid id)
    {
        var entity = PrimitiveStorage.Subjects.SingleOrDefault(x => x.Id == id);

        if (entity == null)
            throw new KeyNotFoundException($"Subject with ID {id} not found");

        PrimitiveStorage.Subjects.Remove(entity);
    }

}