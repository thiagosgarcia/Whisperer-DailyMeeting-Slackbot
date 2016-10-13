using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Whisperer.Models;

namespace MegaStore.Persistence
{
    public class Repository<T, D> : BaseRepository<T, D>, IRepository<T>
        where T : Entity
        where D : DbContext
    {
        public Repository(D context)
            : base(context)
        {
        }

        public IEnumerable<T> Items { get { lock (Locker) { return Context.Set<T>(); } } }

    }
}