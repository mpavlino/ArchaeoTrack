using ArcheoTrack.DAL;
using ArcheoTrack.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ArchaeoTrack {
    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts( fonts => {
                    fonts.AddFont( "OpenSans-Regular.ttf", "OpenSansRegular" );
                    fonts.AddFont( "OpenSans-Semibold.ttf", "OpenSansSemibold" );
                } );

            builder.Services.AddDbContext<AppDbContext>( options =>
            {
                var dbPath = Path.Combine( FileSystem.AppDataDirectory, "notes.db" );
                options.UseSqlite( $"Filename={dbPath}" );
            } );

            builder.Services.AddMauiBlazorWebView();
            //builder.Services.AddScoped( sp => new HttpClient { BaseAddress = new Uri( "https://localhost:7250/" ) } );
            builder.Services.AddScoped<NotesRepository>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            InitializeDatabase( builder.Build().Services );

            return builder.Build();
        }

        private static void InitializeDatabase( IServiceProvider serviceProvider ) {
            using( var scope = serviceProvider.CreateScope() ) {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated(); // Creates the database if it doesn't exist
            }
        }

    }
}
