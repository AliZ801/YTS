using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YTS.Models
{
    public class Ratings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Rating { get; set; }
    }
}
