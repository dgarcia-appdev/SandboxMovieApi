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

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return _dbSet.Where(filter).ToList();
            }


            return _dbSet.ToList();
        }

        public T? Get(object id)
        {            
            return _dbSet.Find(id);            
        }

        public int Update(T rating)
        {
            _dbSet.Update(rating);
            return _dbContext.SaveChanges();
        }
    }

    public interface IRepository<T>
    {
        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null);
        public T Get(object id);
        public void Add(T rating);
        public int Update(T rating);
        public int Delete(T rating);
    }
}
