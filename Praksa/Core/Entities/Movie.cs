using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        [Column(TypeName = "Date")]
        public DateTime ReleaseDate { get; set; }
        [ForeignKey("Genre")]
        public long GenreId { get; set; }
        public Genre Genre { get; set; }
        public int RunTime { get; set; } 
    }
}
