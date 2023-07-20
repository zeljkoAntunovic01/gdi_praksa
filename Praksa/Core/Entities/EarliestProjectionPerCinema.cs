using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class EarliestProjectionPerCinema
    {   
        public long CinemaId { get; set; }
        public string CinemaName { get; set; }
        public double CinemaLatitude { get; set; }
        public double CinemaLongitude { get; set; }
        public string CinemaAdress { get; set; }
        public string MovieTitle { get; set; }  
        public DateTime ProjectionDateTime { get; set; }
        public string GenreName { get; set; }
        public int RunTime { get; set; }
        public string ProjectionType { get; set; }
    }
}
