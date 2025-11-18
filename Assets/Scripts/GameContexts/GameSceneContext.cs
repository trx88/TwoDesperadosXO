using System.Threading.Tasks;
using Services;
using UI.Views.Abstraction;
using UI.ViewsModels.Game;
using UI.ViewsModels.Settings;
using UnityEngine;

namespace GameContexts
{
    public class GameSceneContext : SceneContext
    {
        protected override async Task InitializeStateMachine()
        {
            await UIStateMachine.SetInitialState(UIView.GameScreen);
        }
        
        protected override void CreateSceneViewModels()
        {
            RegisterViewModel(ViewModelFactory.Create<SettingViewModel>());
            RegisterViewModel(ViewModelFactory.Create<GameViewModel>());
            RegisterViewModel(ViewModelFactory.Create<MatchOverViewModel>());
        }

        protected override void RegisterServices()
        {
            // Create counter
            var counterService = new CounterService(0.1f); // 100ms

            // Create runner and attach
            var runnerGO = new GameObject("CounterServiceRunner");
            var runner = runnerGO.AddComponent<CounterServiceRunner>();
            runner.Initialize(counterService);

            RegisterService(counterService);
            
            var matchService = new MatchService(
                GameContextInstance.InMemoryRepositoryFactory,
                GameContextInstance.PlayerPrefsRepositoryFactory,
                counterService);
            RegisterService(matchService);

            var sceneService = new SceneService();
            RegisterService(sceneService);
        }
    }
}
