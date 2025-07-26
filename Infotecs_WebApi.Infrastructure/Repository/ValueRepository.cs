using Infotecs_WebApi.Domain.Entities;
using Infotecs_WebApi.Infrastructure.Data;
using Infotecs_WebApi.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infotecs_WebApi.Infrastructure.Repository
{
    public class ValueRepository : IValueRepository
    {
        private readonly InfotecsDbContext _context;
        public ValueRepository(InfotecsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Value>> GetValuesByFileNameAsync(string fileName)
        {
            return await _context.Values
                .Where(v => v.FileName == fileName)
                .OrderBy(v => v.Date)
                .Take(10)
                .ToListAsync();
        }

        public async Task UpsertDataAsync(IEnumerable<Value> data, string fileName)
        {
            if (data == null || !data.Any())
                return;
            var existingData = _context.Values
                .Where(e => e.FileName == fileName);
            if (existingData.Any())
                _context.Values.RemoveRange(existingData);
            await _context.Values.AddRangeAsync(data);
            await _context.SaveChangesAsync();
        }
    }
}
