using System.Collections.Generic;
using System.Linq;
using Whisperer.Models;

namespace Whisperer.Service
{
    public interface IService<T> : IReadOnlyService<T>
        where T: Entity
    {
        T Add(T entity);
        T Update(T entity);
        bool Delete(T entity);
        bool Delete(int id);

    }
    public interface IReadOnlyService<T>
        where T: Entity
    {
        IEnumerable<T> GetAll();
        T Get(int id);

    }
}