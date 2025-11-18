using System.Linq;
using GameContexts;
using Repository.DataRepositories.Abstraction;
using Repository.DataRepositories.RepositoryFactories.Factories;
using Services;
using UI.Bindables;
using UI.Models.Game;
using UI.Models.Theme;
using UI.StateMachine;

namespace UI.ViewsModels.Game
{
    public class GameViewModel : ViewModel
    {
        public Bindable<GameModel> GameData { get; private set; } = new Bindable<GameModel>();
        public Bindable<ThemeModel> ThemeData { get; private set; } = new Bindable<ThemeModel>();
        
        private IRepository<GameModel> _gameModelRepository;
        private IRepository<ThemeModel> _themeModelRepository;
        private MatchService _matchService;
        
        public override void Construct(
            InMemoryRepositoryFactory inMemoryRepositoryFactory,
            PlayerPrefsRepositoryFactory playerPrefsRepositoryFactory,
            UIStateMachine uiStateMachine)
        {
            base.Construct(inMemoryRepositoryFactory, playerPrefsRepositoryFactory, uiStateMachine);
            
            _gameModelRepository = inMemoryRepositoryFactory.RepositoryOf<GameModel>();
            _themeModelRepository = inMemoryRepositoryFactory.RepositoryOf<ThemeModel>();
            
            _matchService = SceneContext.Instance.GetService<MatchService>();
        }
        
        #region Overrides
        /// <summary>
        /// Get/update data here.
        /// </summary>
        public override void UpdateData()
        {
            base.UpdateData();
            
            //Read the data from the repository and set the values.
            GameModel gameData = _gameModelRepository.Get(_ => true).Single();
            SetGameData(gameData);
            
            ThemeModel themeData = _themeModelRepository.Get(_ => true).Single();
            SetThemeData(themeData);
        }

        /// <summary>
        /// Subscribe to repository changes here.
        /// </summary>
        public override void SubscribeToDataChanges()
        {
            base.SubscribeToDataChanges();
            
            _gameModelRepository.ItemChanged += OnGameDataChanged;
        }

        /// <summary>
        /// Unsubscribe from repository changes here.
        /// </summary>
        public override void UnsubscribeFromDataChanges()
        {
            base.UnsubscribeFromDataChanges();
            
            _gameModelRepository.ItemChanged -= OnGameDataChanged;
        }
        #endregion
        
        #region Repo callbacks
        private void OnGameDataChanged(GameModel gameData)
        {
            SetGameData(gameData);
        }
        #endregion
        
        #region Setters
        private void SetGameData(GameModel gameData)
        {
            GameData.SetPropertyValue(gameData);
        }
        
        public void UpdateGame(GameModel gameData)
        {
            _gameModelRepository.Update(gameData);
        }
        
        private void SetThemeData(ThemeModel themeData)
        {
            ThemeData.SetPropertyValue(themeData);
        }
        #endregion

        public void CellClicked(int index)
        {
            _matchService.HandleMove(index);
        }
        
        public void StartMatch()
        {
            _matchService.StartMatch();
        }
    }
}
