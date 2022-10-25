using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Holding.Base.Infrastructure.Domain
{
    /// <summary>
    /// A collection of ValueObjects of type T for persisting and managing value objects
    /// </summary>
    /// <typeparam name="T">type of value object</typeparam>
    public class ReferenceList<T> : IReferenceList<T> where T : ValueObjectBase<T>
    {
        private const string _CollectionName = "ReferenceList";
        private List<T> InternalItems;
        /// <summary>
        /// Creates a instance of ReferenceList
        /// </summary>
        public ReferenceList()
        {
            Id = string.Format("{0}/{1}", _CollectionName, typeof(T).Name);
            InternalItems = new List<T>();
        }
        /// <summary>
        /// Creates a instance of ReferenceList with specified collection
        /// </summary>
        /// <param name="collection">valueobject collection</param>
        public ReferenceList(IEnumerable<T> collection)
            : this()
        {
            InternalItems = new List<T>(collection);
        }

      
        /// <summary>
        /// Gets ReferenceList Id
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Gets ValueObjects list
        /// </summary>
        public ReadOnlyCollection<T> Items
        {
            get { return InternalItems.AsReadOnly(); }
            private set
            {
                if (value != null)
                    InternalItems = new List<T>(value);
                else
                    InternalItems.Clear();
            }
        }


        /// <summary>
        /// Adds the specified item to a referencelist.
        /// </summary>
        /// <param name="item">The element to add to the referencelist.</param>
        /// <returns>true if the element is added to the referencelist object; false if the element is already present.</returns>
        public bool AddItem(T item)
        {
            if (item == null)
                return false; //throw new ArgumentNullException("item");

            if (Items.Contains(item))
                return false;

            InternalItems.Add(item);
            return true;
        }

        /// <summary>
        /// Removes item from ReferenceList
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool RemoveItem(T item)
        {
            if (item == null)
                return false; //throw new ArgumentNullException("item");

            if (!Items.Contains(item))
                return false;

            InternalItems.Remove(item);
            return true;
        }
        /// <summary>
        /// Clear all items
        /// </summary>
        public void Clear()
        {
            InternalItems.Clear();
        }
    }


}
