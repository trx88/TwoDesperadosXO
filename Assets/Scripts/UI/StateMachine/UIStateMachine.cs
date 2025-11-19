using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Views.Abstraction;
using UnityEngine;

namespace UI.StateMachine
{
    public class UIStateMachine
    {
        private Dictionary<UIView, (ViewBase View, ViewBase Parent)> _states = new();
        private (ViewBase View, ViewBase Parent) _currentViewState;

        public UIStateMachine(ViewStateContainer viewStateContainer)
        {
            InitializeStates(viewStateContainer.viewMap);
        }
        
        private void InitializeStates(List<ViewMap> viewMap)
        {
            _states = viewMap.ToDictionary(x => x.viewType, x => (x.view, x.parent));
        }
        
        public async Task SetInitialState(UIView uiView)
        {
            if (_states.TryGetValue(uiView, out var viewState))
            {
                _currentViewState = viewState;
                await _currentViewState.View.EnterViewState();
            }
        }

        public async Task TransitionTo(UIView uiView)
        {
            if (_states.TryGetValue(uiView, out var nextState))
            {
                //Considered a pop-up
                if (nextState.Parent != null)
                {
                    if (nextState.Parent == _currentViewState.View)
                    {
                        //New pop-up
                        _currentViewState = nextState;
                        await _currentViewState.View.EnterViewState();
                    }
                    else if (nextState.Parent == _currentViewState.Parent)
                    {
                        //Pop-ups from the same parent. Don't hide the parent, just the current pop-up.
                        await _currentViewState.View.ExitViewState();
                        _currentViewState = nextState;
                        await _currentViewState.View.EnterViewState();
                    }
                    else
                    {
                        //Hide both pop-up and parent.
                        await _currentViewState.View.ExitViewState();
                        await _currentViewState.Parent.ExitViewState();
                        _currentViewState = nextState;
                        await _currentViewState.View.EnterViewState();
                    }
                }
                else
                {
                    await _currentViewState.View.ExitViewState();
                    if (_currentViewState.Parent != null)
                    {
                        await _currentViewState.Parent.ExitViewState();
                    }
                    _currentViewState = nextState;
                    await _currentViewState.View.EnterViewState();
                }
                //Contingency plan if TransitionTo is used improperly (jumping from states that are not connected).
                //Hacky, but done to support SubView to act as a pop-up.
            }
            else
            {
                Debug.Log("ViewState not found");
            }
        }
    }
}