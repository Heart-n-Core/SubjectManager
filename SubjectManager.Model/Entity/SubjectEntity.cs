using SubjectManager.Model.Enum;

namespace SubjectManager.Model.Entity;

public class SubjectEntity
{
    public Guid Id { get;}
    public string Name { get; set; }
    public int Credits { get; set; }
    public FieldOfKnowledge FieldOfKnowledge { get; set; }

    public SubjectEntity()
    {
    }

    public SubjectEntity(string name, int credits, FieldOfKnowledge fieldOfKnowledge)
    {
        Id = Guid.NewGuid();
        Name = name;
        Credits = credits;
        FieldOfKnowledge = fieldOfKnowledge;
    }
}