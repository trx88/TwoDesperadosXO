using Actions.Abstraction;
using Repository.DataRepositories.RepositoryFactories.Factories;
using UI.Models.Settings;

public class InitializeSettingsDataAction : InitializeAction
{
    private readonly PlayerPrefsRepositoryFactory _playerPrefsRepositoryFactory;

    public InitializeSettingsDataAction(PlayerPrefsRepositoryFactory repositoryFactory)
    {
        _playerPrefsRepositoryFactory = repositoryFactory;
    }
        
    public override void Invoke()
    {
        _playerPrefsRepositoryFactory.RepositoryOf<SettingsModel>().Create(
            new SettingsModel
            {
                MusicEnabled = true,
                SfxEnabled = true
            });
    }
}
