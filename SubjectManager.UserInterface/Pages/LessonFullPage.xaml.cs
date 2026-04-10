using SubjectManager.UserInterface.ViewModels;

namespace SubjectManager.UserInterface.Pages;

public partial class LessonFullPage : ContentPage
{
    
    private readonly LessonFullViewModel _viewModel;
    public LessonFullPage(LessonFullViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    override protected async void OnAppearing()
    {
        await _viewModel.LoadData();
    }

}