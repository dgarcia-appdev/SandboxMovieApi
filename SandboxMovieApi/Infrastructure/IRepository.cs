using Microsoft.EntityFrameworkCore;
using SandboxMovieApi.Infrastructure.Persistance;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                return _dbSet.Where(filter).ToList();
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

        public int Update(T rating)
        {
            _dbSet.Update(rating);
            return _dbContext.SaveChanges();
        }
    }

    public interface IRepository<T>
    {
        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        public T Get(object id);
        public void Add(T rating);
        public int Update(T rating);
        public int Delete(T rating);
    }
}
