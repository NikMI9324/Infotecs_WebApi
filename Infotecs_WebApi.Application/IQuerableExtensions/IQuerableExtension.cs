using Infotecs_WebApi.Application.Filtration.FilterCollection;

namespace Infotecs_WebApi.Application.IQuerableExtensions
{
    internal static class IQuerableExtension
    {
        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, FilterCollection<T> filters)
        {
            if (query is null)
                throw new ArgumentNullException(nameof(query), "Query cannot be null");
            return filters.Apply(query);
        }
    }
}
