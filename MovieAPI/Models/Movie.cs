using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public string id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string Length { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Director Director { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public List<Actor> Cast { get; set; }
    }

    public class Director
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class Actor
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
