using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FinanceApp.Data;

namespace FinanceApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "financeapp.db3");
            builder.Services.AddSingleton<FinanceDataContext>(_ => new FinanceDataContext(dbPath));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDataContext>();
                db.Database.Migrate();
            }

            return app;
        }
    }
}
