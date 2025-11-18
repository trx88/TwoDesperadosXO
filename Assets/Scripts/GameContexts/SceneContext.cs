using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JSAM;
using UI.StateMachine;
using UI.ViewsModels.Abstraction;
using UI.ViewsModels.Factory;
using UnityEngine;

namespace GameContexts
{
    public abstract class SceneContext : MonoBehaviour
    {
        [SerializeField] private ViewStateContainer viewStateContainer;
        protected UIStateMachine UIStateMachine;
        
        protected readonly Dictionary<Type, IViewModel> ViewModels = new Dictionary<Type, IViewModel>();
        protected readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();
        
        public static SceneContext Instance { get; private set; }

        protected ViewModelFactory ViewModelFactory { get; private set; }
        
        protected GameContext GameContextInstance;
        
        protected void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //TODO: Think about this.
            GameContextInstance = GameContext.Instance;
            
            UIStateMachine = new UIStateMachine(viewStateContainer);
            
            RegisterServices();
            SetupViewModelFactory();
            CreateSceneViewModels();
            InitializeStateMachine();
            
            AudioManager.PlayMusic(AudioLibraryMusic.BackgroundMusic);
            Debug.Log("SceneContext initialized.");
        }
        
        private void SetupViewModelFactory()
        {
            ViewModelFactory = new ViewModelFactory(
                GameContextInstance.InMemoryRepositoryFactory, 
                GameContextInstance.PlayerPrefsRepositoryFactory,
                UIStateMachine);
        }

        protected abstract Task InitializeStateMachine();
        
        /// <summary>
        /// Replaces installer's job of binding ViewModels.
        /// </summary>
        protected abstract void CreateSceneViewModels();

        protected abstract void RegisterServices();

        protected void RegisterViewModel<TViewModel>(TViewModel vm) where TViewModel : IViewModel
        {
            ViewModels[typeof(TViewModel)] = vm;
        }
        
        public TViewModel GetViewModel<TViewModel>(Type type) where TViewModel : IViewModel
        {
            ViewModels.TryGetValue(type, out var viewModel);
            return (TViewModel)viewModel;
        }

        protected void RegisterService<TService>(TService service) where TService : class
        {
            Services[typeof(TService)] = service;
        }
        
        public TService GetService<TService>() where TService : class
        {
            if (Services.TryGetValue(typeof(TService), out var service))
            {
                return service as TService;
            }
            return null;
        }
    }
}
