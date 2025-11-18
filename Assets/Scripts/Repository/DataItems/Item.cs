using Newtonsoft.Json;
using Repository.DataItems.Abstraction;

namespace Repository.DataItems
{
    /// <summary>
    /// Base entity for storage. It's a JSON, since JSON is easy to work with, and massive models can serialized with ease.
    /// </summary>
    public abstract class Item : IItem
    {
        protected Item() => Type = GetType().Name;

        [JsonProperty("id")] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        [JsonProperty("type")] public string Type { get; set; }

        public abstract object Clone();
    }
}