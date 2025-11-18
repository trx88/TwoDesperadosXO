using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Actions.Abstraction;
using Repository.DataItems.Abstraction;
using Repository.DataRepositories.Abstraction;
using Repository.DataRepositories.Repositories;
using Repository.DataRepositories.RepositoryFactories.Abstraction;

namespace Repository.DataRepositories.RepositoryFactories.Factories
{
    public abstract class RepositoryFactory<TItemFamily> : IRepositoryFactory<TItemFamily> where TItemFamily : IItem
    {
        private readonly IServiceContainer _repositories;
        private readonly Dictionary<Type, RepositoryConfig> _repositoryConfigs;

        protected RepositoryFactory()
        {
            _repositories = new ServiceContainer();
            _repositoryConfigs = new Dictionary<Type, RepositoryConfig>();
        }

        public void AddRepositoryConfig(RepositoryConfig config)
        {
            if (typeof(TItemFamily).IsAssignableFrom(config.ItemType))
            {
                if (_repositoryConfigs.TryGetValue(config.ItemType, out _))
                {
                    throw new ArgumentException(
                        $"Repository of type {config.ItemType} has been already declared, you are not allowed to have" +
                        $" multiple configurations for same repository");
                }
                else
                {
                    _repositoryConfigs.Add(config.ItemType, config);
                }
            }
            else
            {
                throw new ArgumentException(
                    $"Trying to add config for the repository of type {config.ItemType} while only" +
                    $" items derived from {typeof(TItemFamily)} is accepted within {GetType()}");
            }
        }

        public IRepository<TItem> RepositoryOf<TItem>() where TItem : class, TItemFamily, new()
        {
            if (new TItem() is TItemFamily)
            {
                if (_repositoryConfigs.TryGetValue(typeof(TItem), out RepositoryConfig config))
                {
                    var requestedRepoType = typeof(Repository<TItem>);

                    if (_repositories.GetService(requestedRepoType) is not Repository<TItem> newRepository)
                    {
                        newRepository = GenerateRepositoryOf<TItem>(config.InitializeAction);
                        _repositories.AddService(requestedRepoType, newRepository);
                    }

                    return newRepository;
                }
                else
                {
                    throw new ArgumentException(
                        $"Trying to get repository of the item type {typeof(TItem)} which is not defined." +
                        $" Use {nameof(AddRepositoryConfig)} to add repository definition for specific type");
                }
            }

            throw new ArgumentException(
                $"Trying to get repository for {typeof(TItem)} which is not instance of type {typeof(IMemoryItem)}");
        }

        protected abstract Repository<TItem> GenerateRepositoryOf<TItem>(InitializeAction initializeAction) where TItem : class, TItemFamily, new();
    }
}