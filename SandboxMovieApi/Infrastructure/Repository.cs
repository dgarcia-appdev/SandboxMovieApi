using Microsoft.EntityFrameworkCore;
using SandboxMovieApi.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace SandboxMovieApi.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            var ratingDb = _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.ToList();
        }

        public T? Get(object id)
        {
            return _dbSet.Find(id);
        }

        public int Update(T entity)
        {
            _dbSet.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
