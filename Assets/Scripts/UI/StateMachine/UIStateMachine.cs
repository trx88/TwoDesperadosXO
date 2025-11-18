using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Views;
using UI.Views.Abstraction;

namespace UI.StateMachine
{
    public class UIStateMachine
    {
        private Dictionary<UIView, ViewBase> _states = new();
        private ViewBase _currentViewState;

        public UIStateMachine(ViewStateContainer viewStateContainer)
        {
            InitializeStates(viewStateContainer.viewMap);
        }
        
        private void InitializeStates(List<ViewMap> viewMap)
        {
            _states = viewMap.ToDictionary(x => x.viewType, x => x.view);
        }
        
        public async Task SetInitialState(UIView uiView)
        {
            if (_states.TryGetValue(uiView, out var viewState))
            {
                _currentViewState = viewState;
                await _currentViewState.EnterViewState();
            }
        }

        public async Task TransitionTo(UIView uiView)
        {
            if (_states.TryGetValue(uiView, out var nextState))
            {
                await _currentViewState.ExitViewState();
                _currentViewState = nextState;
                await _currentViewState.EnterViewState();
            }
        }
    }
}