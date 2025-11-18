using Actions.Abstraction;
using Repository.DataRepositories.RepositoryFactories.Factories;
using UI.Models.Stats;

namespace Actions.DataActions
{
    public class InitializeStatsDataAction : InitializeAction
    {
        private readonly PlayerPrefsRepositoryFactory _playerPrefsRepositoryFactory;

        public InitializeStatsDataAction(PlayerPrefsRepositoryFactory repositoryFactory)
        {
            _playerPrefsRepositoryFactory = repositoryFactory;
        }
        
        public override void Invoke()
        {
            _playerPrefsRepositoryFactory.RepositoryOf<StatsModel>().Create(
                new StatsModel
                {
                    NumberOfMatches = 30,
                    PlayerOneWins = 5,
                    PlayerTwoWins = 5,
                    Draws = 20,
                    AverageMatchTime = 17.3f
                });
        }
    }
}
