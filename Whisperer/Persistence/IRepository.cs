using System.Collections.Generic;
using System.Linq;
using Whisperer.Models;

namespace MegaStore.Persistence
{
    public interface IRepository<T> : IBaseRepository<T>
        where T : Entity
    {
        IEnumerable<T> Items { get; }
    }
}