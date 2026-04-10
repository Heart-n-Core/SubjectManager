using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services;
using SubjectManager.Model.View;

namespace SubjectManager.UserInterface.ViewModels;

[QueryProperty(nameof(LessonId), "lessonId")]
public partial class LessonFullViewModel : ObservableObject
{
    private readonly ILessonService _lessonService;
    private LessonView? _lesson;
    
    public string? LessonId { get; set; }

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

    internal async Task LoadData()
    {
        
        try
        {
            var id = Guid.Parse(LessonId);
            Lesson = await _lessonService.GetLessonByIdAsync(id);

        }       catch
            (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to load lesson: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task Back()
    {
        await Shell.Current.GoToAsync("..");
    }
}