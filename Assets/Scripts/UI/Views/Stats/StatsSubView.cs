using System;
using UI.Models.Stats;
using UI.Views.Abstraction;
using UI.Views.ViewComponents;
using UI.ViewsModels.Stats;
using UnityEngine;

namespace UI.Views.Stats
{
    public class StatsSubView : View<StatsViewModel>
    {
        private ViewComponentStat _statViewComponentNumberOfMatches;
        private ViewComponentStat _statViewComponentPlayerOneWins;
        private ViewComponentStat _statViewComponentPlayerTwoWins;
        private ViewComponentStat _statViewComponentDraws;
        private ViewComponentStat _statViewComponentAverageMatchTime;
        private ButtonViewComponent _closeButton;
        
        protected override void Initialize()
        {
            base.Initialize();
            
            var viewComponents = GetViewComponents<ViewComponentStat>();
            if (viewComponents.Count > 0)
            {
                _statViewComponentNumberOfMatches = viewComponents[0];
                _statViewComponentPlayerOneWins = viewComponents[1];
                _statViewComponentPlayerTwoWins = viewComponents[2];
                _statViewComponentDraws = viewComponents[3];
                _statViewComponentAverageMatchTime = viewComponents[4];
            }
            
            _closeButton = GetViewComponent<ButtonViewComponent>();
        }
        
        protected override void SetupDataBindings()
        {
            base.SetupDataBindings();
            
            ViewModel.StatsData.BindTo(OnStatsDataChanged);
        }
        
        protected override void SetupActionCallbacks()
        {
            base.SetupActionCallbacks();
            
            _closeButton.ButtonClicked = OnCloseButtonClicked;
        }
        
        private void OnStatsDataChanged(StatsModel statsData)
        {
            _statViewComponentNumberOfMatches.UpdateStatValue(statsData.NumberOfMatches.ToString());
            _statViewComponentPlayerOneWins.UpdateStatValue(statsData.PlayerOneWins.ToString());
            _statViewComponentPlayerTwoWins.UpdateStatValue(statsData.PlayerTwoWins.ToString());
            _statViewComponentDraws.UpdateStatValue(statsData.Draws.ToString());
            _statViewComponentAverageMatchTime.UpdateStatValue($@"{statsData.AverageMatchTime:F1} sec");
        }
        
        private async void OnCloseButtonClicked()
        {
            try
            {
                await ViewModel.StateMachine.TransitionTo(UIView.HomeScreen);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
