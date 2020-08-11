using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YTS.Models.ViewModels
{
    public class AdminVM
    {
        public Quality Quality { get; set; }

        public Movies Movies { get; set; }

        //DropDown Lists
        public IEnumerable<SelectListItem> QualityList { get; set; }
    }
}
