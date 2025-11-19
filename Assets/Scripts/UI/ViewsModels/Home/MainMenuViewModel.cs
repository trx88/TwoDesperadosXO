using System.Linq;
using JSAM;
using Repository.DataRepositories.Abstraction;
using Repository.DataRepositories.RepositoryFactories.Factories;
using UI.Bindables;
using UI.Models.Settings;
using UI.StateMachine;

namespace UI.ViewsModels.Home
{
    public class MainMenuViewModel : ViewModel
    {
        public Bindable<SettingsModel> SettingsData { get; private set; } = new Bindable<SettingsModel>();
        
        private IRepository<SettingsModel> _settingsModelRepository;
        
        public override void Construct(
            InMemoryRepositoryFactory inMemoryRepositoryFactory,
            PlayerPrefsRepositoryFactory playerPrefsRepositoryFactory,
            UIStateMachine uiStateMachine)
        {
            //Just need the settings data and StateMachine.
            base.Construct(inMemoryRepositoryFactory, playerPrefsRepositoryFactory, uiStateMachine);
            
            _settingsModelRepository = playerPrefsRepositoryFactory.RepositoryOf<SettingsModel>();
        }
        
        public override void UpdateData()
        {
            base.UpdateData();
            
            SettingsModel settingsData = _settingsModelRepository.Get(_ => true).Single();
            SetSettingsData(settingsData);
        }
        
        private void SetSettingsData(SettingsModel settingsData)
        {
            SettingsData.SetPropertyValue(settingsData);
        }
    }
}
