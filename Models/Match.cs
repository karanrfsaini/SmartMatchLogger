using System;
using System.ComponentModel.DataAnnotations;

namespace SmartMatchLogger.Models
{
    public class Match
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Opponent { get; set; }

        [Required]
        public bool Winner { get; set;}
        public string PlayStyle {get; set;}

        [Required]
        public string Location { get; set; }
        [Required]
        public string Surface { get; set; }
        [Required]
        public string Score { get; set; }
        [Required]
        public string MatchNotes { get; set; }

        public int FatigueLevel { get; set; }  // 1-5 all of these
        public int ForehandRating { get; set; }  
        public int BackhandRating { get; set; }
        public int ServeRating { get; set; }
        public int VolleyRating { get; set; }
    }
}
