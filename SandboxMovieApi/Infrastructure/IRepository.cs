using SandboxMovieApi.Infrastructure.Entities;
using SandboxMovieApi.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace SandboxMovieApi.Infrastructure
{
    public class RatingRepository : IRepository<Rating>
    {
        private readonly AppDbContext _dbContext;

        public RatingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Rating rating)
        {
            var ratingDb = _dbContext.Rating.Add(rating);
            _dbContext.SaveChanges();            
        }

        public int Delete(Rating rating)
        {
            _dbContext.Remove(rating);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Rating> Get(Expression<Func<Rating, bool>> filter = null)
        {
            if (filter != null)
            {
                return _dbContext.Rating.Where(filter).ToList();
            }


            return _dbContext.Rating.ToList();
        }

        public Rating? Get(byte id)
        {            
            var rating = _dbContext.Rating.Find(id);
            return rating;
        }

        public int Update(Rating rating)
        {
            _dbContext.Update(rating);
            return _dbContext.SaveChanges();
        }
    }

    public interface IRepository<T>
    {
        public IEnumerable<T> Get(Expression<Func<Rating, bool>> filter = null);
        public T Get(byte id);
        public void Add(T rating);
        public int Update(T rating);
        public int Delete(Rating rating);
    }
}
