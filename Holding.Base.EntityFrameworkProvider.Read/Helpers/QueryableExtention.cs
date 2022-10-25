using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Holding.Base.EntityFrameworkProvider.Read.Filtering;

namespace Holding.Base.EntityFrameworkProvider.Read.Helpers
{
    public static class QueryableExtention
    {
        private static Filter FilterModel = null;
        public static IQueryable<T> AsQueryable<T>(this IQueryable<T> queryable, Filter filter, Expression<Func<T, bool>> andWhereExpression)
        {
            FilterModel = filter;
            var dto = typeof(T);
            var param = Expression.Parameter(typeof(T), "t");
            Expression temp = null;
            foreach (var field in filter.Fields)
            {
                var propertyInfo = dto.GetProperty(field.Key);
                if (propertyInfo != null)
                {
                    MemberExpression member = Expression.Property(param, propertyInfo.Name);
                    ConstantExpression constant = Expression.Constant(field.Value);
                    Expression expression;
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        if (field.Value.ToString().StartsWith("\"") && field.Value.ToString().EndsWith("\""))
                        {
                            expression = Expression.Equal(member, constant);
                        }
                        else
                        {
                            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                            expression = Expression.Call(member, method, constant);
                        }
                    }
                    else
                    {
                        expression = Expression.Equal(member, constant);
                    }

                    if (temp == null)
                    {
                        temp = expression;
                    }
                    else
                    {
                        temp = FilterModel.FielsConditionType == ConditionType.And ? Expression.AndAlso(temp, expression) : Expression.Or(temp, expression);
                    }
                }
            }
            if (!filter.Fields.Any())
            {
                queryable = queryable.Where(andWhereExpression);
                return queryable;
            }

            Expression<Func<T, bool>> cluser = Expression.Lambda<Func<T, bool>>(temp, param);
            queryable = queryable.Where(cluser);

            queryable = queryable.Where(andWhereExpression);
            return queryable;
        }

        public static IQueryable<T> ApplyOrderBy<T>(this IQueryable<T> queryable)
        {
            var dto = typeof(T);
            var param = Expression.Parameter(typeof(T), "t");

            var orderByType = FilterModel.GetOrderType();
            if (string.IsNullOrEmpty(FilterModel.OrderByOn) || string.IsNullOrWhiteSpace(FilterModel.OrderByOn) || orderByType == OrderByType.None || dto.GetProperty(FilterModel.OrderByOn) == null)
                return queryable.Skip(FilterModel.PageIndex * FilterModel.PageSize).Take(FilterModel.PageSize);

            var propertyInfo = dto.GetProperty(FilterModel.OrderByOn);

            Expression orderByProperty = Expression.Property(param, propertyInfo.Name);
            var orderByExpression = Expression.Lambda<Func<T, string>>
                (orderByProperty, new[] { param });

            queryable = FilterModel.GetOrderType() == OrderByType.Asc
                ? queryable.OrderBy(orderByExpression)
                : queryable.OrderByDescending(orderByExpression);

            queryable = queryable.Skip(FilterModel.PageIndex * FilterModel.PageSize).Take(FilterModel.PageSize);
            return queryable;
        }
    }
}
