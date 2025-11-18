using Newtonsoft.Json;
using Repository.DataItems;
using Repository.DataItems.Abstraction;
using UI.Models.Settings;

namespace UI.Models.Stats
{
    public class StatsModel : Item, IPlayerPrefsItem
    {
        public string PlayerPrefsKey => "StatsData";

        [JsonProperty("numberOfMatches")] public int NumberOfMatches { get; set; }
        [JsonProperty("playerOneWins")] public int PlayerOneWins { get; set; }
        [JsonProperty("playerTwoWins")] public int PlayerTwoWins { get; set; }
        [JsonProperty("draws")] public int Draws { get; set; }
        [JsonProperty("averageMatchTime")] public float AverageMatchTime { get; set; }
        
        public override object Clone()
        {
            return new StatsModel()
            {
                Id = Id,
                Type = Type,
                NumberOfMatches = NumberOfMatches,
                PlayerOneWins = PlayerOneWins,
                PlayerTwoWins = PlayerTwoWins,
                Draws = Draws,
                AverageMatchTime = AverageMatchTime
            };
        }
    }
}
