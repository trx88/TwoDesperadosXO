using System;
using UI.Views.Abstraction;
using UI.Views.ViewComponents;
using UI.ViewsModels.Home;
using UnityEngine;

namespace UI.Views.Home
{
    public class MainMenuSubView : View<MainMenuViewModel>
    {
        private ButtonViewComponent _playButton;
        private ButtonViewComponent _statsButton;
        private ButtonViewComponent _exitButton;
        private ButtonViewComponent _settingButton;

        protected override void Initialize()
        {
            base.Initialize();
            
            var buttonComponents = GetViewComponents<ButtonViewComponent>();
            _playButton = buttonComponents[0];
            _statsButton = buttonComponents[1];
            _exitButton = buttonComponents[2];
            _settingButton = buttonComponents[3];
        }

        protected override void SetupActionCallbacks()
        {
            _playButton.ButtonClicked = OnPlayButtonClick;
            _statsButton.ButtonClicked = OnStatsButtonClick;
            _exitButton.ButtonClicked = OnExitButtonClick;
            _settingButton.ButtonClicked = OnSettingsButtonClick;
        }
        
        private async void OnPlayButtonClick()
        {
            try
            {
                await ViewModel.StateMachine.TransitionTo(UIView.ThemeScreen);
                Debug.Log("Transition to UIView.GameScreen");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        private async void OnStatsButtonClick()
        {
            try
            {
                await ViewModel.StateMachine.TransitionTo(UIView.StatsScreen);
                Debug.Log("Transition to UIView.StatsScreen");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        private void OnExitButtonClick()
        {
            try
            {
                Debug.Log("Exit game");
                // Application.Quit();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        private async void OnSettingsButtonClick()
        {
            try
            {
                await ViewModel.StateMachine.TransitionTo(UIView.SettingsScreen);
                Debug.Log("Transition to UIView.StatsScreen");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
