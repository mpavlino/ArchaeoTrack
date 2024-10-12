using ArcheoTrack.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcheoTrack.DAL {
    public class AppDbContext : DbContext {

        public AppDbContext( DbContextOptions<AppDbContext> options ) : base( options ) {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<NoteUser> NoteUsers { get; set; }

        //protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) {
        //    var dbPath = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData ), "notes.db" );
        //    optionsBuilder.UseSqlite( $"Filename={dbPath}" );
        //}
    }

}
