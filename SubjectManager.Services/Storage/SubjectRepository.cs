using SubjectManager.Model.Entity;
using SubjectManager.Model.View;

namespace Services.Storage;

internal static class SubjectRepository
{
    public static List<SubjectView> GetAllSubjects()
    {
        return PrimitiveStorage.Subjects.Select(mapToView).ToList();
    }

    public static SubjectView? GetSubjectById(Guid id)
    {
        var entity = PrimitiveStorage.Subjects.SingleOrDefault(x => x.Id == id);
        return entity == null ? null : mapToView(entity);
    }

    private static SubjectView mapToView(SubjectEntity entity)
    {
        var lessons = LessonRepository.GetAllLessonsBySubjectId(entity.Id);
        var view = new SubjectView(
            entity.Name,
            entity.Credits,
            entity.FieldOfKnowledge
        );
        view.Id = entity.Id;
        view.Lessons = lessons;
        return view;
    }

    public static SubjectEntity PutSubjectEntity(SubjectView view)
    {
        var entity = view.Id==null ? new SubjectEntity() : PrimitiveStorage.Subjects.Single(x => x.Id == view.Id);
        
        entity.Name = view.Name;
        entity.Credits = view.Credits;
        entity.FieldOfKnowledge = view.FieldOfKnowledge;
        
        if (view.Id==null)
            PrimitiveStorage.Subjects.Add(entity);
        
        return entity;
    }

}