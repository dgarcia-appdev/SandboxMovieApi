using System.Linq.Expressions;

namespace SandboxMovieApi.Infrastructure
{
    public interface IRepository<T>
    {
        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        public T Get(object id);
        public void Add(T entity);
        public int Update(T entity);
        public int Delete(T entity);
    }
}
