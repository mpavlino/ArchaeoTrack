using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcheoTrack.Model
{
    public class User {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Auth0UserId { get; set; }
        public string? Email { get; set; }
        public string Nickname { get; set; }
        public string FullName { get; set; }
        public ICollection<NoteUser> NoteUsers { get; set; } = new List<NoteUser>();
    }

}
