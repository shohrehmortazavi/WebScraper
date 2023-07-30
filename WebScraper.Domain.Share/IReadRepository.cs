using System.Linq.Expressions;

namespace WebScraper.Domain.Share
{
    public interface IReadRepository<T> where T : Entity
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);

        Task<T> GetAsync(Guid id);

        Task<T> GetAsync(Expression<Func<T, bool>> filter);
    }
}
