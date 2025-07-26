using Infotecs_WebApi.Domain.Entities;

namespace Infotecs_WebApi.Infrastructure.Interfaces
{
    public interface IResultRepository
    {
        Task AddOrUpdateAsync(Result result);
    }
}
