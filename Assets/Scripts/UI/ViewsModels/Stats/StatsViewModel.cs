using System.Linq;
using Repository.DataRepositories.Abstraction;
using Repository.DataRepositories.RepositoryFactories.Factories;
using UI.Bindables;
using UI.Models.Stats;
using UI.StateMachine;

namespace UI.ViewsModels.Stats
{
    public class StatsViewModel : ViewModel
    {
        public Bindable<StatsModel> StatsData { get; private set; } = new Bindable<StatsModel>();
        
        private IRepository<StatsModel> _statsModelRepository;
        
        public override void Construct(
            InMemoryRepositoryFactory inMemoryRepositoryFactory,
            PlayerPrefsRepositoryFactory playerPrefsRepositoryFactory,
            UIStateMachine uiStateMachine)
        {
            base.Construct(inMemoryRepositoryFactory, playerPrefsRepositoryFactory, uiStateMachine);
            
            _statsModelRepository = playerPrefsRepositoryFactory.RepositoryOf<StatsModel>();
        }
        
        #region Overrides
        /// <summary>
        /// Get/update data here.
        /// </summary>
        public override void UpdateData()
        {
            base.UpdateData();
            
            //Read the data from the repository and set the values.
            StatsModel statsData = _statsModelRepository.Get(_ => true).Single();
            SetStatsData(statsData);
        }

        /// <summary>
        /// Subscribe to repository changes here.
        /// </summary>
        public override void SubscribeToDataChanges()
        {
            base.SubscribeToDataChanges();
            
            _statsModelRepository.ItemChanged += OnSettingsDataChanged;
        }

        /// <summary>
        /// Unsubscribe from repository changes here.
        /// </summary>
        public override void UnsubscribeFromDataChanges()
        {
            base.UnsubscribeFromDataChanges();
            
            _statsModelRepository.ItemChanged -= OnSettingsDataChanged;
        }
        #endregion
        
        #region Repo callbacks
        private void OnSettingsDataChanged(StatsModel statsData)
        {
            SetStatsData(statsData);
        }
        #endregion
        
        #region Setters
        private void SetStatsData(StatsModel statsData)
        {
            StatsData.SetPropertyValue(statsData);
        }

        public void UpdateSettings(StatsModel statsData)
        {
            _statsModelRepository.Update(statsData);
        }
        #endregion
    }
}
