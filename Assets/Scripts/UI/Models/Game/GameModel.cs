using System.Collections.Generic;
using Newtonsoft.Json;
using Repository.DataItems;
using Repository.DataItems.Abstraction;

namespace UI.Models.Game
{
    public class GameModel : Item, IMemoryItem
    {
        [JsonProperty("currentPlayer")] public int CurrentPlayer { get; set; }
        [JsonProperty("board")] public List<int> Board { get; set; }
        [JsonProperty("matchResult")] public GameOutcome MatchResult { get; set; }
        [JsonProperty("matchTime")] public double MatchTime { get; set; }
        [JsonProperty("playerOneMoves")] public int PlayerOneMoves { get; set; }
        [JsonProperty("playerTwoMoves")] public int PlayerTwoMoves { get; set; }
        
        public override object Clone()
        {
            var clone = new GameModel()
            {
                Id = Id,
                Type = Type,
                CurrentPlayer =  CurrentPlayer,
                Board = new List<int>(),
                MatchResult = MatchResult,
                MatchTime = MatchTime,
                PlayerOneMoves = PlayerOneMoves,
                PlayerTwoMoves = PlayerTwoMoves,
            };

            foreach (var cell in Board)
            {
                clone.Board.Add(cell);
            }
            
            return clone;
        }
    }
    
    public enum GameOutcome
    {
        None,
        WinX,
        WinO,
        Draw
    }
}
