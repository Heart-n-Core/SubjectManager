using SubjectManager.CommonComponents.Enum;

namespace SubjectManager.Model.Entity;

public class SubjectEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Credits { get; set; }
    public FieldOfKnowledge FieldOfKnowledge { get; set; }

    public SubjectEntity()
    {
    }

    public SubjectEntity(string name, int credits, FieldOfKnowledge fieldOfKnowledge) : this(Guid.NewGuid(), name, credits, fieldOfKnowledge)
    {
    }

    public SubjectEntity(Guid id, string name, int credits, FieldOfKnowledge fieldOfKnowledge)
    {
        Id = id;
        Name = name;
        Credits = credits;
        FieldOfKnowledge = fieldOfKnowledge;
    }
}