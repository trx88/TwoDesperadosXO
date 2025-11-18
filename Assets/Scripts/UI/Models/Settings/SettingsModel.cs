using Newtonsoft.Json;
using Repository.DataItems;
using Repository.DataItems.Abstraction;

namespace UI.Models.Settings
{
    public class SettingsModel : Item, IPlayerPrefsItem
    {
        public string PlayerPrefsKey => "SettingsData";

        [JsonProperty("musicEnabled")] public bool MusicEnabled { get; set; }
        [JsonProperty("sfxEnabled")] public bool SfxEnabled { get; set; }
        
        public override object Clone()
        {
            return new SettingsModel()
            {
                Id = Id,
                Type = Type,
                MusicEnabled = MusicEnabled,
                SfxEnabled = SfxEnabled
            };
        }
    }
}
