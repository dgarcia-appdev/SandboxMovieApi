using SandboxMovieApi.Entities;
using SandboxMovieApi.Infrastructure.Persistance;

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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rating> Get()
        {
            return _dbContext.Rating.ToList();
        }

        public Rating Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Rating rating)
        {
            throw new NotImplementedException();
        }
    }

    public interface IRepository<T>
    {
        public IEnumerable<T> Get();
        public T Get(int id);
        public void Add(T rating);
        public void Update(T rating);
        public void Delete(int id);
    }
}
