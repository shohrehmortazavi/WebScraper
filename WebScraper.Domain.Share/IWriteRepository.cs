namespace WebScraper.Domain.Share
{
    public interface IWriteRepository<T> where T : Entity
    {
        Task CreateAsync(T entity);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(T entity);
    }
}
