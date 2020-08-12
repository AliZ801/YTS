using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YTS.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Movies> MoviesList { get; set; }
    }
}
