using System.Collections.Generic;
using System.Linq;
using GameContexts;
using Repository.DataRepositories.Abstraction;
using Repository.DataRepositories.RepositoryFactories.Factories;
using Services;
using UI.Bindables;
using UI.Models.Game;
using UI.StateMachine;

namespace UI.ViewsModels.Game
{
    public class MatchOverViewModel : ViewModel
    {
        public Bindable<GameOutcome> MatchResult { get; private set; } = new Bindable<GameOutcome>();
        public Bindable<double> MatchTime { get; private set; } = new Bindable<double>();
        
        private IRepository<GameModel> _gameModelRepository;
        
        private SceneService _sceneService;
        
        //Cache
        GameModel _gameData;
        
        public override void Construct(
            InMemoryRepositoryFactory inMemoryRepositoryFactory,
            PlayerPrefsRepositoryFactory playerPrefsRepositoryFactory,
            UIStateMachine uiStateMachine)
        {
            base.Construct(inMemoryRepositoryFactory, playerPrefsRepositoryFactory, uiStateMachine);
            
            _gameModelRepository = inMemoryRepositoryFactory.RepositoryOf<GameModel>();
            
            _sceneService = SceneContext.Instance.GetService<SceneService>();
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
            SetMatchData(gameData);
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
            SetMatchData(gameData);
        }
        #endregion
        
        #region Setters
        private void SetMatchData(GameModel gameData)
        {
            _gameData = gameData;
            MatchResult.SetPropertyValue(gameData.MatchResult);
            MatchTime.SetPropertyValue(gameData.MatchTime);
        }

        public void ResetGameData()
        {
            _gameData.CurrentPlayer = 1;
            _gameData.Board = new List<int>()
            {
                0, 0, 0,
                0, 0, 0,
                0, 0, 0
            };
            _gameData.MatchResult = GameOutcome.None;
            _gameData.MatchTime = 0;
            _gameData.PlayerOneMoves = 0;
            _gameData.PlayerTwoMoves = 0;
            _gameModelRepository.Update(_gameData);
        }
        #endregion
        
        public void LoadPlayScene()
        {
            _sceneService.LoadScene((int)SceneIndices.PlayScene);
        }
    }
}
