using Actions.Abstraction;
using Repository.DataItems.Abstraction;
using Repository.DataRepositories.Repositories;

namespace Repository.DataRepositories.RepositoryFactories.Factories
{
    public class InMemoryRepositoryFactory : RepositoryFactory<IMemoryItem>
    {
        protected override Repository<TItem> GenerateRepositoryOf<TItem>(InitializeAction initializeAction)
        {
            return new InMemoryRepository<TItem>(initializeAction);
        }
    }
}