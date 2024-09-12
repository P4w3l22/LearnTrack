using LearnTrack.MVVM.Models;
using LearnTrack.Repositories;
using LearnTrack.Repositories.IRepository;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using LearnTrack.Services;
using LearnTrack.MVVM.ViewModels;
using Microcharts.Maui;

namespace LearnTrack
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("calendar.ttf", "Icons");
            });
            builder.Services.AddSingleton<IBaseRepository<Subject>, BaseRepository<Subject>>();
            builder.Services.AddSingleton<IBaseRepository<Topic>, BaseRepository<Topic>>();
            builder.Services.AddSingleton<IBaseRepository<TopicNote>, BaseRepository<TopicNote>>();
            builder.Services.AddSingleton<IBaseRepository<DailyNote>, BaseRepository<DailyNote>>();

            builder.Services.AddSingleton<ICalendarService, CalendarService>();
            builder.Services.AddSingleton<IStatisticsService, StatisticsService>();
#if DEBUG
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<CalendarViewModel>();

            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}