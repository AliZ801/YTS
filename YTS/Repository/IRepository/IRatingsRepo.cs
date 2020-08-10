using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTS.Models;

namespace YTS.Repository.IRepository
{
    public interface IRatingsRepo : IRepository<Ratings>
    {
        IEnumerable<SelectListItem> GetDropDownListForRatings();

        void Update(Ratings ratings);
    }
}
