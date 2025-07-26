using Microsoft.AspNetCore.Http;

namespace Infotecs_WebApi.Application.Interfaces
{
    public interface IDataProcessingService
    {
        Task ParseCsvFileAsync(IFormFile file);
    }
}
