using System;
using Actions.Abstraction;

namespace Repository.DataRepositories.RepositoryFactories.Factories
{
    /// <summary>
    /// In order to get or create a repository for a specific data model, 
    /// RepositoryConfig must be created for that specific data model 
    /// that consists of a type and an optional InitializationAction.
    /// </summary>
    public class RepositoryConfig
    {
        public Type ItemType { get; }
        public InitializeAction InitializeAction { get; }

        public RepositoryConfig(Type itemType)
        {
            ItemType = itemType;
        }

        public RepositoryConfig(Type itemType, InitializeAction initializeAction) : this(itemType)
        {
            InitializeAction = initializeAction;
        }
    }
}