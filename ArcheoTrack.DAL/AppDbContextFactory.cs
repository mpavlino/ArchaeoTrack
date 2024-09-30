using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ArcheoTrack.DAL {
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext> {
        public AppDbContext CreateDbContext( string[] args ) {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Configure the database connection (use SQLite for MAUI Blazor or SQL Server for development)
            optionsBuilder.UseSqlite( "Data Source=notes.db" );

            return new AppDbContext( optionsBuilder.Options );
        }
    }
}
