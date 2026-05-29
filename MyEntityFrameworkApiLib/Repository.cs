using Microsoft.EntityFrameworkCore;
using MyAspNetApiLib;
using System;

namespace MyEntityFrameworkApiLib
{
    /// <summary>
    /// Repositório padrão para operações no EntityFramework.
    /// </summary>
    /// <typeparam name="TEntity">A entidade ou modelo do banco de dados.</typeparam>
    /// <typeparam name="TContext">O DbContext da aplicação.</typeparam>
    public class Repository<TEntity, TContext> : IContextRepository<TContext>, IRepository<TEntity> 
        where TContext : DbContext where TEntity : class, new()
    {
        public TContext AppDbContext { get; private set; }

        public Repository(TContext appContext)
        {
            AppDbContext = appContext;
        }

        public IQueryable<TEntity> ToQuery()
        {
            return AppDbContext.Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> ToReadOnlyQuery()
        {
            return AppDbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<int> AddAndSaveAsync(TEntity entity)
        {
            if (entity == null)
                return 0;

            AppDbContext.Add(entity);
            return await SaveChangesAsync();
        }

        public async Task<int> UpdateAndSaveAsync(TEntity entity, bool force)
        {
            if (entity == null)
                return 0;

            if (force) AppDbContext.Update(entity);

            return await SaveChangesAsync();
        }

        public async Task<int> DeleteAndSaveAsync(TEntity entity)
        {
            if (entity == null)
                return 0;

            AppDbContext.Remove(entity);
            return await SaveChangesAsync();
        }

        public void AddRange(IEnumerable<TEntity> list)
        {
            if (list == null || !list.Any())
                return;

            AppDbContext.Set<TEntity>().AddRange(list);
        }

        public async Task<int> AddRangeAndSaveAsync(IEnumerable<TEntity> list)
        {
            if (list == null || !list.Any())
                return 0;

            AppDbContext.Set<TEntity>().AddRange(list);
            return await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await AppDbContext.SaveChangesAsync();
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
                return;

            AppDbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                return;

            AppDbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                return;

            AppDbContext.Set<TEntity>().Remove(entity);
        }
    }
}
