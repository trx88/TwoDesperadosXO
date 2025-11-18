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
        MatchOverScreen,
    }
    
    public interface IUIState
    {
        Task EnterViewState();
        Task ExitViewState();
    }
}