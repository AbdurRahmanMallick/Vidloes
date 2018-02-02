using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidloes.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

       

       
        [Required]
        public byte GenreId { get; set; }

        

      
        [Required]
        public DateTime ReleaseDate { get; set; }

      
        [Range(1, 20)]
        [Required(ErrorMessage = "The field Number in stock must be between 1 and 20")]
        public byte NumberInStock { get; set; }

     
    }
}