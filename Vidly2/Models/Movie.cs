using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(50)]
        public Genre Genre { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ? ReleaseDate { get; set; }

        [Display(Name = "Added Date")]
        public DateTime ? AddedDate { get; set; }

        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }

    }
}