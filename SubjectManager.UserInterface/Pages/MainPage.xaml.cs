using Storage;
using SubjectManager.UserInterface.ViewModels;

namespace SubjectManager.UserInterface.Pages;

public partial class MainPage : ContentPage
{
    
    private readonly MainPageViewModel _viewModel;
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    override protected async void OnAppearing()
    {
        await _viewModel.LoadData();
    }
}