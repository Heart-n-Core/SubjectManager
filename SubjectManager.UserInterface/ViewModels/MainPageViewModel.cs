using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services;
using SubjectManager.CommonComponents.Enum;
using SubjectManager.Model.View;
using SubjectManager.UserInterface.Pages;

namespace SubjectManager.UserInterface.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private readonly ISubjectService _subjectService;
    
    private List<SubjectListItem> _allSubjects = new();

    public ObservableCollection<SubjectListItem> Subjects { get; set; } = new();

    [ObservableProperty]
    private SubjectListItem _selectedSubject;

    public MainPageViewModel(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }
    

    [RelayCommand]
    private async Task SubjectSelected()
    {
        if (SelectedSubject == null) return;

        await Shell.Current.GoToAsync($"{nameof(SubjectFullPage)}?subjectId={SelectedSubject.Id}");
        
        SelectedSubject = null;
    }
    
    [RelayCommand]
    private async Task AddSubject()
    {
        await Shell.Current.GoToAsync(nameof(SubjectEditPage));
    }

    [RelayCommand]
    private async Task EditSubject(SubjectListItem subject)
    {
        if (subject == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(SubjectEditPage)}?subjectId={subject.Id}");
    }

    [RelayCommand]
    private async Task DeleteSubject(SubjectListItem subject)
    {
        if (subject == null)
            return;

        bool confirm = await Shell.Current.DisplayAlert(
            "Delete",
            "Are you sure?",
            "Yes",
            "No");

        if (!confirm)
            return;

        await _subjectService.DeleteSubjectAsync(subject.Id.Value);

        await LoadData();
    }
    
    [ObservableProperty]
    private string searchText;

    [ObservableProperty]
    private FieldOfKnowledge? selectedField;

    public List<FieldOfKnowledge> Fields { get; } = Enum.GetValues(typeof(FieldOfKnowledge)).Cast<FieldOfKnowledge>().ToList();
    
    internal async Task LoadData()
    {
        try
        {
            _allSubjects.Clear();
            await foreach (var department in _subjectService.GetAllSubjectsAsync())
            {
                _allSubjects.Add(new SubjectListItem(department));
            }

            ApplyFilters();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to load subjects: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private void ApplyFilters()
    {
        var filtered = _allSubjects.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
            filtered = filtered.Where(s => s.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        if (SelectedField != null)
            filtered = filtered.Where(s => s.FieldOfKnowledge == SelectedField);

        Subjects.Clear();
        foreach (var s in filtered)
            Subjects.Add(s);
    }

    [RelayCommand]
    private void ClearSearch()
    {
        SearchText = string.Empty;
        ApplyFilters();
    }

    [RelayCommand]
    private void ClearFieldFilter()
    {
        SelectedField = null;
        ApplyFilters();
    }
    
    partial void OnSelectedFieldChanged(FieldOfKnowledge? value)
    {
        ApplyFilters();
    }
    
    [RelayCommand]
    private void ClearAllFilters()
    {
        SearchText = string.Empty;
        SelectedField = null;
        ApplyFilters();
    }
}