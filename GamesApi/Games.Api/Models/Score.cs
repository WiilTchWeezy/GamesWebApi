using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Games.Api.Models
{
    public class Score
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public int ScorePoint { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}