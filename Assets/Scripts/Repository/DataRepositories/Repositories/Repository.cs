using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Actions.Abstraction;
using Repository.DataItems.Abstraction;
using Repository.DataRepositories.Abstraction;

namespace Repository.DataRepositories.Repositories
{
    /// <summary>
    /// Base Repository is implemented in a standard way (with CRUD methods) 
    /// with addition of data change Actions other classes subscribe to.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public abstract class Repository<TItem> : IRepository<TItem> where TItem : class, IItem
    {
        protected ConcurrentDictionary<string, TItem> _items;

        //Each repository can initialize using an initialization action for specific data model
        //(basically set default values and/or load stored data in case of PlayerPrefsRepository).
        protected InitializeAction InitializeAction { get; private set; }

        public Action<TItem> ItemAdded { get; set; }
        public Action<TItem> ItemChanged { get; set; }
        public Action<TItem> ItemRemoved { get; set; }

        protected Repository(InitializeAction initializeAction)
        {
            InitializeAction = initializeAction;
        }

        protected abstract void LoadOrInitializeRepository();

        public int Count()
        {
            if (_items == null)
            {
                LoadOrInitializeRepository();
            }

            return _items?.Count ?? 0;
        }

        public virtual TItem Create(TItem value)
        {
            _items ??= new ConcurrentDictionary<string, TItem>();
            
            TItem result = default;

            if (value != null)
            {
                if (!_items.TryAdd(value.Id, value))
                {
                    throw new ArgumentException($"Trying to create {typeof(TItem)} while the item with same Id: {value.Id} already exists in repository!");
                }

                ItemAdded?.Invoke(result);
            }

            return result;
        }

        public virtual void Delete(string id)
        {
            if (_items == null)
            {
                LoadOrInitializeRepository();
            }

            if (_items.TryRemove(id, out TItem removedItem))
            {
                ItemRemoved?.Invoke(removedItem);
            }
        }

        public virtual bool Exists(string id)
        {
            try
            {
                return Get(id) != null;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public virtual TItem Get(string id)
        {
            if (_items == null)
            {
                LoadOrInitializeRepository();
            }

            if (_items.TryGetValue(id, out TItem item))
            {
                return item.Clone() as TItem;
            }
            else
            {
                throw new KeyNotFoundException($"{typeof(TItem)} with Id: {id} not found in repository!");
            }
        }

        public IEnumerable<TItem> Get(Func<TItem, bool> predicate)
        {
            if (_items == null)
            {
                LoadOrInitializeRepository();
            }

            return _items.Values.Where(predicate).Select(x => x.Clone() as TItem).ToList();
        }

        public virtual void Update(TItem value)
        {
            if (_items == null)
            {
                LoadOrInitializeRepository();
            }

            if (_items.TryGetValue(value.Id, out TItem item))
            {
                _items[item.Id] = value;
                ItemChanged?.Invoke(value);
            }
            else
            {
                throw new KeyNotFoundException($"{typeof(TItem)} with Id: {value.Id} not found in repository!");
            }
        }
    }
}