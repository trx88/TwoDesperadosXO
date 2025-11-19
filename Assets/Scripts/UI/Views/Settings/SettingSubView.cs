using System;
using JSAM;
using UI.Models.Settings;
using UI.Views.Abstraction;
using UI.Views.ViewComponents;
using UI.ViewsModels.Settings;
using UnityEngine;

namespace UI.Views.Settings
{
    public class SettingSubView : View<SettingViewModel>
    {
        private ButtonViewComponent _closeButton;
        private ViewComponentToggle _toggleViewComponentMusic;
        private ViewComponentToggle _toggleViewComponentSfx;
        
        //Cache
        private SettingsModel _settingsData;

        protected override void Initialize()
        {
            base.Initialize();
            
            _closeButton = GetViewComponent<ButtonViewComponent>();
            var viewComponents = GetViewComponents<ViewComponentToggle>();
            if (viewComponents.Count > 0)
            {
                _toggleViewComponentMusic = viewComponents[0];
                _toggleViewComponentSfx = viewComponents[1];
            }
        }
        
        protected override void SetupDataBindings()
        {
            base.SetupDataBindings();
            
            ViewModel.SettingsData.BindTo(OnSettingsDataChanged);
        }

        protected override void SetupActionCallbacks()
        {
            base.SetupActionCallbacks();
            
            _closeButton.ButtonClicked = OnCloseButtonClicked;
            _toggleViewComponentMusic.ToggleValueChanged = OnMusicToggleValueChanged;
            _toggleViewComponentSfx.ToggleValueChanged = OnSfxToggleValueChanged;
        }
        
        private void OnSettingsDataChanged(SettingsModel settingsData)
        {
            _settingsData = settingsData;
            
            _toggleViewComponentMusic?.SetToggleValue(_settingsData.MusicEnabled);
            _toggleViewComponentSfx?.SetToggleValue(_settingsData.SfxEnabled);
            
            AudioManager.MainMusicHelper.AudioSource.volume = _settingsData.MusicEnabled ? 0.4f : 0f;
            AudioManager.SoundMuted = !_settingsData.SfxEnabled;
        }

        private void OnMusicToggleValueChanged(bool value)
        {
            _settingsData.MusicEnabled = value;
            
            ViewModel.UpdateSettings(_settingsData);
        }
        
        private void OnSfxToggleValueChanged(bool value)
        {
            _settingsData.SfxEnabled = value;
            
            ViewModel.UpdateSettings(_settingsData);
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
