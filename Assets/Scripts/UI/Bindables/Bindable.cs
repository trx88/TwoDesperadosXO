using System;
using UI.Bindables.Abstraction;

namespace UI.Bindables
{
    /// <summary>
    /// A Bindable binds to an Action provided by the View. 
    /// When the Bindable data is set, 
    /// provided Action is invoked triggering the View and notifying it what has changed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Bindable<T> : IBindable<T>
    {
        private T _value;
        private Action<T> _itemChanged;

        // public T Value { get; private set; }
        public T Value => _value;

        public void BindTo(Action<T> action)
        {
            if (action == null)
            {
                throw new Exception();
            }

            _itemChanged += action;
        }

        public void SetPropertyValue(T value)
        {
            _value = value;
            _itemChanged?.Invoke(value);
        }
    }
}
