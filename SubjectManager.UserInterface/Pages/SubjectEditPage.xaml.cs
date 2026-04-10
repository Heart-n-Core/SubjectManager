using SubjectManager.UserInterface.ViewModels;

namespace SubjectManager.UserInterface.Pages;

public partial class SubjectEditPage : ContentPage
{
    private readonly SubjectEditViewModel _viewModel;
    
    public SubjectEditPage(SubjectEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = _viewModel = vm;
    }

    protected async override void OnAppearing()
    {
        await _viewModel.LoadData();
    }
}