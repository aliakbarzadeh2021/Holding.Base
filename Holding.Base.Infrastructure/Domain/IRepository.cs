using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Holding.Base.Infrastructure.Domain
{
    //this class after marge must be delete.
    public interface IRepository<T, TEntityKey> where T : EntityBase<TEntityKey>, IAggregateRoot
    {
        void Save(T entity);
        void Add(T entity);
        void AddMany(T[] entity);
        void Remove(TEntityKey id);
        /// <summary>
        /// UPDATE Document SET StudentClass=value WHERE ID IN (SELECT ID FROM values).
        /// </summary>
        /// <param name="filterField">Expression to find document. like x => x.Id.</param>
        /// <param name="values">Creates an in filter.. document.Select(y => y.Id).</param>
        /// <param name="updatefield">Creates a set operator. like z => z.StudentClass</param>
        /// <param name="value">The value. like studentClass.</param>
        void UpdateManyAsync<TValue>(Expression<Func<T, TEntityKey>> filterField, IEnumerable<TEntityKey> values,
            Expression<Func<T, TValue>> updatefield, TValue value);

        /// <summary>
        /// Updates one field [no array type].
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="filterField">Key field to find data. like s => s.Id.</param>
        /// <param name="filterValue">Value of key to find data. like personId.</param>
        /// <param name="updateField">Field for update. like s => s.name.</param>
        /// <param name="updateValue">Value of updateField. like "Ilia".</param>
        /// <returns>Default UpdateOneAsync return value.</returns>
        void UpdateOneAsync<TField, TValue>(Expression<Func<T, TField>> filterField,
            TField filterValue, Expression<Func<T, TValue>> updateField, TValue updateValue);

        /// <summary>
        /// Updates an array field (addes an item to list).
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="filterField">Key field to find data. like s => s.Id.</param>
        /// <param name="filterValue">Value of key to find data. like personId.</param>
        /// <param name="updateField">Field for update. like s => s.name.</param>
        /// <param name="updateValue">List of values for updateField.</param>
        /// <returns>Default UpdateOneAsync return value.</returns>
        void UpdateListAsync<TField, TItem>(Expression<Func<T, TField>> filterField,
            TField filterValue, Expression<Func<T, IEnumerable<TItem>>> updateField, TItem updateValue);

        /// <summary>
        /// Replaces an array field.
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="filterField">Key field to find data. like s => s.Id.</param>
        /// <param name="filterValue">Value of key to find data. like personId.</param>
        /// <param name="updateValues">Value list for update. like person.list.</param>
        /// <param name="mongoDocumentName">For example: 'myArrayNameInMongo'.</param>
        /// <returns>Default UpdateOneAsync return value.</returns>
        void ReplaceListAsync<TField, TItem>(Expression<Func<T, TField>> filterField,
            TField filterValue, IEnumerable<TItem> updateValues, string mongoDocumentName);

        /// <summary>
        /// Updates a subdocument.
        /// </summary>
        /// <typeparam name="TMainField"></typeparam>
        /// <typeparam name="TClassField"></typeparam>
        /// <param name="docFilterField">Key field to find document. like s => s.Id.</param>
        /// <param name="docFilterValue">Value of key to find data. like personId.</param>
        /// <param name="subDocumentFilterField">Key field to find subdocument list. like s => s.subDocList.</param>
        /// <param name="expressionToFindSubDocument">Expression to fint subdocument. like t => t.Id == aClass.Id.</param>
        /// <param name="mongoDocumentName">For example: SubDocument.$</param>
        /// <param name="mongoDocumentValue">Document for update.</param>
        /// <returns>Default UpdateOneAsync return value.</returns>
        void UpdateSubDocumentListAsync<TMainField, TClassField>(
            Expression<Func<T, TMainField>> docFilterField, TMainField docFilterValue,
            Expression<Func<T, IEnumerable<TClassField>>> subDocumentFilterField,
            Expression<Func<TClassField, bool>> expressionToFindSubDocument,
            TClassField mongoDocumentValue, string mongoDocumentName);

        /// <summary>
        /// Removes a subdocument (value object with all fields).
        /// </summary>
        /// <typeparam name="TMainField"></typeparam>
        /// <typeparam name="TClassField"></typeparam>
        /// <param name="docFilterField">Key field to find document. like s => s.Id.</param>
        /// <param name="docFilterValue">Value of key to find data. like personId.</param>
        /// <param name="subDocumentFilterField">Key field to find subdocument list. like s => s.subDocList.</param>
        /// <param name="expressionToFintSubDocument">Expression to fint subdocument. like t => t.Id == aClass.Id.</param>
        /// <param name="mongoDocumentValue">Document for delete (must be with all fields to find!).</param>
        /// <returns>Default UpdateOneAsync return value.</returns>
        void RemoveDocumentFromListAsync<TMainField, TClassField>(
            Expression<Func<T, TMainField>> docFilterField, TMainField docFilterValue,
            Expression<Func<T, IEnumerable<TClassField>>> subDocumentFilterField,
            Expression<Func<TClassField, bool>> expressionToFintSubDocument,
            TClassField mongoDocumentValue);

        /// <summary>
        /// Removes a subdocument by Id.
        /// </summary>
        /// <typeparam name="TMainField"></typeparam>
        /// <typeparam name="TClassField"></typeparam>
        /// <param name="docFilterField">Key field to find document. like s => s.Id.</param>
        /// <param name="docFilterValue">Value of key to find data. like personId.</param>
        /// <param name="subDocumentFilterField">Key field to find subdocument list. like s => s.subDocList.</param>
        /// <param name="expressionToFindSubDocument">Expression to fint subdocument. like t => t.Id == aClass.Id.</param>
        /// <returns>Default UpdateOneAsync return value.</returns>
        void RemoveDocumentFromListAsync<TMainField, TClassField>(
            Expression<Func<T, TMainField>> docFilterField, TMainField docFilterValue,
            Expression<Func<T, IEnumerable<TClassField>>> subDocumentFilterField,
            Expression<Func<TClassField, bool>> expressionToFindSubDocument);

    }
}