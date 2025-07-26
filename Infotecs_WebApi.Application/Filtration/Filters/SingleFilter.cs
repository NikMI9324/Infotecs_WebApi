using Infotecs_WebApi.Application.Interfaces;
using System.Linq.Expressions;

namespace Infotecs_WebApi.Application.Filtration.Filters
{
    internal class SingleFilter<T, TProperty> : IFilter<T>
    {
        private readonly Expression<Func<T, TProperty>> _propertySelector;
        private readonly TProperty _value;
        public SingleFilter(Expression<Func<T, TProperty>> propertySelector, TProperty value)
        {
            _propertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
            _value = value;
        }

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            if (query is null)
                return query;
            var parameter = Expression.Parameter(typeof(T), "item");
            var property = Expression.Invoke(_propertySelector, parameter);
            var constant = Expression.Constant(_value, typeof(TProperty));
            var equalsExpression = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(equalsExpression, parameter);
            return query.Where(lambda);
        }
    }
}
