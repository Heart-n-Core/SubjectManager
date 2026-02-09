using SubjectManager.Model.Enum;

namespace SubjectManager.Model.Entity;

public class LessonEntity
{
    public Guid Id { get;}
    public Guid SubjectId { get; set; }
    public string Topic { get; set; }
    public LessonType  LessonType { get; set; }
    public DateTimeOffset BeginDate { get; set; }
    public DateTimeOffset EndDate { get; set; }

    public LessonEntity(){
    }

    public LessonEntity(Guid subjectId, string topic, LessonType lessonType, DateTimeOffset beginDate, DateTimeOffset endDate)
    {
        Id = Guid.NewGuid();
        SubjectId = subjectId;
        Topic = topic;
        LessonType = lessonType;
        BeginDate = beginDate;
        EndDate = endDate;
    }
}