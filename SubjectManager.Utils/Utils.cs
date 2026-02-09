using SubjectManager.Model.View;

namespace SubjectManager.Utils;

public static class Utils
{
    
    //In order to display int indexes in user interface instead of GUID
    public static Dictionary<int, SubjectView> GetIndexedSubjects(List<SubjectView> subjects)
    {
        return Enumerable.Range(1, subjects.Count)
            .ToDictionary(i => i, i => subjects[i - 1]);
    }
}