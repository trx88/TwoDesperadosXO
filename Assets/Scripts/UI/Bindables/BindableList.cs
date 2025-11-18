using System;
using System.Collections.Generic;
using UI.Bindables.Abstraction;

namespace UI.Bindables
{
    public class BindableList<T> : IBindableList<T>
    {
        private readonly List<T> _items = new List<T>();

        // public IReadOnlyList<T> Items => _items;
        public List<T> Items => _items;

        private Action<List<T>> _onListChanged;
        private Action<T> _onItemAdded;
        private Action<T> _onItemRemoved;
        private Action _onCleared;
        
        public void BindToListChanged(Action<List<T>> action)
        {
            _onListChanged += action;
            action?.Invoke(new List<T>(_items)); // Initial push
        }

        public void BindToItemAdded(Action<T> action)
        {
            _onItemAdded += action;
        }

        public void BindToItemRemoved(Action<T> action)
        {
            _onItemRemoved += action;
        }

        public void BindToCleared(Action action)
        {
            _onCleared += action;
        }
        
        public void AddItem(T item)
        {
            _items.Add(item);
            _onItemAdded?.Invoke(item);
            _onListChanged?.Invoke(new List<T>(_items));
        }

        public void RemoveItem(T item)
        {
            if (_items.Remove(item))
            {
                _onItemRemoved?.Invoke(item);
                _onListChanged?.Invoke(new List<T>(_items));
            }
        }

        public void Clear()
        {
            _items.Clear();
            _onCleared?.Invoke();
            _onListChanged?.Invoke(new List<T>(_items));
        }

        public void SetItems(List<T> newList)
        {
            _items.Clear();
            _items.AddRange(newList);
            _onListChanged?.Invoke(new List<T>(_items));
        }
    }
}