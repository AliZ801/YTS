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
    public class QualityRepo : Repository<Quality>, IQualityRepo
    {
        private readonly ApplicationDbContext _db;

        public QualityRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDropDownListForQuality()
        {
            return _db.Qualities.Select(i => new SelectListItem()
            {
                Text = i.Qual,
                Value = i.Id.ToString()
            });
        }

        public void Update(Quality quality)
        {
            var qFromDb = _db.Qualities.FirstOrDefault(i => i.Id == quality.Id);

            qFromDb.Qual = quality.Qual;

            _db.SaveChanges();
        }
    }
}
