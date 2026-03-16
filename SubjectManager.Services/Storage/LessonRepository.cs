using SubjectManager.Model.Entity;
using SubjectManager.Model.View;

namespace Services.Storage;

public class LessonRepository : ILessonRepository
{
    public List<LessonView> GetAllLessons()
    {
        return PrimitiveStorage.Lessons.Select(mapToView).ToList();
    }

    public List<LessonView> GetAllLessonsBySubjectId(Guid subjecId)
    {
        return PrimitiveStorage.Lessons
            .Where(x => x.SubjectId == subjecId)
            .Select(mapToView)
            .ToList();    
    }
    
    public LessonView? GetLessonById(Guid id)
    {
        var entity = PrimitiveStorage.Lessons.SingleOrDefault(x => x.Id == id);
        return entity == null ? null : mapToView(entity);
    }

    private static LessonView mapToView(LessonEntity entity)
    {
        var view = new LessonView(
            entity.SubjectId,
            entity.Topic,
            entity.LessonType,
            entity.BeginDate,
            entity.EndDate
        );
        view.Id=entity.Id;
        return view;
    }
    
    public LessonEntity PutLessonEntity(LessonView view)
    {
        var entity = view.Id==null ? new LessonEntity() : PrimitiveStorage.Lessons.Single(x => x.Id == view.Id);

        if (entity.SubjectId.Equals(Guid.Empty))
        {
            entity.SubjectId=view.SubjectId;
        }
        entity.Topic=view.Topic;
        entity.LessonType=view.LessonType;
        entity.BeginDate=view.BeginDate;
        entity.EndDate=view.EndDate;
        
        if (view.Id==null)
            PrimitiveStorage.Lessons.Add(entity);
        
        return entity;
    }
    
    public void DeleteLessonEntity(Guid id)
    {
        var entity = PrimitiveStorage.Lessons.SingleOrDefault(x => x.Id == id);
        
        if (entity == null)
            throw new KeyNotFoundException($"Lesson with ID {id} not found");
        
        PrimitiveStorage.Lessons.Remove(entity);
    }
}