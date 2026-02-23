namespace SubjectManager.UserInterface.Pages;

using Services;
using SubjectManager.Model.View;

[QueryProperty(nameof(SubjectId), "subjectId")]
public partial class SubjectFullPage : ContentPage
{
    private readonly SubjectService _subjectService;
    private readonly LessonService _lessonService;

    public string SubjectId { get; set; }

    public SubjectFullPage(SubjectService subjectService, LessonService lessonService)
    {
        InitializeComponent();
        _subjectService = subjectService;
        _lessonService = lessonService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (Guid.TryParse(SubjectId, out Guid id))
        {
            
            var subject = _subjectService.GetSubjectById(id);

            NameLabel.Text = $"Name: {subject.Name}";
            CreditsLabel.Text = $"Credits: {subject.Credits}";
            FieldLabel.Text = $"Field: {subject.FieldOfKnowledge}";
            DurationLabel.Text = $"Total Duration: {subject.DurationTotal}";

            LessonsCollection.ItemsSource = _lessonService.GetAllLessonsBySubjectId(id);
        }
    }

    private async void OnLessonSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is LessonView lesson)
        {
            await Shell.Current.GoToAsync($"{nameof(LessonFullPage)}?lessonId={lesson.Id}");
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}