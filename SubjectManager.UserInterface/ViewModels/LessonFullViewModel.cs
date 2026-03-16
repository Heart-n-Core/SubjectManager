using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services;
using SubjectManager.Model.View;

namespace SubjectManager.UserInterface.ViewModels;

[QueryProperty(nameof(LessonId), "lessonId")]
public partial class LessonFullViewModel : ObservableObject
{
    private readonly ILessonService _lessonService;
    private string? _lessonId;
    private LessonView? _lesson;
    
    public string? LessonId
    {
        get => _lessonId;
        set
        {
            _lessonId = value;
            LoadLesson();
        }
    }
    
    public LessonView? Lesson
    {
        get => _lesson;
        private set
        {
            _lesson = value;
            OnPropertyChanged();
        }
    }

    public LessonFullViewModel(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    private void LoadLesson()
    {
        if (string.IsNullOrWhiteSpace(LessonId) ||
            !Guid.TryParse(LessonId, out var id))
            return;

        Lesson = _lessonService.GetLessonById(id);
    }

    [RelayCommand]
    private async Task Back()
    {
        await Shell.Current.GoToAsync("..");
    }
}