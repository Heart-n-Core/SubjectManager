using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services;
using SubjectManager.CommonComponents.Enum;
using SubjectManager.Model.View;

namespace SubjectManager.UserInterface.ViewModels;

[QueryProperty(nameof(SubjectId), "subjectId")]
public partial class SubjectEditViewModel : ObservableObject
{
    private readonly ISubjectService _subjectService;

    public string? SubjectId { get; set; }
    
    private string? subjectId;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private int credits;

    [ObservableProperty]
    private FieldOfKnowledge selectedField;
    

    public List<FieldOfKnowledge> Fields { get; } =
        Enum.GetValues(typeof(FieldOfKnowledge)).Cast<FieldOfKnowledge>().ToList();

    public SubjectEditViewModel(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    internal async Task LoadData()
    {
        if (string.IsNullOrWhiteSpace(SubjectId))
            return;

        if (!Guid.TryParse(SubjectId, out var id))
            return;

        var subject = await _subjectService.GetSubjectByIdAsync(id);

        Name = subject.Name;
        Credits = subject.Credits;
        SelectedField = subject.FieldOfKnowledge;
    }

    [RelayCommand]
    private async Task Save()
    {
        try{
        if (!string.IsNullOrWhiteSpace(SubjectId))
        {
            // UPDATE
            var subject = new SubjectView(Name, Credits, SelectedField)
            {
                Id = Guid.Parse(SubjectId)
            };

            await _subjectService.UpdateSubjectAsync(subject);
        }
        else
        {
            // CREATE
            var subject = new SubjectView(Name, Credits, SelectedField);

            await _subjectService.CreateSubjectAsync(subject);
        }
    }        catch (Exception e)
    {
        await Shell.Current.DisplayAlert("Validation error", e.Message, "OK");
        return;
    }

    await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}