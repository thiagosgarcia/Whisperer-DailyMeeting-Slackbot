using System.Collections.Generic;
using System.Linq;
using Whisperer.Models;
using Whisperer.Persistence;

namespace MegaStore.Persistence
{
    public interface IRepository<T> : IBaseRepository<T>
        where T : Entity
    {
        IQueryable<T> Items { get; }
    }
}