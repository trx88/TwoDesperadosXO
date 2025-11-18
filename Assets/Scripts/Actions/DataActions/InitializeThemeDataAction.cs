using Actions.Abstraction;
using Repository.DataRepositories.RepositoryFactories.Factories;
using UI.Models.Theme;

namespace Actions.DataActions
{
    public class InitializeThemeDataAction : InitializeAction
    {
        private readonly InMemoryRepositoryFactory _inMemoryRepositoryFactory;

        public InitializeThemeDataAction(InMemoryRepositoryFactory repositoryFactory)
        {
            _inMemoryRepositoryFactory = repositoryFactory;
        }
        
        public override void Invoke()
        {
            _inMemoryRepositoryFactory.RepositoryOf<ThemeModel>().Create(
                new ThemeModel
                {
                    XThemeAsset = ThemeAssetNames.SignXTheme1,
                    OThemeAsset = ThemeAssetNames.SignOTheme1,
                });
        }
    }
}
