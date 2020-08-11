using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTS.Data;
using YTS.Repository.IRepository;

namespace YTS.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _db;

        public UnitofWork(ApplicationDbContext db)
        {
            _db = db;
            Quality = new QualityRepo(_db);
            Genres = new GenresRepo(_db);
            Ratings = new RatingsRepo(_db);
            Movies = new MoviesRepo(_db);
        }

        public IQualityRepo Quality { get; private set; }

        public IGenresRepo Genres { get; private set; }

        public IRatingsRepo Ratings { get; private set; }

        public IMoviesRepo Movies { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
