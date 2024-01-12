using System;
using System.ComponentModel.DataAnnotations;

namespace EfCoreConsole.Models
{
    public class Sample
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
    }

    public class Foo {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
