using Repository.DataItems.Abstraction;
using Repository.DataRepositories.Abstraction;
using Repository.DataRepositories.RepositoryFactories.Factories;

namespace Repository.DataRepositories.RepositoryFactories.Abstraction
{
    public interface IRepositoryFactory<TItemFamily> where TItemFamily : IItem
    {
        IRepository<TItem> RepositoryOf<TItem>() where TItem : class, TItemFamily, new();
        void AddRepositoryConfig(RepositoryConfig config);
    }
}