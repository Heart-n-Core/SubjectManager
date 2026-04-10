using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services;
using SubjectManager.Model.View;
using System.Collections.ObjectModel;
using SubjectManager.CommonComponents.Enum;
using SubjectManager.UserInterface.Pages;

namespace SubjectManager.UserInterface.ViewModels;

[QueryProperty(nameof(SubjectId), "subjectId")]
public partial class SubjectFullViewModel : ObservableObject
{
    private readonly ISubjectService _subjectService;
    private readonly ILessonService _lessonService;
    private SubjectView? _subject;

    [ObservableProperty]
    private LessonListItem _selectedLesson;
    
    public string? SubjectId { get; set; }

    public SubjectView? Subject
    {
        get => _subject;
        private set
        {
            _subject = value;
            OnPropertyChanged();
        }
    }
    public ObservableCollection<LessonListItem> Lessons { get; } = new();

    public SubjectFullViewModel(
        ISubjectService subjectService,
        ILessonService lessonService)
    {
        _subjectService = subjectService;
        _lessonService = lessonService;
    }

    internal async Task LoadData()
    {
        try
        {
            var id = Guid.Parse(SubjectId);

            Subject = await _subjectService.GetSubjectByIdAsync(id);

            _allLessons.Clear();

            var lessons = await _lessonService.GetAllLessonsBySubjectIdAsync(id);

            foreach (var lesson in lessons)
                _allLessons.Add(new LessonListItem(lesson));

            ApplyFilters();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to load subject: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task LessonSelected()
    {
        if (_selectedLesson == null)
        {
            return;
        }
        
        await Shell.Current.GoToAsync($"{nameof(LessonFullPage)}?lessonId={_selectedLesson.Id}");
        
        _selectedLesson = null;
    }

    [RelayCommand]
    private async Task Back()
    {
        await Shell.Current.GoToAsync("..");
    }
    
    [RelayCommand]
    private async Task AddLesson()
    {
        if (string.IsNullOrEmpty(SubjectId))
            return;

        await Shell.Current.GoToAsync($"{nameof(LessonEditPage)}?subjectId={SubjectId}");
    }

    [RelayCommand]
    private async Task EditLesson(LessonListItem lesson)
    {
        if (lesson == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(LessonEditPage)}?lessonId={lesson.Id}&subjectId={Guid.Parse(SubjectId)}");
    }

    [RelayCommand]
    private async Task DeleteLesson(LessonListItem lesson)
    {
        if (lesson == null)
            return;

        bool confirm = await Shell.Current.DisplayAlert(
            "Delete",
            "Are you sure?",
            "Yes",
            "No");

        if (!confirm)
            return;

        await _lessonService.DeleteLessonAsync(lesson.Id.Value);

        await LoadData();
    }
    
    private List<LessonListItem> _allLessons = new();

    [ObservableProperty]
    private LessonType? selectedLessonType;

    [ObservableProperty]
    private DateTime beginDateFrom = DateTime.MinValue;

    [ObservableProperty]
    private DateTime beginDateTo = DateTime.MaxValue;

    public List<LessonType> LessonTypes { get; } = Enum.GetValues(typeof(LessonType)).Cast<LessonType>().ToList();
    
    partial void OnSelectedLessonTypeChanged(LessonType? value)
    {
        ApplyFilters();
    }

    partial void OnBeginDateFromChanged(DateTime value)
    {
        ApplyFilters();
    }

    partial void OnBeginDateToChanged(DateTime value)
    {
        ApplyFilters();
    }
    
    [RelayCommand]
    private void ApplyFilters()
    {
        var filtered = _allLessons.AsEnumerable();

        if (SelectedLessonType != null)
            filtered = filtered.Where(l => l.LessonType == SelectedLessonType);

        filtered = filtered.Where(l => l.BeginDate.Date >= BeginDateFrom.Date && l.BeginDate.Date <= BeginDateTo.Date);

        Lessons.Clear();
        foreach (var l in filtered)
            Lessons.Add(l);
    }
    
    [RelayCommand]
    private void ClearFilters()
    {
        SelectedLessonType = null;
        BeginDateFrom = DateTime.MinValue;
        BeginDateTo = DateTime.MaxValue;

        ApplyFilters();
    }
}