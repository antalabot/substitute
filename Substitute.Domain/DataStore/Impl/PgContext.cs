using Microsoft.EntityFrameworkCore;
using Npgsql;
using Substitute.Domain.Data;
using Substitute.Domain.Data.Entities;
using Substitute.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Substitute.Domain.DataStore.Impl
{
    public class PgContext : DbContext, IContext
    {
        #region Database Sets
        public DbSet<User> Users { get; set; }
        public DbSet<GuildRole> GuildRoles { get; set; }
        public DbSet<ImageResponse> ImageReponses { get; set; }
        #endregion

        #region Configuration
        public PgContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<EAccessLevel>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<ERole>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(Settings.PostgresConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.ForNpgsqlHasEnum<EAccessLevel>()
                        .ForNpgsqlHasEnum<ERole>();
        }
        #endregion

        #region Interface implementation
        public async new Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IEntity
        {
            await base.AddAsync(entity, cancellationToken);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity
        {
            base.AddRange(entities);
        }

        public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, IEntity
        {
            await base.AddRangeAsync(entities, cancellationToken);
        }

        public IQueryable<TEntity> Get<TEntity>() where TEntity : class, IEntity
        {
            return base.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class, IEntity
        {
            return await Task.FromResult(base.Set<TEntity>());
        }

        public TEntity GetById<TEntity>(ulong id) where TEntity : class, IEntity
        {
            return base.Set<TEntity>().SingleOrDefault(e => e.Id == id);
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(ulong id, CancellationToken cancellationToken = default) where TEntity : class, IEntity
        {
            return await base.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IEntity
        {
            await Task.FromResult(base.Remove(entity));
        }

        public void RemoveById<TEntity>(ulong id) where TEntity : class, IEntity
        {
            base.Remove(GetById<TEntity>(id));
        }

        public async Task RemoveByIdAsync<TEntity>(ulong id, CancellationToken cancellationToken = default) where TEntity : class, IEntity
        {
            base.Remove(await GetByIdAsync<TEntity>(id, cancellationToken));
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity
        {
            base.RemoveRange(entities);
        }

        public async Task RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, IEntity
        {
            await Task.Factory.StartNew(() => base.RemoveRange(entities), cancellationToken);
        }

        public async Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IEntity
        {
            await Task.Factory.StartNew(() => base.Update(entity), cancellationToken);
        }

        public void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity
        {
            base.UpdateRange(entities);
        }

        public async Task UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, IEntity
        {
            await Task.Factory.StartNew(() => base.UpdateRange(entities), cancellationToken);
        }

        void IContext.Add<TEntity>(TEntity entity)
        {
            base.Add(entity);
        }

        void IContext.Remove<TEntity>(TEntity entity)
        {
            base.Remove(entity);
        }

        void IContext.Update<TEntity>(TEntity entity)
        {
            base.Update(entity);
        }

        async Task<IQueryable<TEntity>> IContext.GetAsync<TEntity>(CancellationToken cancellationToken)
        {
            return await Task.FromResult(base.Set<TEntity>());
        }
        #endregion
    }
}
