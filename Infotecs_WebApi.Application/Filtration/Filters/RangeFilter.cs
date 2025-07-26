using Infotecs_WebApi.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infotecs_WebApi.Application.Filtration.Filters
{
    internal class RangeFilter<T, TProperty> : IFilter<T> where TProperty : struct, IComparable<TProperty>
    {
        private readonly Expression<Func<T, TProperty>> _propertySelector;
        private readonly TProperty? _minValue;
        private readonly TProperty? _maxValue;

        public RangeFilter(Expression<Func<T, TProperty>> propertySelector, TProperty? minValue, TProperty? maxValue)
        {
            _propertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
            _minValue = minValue ?? throw new ArgumentNullException(nameof(minValue));
            _maxValue = maxValue ?? throw new ArgumentNullException(nameof(maxValue));
        }
        public IQueryable<T> Apply(IQueryable<T> query)
        {
            if(query is null)
                return query;
            return query.Where(CreateRangeExpression());
        }
        private Expression<Func<T, bool>> CreateRangeExpression()
        {
            var parameter = Expression.Parameter(typeof(T), "item");
            var property = Expression.Invoke(_propertySelector, parameter);
            var minConstant = Expression.Constant(_minValue, typeof(TProperty));
            var maxConstant = Expression.Constant(_maxValue, typeof(TProperty));
            var greaterThanOrEqual = Expression.GreaterThanOrEqual(property, minConstant);
            var lessThanOrEqual = Expression.LessThanOrEqual(property, maxConstant);
            var andExpression = Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual);
            return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
        }
    }
}
