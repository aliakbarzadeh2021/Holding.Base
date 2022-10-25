using System;
using System.Linq;
using System.Linq.Expressions;

namespace Holding.Base.QueryService.SeedWorks.FTS
{
    public static class FullTextSearchExtension
    {
        public static IQueryable<TEntity> FreeText<TEntity>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, object>> expression, string searchTerm) where TEntity : class
        {
            return FreeTextSearchImp(source, expression, FullTextPrefixes.Freetext(searchTerm));
        }

        public static IQueryable<TEntity> ContainsText<TEntity>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, object>> expression, string searchTerm) where TEntity : class
        {
            return FreeTextSearchImp(source, expression, FullTextPrefixes.Contains(searchTerm));
        }

        private static IQueryable<TEntity> FreeTextSearchImp<TEntity>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, object>> expression, string searchTerm)
        {
            if (String.IsNullOrEmpty(searchTerm))
            {
                return source;
            }

            var searchTermExpression = Expression.Property(Expression.Constant(new {Value = searchTerm}), "Value");
            var checkContainsExpression = Expression.Call(expression.Body, typeof (string).GetMethod("Contains"),
                searchTermExpression);

            //Join not null and contains expressions

            var methodCallExpression = Expression.Call(typeof (Queryable),
                "Where",
                new[] {source.ElementType},
                source.Expression,
                Expression.Lambda<Func<TEntity, bool>>(checkContainsExpression, expression.Parameters));

            return source.Provider.CreateQuery<TEntity>(methodCallExpression);
        }
    }
}
