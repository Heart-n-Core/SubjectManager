using CommunityToolkit.Maui;
using Services;
using Storage;
using SubjectManager.UserInterface.Pages;
using SubjectManager.UserInterface.ViewModels;

namespace SubjectManager.UserInterface;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        builder.Services.AddSingleton<IStorage, FileStorage>();
        
        builder.Services.AddSingleton<ILessonRepository, LessonRepository>();
        builder.Services.AddSingleton<ISubjectRepository, SubjectRepository>();
        builder.Services.AddSingleton<ILessonService, LessonService>();
        builder.Services.AddSingleton<ISubjectService, SubjectService>();
        
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<MainPage>();
        
        builder.Services.AddTransient<SubjectFullViewModel>();
        builder.Services.AddTransient<SubjectFullPage>();
        
        builder.Services.AddTransient<LessonFullViewModel>();
        builder.Services.AddTransient<LessonFullPage>();
        
        builder.Services.AddTransient<LessonEditViewModel>();
        builder.Services.AddTransient<LessonEditPage>();
        
        builder.Services.AddTransient<SubjectEditViewModel>();
        builder.Services.AddTransient<SubjectEditPage>();
        
        return builder.Build();
    }
}