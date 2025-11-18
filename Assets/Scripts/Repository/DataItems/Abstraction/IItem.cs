using System;

namespace Repository.DataItems.Abstraction
{
    public interface IItem : ICloneable
    {
        string Id { get; set; }
    }
}