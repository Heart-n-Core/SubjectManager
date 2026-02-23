using SubjectManager.UserInterface.Pages;

namespace SubjectManager.UserInterface;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(SubjectFullPage), typeof(SubjectFullPage));
        Routing.RegisterRoute(nameof(LessonFullPage), typeof(LessonFullPage));
    }
}