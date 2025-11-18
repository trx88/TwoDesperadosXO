using Repository.DataRepositories.RepositoryFactories.Factories;
using UI.StateMachine;
using UI.ViewsModels.Abstraction;

namespace UI.ViewsModels
{
    /// <summary>
    /// Base ViewModel class.
    /// </summary>
    public abstract class ViewModel : IViewModel
    {
        public UIStateMachine StateMachine { get; private set; }
        protected InMemoryRepositoryFactory InMemoryRepositoryFactory;
        protected PlayerPrefsRepositoryFactory PlayerPrefsRepositoryFactory;

        public virtual void Construct(
            InMemoryRepositoryFactory inMemoryRepositoryFactory, 
            PlayerPrefsRepositoryFactory playerPrefsRepositoryFactory,
            UIStateMachine stateMachine)
        {
            InMemoryRepositoryFactory = inMemoryRepositoryFactory;
            PlayerPrefsRepositoryFactory = playerPrefsRepositoryFactory;
            StateMachine = stateMachine;
        }

        protected ViewModel()
        {
            
        }

        /// <summary>
        /// Subscribe to repository changes here.
        /// </summary>
        public virtual void SubscribeToDataChanges()
        {

        }

        /// <summary>
        /// Unsubscribe from repository changes here.
        /// </summary>
        public virtual void UnsubscribeFromDataChanges()
        {

        }

        /// <summary>
        /// Get/update data here.
        /// </summary>
        public virtual void UpdateData()
        {

        }
    }
}