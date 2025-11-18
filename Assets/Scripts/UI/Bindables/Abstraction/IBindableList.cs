using System;
using System.Collections.Generic;

namespace UI.Bindables.Abstraction
{
    public interface IBindableList<T>
    {
        public void BindToListChanged(Action<List<T>> action);

        public void BindToItemAdded(Action<T> action);

        public void BindToItemRemoved(Action<T> action);

        public void BindToCleared(Action action);
    }
}