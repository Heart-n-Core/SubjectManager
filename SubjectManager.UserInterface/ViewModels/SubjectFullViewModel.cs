using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services;
using SubjectManager.Model.View;
using System.Collections.ObjectModel;
using SubjectManager.UserInterface.Pages;

namespace SubjectManager.UserInterface.ViewModels;

[QueryProperty(nameof(SubjectId), "subjectId")]
public partial class SubjectFullViewModel : ObservableObject
{
    private readonly ISubjectService _subjectService;
    private readonly ILessonService _lessonService;
    private string? _subjectId;
    private SubjectView? _subject;

    [ObservableProperty]
    private LessonListItem _selectedLesson;
    
    public string? SubjectId
    {
        get => _subjectId;
        set
        {
            _subjectId = value;
            LoadSubject();
        }
    }

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

    private void LoadSubject()
    {
        if (string.IsNullOrWhiteSpace(SubjectId) ||
            !Guid.TryParse(SubjectId, out var id))
            return;

        Subject = _subjectService.GetSubjectById(id);

        Lessons.Clear();

        var lessons = _lessonService
            .GetAllLessonsBySubjectId(id)
            .Select(x => new LessonListItem(x));

        foreach (var lesson in lessons)
            Lessons.Add(lesson);
    }

    [RelayCommand]
    private async Task LessonSelected()
    {
        if (_selectedLesson == null)
        {
            return;
        }
        
        await Shell.Current.GoToAsync(
            $"{nameof(LessonFullPage)}?lessonId={_selectedLesson.Id}");
        
        _selectedLesson = null;
    }

    [RelayCommand]
    private async Task Back()
    {
        await Shell.Current.GoToAsync("..");
    }
}