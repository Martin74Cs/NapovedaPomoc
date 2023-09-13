using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace NapovedaPomoc.Entity
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public interface IEntityBase<T> where T : class, IEntity, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }

    public class EntityBase<T> : IEntityBase<T> where T : class, IEntity, new()
    {
        //Nazev předané databáze mustí mí nastaveno IEntity
        private readonly TractebelContext context;

        public EntityBase(TractebelContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// Přidání do databaze
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Detele v databaze radku dle ID
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            T entity = await context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            if (entity == null) return;
            EntityEntry entry = context.Entry<T>(entity);
            entry.State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id) => await context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);



        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task UpdateAsync(int id, T entity)
        {
            EntityEntry entityEntry = context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
