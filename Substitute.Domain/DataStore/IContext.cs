using Substitute.Domain.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Substitute.Domain.DataStore
{
    public interface IContext
    {
        Task<IQueryable<TEntity>> GetAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class, IEntity;
        Task<TEntity> GetByIdAsync<TEntity>(ulong id, CancellationToken cancellationToken = default) where TEntity : class, IEntity;
        Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IEntity;
        Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, IEntity;
        Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IEntity;
        Task UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, IEntity;
        Task RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IEntity;
        Task RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, IEntity;
        Task RemoveByIdAsync<TEntity>(ulong id, CancellationToken cancellationToken = default) where TEntity : class, IEntity;

        IQueryable<TEntity> Get<TEntity>() where TEntity : class, IEntity;
        TEntity GetById<TEntity>(ulong id) where TEntity : class, IEntity;
        void Add<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;
        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;
        void Remove<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;
        void RemoveById<TEntity>(ulong id) where TEntity : class, IEntity;

        int SaveChanges();
    }
}
