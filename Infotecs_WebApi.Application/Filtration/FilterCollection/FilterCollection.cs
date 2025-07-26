using Infotecs_WebApi.Application.Filtration.Filters;
using Infotecs_WebApi.Application.Interfaces;
using System.Linq.Expressions;

namespace Infotecs_WebApi.Application.Filtration.FilterCollection
{
    internal class FilterCollection<T> 
    {
        private readonly List<IFilter<T>> _filters = new();
        public FilterCollection<T> AddSingleFilter<TProperty>(
            Expression<Func<T, TProperty>> propertySelector, 
            TProperty value)
        {
            if (value is not null)
                _filters.Add(new SingleFilter<T, TProperty>(propertySelector, value));
            return this;
        }
        public FilterCollection<T> AddRangeFilter<TProperty>(
            Expression<Func<T, TProperty>> propertySelector, 
            TProperty? minValue, 
            TProperty? maxValue) where TProperty : struct, IComparable<TProperty>
        {
            if (minValue is not null || maxValue is not null)
                _filters.Add(new RangeFilter<T, TProperty>(propertySelector, minValue, maxValue));
            return this;
        }
        public IQueryable<T> Apply(IQueryable<T> query)
        {
            if (query is null)
                throw new ArgumentNullException(nameof(query), "Query cannot be null");
            
            foreach (var filter in _filters)
            {
                query = filter.Apply(query);
            }
            return query;
        }
    }
}
