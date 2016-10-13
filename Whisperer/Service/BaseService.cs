using System.Collections.Generic;
using System.Linq;
using MegaStore.Persistence;
using MegaStore.Service;
using Whisperer.Models;

namespace Whisperer.Service
{
    public class BaseService<T> : ReadOnlyService<T>, IService<T> where T : Entity
    {
        public BaseService(IRepository<T> repository) : base(repository)
        {
            _repository = repository;
        }

        public virtual T Add(T entity)
        {
            OnBeforeAdd(entity);
            entity = _repository.Add(entity);
            OnAfterAdd(entity);
            var r = entity;
            return r;
        }

        public virtual T Update(T entity)
        {
            OnBeforeUpdate(entity);
            entity = _repository.Update(entity);
            OnAfterUpdate(entity);
            var r = entity;
            return r;
        }

        public virtual bool Delete(T entity)
        {
            OnBeforeDelete(entity);
            var r = _repository.Delete(entity);
            OnAfterDelete(entity);
            return r;
        }

        protected void OnBeforeAdd(T entity)
        {
            BeforeAdd?.Invoke(entity);
        }

        protected void OnAfterAdd(T entity)
        {
            AfterAdd?.Invoke(entity);
        }

        protected void OnBeforeUpdate(T entity)
        {
            BeforeUpdate?.Invoke(entity);
        }

        protected void OnAfterUpdate(T entity)
        {
            AfterUpdate?.Invoke(entity);
        }

        protected void OnBeforeDelete(T entity)
        {
            BeforeDelete?.Invoke(entity);
        }

        protected void OnAfterDelete(T entity)
        {
            AfterDelete?.Invoke(entity);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }


        protected event AfterHandler AfterAdd;
        protected event AfterHandler AfterDelete;
        protected event AfterHandler AfterUpdate;
        protected event BeforeHandler BeforeAdd;
        protected event BeforeHandler BeforeDelete;
        protected event BeforeHandler BeforeUpdate;
        protected delegate void AfterHandler(T entity);
        protected delegate void BeforeHandler(T entity);
    }
}