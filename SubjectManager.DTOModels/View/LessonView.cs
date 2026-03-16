using SubjectManager.CommonComponents.Enum;

namespace SubjectManager.Model.View;

public class LessonView
{
    
    private Guid? _id;
    private Guid _subjectId;
    private string _topic;
    private LessonType _lessonType;
    private DateTimeOffset  _beginDate;
    private DateTimeOffset _endDate;

    public TimeSpan Duration => EndDate - BeginDate;

    public Guid? Id
    {
        get => _id;
        set => _id = value;
    }

    public Guid SubjectId
    {
        get => _subjectId;
        // set => _subjectId = value;
    }
    
    public string Topic
    {
        get => _topic;
        set => _topic = value;
        // set => _topic = value ?? throw new ArgumentNullException(nameof(value));
    }

    public LessonType LessonType
    {
        get => _lessonType;
        set => _lessonType = value;
    }

    public DateTimeOffset BeginDate
    {
        get => _beginDate;
        set => _beginDate = value;
    }

    public DateTimeOffset EndDate
    {
        get => _endDate;
        set => _endDate = value;
    }
    
    public LessonView(Guid subjectId, string topic, LessonType lessonType, DateTimeOffset beginDate, DateTimeOffset endDate)    //No id here and that's okay
    {
        _subjectId = subjectId;
        _topic = topic;
        _lessonType = lessonType;
        _beginDate = beginDate;
        _endDate = endDate;
    }
    
    public override string ToString()
    {
        return $"Topic: {Topic}; LessonType: {LessonType}; BeginDate: {BeginDate}; EndDate: {EndDate}; Duration: {Duration}";
    }

}