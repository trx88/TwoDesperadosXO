using System.Threading.Tasks;
using Services;
using UI.Views.Abstraction;
using UI.ViewsModels.Home;
using UI.ViewsModels.Settings;
using UI.ViewsModels.Stats;
using UI.ViewsModels.Theme;

namespace GameContexts
{
    public class PlaySceneContext : SceneContext
    {
        protected override async Task InitializeStateMachine()
        {
            await UIStateMachine.SetInitialState(UIView.HomeScreen);
        }

        protected override void CreateSceneViewModels()
        {
            RegisterViewModel(ViewModelFactory.Create<MainMenuViewModel>());
            RegisterViewModel(ViewModelFactory.Create<SettingViewModel>());
            RegisterViewModel(ViewModelFactory.Create<StatsViewModel>());
            RegisterViewModel(ViewModelFactory.Create<ThemeViewModel>());
        }

        protected override void RegisterServices()
        {
            var sceneService = new SceneService();
            RegisterService(sceneService);
        }
    }
}
