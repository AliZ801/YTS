using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YTS.Models
{
    public class Quality
    {
        [Key]
        public int Id { get; set; }

        public string Qual { get; set; }
    }
}
