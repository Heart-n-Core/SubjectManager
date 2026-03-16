using SubjectManager.UserInterface.ViewModels;

namespace SubjectManager.UserInterface.Pages;

public partial class SubjectFullPage : ContentPage
{
    public SubjectFullPage(SubjectFullViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    
}