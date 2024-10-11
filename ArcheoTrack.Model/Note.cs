using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcheoTrack.Model {
    public class Note {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Project { get; set; }
        [DataType( DataType.Date )]
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true )]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime( DateTime.Now );
        public string? Object { get; set; }
        public int Number { get; set; }
        public string? Location { get; set; }
        public string? Weather { get; set; }
        public string? Results { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Sketch { get; set; }
        //public ICollection<Person> People { get; set; }
    }

}
