using SubjectManager.CommonComponents.Enum;

namespace SubjectManager.Model.View;

public class LessonListItem
{
    private Guid? _id;
    private Guid _subjectId;
    private DateTimeOffset  _beginDate;
    private LessonType _lessonType;

    public Guid? Id => _id;

    public Guid SubjectId => _subjectId;

    public DateTimeOffset BeginDate => _beginDate;

    public LessonType LessonType => _lessonType;
    
    public LessonListItem(Guid? id, Guid subjectId, DateTimeOffset beginDate, LessonType lessonType)
    {
        _id = id;
        _subjectId = subjectId;
        _beginDate = beginDate;
        _lessonType = lessonType;
    }
    
    public LessonListItem(LessonView view)
    {
        _id = view.Id;
        _subjectId = view.SubjectId;
        _beginDate = view.BeginDate;
        _lessonType = view.LessonType;
    }
}