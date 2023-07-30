namespace WebScraper.Domain.Share
{
    public interface IBaseUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}