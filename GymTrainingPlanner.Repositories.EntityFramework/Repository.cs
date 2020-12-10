namespace GymTrainingPlanner.Repositories.EntityFramework
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IRepository
    {
        TEntity Add<TEntity>(TEntity entity) where TEntity : class;
        bool Delete<TEntity>(TEntity entity) where TEntity : class;
        IQueryable<TEntity> FilterBy<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        TEntity Update<TEntity>(TEntity entity) where TEntity : class;
    }

    public class Repository : IRepository, IDisposable
    {
        private bool _disposed = false;
        private readonly GymTrainingPlannerDbContext _context;

        public Repository(GymTrainingPlannerDbContext context)
        {
            _context = context;
        }

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            return _context.Add(entity).Entity;
        }

        public bool Delete<TEntity>(TEntity entity) where TEntity : class
        {
            return _context.Remove(entity).State == EntityState.Deleted;
        }

        public IQueryable<TEntity> FilterBy<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return _context.Set<TEntity>().Where(filter);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : class
        {
            return _context.Update(entity).Entity;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
