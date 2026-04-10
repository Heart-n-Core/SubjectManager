namespace SubjectManager.Model.View;

using SubjectManager.CommonComponents.Enum;

public class SubjectListItem
{
    private Guid? _id;
    private String _name;
    private FieldOfKnowledge _fieldOfKnowledge;

    public Guid? Id => _id;

    public string Name => _name;

    public FieldOfKnowledge FieldOfKnowledge => _fieldOfKnowledge;
    
    public SubjectListItem(SubjectView view)
    {
        _id = view.Id;
        _name = view.Name;
        _fieldOfKnowledge = view.FieldOfKnowledge;
    }
}