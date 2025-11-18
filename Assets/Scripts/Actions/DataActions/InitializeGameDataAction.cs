using System.Collections.Generic;
using Actions.Abstraction;
using Repository.DataRepositories.RepositoryFactories.Factories;
using UI.Models.Game;

namespace Actions.DataActions
{
    public class InitializeGameDataAction : InitializeAction
    {
        private readonly InMemoryRepositoryFactory _inMemoryRepositoryFactory;

        public InitializeGameDataAction(InMemoryRepositoryFactory repositoryFactory)
        {
            _inMemoryRepositoryFactory = repositoryFactory;
        }
        
        public override void Invoke()
        {
            _inMemoryRepositoryFactory.RepositoryOf<GameModel>().Create(
                new GameModel
                {
                    CurrentPlayer = 1,
                    Board = new List<int>()
                    {
                        0, 0, 0,
                        0, 0, 0,
                        0, 0, 0
                    },
                    MatchResult = GameOutcome.None,
                    WinningLine = null,
                    MatchTime = 0,
                    PlayerOneMoves = 0,
                    PlayerTwoMoves = 0
                });
        }
    }
}
