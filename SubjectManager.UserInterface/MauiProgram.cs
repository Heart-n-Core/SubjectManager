using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Services;
using Services.Storage;
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
        
        builder.Services.AddSingleton<ILessonRepository, LessonRepository>();
        
        builder.Services.AddSingleton<ISubjectRepository>(sp =>
        {
            var lessonRepo = sp.GetRequiredService<ILessonRepository>();
            return new SubjectRepository(lessonRepo);
        });
        
        builder.Services.AddSingleton<ILessonService>(sp =>
        {
            var lessonRepo = sp.GetRequiredService<ILessonRepository>();
            return new LessonService(lessonRepo);
        });
        
        builder.Services.AddSingleton<ISubjectService>(sp =>
        {
            var subjectRepo = sp.GetRequiredService<ISubjectRepository>();
            return new SubjectService(subjectRepo);
        });
        
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<MainPage>();
        
        builder.Services.AddTransient<SubjectFullViewModel>();
        builder.Services.AddTransient<SubjectFullPage>();
        
        builder.Services.AddTransient<LessonFullViewModel>();
        builder.Services.AddTransient<LessonFullPage>();
        
        return builder.Build();
    }
}