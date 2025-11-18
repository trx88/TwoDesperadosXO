using Repository.DataRepositories.RepositoryFactories.Factories;
using UI.StateMachine;

namespace UI.ViewsModels.Exit
{
    public class ExitViewModel : ViewModel
    {
        public override void Construct(
            InMemoryRepositoryFactory inMemoryRepositoryFactory,
            PlayerPrefsRepositoryFactory playerPrefsRepositoryFactory,
            UIStateMachine uiStateMachine)
        {
            //Just need the StateMachine.
            base.Construct(inMemoryRepositoryFactory, playerPrefsRepositoryFactory, uiStateMachine);
        }
    }
}
