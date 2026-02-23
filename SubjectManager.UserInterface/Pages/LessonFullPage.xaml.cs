namespace SubjectManager.UserInterface.Pages;

using Services;

[QueryProperty(nameof(LessonId), "lessonId")]
public partial class LessonFullPage : ContentPage
{
    private readonly LessonService _lessonService;

    public string LessonId { get; set; }

    public LessonFullPage(LessonService lessonService)
    {
        InitializeComponent();
        _lessonService = lessonService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (Guid.TryParse(LessonId, out Guid id))
        {
            var lesson = _lessonService.GetLessonById(id);

            TopicLabel.Text = $"Topic: {lesson.Topic}";
            TypeLabel.Text = $"Type: {lesson.LessonType}";
            BeginLabel.Text = $"Begin: {lesson.BeginDate}";
            EndLabel.Text = $"End: {lesson.EndDate}";
            DurationLabel.Text = $"Duration: {lesson.Duration}";
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}