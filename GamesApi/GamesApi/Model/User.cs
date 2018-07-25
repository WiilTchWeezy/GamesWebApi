using System;
using System.ComponentModel.DataAnnotations;

namespace GamesApi.Model
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }

        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
