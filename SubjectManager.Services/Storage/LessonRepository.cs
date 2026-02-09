using SubjectManager.Model.Entity;
using SubjectManager.Model.View;

namespace Services.Storage;

internal static class LessonRepository
{
    public static List<LessonView> GetAllLessons()
    {
        return PrimitiveStorage.Lessons.Select(mapToView).ToList();
    }

    public static List<LessonView> GetAllLessonsBySubjectId(Guid subjecId)
    {
        return PrimitiveStorage.Lessons
            .Where(x => x.SubjectId == subjecId)
            .Select(mapToView)
            .ToList();    
    }
    
    public static LessonView? GetLessonById(Guid id)
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
    
    public static LessonEntity PutLessonEntity(LessonView view)
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
}