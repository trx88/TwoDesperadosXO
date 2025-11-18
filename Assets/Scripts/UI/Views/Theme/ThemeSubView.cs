using System;
using System.Linq;
using UI.Models.Theme;
using UI.Views.ViewComponents;
using UI.ViewsModels.Theme;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views.Theme
{
    public class ThemeSubView : View<ThemeViewModel>
    {
        private ButtonViewComponent _playButton;
        [SerializeField] private ToggleGroup _toggleGroup;
        
        //Cache
        private ThemeModel _themeData;
        
        protected override void Initialize()
        {
            base.Initialize();
            
            var viewComponents = GetViewComponents<ButtonViewComponent>();
            if (viewComponents.Count > 0)
            {
                _playButton = viewComponents[0];
            }
        }
        
        protected override void SetupDataBindings()
        {
            base.SetupDataBindings();
            
            //Proved actions to Bindables.
            ViewModel.ThemeData.BindTo(OnThemeDataChanged);
        }
        
        protected override void SetupActionCallbacks()
        {
            base.SetupActionCallbacks();
            
            _playButton.ButtonClicked = OnCloseButtonClicked;
        }
        
        private void OnThemeDataChanged(ThemeModel themeData)
        {
            _themeData = themeData;
        }
        
        private void OnCloseButtonClicked()
        {
            try
            {
                var toggle = _toggleGroup.ActiveToggles().First();

                switch (toggle.GetComponent<ThemeToggle>().Theme)
                {
                    case ThemeType.Theme1:
                    {
                        _themeData.XThemeAsset = ThemeAssetNames.SignXTheme1;
                        _themeData.OThemeAsset = ThemeAssetNames.SignOTheme1;
                    }
                        break;
                    case ThemeType.Theme2:
                    {
                        _themeData.XThemeAsset = ThemeAssetNames.SignXTheme2;
                        _themeData.OThemeAsset = ThemeAssetNames.SignOTheme2;
                    }break;
                    case ThemeType.Theme3:
                    {
                        _themeData.XThemeAsset = ThemeAssetNames.SignXTheme3;
                        _themeData.OThemeAsset = ThemeAssetNames.SignOTheme3;
                    }break;
                    default:
                    {
                        _themeData.XThemeAsset = ThemeAssetNames.SignXTheme1;
                        _themeData.OThemeAsset = ThemeAssetNames.SignOTheme1;
                    }break;
                }

                ViewModel.UpdateTheme(_themeData);
                
                ViewModel.LoadGameScene();
                Debug.Log("Transition to UIView.GameScreen");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
