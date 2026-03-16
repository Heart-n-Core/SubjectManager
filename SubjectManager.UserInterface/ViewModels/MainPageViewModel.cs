using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services;
using SubjectManager.Model.View;
using SubjectManager.UserInterface.Pages;

namespace SubjectManager.UserInterface.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private readonly ISubjectService _subjectService;

    public ObservableCollection<SubjectListItem> Subjects { get; } = new();

    [ObservableProperty]
    private SubjectListItem _selectedSubject;

    public MainPageViewModel(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [RelayCommand]
    private async Task LoadSubjectsAsync()
    {
        var items = _subjectService.GetAllSubjects()
            .Select(x => new SubjectListItem(x));

        Subjects.Clear();
        foreach (var item in items)
        {
            Subjects.Add(item);
        }
    }

    [RelayCommand]
    private async Task SubjectSelected()
    {
        if (SelectedSubject == null) return;

        await Shell.Current.GoToAsync($"{nameof(SubjectFullPage)}?subjectId={SelectedSubject.Id}");
        
        SelectedSubject = null;
    }
}