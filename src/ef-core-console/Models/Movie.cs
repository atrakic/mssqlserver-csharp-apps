using System;
using System.ComponentModel.DataAnnotations;

namespace EfCoreConsole.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Name { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public int RuntimeMinutes { get; set; }
    }
}
