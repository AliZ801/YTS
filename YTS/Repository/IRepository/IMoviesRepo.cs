using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTS.Models;

namespace YTS.Repository.IRepository
{
    public interface IMoviesRepo : IRepository<Movies>
    {
        void Update(Movies movies);
    }
}
