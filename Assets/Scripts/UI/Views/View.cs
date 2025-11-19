using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameContexts;
using UI.Views.Abstraction;
using UI.ViewsModels.Abstraction;
using UnityEngine;

namespace UI.Views
{
    /// <summary>
    /// Skeleton view class.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class View<TViewModel> : ViewBase where TViewModel : IViewModel
    {
        protected TViewModel ViewModel { get; private set; }
        private Dictionary<Type, List<ViewComponentBase>> _viewComponentsMap;
        [SerializeField] private List<ViewComponentBase> viewComponents;

        /// <summary>
        /// Replaces Zenject injection of ViewModel to the View.
        /// </summary>
        private void ConstructViewModel()
        {
            ViewModel = SceneContext.Instance.GetViewModel<TViewModel>(typeof(TViewModel));
        }
        
        protected virtual void Initialize()
        {
            _viewComponentsMap = 
                viewComponents
                .GroupBy(c =>  c.GetType())
                .ToDictionary(x => x.Key, x => x.ToList());
        }

        protected virtual void Deinitialize()
        {
            //For what needs to happen when View is closed.
        }

        protected virtual void Finish()
        {
            //Any last-minute code that needs to be executed.
        }
        
        protected virtual void SetupDataBindings()
        {
            //For base data binding. 
        }

        protected virtual void SetupActionCallbacks()
        {
            //Good use cases are Actions (so ViewComponents can communicate with the View).
        }

        protected void OnEnable()
        {
            ConstructViewModel();
            SetupDataBindings();
            Initialize();
            SetupActionCallbacks();
            ViewModel.SubscribeToDataChanges();
            ViewModel.UpdateData();
            Finish();
        }

        protected void OnDisable()
        {
            ViewModel.UnsubscribeFromDataChanges();
            Deinitialize();
        }

        public override async Task Hide()
        {
            viewComponents.ForEach(c => _ = c.Hide());
            await base.Hide();
        }

        public override async Task Show()
        {
            viewComponents.ForEach(c => _ = c.VisibleOnStart ? c.Show() : c.Hide());
            await base.Show();
        }
        
        protected T GetViewComponent<T>() where T : ViewComponentBase
        {
            if (_viewComponentsMap.TryGetValue(typeof(T), out var comp) && comp.Count > 0)
                return comp.Single() as T;

            Debug.LogWarning($"Component of type {typeof(T).Name} not found.");
            return null;
        }

        protected List<T> GetViewComponents<T>() where T : ViewComponentBase
        {
            if (_viewComponentsMap.TryGetValue(typeof(T), out var comps))
                return comps.Cast<T>().ToList();

            Debug.LogWarning($"Component of type {typeof(T).Name} not found. Returning empty list.");
            return new List<T>();
        }
    }
}