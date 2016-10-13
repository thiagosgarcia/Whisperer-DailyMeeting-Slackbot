using System;
using System.Collections.Generic;
using Whisperer.Models;

namespace MegaStore.Persistence
{
    public interface IBaseRepository<T> : IDisposable
        where T : Entity
    {
        T Get(int id);
        T Add(T entity);
        ICollection<T> Add(ICollection<T> entity);
        T Update(T entity);
        bool Delete(T entity);
        bool Delete(int id);
        int Save();
    }
}