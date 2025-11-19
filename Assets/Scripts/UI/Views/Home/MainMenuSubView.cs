using System;
using System.Threading.Tasks;
using JSAM;
using UI.Models.Settings;
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

        public override async Task Show()
        {
            await base.Show();
            await Task.Delay(1000);
            if (!AudioManager.IsMusicPlaying(AudioLibraryMusic.BackgroundMusic))
            {
                AudioManager.PlayMusic(AudioLibraryMusic.BackgroundMusic);
                
                AudioManager.MainMusicHelper.AudioSource.volume = ViewModel.SettingsData.Value.MusicEnabled ? 0.4f : 0f;
                AudioManager.InternalInstance.SoundMuted = !ViewModel.SettingsData.Value.SfxEnabled;
            }
        }

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
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        private async void OnExitButtonClick()
        {
            try
            {
                await ViewModel.StateMachine.TransitionTo(UIView.ExitScreen);
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
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
