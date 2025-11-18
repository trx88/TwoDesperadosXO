using Actions.Abstraction;
using Repository.DataItems.Abstraction;
using Repository.DataRepositories.Repositories;

namespace Repository.DataRepositories.RepositoryFactories.Factories
{
    public class PlayerPrefsRepositoryFactory : RepositoryFactory<IPlayerPrefsItem>
    {
        protected override Repository<TItem> GenerateRepositoryOf<TItem>(InitializeAction initializeAction)
        {
            return new PlayerPrefsRepository<TItem>(initializeAction);
        }
    }
}