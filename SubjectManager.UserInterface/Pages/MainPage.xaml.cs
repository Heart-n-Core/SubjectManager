using SubjectManager.UserInterface.ViewModels;

namespace SubjectManager.UserInterface.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    
        if (BindingContext is MainPageViewModel vm)
        {
            vm.LoadSubjectsCommand.Execute(null);
        }
    }
}