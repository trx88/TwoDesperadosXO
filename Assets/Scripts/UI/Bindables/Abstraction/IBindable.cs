using System;

namespace UI.Bindables.Abstraction
{
    public interface IBindable<out T>
    {
        void BindTo(Action<T> action);
    }
}