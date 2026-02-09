using System.Text;
using SubjectManager.Model.Enum;

namespace SubjectManager.Model.View;

public class SubjectView
{
    //Actual code
    public TimeSpan DurationTotal {
        get => _lessons.Aggregate(TimeSpan.Zero, (total, lesson) => total + lesson.Duration);
    }
    
    public SubjectView(string name, int credits, FieldOfKnowledge fieldOfKnowledge)
    {
        _name = name;
        _credits = credits;
        _fieldOfKnowledge = fieldOfKnowledge;
    }
    
    public override string ToString()
    {
        return $"Name: {Name}; Credits: {Credits}; FieldOfKnowledge: {_fieldOfKnowledge}; TotalDuration: {DurationTotal}";
    }
    
    public String allData => ToString()+"\nSubject's lessons:\n"+string.Join("\n", _lessons);


    //Boilerplate
    private Guid? _id;
    private string _name;
    private int _credits;
    private FieldOfKnowledge _fieldOfKnowledge;
    private List<LessonView> _lessons;

    public Guid? Id
    {
        get => _id;
        set => _id = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
        // set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Credits
    {
        get => _credits;
        set => _credits = value;
    }

    public FieldOfKnowledge FieldOfKnowledge
    {
        get => _fieldOfKnowledge;
        set => _fieldOfKnowledge = value;
    }

    public List<LessonView> Lessons
    {
        get => _lessons;
        set => _lessons = value;
        // set => _lessons = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    
}