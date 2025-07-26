using Infotecs_WebApi.Domain.Entities;
using Infotecs_WebApi.Infrastructure.Data;
using Infotecs_WebApi.Infrastructure.Interfaces;

namespace Infotecs_WebApi.Infrastructure.Repository
{
    public class ResultRepository : IResultRepository
    {
        private readonly InfotecsDbContext _context;
        public ResultRepository(InfotecsDbContext context)
        {
            _context = context;
        }
        public async Task AddOrUpdateAsync(Result result)
        {
            if (result == null || string.IsNullOrEmpty(result.FileName))
                return;
            var existingResult = await _context.Results.FindAsync(result.FileName);
            if (existingResult != null)
                await this.UpdateAsync(result);
            else
                await this.AddAsync(result);

        }

        private async Task AddAsync(Result result)
        {
            await _context.Results.AddAsync(result);
            await _context.SaveChangesAsync();
        }
        private async Task UpdateAsync(Result result)
        {
            _context.Results.Update(result);
            await _context.SaveChangesAsync();
        }
    }
}
