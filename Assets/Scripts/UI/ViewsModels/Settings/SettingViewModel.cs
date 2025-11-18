using System.Linq;
using Repository.DataRepositories.Abstraction;
using Repository.DataRepositories.RepositoryFactories.Factories;
using UI.Bindables;
using UI.Models.Settings;
using UI.StateMachine;

namespace UI.ViewsModels.Settings
{
    public class SettingViewModel : ViewModel
    {
        public Bindable<SettingsModel> SettingsData { get; private set; } = new Bindable<SettingsModel>();
        
        private IRepository<SettingsModel> _settingsModelRepository;

        public override void Construct(
            InMemoryRepositoryFactory inMemoryRepositoryFactory,
            PlayerPrefsRepositoryFactory playerPrefsRepositoryFactory,
            UIStateMachine uiStateMachine)
        {
            base.Construct(inMemoryRepositoryFactory, playerPrefsRepositoryFactory, uiStateMachine);
            
            _settingsModelRepository = playerPrefsRepositoryFactory.RepositoryOf<SettingsModel>();
        }
        
        #region Overrides
        /// <summary>
        /// Get/update data here.
        /// </summary>
        public override void UpdateData()
        {
            base.UpdateData();
            
            //Read the data from the repository and set the values.
            SettingsModel settingsData = _settingsModelRepository.Get(_ => true).Single();
            SetSettingsData(settingsData);
        }

        /// <summary>
        /// Subscribe to repository changes here.
        /// </summary>
        public override void SubscribeToDataChanges()
        {
            base.SubscribeToDataChanges();
            
            _settingsModelRepository.ItemChanged += OnSettingsDataChanged;
        }

        /// <summary>
        /// Unsubscribe from repository changes here.
        /// </summary>
        public override void UnsubscribeFromDataChanges()
        {
            base.UnsubscribeFromDataChanges();
            
            _settingsModelRepository.ItemChanged -= OnSettingsDataChanged;
        }
        #endregion
        
        #region Repo callbacks
        private void OnSettingsDataChanged(SettingsModel settingsData)
        {
            SetSettingsData(settingsData);
        }
        #endregion
        
        #region Setters
        private void SetSettingsData(SettingsModel settingsData)
        {
            SettingsData.SetPropertyValue(settingsData);
        }

        public void UpdateSettings(SettingsModel settingsData)
        {
            _settingsModelRepository.Update(settingsData);
        }
        #endregion
    }
}
