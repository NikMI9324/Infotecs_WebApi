using Infotecs_WebApi.Application.QueryClass;
using Infotecs_WebApi.Domain.Entities;
namespace Infotecs_WebApi.Application.Interfaces
{
    public interface IResultQueryService
    {
        Task<IEnumerable<Result>> GetFilteredDataAsync(ResultFilterQuery filterQuery);
    }
}
