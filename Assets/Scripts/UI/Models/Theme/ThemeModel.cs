using Newtonsoft.Json;
using Repository.DataItems;
using Repository.DataItems.Abstraction;

namespace UI.Models.Theme
{
    public class ThemeModel : Item, IMemoryItem
    {
        [JsonProperty("xThemeAsset")] public string XThemeAsset { get; set; }
        [JsonProperty("oThemeAsset")] public string OThemeAsset { get; set; }
        
        public override object Clone()
        {
            return new ThemeModel()
            {
                Id = Id,
                Type = Type,
                XThemeAsset =  XThemeAsset,
                OThemeAsset = OThemeAsset
            };
        }
    }
    
    public static class ThemeAssetNames
    {
        public const string SignXTheme1 = "SignX_Theme1";
        public const string SignOTheme1 = "SignO_Theme1";
        public const string SignXTheme2 = "SignX_Theme2";
        public const string SignOTheme2 = "SignO_Theme2";
        public const string SignXTheme3 = "SignX_Theme3";
        public const string SignOTheme3 = "SignO_Theme3";
    }

    public enum ThemeType
    {
        Theme1,
        Theme2,
        Theme3,
    }
}
