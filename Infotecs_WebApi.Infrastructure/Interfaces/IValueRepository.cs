using Infotecs_WebApi.Domain.Entities;

namespace Infotecs_WebApi.Infrastructure.Interfaces
{
    public interface IValueRepository
    {
        Task UpsertDataAsync(IEnumerable<Value> data, string fileName);
        Task<IEnumerable<Value>> GetValuesByFileNameAsync(string fileName);
    }
}
