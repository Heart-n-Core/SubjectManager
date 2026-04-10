using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services;
using SubjectManager.CommonComponents.Enum;
using SubjectManager.Model.View;
using SubjectManager.UserInterface.ViewModels;

[QueryProperty(nameof(LessonId), "lessonId")]
[QueryProperty(nameof(SubjectId), "subjectId")]
public partial class LessonEditViewModel : ObservableObject
{
    private readonly ILessonService _lessonService;

    public string? LessonId { get; set; }
    public string? SubjectId { get; set; }

    [ObservableProperty]
    private string topic;

    [ObservableProperty]
    private LessonType selectedLessonType;

    [ObservableProperty]
    private DateTime beginDate = DateTime.Now;

    [ObservableProperty]
    private DateTime endDate = DateTime.Now.AddHours(1);
    
    [ObservableProperty]
    private TimeSpan beginTime;

    [ObservableProperty]
    private TimeSpan endTime;

    public List<LessonType> LessonTypes { get; } =
        Enum.GetValues(typeof(LessonType)).Cast<LessonType>().ToList();

    public LessonEditViewModel(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    public async Task LoadData()
    {

        if (!string.IsNullOrEmpty(LessonId))
        {
            
            if (!Guid.TryParse(LessonId, out var lessonId))
            {
                await Shell.Current.DisplayAlert("Error", "Invalid lesson ID", "OK");
                return;
            }

            var lesson = await _lessonService.GetLessonByIdAsync(lessonId);
            
            Topic = lesson.Topic;
            SubjectId = lesson.SubjectId.ToString();
            SelectedLessonType = lesson.LessonType;
            
            BeginDate = lesson.BeginDate.DateTime.Date;
            BeginTime = lesson.BeginDate.DateTime.TimeOfDay;

            EndDate = lesson.EndDate.DateTime.Date;
            EndTime = lesson.EndDate.DateTime.TimeOfDay;
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        try
        {
            var beginDateTime = BeginDate.Date + BeginTime;
            var endDateTime = EndDate.Date + EndTime;
            
            if (!string.IsNullOrEmpty(LessonId))
            {
                // UPDATE
                var lesson = new LessonView(
                    Guid.Parse(SubjectId),
                    Topic,
                    SelectedLessonType,
                    beginDateTime,
                    endDateTime)
                {
                    Id = Guid.Parse(LessonId)
                };

                await _lessonService.UpdateLessonAsync(lesson);
            }
            else
            {
                // CREATE
                var lesson = new LessonView(
                    Guid.Parse(SubjectId),
                    Topic,
                    SelectedLessonType,
                    beginDateTime,
                    endDateTime);

                await _lessonService.CreateLessonAsync(lesson);
            }
        }
        catch (Exception e)
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