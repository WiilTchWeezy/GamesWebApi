using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesApi.Model
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
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
