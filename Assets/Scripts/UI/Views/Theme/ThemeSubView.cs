using System;
using System.Linq;
using System.Threading.Tasks;
using JSAM;
using UI.Models.Theme;
using UI.Views.ViewComponents;
using UI.ViewsModels.Theme;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views.Theme
{
    public class ThemeSubView : View<ThemeViewModel>
    {
        private ViewComponentToggle _toggleViewComponentTheme1;
        private ViewComponentToggle _toggleViewComponentTheme2;
        private ViewComponentToggle _toggleViewComponentTheme3;
        private ButtonViewComponent _playButton;
        [SerializeField] private ToggleGroup toggleGroup;
        
        //Cache
        private ThemeModel _themeData;
        
        public override async Task Show()
        {
            await Task.Delay(200);
            AudioManager.PlaySound(AudioLibrarySounds.Popup);
            await base.Show();
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            
            var viewComponents = GetViewComponents<ViewComponentToggle>();
            if (viewComponents.Count > 0)
            {
                _toggleViewComponentTheme1 = viewComponents[0];
                _toggleViewComponentTheme2 = viewComponents[1];
                _toggleViewComponentTheme3 = viewComponents[2];
            }
            _playButton = GetViewComponent<ButtonViewComponent>();
        }
        
        protected override void SetupDataBindings()
        {
            base.SetupDataBindings();
            
            ViewModel.ThemeData.BindTo(OnThemeDataChanged);
        }
        
        protected override void SetupActionCallbacks()
        {
            base.SetupActionCallbacks();
            
            _playButton.ButtonClicked = OnPlayButtonClicked;
            _toggleViewComponentTheme1.ToggleValueChanged = OnThemeOneToggleValueChanged;
            _toggleViewComponentTheme2.ToggleValueChanged = OnThemeTwoToggleValueChanged;
            _toggleViewComponentTheme3.ToggleValueChanged = OnThemeThreeToggleValueChanged;
        }
        
        private void OnThemeDataChanged(ThemeModel themeData)
        {
            _themeData = themeData;
        }
        
        private void OnThemeOneToggleValueChanged(bool value)
        {
            _themeData.XThemeAsset = ThemeAssetNames.SignXTheme1;
            _themeData.OThemeAsset = ThemeAssetNames.SignOTheme1;
            
            ViewModel.UpdateTheme(_themeData);
        }
        
        private void OnThemeTwoToggleValueChanged(bool value)
        {
            _themeData.XThemeAsset = ThemeAssetNames.SignXTheme2;
            _themeData.OThemeAsset = ThemeAssetNames.SignOTheme2;
            
            ViewModel.UpdateTheme(_themeData);
        }
        
        private void OnThemeThreeToggleValueChanged(bool value)
        {
            _themeData.XThemeAsset = ThemeAssetNames.SignXTheme3;
            _themeData.OThemeAsset = ThemeAssetNames.SignOTheme3;
            
            ViewModel.UpdateTheme(_themeData);
        }
        
        private void OnPlayButtonClicked()
        {
            try
            {
                ViewModel.LoadGameScene();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
