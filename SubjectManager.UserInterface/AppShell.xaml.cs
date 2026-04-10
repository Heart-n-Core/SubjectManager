using Services;
using Microsoft.Maui.Controls;
using SubjectManager.UserInterface.Pages;

namespace SubjectManager.UserInterface;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(SubjectFullPage), typeof(SubjectFullPage));
        Routing.RegisterRoute(nameof(LessonFullPage), typeof(LessonFullPage));
        Routing.RegisterRoute(nameof(LessonEditPage), typeof(LessonEditPage));
        Routing.RegisterRoute(nameof(SubjectEditPage), typeof(SubjectEditPage));
    }
}