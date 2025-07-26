using Infotecs_WebApi.Application.Filtration.FilterCollection;
using Infotecs_WebApi.Application.Interfaces;
using Infotecs_WebApi.Application.IQuerableExtensions;
using Infotecs_WebApi.Application.QueryClass;
using Infotecs_WebApi.Domain.Entities;
using Infotecs_WebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infotecs_WebApi.Application.Services
{
    public class ResultQueryService : IResultQueryService
    {
        private readonly InfotecsDbContext _context;
        private readonly FilterCollection<Result> _filterCollection = new FilterCollection<Result>();
        public ResultQueryService(InfotecsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Database context cannot be null");
        }
        public async Task<IEnumerable<Result>> GetFilteredDataAsync(ResultFilterQuery filterQuery)
        {
            _filterCollection.AddSingleFilter(r => r.FileName, filterQuery.FileName)
                            .AddRangeFilter(r => r.MinimalDate, filterQuery.StartTimeFrom, filterQuery.StartTimeTo)
                            .AddRangeFilter(r => r.AverageValueDefinition, filterQuery.AverageValueFrom, filterQuery.AverageValueTo)
                            .AddRangeFilter(r => r.AverageExecutionTime, filterQuery.AverageExecutionTimeFrom, filterQuery.AverageValueTo);
            
            return await _context.Results
                .ApplyFilters(_filterCollection)
                .ToListAsync();
        }
    }
}
