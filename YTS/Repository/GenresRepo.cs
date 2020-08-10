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
    public class GenresRepo : Repository<Genres>, IGenresRepo
    {
        private readonly ApplicationDbContext _db;

        public GenresRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDropDownListForGenres()
        {
            return _db.Genres.Select(i => new SelectListItem()
            {
                Text = i.Genre,
                Value = i.Id.ToString()
            });
        }

        public void Update(Genres genres)
        {
            var gFromDb = _db.Genres.FirstOrDefault(i => i.Id == genres.Id);

            gFromDb.Genre = genres.Genre;

            _db.SaveChanges();
        }
    }
}
