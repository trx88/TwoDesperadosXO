using Newtonsoft.Json;

namespace Repository.DataItems.Abstraction
{
    /// <summary>
    /// PlayerPrefs specific Item. PlayerPrefsKey is needed for accessing the data, 
    /// but it shouldn't be stored so it's ignored.
    /// </summary>
    public interface IPlayerPrefsItem : IItem
    {
        [JsonIgnore] public string PlayerPrefsKey { get; }
    }
}