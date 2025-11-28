using System;
using System.Collections.Generic;
using System.Text;
using Services.Interfaces;
using Models;

namespace Services.InMemory
{
    public class Service<T> : IService<T> where T : Entity
    {
        private readonly ICollection<T> _entities;
        public Service() : this([])
        {
        }
        public Service(ICollection<T> entities)
        {
            _entities = entities;
        }

        public Task<int> Create(T entity)
        {
            entity.Id = _entities.Select(x => x.Id).DefaultIfEmpty().Max() + 1;
            _entities.Add(entity);
            return Task.FromResult(entity.Id);
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(_entities.AsEnumerable());
        }

        public Task<T?> GetByIdAsync(int id)
        {
            var entity = _entities.SingleOrDefault(e => e.Id == id);
            return Task.FromResult(entity);
        }
    }
}
