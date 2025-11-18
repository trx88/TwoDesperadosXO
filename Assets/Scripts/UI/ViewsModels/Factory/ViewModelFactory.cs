using Repository.DataRepositories.RepositoryFactories.Factories;
using UI.StateMachine;

namespace UI.ViewsModels.Factory
{
    public class ViewModelFactory
    {
        private readonly InMemoryRepositoryFactory _inMemoryRepoFactory;
        private readonly PlayerPrefsRepositoryFactory _playerPrefsRepositoryFactory;
        private readonly UIStateMachine _stateMachine;

        public ViewModelFactory(
            InMemoryRepositoryFactory inMemoryRepoFactory, 
            PlayerPrefsRepositoryFactory playerPrefsRepositoryFactory, UIStateMachine stateMachine)
        {
            _inMemoryRepoFactory = inMemoryRepoFactory;
            _playerPrefsRepositoryFactory = playerPrefsRepositoryFactory;
            _stateMachine = stateMachine;
        }

        public T Create<T>() where T : ViewModel, new()
        {
            var viewModel = new T();
            viewModel.Construct(_inMemoryRepoFactory, _playerPrefsRepositoryFactory, _stateMachine);
            // vm.Construct(_stateMachine);
            return viewModel;
        }
    }
}
