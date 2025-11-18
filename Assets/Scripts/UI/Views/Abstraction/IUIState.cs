using System.Threading.Tasks;

namespace UI.Views.Abstraction
{
    public enum UIView
    {
        HomeScreen,
        SettingsScreen,
        StatsScreen,
        ThemeScreen,
        ExitScreen,
        GameScreen,
        MatchOverScreen,
    }
    
    public interface IUIState
    {
        Task EnterViewState();
        Task ExitViewState();
    }
}