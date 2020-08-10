using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTS.Data;
using YTS.Models;
using YTS.Repository.IRepository;

namespace YTS.Repository
{
    public class RatingsRepo : Repository<Ratings>, IRatingsRepo
    {
        private readonly ApplicationDbContext _db;

        public RatingsRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDropDownListForRatings()
        {
            return _db.Ratings.Select(i => new SelectListItem()
            {
                Text = i.Rating,
                Value = i.Id.ToString()
            });
        }

        public void Update(Ratings ratings)
        {
            var rFromDb = _db.Ratings.FirstOrDefault(i => i.Id == ratings.Id);

            rFromDb.Rating = ratings.Rating;

            _db.SaveChanges();
        }
    }
}
