using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;
using MegaStore.Persistence;
using Whisperer.Models;
using Whisperer.Service;

namespace MegaStore.Service
{
    public class ReadOnlyService<T> : IReadOnlyService<T> where T : Entity
    {
        protected IRepository<T> _repository;

        public ReadOnlyService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.Items.OrderBy(x => x.Id);
        }

        public virtual T Get(int id)
        {
            return _repository.Items.SingleOrDefault(x => x.Id == id);
        }
    }
}