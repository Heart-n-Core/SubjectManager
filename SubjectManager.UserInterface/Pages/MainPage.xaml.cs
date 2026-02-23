namespace SubjectManager.UserInterface.Pages;

using Services;
using SubjectManager.Model.View;

public partial class MainPage : ContentPage
{
    private readonly SubjectService _subjectService;

    public MainPage(SubjectService subjectService)
    {
        InitializeComponent();
        _subjectService = subjectService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        SubjectsCollection.ItemsSource = _subjectService.GetAllSubjects();
    }

    private async void OnSubjectSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is SubjectView selectedSubject)
        {
            await Shell.Current.GoToAsync(
                $"{nameof(SubjectFullPage)}?subjectId={selectedSubject.Id}");
        }
    }
}