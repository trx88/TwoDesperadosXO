using System.Linq;
using GameContexts;
using Repository.DataRepositories.Abstraction;
using Repository.DataRepositories.RepositoryFactories.Factories;
using Services;
using UI.Bindables;
using UI.Models.Theme;
using UI.StateMachine;
using UnityEngine;

namespace UI.ViewsModels.Theme
{
    public class ThemeViewModel : ViewModel
    {
        public Bindable<ThemeModel> ThemeData { get; private set; } = new Bindable<ThemeModel>();
        
        private IRepository<ThemeModel> _themeModelRepository;
        
        private SceneService _sceneService;
        
        public override void Construct(
            InMemoryRepositoryFactory inMemoryRepositoryFactory,
            PlayerPrefsRepositoryFactory playerPrefsRepositoryFactory,
            UIStateMachine uiStateMachine)
        {
            base.Construct(inMemoryRepositoryFactory, playerPrefsRepositoryFactory, uiStateMachine);
            
            _themeModelRepository = inMemoryRepositoryFactory.RepositoryOf<ThemeModel>();
            
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
            ThemeModel themeData = _themeModelRepository.Get(_ => true).Single();
            SetThemeData(themeData);
        }

        /// <summary>
        /// Subscribe to repository changes here.
        /// </summary>
        public override void SubscribeToDataChanges()
        {
            base.SubscribeToDataChanges();
            
            _themeModelRepository.ItemChanged += OnThemeDataChanged;
        }

        /// <summary>
        /// Unsubscribe from repository changes here.
        /// </summary>
        public override void UnsubscribeFromDataChanges()
        {
            base.UnsubscribeFromDataChanges();
            
            _themeModelRepository.ItemChanged -= OnThemeDataChanged;
        }
        #endregion
        
        #region Repo callbacks
        private void OnThemeDataChanged(ThemeModel themeData)
        {
            SetThemeData(themeData);
        }
        #endregion
        
        #region Setters
        private void SetThemeData(ThemeModel themeData)
        {
            ThemeData.SetPropertyValue(themeData);
        }
        
        public void UpdateTheme(ThemeModel themeData)
        {
            _themeModelRepository.Update(themeData);
        }
        #endregion

        public void LoadGameScene()
        {
            _sceneService.LoadScene((int)SceneIndices.GameScene);
        }
    }
}
