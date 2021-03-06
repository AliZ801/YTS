﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTS.Data;
using YTS.Models;
using YTS.Repository.IRepository;

namespace YTS.Repository
{
    public class MoviesRepo : Repository<Movies>, IMoviesRepo
    {
        private readonly ApplicationDbContext _db;

        public MoviesRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Movies movies)
        {
            var mFromDb = _db.Movies.FirstOrDefault(i => i.Id == movies.Id);

            mFromDb.Title = movies.Title;
            mFromDb.QualityId = movies.QualityId;
            mFromDb.Genre = movies.Genre;
            mFromDb.Rating = movies.Rating;
            mFromDb.Date = movies.Date;
            mFromDb.ImageUrl = movies.ImageUrl;
            mFromDb.Description = movies.Description;

            _db.SaveChanges();
        }
    }
}
