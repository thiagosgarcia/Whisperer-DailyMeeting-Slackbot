using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Whisperer.Models;
using Whisperer.Persistence;

namespace MegaStore.Persistence
{
    public class BaseRepository<T, D> : IBaseRepository<T> 
        where T: Entity
        where D : DbContext
    {
        protected static Object Locker = new Object();
        protected readonly D Context;

        public BaseRepository(D context)
        {
            this.Context = context;
        }

        public T Get(long id)
        {
            lock (Locker)
            {
                return Context.Set<T>().SingleOrDefault(x => x.Id == id);
            }
        }

        public T Add(T entity)
        {
            lock (Locker)
            {
                var item = Context.Set<T>().Add(entity);
                Save();
                return item;
            }
        }

        public ICollection<T> Add(ICollection<T> entity)
        {
            lock (Locker)
            {
                var list = new Collection<T>();
                foreach (var item in entity)
                {
                    list.Add(Add(item));
                }
                Save();
                return list;
            }
        }

        public T Update(T entity)
        {
            lock (Locker)
            {
                var item = Context.Entry(entity);
                var returnItem = item.Entity;

                if (item.State == EntityState.Detached)
                {
                    var set = Context.Set<T>();
                    var attachedEntity = set.Local.SingleOrDefault(x => x.Id == entity.Id);

                    if (attachedEntity != null)
                    {
                        var attachedEntry = Context.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(entity);
                        returnItem = attachedEntity;
                    }
                    else
                    {
                        item.State = EntityState.Modified;
                        returnItem = item.Entity;
                    }
                }
                Save();
                return returnItem;
            }
        }

        public bool Delete(T entity)
        {
            lock (Locker)
            {
                Context.Set<T>().Remove(entity);
                Save();
            }
            return true;
        }

        public bool Delete(long id)
        {
            lock (Locker)
            {
                var item = Context.Set<T>().SingleOrDefault(x => x.Id == id);
                if (item == null)
                    return false;

                return Delete(item);
            }
        }

        public int Save()
        {
            lock (Locker)
            {
                return Context.SaveChanges();
            }
        }

        public void Dispose()
        {
            GC.ReRegisterForFinalize(this);
        }
    }
}