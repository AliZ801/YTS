using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YTS.Models
{
    public class Movies
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int QualityId { get; set; }

        [ForeignKey("QualityId")]
        public Quality Quality { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public float Rating { get; set; }

        [Required]
        public string Date { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Art")]
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }
    }
}