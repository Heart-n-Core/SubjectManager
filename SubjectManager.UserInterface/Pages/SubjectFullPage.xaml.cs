using SubjectManager.UserInterface.ViewModels;

namespace SubjectManager.UserInterface.Pages;

public partial class SubjectFullPage : ContentPage
{
    private readonly SubjectFullViewModel _viewModel;
    public SubjectFullPage(SubjectFullViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    override protected async void OnAppearing()
    {
        await _viewModel.LoadData();
    }
}