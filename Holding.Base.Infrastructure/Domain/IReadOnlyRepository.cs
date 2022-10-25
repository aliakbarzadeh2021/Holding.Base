using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Holding.Base.Infrastructure.Domain
{
    //this class after marge must be delete.
    public interface IReadOnlyRepository<T, in TEntityKey> : IAggregateRoot
    {
        IQueryable<T> FindAll();

        /// <summary>
        /// returns all documents by fieldname that equals to a value.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> FindAll(string fieldName, object value);

        /// <summary>
        /// returns count of documents by fieldname that equals to a value.
        /// </summary>
        /// <returns></returns>
        long Count(string fieldName, object value);


        IQueryable<T> FindBy(params TEntityKey[] ids);
        Task<T> FindBy(TEntityKey id);

        /// <summary>
        /// SELECT listField FROM document WHERE filterField;
        /// </summary>
        /// <typeparam name="TClassField"></typeparam>
        /// <param name="filterField">Expression to find subdocument. like t => t.Id == aClass.Id.</param>
        /// <param name="listField">Key field to find subdocument list. like s => s.subDocList.</param>
        /// <returns></returns>
        /// <example>return GetOnlyOneSubDocList(x => x.Id == schoolId, z => z.ShiftSchedules);</example>
        /// <value>return Collection.AsQueryable().Where(e => e.Id == schoolId).Select(e => e.ShiftSchedules).FirstOrDefault();</value>
        IEnumerable<TClassField> GetOnlyOneSubDocList<TClassField>(Expression<Func<T, bool>> filterField,
            Expression<Func<T, IEnumerable<TClassField>>> listField);
    }
}