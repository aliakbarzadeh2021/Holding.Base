using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Holding.Base.Infrastructure.Domain
{
    /// <summary>
    /// A collection of ValueObjects of type T for persisting and managing value objects
    /// </summary>
    /// <typeparam name="T">type of value object</typeparam>
    public interface IReferenceList<T> where T : ValueObjectBase<T>
    {
        /// <summary>
        /// Gets value objects set
        /// </summary>
        ReadOnlyCollection<T> Items { get; }

        /// <summary>
        /// Gets ReferenceList Id
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Adds the specified item to a referencelist.
        /// </summary>
        /// <param name="item">The element to add to the referencelist.</param>
        /// <returns>true if the element is added to the referencelist object; false if the element is already present.</returns>
        bool AddItem(T item);

        /// <summary>
        /// Removes item from ReferenceList
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool RemoveItem(T item);

        /// <summary>
        /// Clear all items
        /// </summary>
        void Clear();
    }
}