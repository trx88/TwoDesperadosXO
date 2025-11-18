#nullable enable
using System;
using System.Collections.Generic;
using Repository.DataItems.Abstraction;

namespace Repository.DataRepositories.Abstraction
{
    public interface IRepository<TItem> where TItem : class, IItem
    {
        public Action<TItem>? ItemAdded { get; set; }
        public Action<TItem>? ItemChanged { get; set; }
        public Action<TItem>? ItemRemoved { get; set; }

        int Count();
        TItem Create(TItem value);
        void Delete(string id);
        bool Exists(string id);
        TItem Get(string id);
        IEnumerable<TItem> Get(Func<TItem, bool> predicate);
        void Update(TItem value);
    }
}