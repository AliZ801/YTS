using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YTS.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        IQualityRepo Quality { get; }

        IMoviesRepo Movies { get; }

        void Save();
    }
}
