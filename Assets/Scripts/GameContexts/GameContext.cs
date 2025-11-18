using Actions.DataActions;
using Repository.DataRepositories.RepositoryFactories.Factories;
using Services;
using UI.Models.Game;
using UI.Models.Settings;
using UI.Models.Stats;
using UI.Models.Theme;
using UnityEngine;

namespace GameContexts
{
    //Replaces Zenject Repository Installer.
    public class GameContext : MonoBehaviour
    {
        public static GameContext Instance { get; private set; }
        
        public InMemoryRepositoryFactory InMemoryRepositoryFactory { get; private set; }
        public PlayerPrefsRepositoryFactory PlayerPrefsRepositoryFactory { get; private set; }
        
        // public SceneService SceneService { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            SetupInMemoryRepositoryFactory();
            SetupPlayerPrefsRepositoryFactory();

            // SceneService = new SceneService();
            
            Debug.Log("GameContext initialized.");
        }

        private void SetupInMemoryRepositoryFactory()
        {
            InMemoryRepositoryFactory = new InMemoryRepositoryFactory();
            
            InMemoryRepositoryFactory.AddRepositoryConfig(
                new RepositoryConfig(typeof(ThemeModel),
                    new InitializeThemeDataAction(InMemoryRepositoryFactory)
                ));
            
            InMemoryRepositoryFactory.AddRepositoryConfig(
                new RepositoryConfig(typeof(GameModel),
                    new InitializeGameDataAction(InMemoryRepositoryFactory)
                ));
        }
        
        private void SetupPlayerPrefsRepositoryFactory()
        {
            PlayerPrefsRepositoryFactory = new PlayerPrefsRepositoryFactory();
            
            PlayerPrefsRepositoryFactory.AddRepositoryConfig(
                new RepositoryConfig(typeof(SettingsModel),
                    new InitializeSettingsDataAction(PlayerPrefsRepositoryFactory)
                ));
            
            PlayerPrefsRepositoryFactory.AddRepositoryConfig(
                new RepositoryConfig(typeof(StatsModel),
                    new InitializeStatsDataAction(PlayerPrefsRepositoryFactory)
                ));
        }
    }
}
