using SubjectManager.UserInterface.ViewModels;

namespace SubjectManager.UserInterface.Pages;

public partial class LessonFullPage : ContentPage
{
    public LessonFullPage(LessonFullViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}