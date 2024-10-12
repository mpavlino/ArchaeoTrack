using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcheoTrack.Model {
    public class NoteUser {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int UserId { get; set; }
    }
}
